namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源包下载失败事件。
    /// </summary>
    public class ResourcePackageDownloadFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化资源包下载失败事件的新实例。
        /// </summary>
        public ResourcePackageDownloadFailureEventArgs()
        {
            PackageName = null;
            FileName = null;
            ErrorMessage = null;
        }

        /// <summary>
        /// 所属包裹名称
        /// </summary>
        public string PackageName { get; private set; }

        /// <summary>
        /// 下载失败的文件名称
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 创建资源包下载失败事件。
        /// </summary>
        /// <param name="packageName">资源包名称。</param>
        /// <param name="fileName">下载失败的文件名称。</param>
        /// <param name="errorMessage">错误信息。</param>
        /// <returns>创建的资源包下载失败事件。</returns>
        public static ResourcePackageDownloadFailureEventArgs Create(string packageName, string fileName,
            string errorMessage)
        {
            ResourcePackageDownloadFailureEventArgs resourcePackageDownloadFailureEventArgs = ReferencePool.Acquire<ResourcePackageDownloadFailureEventArgs>();
            resourcePackageDownloadFailureEventArgs.PackageName = packageName;
            resourcePackageDownloadFailureEventArgs.FileName = fileName;
            resourcePackageDownloadFailureEventArgs.ErrorMessage = errorMessage;
            return resourcePackageDownloadFailureEventArgs;
        }

        /// <summary>
        /// 清理资源包下载失败事件。
        /// </summary>
        public override void Clear()
        {
            PackageName = null;
            FileName = null;
            ErrorMessage = null;
        }
    }
}
