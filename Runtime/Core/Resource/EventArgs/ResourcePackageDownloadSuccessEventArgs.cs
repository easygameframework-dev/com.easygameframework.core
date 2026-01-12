namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源包下载成功事件。
    /// </summary>
    public class ResourcePackageDownloadSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化资源包下载成功事件的新实例。
        /// </summary>
        public ResourcePackageDownloadSuccessEventArgs()
        {
            PackageName = null;
        }

        /// <summary>
        /// 所属包裹名称
        /// </summary>
        public string PackageName { get; private set; }

        /// <summary>
        /// 创建资源包下载成功事件。
        /// </summary>
        /// <param name="packageName">资源包名称。</param>
        /// <returns>创建的资源包下载成功事件。</returns>
        public static ResourcePackageDownloadSuccessEventArgs Create(string packageName)
        {
            ResourcePackageDownloadSuccessEventArgs packageDownloadSuccessEventArgs = ReferencePool.Acquire<ResourcePackageDownloadSuccessEventArgs>();
            packageDownloadSuccessEventArgs.PackageName = packageName;
            return packageDownloadSuccessEventArgs;
        }

        /// <summary>
        /// 清理资源包下载成功事件。
        /// </summary>
        public override void Clear()
        {
            PackageName = null;
        }
    }
}
