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
    /// 打开界面失败事件。
    /// </summary>
    public sealed class OpenUIFormFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化打开界面失败事件的新实例。
        /// </summary>
        public OpenUIFormFailureEventArgs()
        {
            SerialId = 0;
            UIFormAssetAddress = AssetAddress.Empty;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
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
        /// 获取界面组名称。
        /// </summary>
        public string UIGroupName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string ErrorMessage
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
        /// 创建打开界面失败事件。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetAddress">界面资源地址。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的打开界面失败事件。</returns>
        public static OpenUIFormFailureEventArgs Create(int serialId, AssetAddress uiFormAssetAddress, string uiGroupName, bool pauseCoveredUIForm, string errorMessage, object userData)
        {
            OpenUIFormFailureEventArgs openUIFormFailureEventArgs = ReferencePool.Acquire<OpenUIFormFailureEventArgs>();
            openUIFormFailureEventArgs.SerialId = serialId;
            openUIFormFailureEventArgs.UIFormAssetAddress = uiFormAssetAddress;
            openUIFormFailureEventArgs.UIGroupName = uiGroupName;
            openUIFormFailureEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormFailureEventArgs.ErrorMessage = errorMessage;
            openUIFormFailureEventArgs.UserData = userData;
            return openUIFormFailureEventArgs;
        }

        /// <summary>
        /// 清理打开界面失败事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetAddress = AssetAddress.Empty;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
