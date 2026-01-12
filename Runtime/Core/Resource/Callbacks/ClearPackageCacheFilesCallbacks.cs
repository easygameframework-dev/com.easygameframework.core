namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 清理资源包缓存文件成功时的回调函数。
    /// </summary>
    /// <param name="packageName">资源包名称。</param>
    public delegate void ClearPackageCacheFilesSuccessCallback(string packageName);

    /// <summary>
    /// 清理资源包缓存文件失败时的回调函数。
    /// </summary>
    /// <param name="packageName">资源包名称。</param>
    /// <param name="errorMessage">错误信息。</param>
    public delegate void ClearPackageCacheFilesFailureCallback(string packageName, string errorMessage);

    /// <summary>
    /// 清理资源包缓存文件时的回调函数集。
    /// </summary>
    public class ClearPackageCacheFilesCallbacks
    {
        private ClearPackageCacheFilesSuccessCallback m_ClearPackageCacheFilesSuccess;
        private ClearPackageCacheFilesFailureCallback m_ClearPackageCacheFilesFailure;

        /// <summary>
        /// 初始化清理资源包缓存文件回调函数集的新实例。
        /// </summary>
        /// <param name="clearPackageCacheFilesSuccess">清理资源包缓存文件成功时的回调函数。</param>
        /// <param name="clearPackageCacheFilesFailure">清理资源包缓存文件失败时的回调函数。</param>
        public ClearPackageCacheFilesCallbacks(
            ClearPackageCacheFilesSuccessCallback clearPackageCacheFilesSuccess,
            ClearPackageCacheFilesFailureCallback clearPackageCacheFilesFailure)
        {
            m_ClearPackageCacheFilesSuccess = clearPackageCacheFilesSuccess;
            m_ClearPackageCacheFilesFailure = clearPackageCacheFilesFailure;
        }

        /// <summary>
        /// 获取清理资源包缓存文件成功时的回调函数。
        /// </summary>
        public ClearPackageCacheFilesSuccessCallback ClearPackageCacheFilesSuccess => m_ClearPackageCacheFilesSuccess;

        /// <summary>
        /// 获取清理资源包缓存文件失败时的回调函数。
        /// </summary>
        public ClearPackageCacheFilesFailureCallback ClearPackageCacheFilesFailure => m_ClearPackageCacheFilesFailure;
    }
}
