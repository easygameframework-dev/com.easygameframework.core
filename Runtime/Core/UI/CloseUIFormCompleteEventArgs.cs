//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using EasyGameFramework.Core.Resource;

namespace EasyGameFramework.Core.UI
{
    /// <summary>
    /// 关闭界面完成事件。
    /// </summary>
    public sealed class CloseUIFormCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化关闭界面完成事件的新实例。
        /// </summary>
        public CloseUIFormCompleteEventArgs()
        {
            SerialId = 0;
            UIFormAssetAddress = AssetAddress.Empty;
            UIGroup = null;
            UserData = null;
        }

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取界面资源地址。
        /// </summary>
        public AssetAddress UIFormAssetAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        public IUIGroup UIGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建关闭界面完成事件。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetAddress">界面资源地址。</param>
        /// <param name="uiGroup">界面所属的界面组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的关闭界面完成事件。</returns>
        public static CloseUIFormCompleteEventArgs Create(int serialId, AssetAddress uiFormAssetAddress, IUIGroup uiGroup, object userData)
        {
            CloseUIFormCompleteEventArgs closeUIFormCompleteEventArgs = ReferencePool.Acquire<CloseUIFormCompleteEventArgs>();
            closeUIFormCompleteEventArgs.SerialId = serialId;
            closeUIFormCompleteEventArgs.UIFormAssetAddress = uiFormAssetAddress;
            closeUIFormCompleteEventArgs.UIGroup = uiGroup;
            closeUIFormCompleteEventArgs.UserData = userData;
            return closeUIFormCompleteEventArgs;
        }

        /// <summary>
        /// 清理关闭界面完成事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetAddress = AssetAddress.Empty;
            UIGroup = null;
            UserData = null;
        }
    }
}
