namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源对象。
    /// </summary>
    public class AssetObject : IReference
    {
        /// <summary>
        /// 资源。
        /// </summary>
        public object Asset { get; private set; }

        /// <summary>
        /// 是否为场景资源。
        /// </summary>
        public bool IsScene { get; private set; }

        /// <summary>
        /// 用户自定义数据。
        /// </summary>
        public object UserData { get; private set; }

        /// <summary>
        /// 创建资源对象。
        /// </summary>
        /// <param name="asset">资源。</param>
        /// <param name="isScene">是否为场景资源。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的资源对象。</returns>
        public static AssetObject Create(object asset, bool isScene, object userData)
        {
            var assetObject = ReferencePool.Acquire<AssetObject>();
            assetObject.Asset = asset;
            assetObject.IsScene = isScene;
            assetObject.UserData = userData;
            return assetObject;
        }

        /// <summary>
        /// 获取资源对象的哈希码。
        /// </summary>
        /// <returns>资源对象的哈希码。</returns>
        public override int GetHashCode()
        {
            return Asset.GetHashCode();
        }

        /// <summary>
        /// 获取资源对象的字符串表示。
        /// </summary>
        /// <returns>资源对象的字符串表示。</returns>
        public override string ToString()
        {
            return Asset.ToString();
        }

        /// <summary>
        /// 清理资源对象。
        /// </summary>
        public void Clear()
        {
            Asset = null;
            IsScene = false;
            UserData = null;
        }
    }
}
