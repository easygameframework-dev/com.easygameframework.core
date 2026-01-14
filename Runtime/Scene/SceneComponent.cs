//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using EasyGameFramework.Core;
using EasyGameFramework.Core.Resource;
using EasyGameFramework.Core.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace EasyGameFramework
{
    /// <summary>
    /// 场景组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Scene")]
    public sealed class SceneComponent : GameFrameworkComponent
    {
        private ISceneManager m_SceneManager = null;
        private EventComponent m_EventComponent = null;
        private readonly Dictionary<AssetAddress, string> m_SceneAssetAddressToSceneName = new Dictionary<AssetAddress, string>();
        private readonly Dictionary<AssetAddress, int> m_SceneOrder = new Dictionary<AssetAddress, int>();
        private Camera m_MainCamera = null;
        private Scene m_GameFrameworkScene = default(Scene);

        /// <summary>
        /// 获取当前场景主摄像机。
        /// </summary>
        public Camera MainCamera
        {
            get
            {
                return m_MainCamera;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_SceneManager = GameFrameworkEntry.GetModule<ISceneManager>();
            if (m_SceneManager == null)
            {
                Log.Fatal("Scene manager is invalid.");
                return;
            }

            m_SceneManager.LoadSceneSuccess += OnLoadSceneSuccess;
            m_SceneManager.LoadSceneFailure += OnLoadSceneFailure;

            m_SceneManager.UnloadSceneSuccess += OnUnloadSceneSuccess;
            m_SceneManager.UnloadSceneFailure += OnUnloadSceneFailure;

            m_GameFrameworkScene = SceneManager.GetSceneAt(GameEntry.GameFrameworkSceneId);
            if (!m_GameFrameworkScene.IsValid())
            {
                Log.Fatal("Game Framework scene is invalid.");
                return;
            }
        }

        private void Start()
        {
            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_SceneManager.SetResourceManager(GameFrameworkEntry.GetModule<IResourceManager>());
        }

        /// <summary>
        /// 获取场景名称。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景名称。</returns>
        public string GetSceneName(AssetAddress sceneAssetAddress)
        {
            // if (string.IsNullOrEmpty(sceneAssetAddress))
            // {
            //     Log.Error("Scene asset name is invalid.");
            //     return null;
            // }
            //
            // int sceneNamePosition = sceneAssetAddress.LastIndexOf('/');
            // if (sceneNamePosition + 1 >= sceneAssetAddress.Length)
            // {
            //     Log.Error("Scene asset name '{0}' is invalid.", sceneAssetAddress);
            //     return null;
            // }
            //
            // string sceneName = sceneAssetAddress.Substring(sceneNamePosition + 1);
            // sceneNamePosition = sceneName.LastIndexOf(".unity");
            // if (sceneNamePosition > 0)
            // {
            //     sceneName = sceneName.Substring(0, sceneNamePosition);
            // }
            //
            // return sceneName;

            if (m_SceneAssetAddressToSceneName.TryGetValue(sceneAssetAddress, out var sceneName))
            {
                return sceneName;
            }
            Log.Error("Scene asset name '{0}' is not loaded.", sceneAssetAddress);
            return null;
        }

        /// <summary>
        /// 获取场景是否已加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否已加载。</returns>
        public bool SceneIsLoaded(AssetAddress sceneAssetAddress)
        {
            return m_SceneManager.SceneIsLoaded(sceneAssetAddress);
        }

        /// <summary>
        /// 获取已加载场景的资源地址。
        /// </summary>
        /// <returns>已加载场景的资源地址。</returns>
        public AssetAddress[] GetLoadedSceneAssetAddresses()
        {
            return m_SceneManager.GetLoadedSceneAssetAddresses();
        }

        /// <summary>
        /// 获取场景是否正在加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在加载。</returns>
        public bool SceneIsLoading(AssetAddress sceneAssetAddress)
        {
            return m_SceneManager.SceneIsLoading(sceneAssetAddress);
        }

        /// <summary>
        /// 获取正在加载场景的资源地址。
        /// </summary>
        /// <returns>正在加载场景的资源地址。</returns>
        public AssetAddress[] GetLoadingSceneAssetAddresses()
        {
            return m_SceneManager.GetLoadingSceneAssetAddresses();
        }

        /// <summary>
        /// 获取场景是否正在卸载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在卸载。</returns>
        public bool SceneIsUnloading(AssetAddress sceneAssetAddress)
        {
            return m_SceneManager.SceneIsUnloading(sceneAssetAddress);
        }

        /// <summary>
        /// 获取正在卸载场景的资源地址。
        /// </summary>
        /// <returns>正在卸载场景的资源地址。</returns>
        public AssetAddress[] GetUnloadingSceneAssetAddresses()
        {
            return m_SceneManager.GetUnloadingSceneAssetAddresses();
        }

        /// <summary>
        /// 检查场景资源是否存在。
        /// </summary>
        /// <param name="sceneAssetAddress">要检查场景资源的名称。</param>
        /// <returns>场景资源是否存在。</returns>
        public bool HasScene(AssetAddress sceneAssetAddress)
        {
            if (!sceneAssetAddress.IsValid())
            {
                Log.Error("Scene asset name is invalid.");
                return false;
            }

            // if (!sceneAssetAddress.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetAddress.EndsWith(".unity", StringComparison.Ordinal))
            // {
            //     Log.Error("Scene asset name '{0}' is invalid.", sceneAssetAddress);
            //     return false;
            // }

            return m_SceneManager.HasScene(sceneAssetAddress);
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
                Log.Error("Scene asset name is invalid.");
                return;
            }

            // if (!sceneAssetAddress.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetAddress.EndsWith(".unity", StringComparison.Ordinal))
            // {
            //     Log.Error("Scene asset name '{0}' is invalid.", sceneAssetAddress);
            //     return;
            // }

            m_SceneManager.LoadScene(sceneAssetAddress, customPriority, userData);
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
                Log.Error("Scene asset name is invalid.");
                return;
            }

            // if (!sceneAssetAddress.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetAddress.EndsWith(".unity", StringComparison.Ordinal))
            // {
            //     Log.Error("Scene asset name '{0}' is invalid.", sceneAssetAddress);
            //     return;
            // }

            m_SceneManager.UnloadScene(sceneAssetAddress, userData);
            m_SceneOrder.Remove(sceneAssetAddress);
        }

        /// <summary>
        /// 设置场景顺序。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <param name="sceneOrder">要设置的场景顺序。</param>
        public void SetSceneOrder(AssetAddress sceneAssetAddress, int sceneOrder)
        {
            if (!sceneAssetAddress.IsValid())
            {
                Log.Error("Scene asset name is invalid.");
                return;
            }

            // if (!sceneAssetAddress.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetAddress.EndsWith(".unity", StringComparison.Ordinal))
            // {
            //     Log.Error("Scene asset name '{0}' is invalid.", sceneAssetAddress);
            //     return;
            // }

            if (SceneIsLoading(sceneAssetAddress))
            {
                m_SceneOrder[sceneAssetAddress] = sceneOrder;
                return;
            }

            if (SceneIsLoaded(sceneAssetAddress))
            {
                m_SceneOrder[sceneAssetAddress] = sceneOrder;
                RefreshSceneOrder();
                return;
            }

            Log.Error("Scene '{0}' is not loaded or loading.", sceneAssetAddress);
        }

        /// <summary>
        /// 刷新当前场景主摄像机。
        /// </summary>
        public void RefreshMainCamera()
        {
            m_MainCamera = Camera.main;
        }

        private void RefreshSceneOrder()
        {
            if (m_SceneOrder.Count > 0)
            {
                AssetAddress maxSceneName = AssetAddress.Empty;
                int maxSceneOrder = 0;
                foreach (KeyValuePair<AssetAddress, int> sceneOrder in m_SceneOrder)
                {
                    if (SceneIsLoading(sceneOrder.Key))
                    {
                        continue;
                    }

                    if (maxSceneName == AssetAddress.Empty)
                    {
                        maxSceneName = sceneOrder.Key;
                        maxSceneOrder = sceneOrder.Value;
                        continue;
                    }

                    if (sceneOrder.Value > maxSceneOrder)
                    {
                        maxSceneName = sceneOrder.Key;
                        maxSceneOrder = sceneOrder.Value;
                    }
                }

                if (maxSceneName == AssetAddress.Empty)
                {
                    SetActiveScene(m_GameFrameworkScene);
                    return;
                }

                Scene scene = SceneManager.GetSceneByName(GetSceneName(maxSceneName));
                if (!scene.IsValid())
                {
                    Log.Error("Active scene '{0}' is invalid.", maxSceneName);
                    return;
                }

                SetActiveScene(scene);
            }
            else
            {
                SetActiveScene(m_GameFrameworkScene);
            }
        }

        private void SetActiveScene(Scene activeScene)
        {
            Scene lastActiveScene = SceneManager.GetActiveScene();
            if (lastActiveScene != activeScene)
            {
                SceneManager.SetActiveScene(activeScene);
                m_EventComponent.Fire(this, ActiveSceneChangedEventArgs.Create(lastActiveScene, activeScene));
            }

            RefreshMainCamera();
        }

        private void OnLoadSceneSuccess(object sender, EasyGameFramework.Core.Scene.LoadSceneSuccessEventArgs e)
        {
            m_SceneAssetAddressToSceneName[e.SceneAssetAddress] = ((Scene)e.SceneAsset).name;
            if (!m_SceneOrder.ContainsKey(e.SceneAssetAddress))
            {
                m_SceneOrder.Add(e.SceneAssetAddress, 0);
            }

            m_EventComponent.Fire(this, LoadSceneSuccessEventArgs.Create(e));
            RefreshSceneOrder();
        }

        private void OnLoadSceneFailure(object sender, EasyGameFramework.Core.Scene.LoadSceneFailureEventArgs e)
        {
            Log.Warning("Load scene failure, scene asset name '{0}', error message '{1}'.", e.SceneAssetAddress, e.ErrorMessage);
            m_EventComponent.Fire(this, LoadSceneFailureEventArgs.Create(e));
        }

        private void OnUnloadSceneSuccess(object sender, EasyGameFramework.Core.Scene.UnloadSceneSuccessEventArgs e)
        {
            m_SceneAssetAddressToSceneName.Remove(e.SceneAssetAddress);
            m_EventComponent.Fire(this, UnloadSceneSuccessEventArgs.Create(e));
            m_SceneOrder.Remove(e.SceneAssetAddress);
            RefreshSceneOrder();
        }

        private void OnUnloadSceneFailure(object sender, EasyGameFramework.Core.Scene.UnloadSceneFailureEventArgs e)
        {
            Log.Warning("Unload scene failure, scene asset name '{0}'.", e.SceneAssetAddress);
            m_EventComponent.Fire(this, UnloadSceneFailureEventArgs.Create(e));
        }
    }
}
