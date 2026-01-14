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
    /// 场景管理器接口。
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        /// 加载场景成功事件。
        /// </summary>
        event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess;

        /// <summary>
        /// 加载场景失败事件。
        /// </summary>
        event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure;

        /// <summary>
        /// 卸载场景成功事件。
        /// </summary>
        event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess;

        /// <summary>
        /// 卸载场景失败事件。
        /// </summary>
        event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure;

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <param name="resourceManager">资源管理器。</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// 获取场景是否已加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否已加载。</returns>
        bool SceneIsLoaded(AssetAddress sceneAssetAddress);

        /// <summary>
        /// 获取已加载场景的资源地址。
        /// </summary>
        /// <returns>已加载场景的资源地址。</returns>
        AssetAddress[] GetLoadedSceneAssetAddresses();

        /// <summary>
        /// 获取场景是否正在加载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在加载。</returns>
        bool SceneIsLoading(AssetAddress sceneAssetAddress);

        /// <summary>
        /// 获取正在加载场景的资源地址。
        /// </summary>
        /// <returns>正在加载场景的资源地址。</returns>
        AssetAddress[] GetLoadingSceneAssetAddresses();

        /// <summary>
        /// 获取场景是否正在卸载。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <returns>场景是否正在卸载。</returns>
        bool SceneIsUnloading(AssetAddress sceneAssetAddress);

        /// <summary>
        /// 获取正在卸载场景的资源地址。
        /// </summary>
        /// <returns>正在卸载场景的资源地址。</returns>
        AssetAddress[] GetUnloadingSceneAssetAddresses();

        /// <summary>
        /// 检查场景资源是否存在。
        /// </summary>
        /// <param name="sceneAssetAddress">要检查场景资源的名称。</param>
        /// <returns>场景资源是否存在。</returns>
        bool HasScene(AssetAddress sceneAssetAddress);

        /// <summary>
        /// 加载场景。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <param name="customPriority">加载场景资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        void LoadScene(AssetAddress sceneAssetAddress, int? customPriority = null, object userData = null);

        /// <summary>
        /// 卸载场景。
        /// </summary>
        /// <param name="sceneAssetAddress">场景资源地址。</param>
        /// <param name="userData">用户自定义数据。</param>
        void UnloadScene(AssetAddress sceneAssetAddress, object userData = null);
    }
}
