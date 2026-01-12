using System;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源信息。
    /// </summary>
    public class AssetInfo
    {
        private readonly string m_PackageName;
        private readonly Type m_AssetType;
        private readonly string m_Error;
        private readonly string m_AssetName;

        private readonly object m_UserData;


        /// <summary>
        /// 所属包裹
        /// </summary>
        public string PackageName => m_PackageName;

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type AssetType => m_AssetType;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error => m_Error;

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName => m_AssetName;

        /// <summary>
        /// 引用对象
        /// </summary>
        public object UserData => m_UserData;

        /// <summary>
        /// 初始化资源信息的新实例。
        /// </summary>
        /// <param name="packageName">资源包名称。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="assetName">资源名称。</param>
        /// <param name="error">错误信息。</param>
        /// <param name="userData">用户自定义数据。</param>
        public AssetInfo(string packageName, Type assetType, string assetName, string error, object userData)
        {
            m_PackageName = packageName;
            m_AssetType = assetType;
            m_AssetName = assetName;
            m_Error = error;
            m_UserData = userData;
        }
    }
}
