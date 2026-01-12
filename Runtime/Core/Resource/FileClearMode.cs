namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源文件清理模式
    /// </summary>
    public enum FileClearMode
    {
        /// <summary>
        /// 清理所有文件
        /// </summary>
        ClearAllBundleFiles,
    
        /// <summary>
        /// 清理未在使用的文件
        /// </summary>
        ClearUnusedBundleFiles,
    
        /// <summary>
        /// 清理指定标签的文件
        /// 说明：需要指定参数，可选：string, string[], List&lt;string&gt;
        /// </summary>
        ClearBundleFilesByTags,
    
        /// <summary>
        /// 清理所有清单
        /// </summary>
        ClearAllManifestFiles,
    
        /// <summary>
        /// 清理未在使用的清单
        /// </summary>
        ClearUnusedManifestFiles,
    }
}
