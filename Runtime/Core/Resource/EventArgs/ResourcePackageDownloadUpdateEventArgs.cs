namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源包下载更新事件。
    /// </summary>
    public class ResourcePackageDownloadUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化资源包下载更新事件的新实例。
        /// </summary>
        public ResourcePackageDownloadUpdateEventArgs()
        {
            PackageName = null;
            Progress = 0f;
            TotalDownloadCount = 0;
            CurrentDownloadCount = 0;
            TotalDownloadBytes = 0;
            CurrentDownloadBytes = 0;
        }

        /// <summary>
        /// 所属包裹名称
        /// </summary>
        public string PackageName { get; private set; }

        /// <summary>
        /// 下载进度 (0-1f)
        /// </summary>
        public float Progress { get; private set; }

        /// <summary>
        /// 下载文件总数
        /// </summary>
        public int TotalDownloadCount { get; private set; }

        /// <summary>
        /// 当前完成的下载文件数量
        /// </summary>
        public int CurrentDownloadCount { get; private set; }

        /// <summary>
        /// 下载数据总大小（单位：字节）
        /// </summary>
        public long TotalDownloadBytes { get; private set; }

        /// <summary>
        /// 当前完成的下载数据大小（单位：字节）
        /// </summary>
        public long CurrentDownloadBytes { get; private set; }

        /// <summary>
        /// 创建资源包下载更新事件。
        /// </summary>
        /// <param name="packageName">资源包名称。</param>
        /// <param name="progress">下载进度。</param>
        /// <param name="totalDownloadCount">下载文件总数。</param>
        /// <param name="currentDownloadCount">当前完成的下载文件数量。</param>
        /// <param name="totalDownloadBytes">下载数据总大小（单位：字节）。</param>
        /// <param name="currentDownloadBytes">当前完成的下载数据大小（单位：字节）。</param>
        /// <returns>创建的资源包下载更新事件。</returns>
        public static ResourcePackageDownloadUpdateEventArgs Create(string packageName, float progress,
            int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
        {
            ResourcePackageDownloadUpdateEventArgs resourcePackageDownloadUpdateEventArgs = ReferencePool.Acquire<ResourcePackageDownloadUpdateEventArgs>();
            resourcePackageDownloadUpdateEventArgs.PackageName = packageName;
            resourcePackageDownloadUpdateEventArgs.Progress = progress;
            resourcePackageDownloadUpdateEventArgs.TotalDownloadCount = totalDownloadCount;
            resourcePackageDownloadUpdateEventArgs.CurrentDownloadCount = currentDownloadCount;
            resourcePackageDownloadUpdateEventArgs.TotalDownloadBytes = totalDownloadBytes;
            resourcePackageDownloadUpdateEventArgs.CurrentDownloadBytes = currentDownloadBytes;
            return resourcePackageDownloadUpdateEventArgs;
        }

        /// <summary>
        /// 清理资源包下载更新事件。
        /// </summary>
        public override void Clear()
        {
            PackageName = null;
            Progress = 0f;
            TotalDownloadCount = 0;
            CurrentDownloadCount = 0;
            TotalDownloadBytes = 0;
            CurrentDownloadBytes = 0;
        }
    }
}
