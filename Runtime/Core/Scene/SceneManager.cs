//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using EasyGameFramework.Core.Resource;

namespace EasyGameFramework.Core.Scene
{
    /// <summary>
    /// 场景管理器。
    /// </summary>
    internal sealed class SceneManager : GameFrameworkModule, ISceneManager
    {
        private readonly List<AssetAddress> m_LoadedSceneAssetAddresses;
        private readonly List<AssetAddress> m_LoadingSceneAssetAddresses;
        private readonly List<AssetAddress> m_UnloadingSceneAssetAddresses;
        private readonly LoadSceneCallbacks m_LoadSceneCallbacks;
        private readonly UnloadSceneCallbacks m_UnloadSceneCallbacks;
        private IResourceManager m_ResourceManager;
        private EventHandler<LoadSceneSuccessEventArgs> m_LoadSceneSuccessEventHandler;
        private EventHandler<LoadSceneFailureEventArgs> m_LoadSceneFailureEventHandler;
        private EventHandler<UnloadSceneSuccessEventArgs> m_UnloadSceneSuccessEventHandler;
        private EventHandler<UnloadSceneFailureEventArgs> m_UnloadSceneFailureEventHandler;

        /// <summary>
        /// 初始化场景管理器的新实例。
        /// </summary>
        public SceneManager()
        {
            m_LoadedSceneAssetAddresses = new List<AssetAddress>();
            m_LoadingSceneAssetAddresses = new List<AssetAddress>();
            m_UnloadingSceneAssetAddresses = new List<AssetAddress>();
            m_LoadSceneCallbacks = new LoadSceneCallbacks(LoadSceneSuccessCallback, LoadSceneFailureCallback);
            m_UnloadSceneCallbacks = new UnloadSceneCallbacks(UnloadSceneSuccessCallback, UnloadSceneFailureCallback);
            m_ResourceManager = null;
            m_LoadSceneSuccessEventHandler = null;
            m_LoadSceneFailureEventHandler = null;
            m_UnloadSceneSuccessEventHandler = null;
            m_UnloadSceneFailureEventHandler = null;
        }

        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        internal override int Priority
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// 加载场景成功事件。
        /// </summary>
        public event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess
        {
            add
            {
                m_LoadSceneSuccessEventHandler += value;
            }
            remove
            {
                m_LoadSceneSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// 加载场景失败事件。
        /// </summary>
        public event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure
        {
            add
            {
                m_LoadSceneFailureEventHandler += value;
            }
            remove
            {
                m_LoadSceneFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// 卸载场景成功事件。
        /// </summary>
        public event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess
        {
            add
            {
                m_UnloadSceneSuccessEventHandler += value;
            }
            remove
            {
                m_UnloadSceneSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// 卸载场景失败事件。
        /// </summary>
        public event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure
        {
            add
            {
                m_UnloadSceneFailureEventHandler += value;
            }
            remove
            {
                m_UnloadSceneFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// 场景管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 关闭并清理场景管理器。
        /// </summary>
        internal override void Shutdown()
        {
            AssetAddress[] loadedSceneAssetAddresses = m_LoadedSceneAssetAddresses.ToArray();

            foreach (AssetAddress loadedSceneAssetAddress in loadedSceneAssetAddresses)
            {
                if (SceneIsUnloading(loadedSceneAssetAddress))
                {
                    continue;
                }

                UnloadScene(loadedSceneAssetAddress);
            }

            m_LoadedSceneAssetAddresses.Clear();
            m_LoadingSceneAssetAddresses.Clear();
            m_UnloadingSceneAssetAddresses.Clear();
        }

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <param name="resourceManager">资源管理器。</param>
        public void SetResourceManager(IResourceManager resourceManager)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            m_ResourceManager = resourceManager;
        }

        /// <summary>
        /// 获取场景是否已加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否已加载。</returns>
        public bool SceneIsLoaded(AssetAddress sceneAssetAddress)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            return m_LoadedSceneAssetAddresses.Contains(sceneAssetAddress);
        }

        /// <summary>
        /// 获取已加载场景的资源地址。
        /// </summary>
        /// <returns>已加载场景的资源地址。</returns>
        public AssetAddress[] GetLoadedSceneAssetAddresses()
        {
            return m_LoadedSceneAssetAddresses.ToArray();
        }

        /// <summary>
        /// 获取场景是否正在加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在加载。</returns>
        public bool SceneIsLoading(AssetAddress sceneAssetAddress)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            return m_LoadingSceneAssetAddresses.Contains(sceneAssetAddress);
        }

        /// <summary>
        /// 获取正在加载场景的资源地址。
        /// </summary>
        /// <returns>正在加载场景的资源地址。</returns>
        public AssetAddress[] GetLoadingSceneAssetAddresses()
        {
            return m_LoadingSceneAssetAddresses.ToArray();
        }

        /// <summary>
        /// 获取场景是否正在卸载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在卸载。</returns>
        public bool SceneIsUnloading(AssetAddress sceneAssetAddress)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            return m_UnloadingSceneAssetAddresses.Contains(sceneAssetAddress);
        }

        /// <summary>
        /// 获取正在卸载场景的资源地址。
        /// </summary>
        /// <returns>正在卸载场景的资源地址。</returns>
        public AssetAddress[] GetUnloadingSceneAssetAddresses()
        {
            return m_UnloadingSceneAssetAddresses.ToArray();
        }

        /// <summary>
        /// 检查场景资源是否存在。
        /// </summary>
        /// <param name="sceneAssetAddress">要检查场景资源的地址。</param>
        /// <returns>场景资源是否存在。</returns>
        public bool HasScene(AssetAddress sceneAssetAddress)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            return m_ResourceManager.HasAsset(sceneAssetAddress) != HasAssetResult.NotExist;
        }

        /// <summary>
        /// 加载场景。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <param name="customPriority">加载场景资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void LoadScene(AssetAddress sceneAssetAddress, int? customPriority = null, object userData = null)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (SceneIsUnloading(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is being unloaded.", sceneAssetAddress));
            }

            if (SceneIsLoading(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is being loaded.", sceneAssetAddress));
            }

            if (SceneIsLoaded(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is already loaded.", sceneAssetAddress));
            }

            m_LoadingSceneAssetAddresses.Add(sceneAssetAddress);
            m_ResourceManager.LoadScene(sceneAssetAddress, m_LoadSceneCallbacks, customPriority, userData);
        }

        /// <summary>
        /// 卸载场景。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void UnloadScene(AssetAddress sceneAssetAddress, object userData = null)
        {
            if (!sceneAssetAddress.IsValid())
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (SceneIsUnloading(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is being unloaded.", sceneAssetAddress));
            }

            if (SceneIsLoading(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is being loaded.", sceneAssetAddress));
            }

            if (!SceneIsLoaded(sceneAssetAddress))
            {
                throw new GameFrameworkException(Utility.Text.Format("Scene asset '{0}' is not loaded yet.", sceneAssetAddress));
            }

            m_UnloadingSceneAssetAddresses.Add(sceneAssetAddress);
            m_ResourceManager.UnloadScene(sceneAssetAddress, m_UnloadSceneCallbacks, userData);
        }

        private void LoadSceneSuccessCallback(AssetAddress sceneAssetAddress, object sceneAsset, float duration, object userData)
        {
            m_LoadingSceneAssetAddresses.Remove(sceneAssetAddress);
            m_LoadedSceneAssetAddresses.Add(sceneAssetAddress);
            if (m_LoadSceneSuccessEventHandler != null)
            {
                LoadSceneSuccessEventArgs loadSceneSuccessEventArgs = LoadSceneSuccessEventArgs.Create(sceneAssetAddress, sceneAsset, duration, userData);
                m_LoadSceneSuccessEventHandler(this, loadSceneSuccessEventArgs);
                ReferencePool.Release(loadSceneSuccessEventArgs);
            }
        }

        private void LoadSceneFailureCallback(AssetAddress sceneAssetAddress, LoadResourceStatus status, string errorMessage, object userData)
        {
            m_LoadingSceneAssetAddresses.Remove(sceneAssetAddress);
            string appendErrorMessage = Utility.Text.Format("Load scene failure, scene asset name '{0}', status '{1}', error message '{2}'.", sceneAssetAddress, status, errorMessage);
            if (m_LoadSceneFailureEventHandler != null)
            {
                LoadSceneFailureEventArgs loadSceneFailureEventArgs = LoadSceneFailureEventArgs.Create(sceneAssetAddress, appendErrorMessage, userData);
                m_LoadSceneFailureEventHandler(this, loadSceneFailureEventArgs);
                ReferencePool.Release(loadSceneFailureEventArgs);
                return;
            }

            throw new GameFrameworkException(appendErrorMessage);
        }

        private void UnloadSceneSuccessCallback(AssetAddress sceneAssetAddress, object userData)
        {
            m_UnloadingSceneAssetAddresses.Remove(sceneAssetAddress);
            m_LoadedSceneAssetAddresses.Remove(sceneAssetAddress);
            if (m_UnloadSceneSuccessEventHandler != null)
            {
                UnloadSceneSuccessEventArgs unloadSceneSuccessEventArgs = UnloadSceneSuccessEventArgs.Create(sceneAssetAddress, userData);
                m_UnloadSceneSuccessEventHandler(this, unloadSceneSuccessEventArgs);
                ReferencePool.Release(unloadSceneSuccessEventArgs);
            }
        }

        private void UnloadSceneFailureCallback(AssetAddress sceneAssetAddress, string errorMessage, object userData)
        {
            m_UnloadingSceneAssetAddresses.Remove(sceneAssetAddress);
            if (m_UnloadSceneFailureEventHandler != null)
            {
                UnloadSceneFailureEventArgs unloadSceneFailureEventArgs = UnloadSceneFailureEventArgs.Create(sceneAssetAddress, errorMessage, userData);
                m_UnloadSceneFailureEventHandler(this, unloadSceneFailureEventArgs);
                ReferencePool.Release(unloadSceneFailureEventArgs);
                return;
            }

            throw new GameFrameworkException(Utility.Text.Format("Unload scene failure, scene asset name '{0}'.", sceneAssetAddress));
        }
    }
}
