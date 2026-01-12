using System;

namespace EasyGameFramework.Core.Resource
{
    /// <summary>
    /// 资源地址。
    /// </summary>
    public readonly struct AssetAddress : IEquatable<AssetAddress>
    {
        /// <summary>
        /// 空的资源地址。
        /// </summary>
        public static readonly AssetAddress Empty = new AssetAddress("", "");

        /// <summary>
        /// 资源包名称。
        /// </summary>
        public readonly string PackageName;

        /// <summary>
        /// 资源位置。
        /// </summary>
        public readonly string Location;

        /// <summary>
        /// 初始化资源地址的新实例。
        /// </summary>
        /// <param name="packageName">资源包名称。</param>
        /// <param name="location">资源位置。</param>
        public AssetAddress(string packageName, string location)
        {
            PackageName = packageName;
            Location = location;
        }

        /// <summary>
        /// 检查资源地址是否有效。
        /// </summary>
        /// <returns>资源地址是否有效。</returns>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(PackageName) && !string.IsNullOrEmpty(Location);
        }

        /// <summary>
        /// 获取资源地址的字符串表示。
        /// </summary>
        /// <returns>资源地址的字符串表示。</returns>
        public override string ToString()
        {
            return $"{PackageName}/{Location}";
        }

        /// <summary>
        /// 获取资源地址的哈希码。
        /// </summary>
        /// <returns>资源地址的哈希码。</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(PackageName, Location);
        }

        /// <summary>
        /// 检查资源地址是否等于指定对象。
        /// </summary>
        /// <param name="obj">要比较的对象。</param>
        /// <returns>资源地址是否等于指定对象。</returns>
        public override bool Equals(object obj)
        {
            return obj is AssetAddress other && Equals(other);
        }

        /// <summary>
        /// 检查资源地址是否等于指定资源地址。
        /// </summary>
        /// <param name="other">要比较的资源地址。</param>
        /// <returns>资源地址是否等于指定资源地址。</returns>
        public bool Equals(AssetAddress other)
        {
            return PackageName == other.PackageName && Location == other.Location;
        }

        /// <summary>
        /// 检查两个资源地址是否相等。
        /// </summary>
        /// <param name="left">左边的资源地址。</param>
        /// <param name="right">右边的资源地址。</param>
        /// <returns>两个资源地址是否相等。</returns>
        public static bool operator ==(AssetAddress left, AssetAddress right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 检查两个资源地址是否不相等。
        /// </summary>
        /// <param name="left">左边的资源地址。</param>
        /// <param name="right">右边的资源地址。</param>
        /// <returns>两个资源地址是否不相等。</returns>
        public static bool operator !=(AssetAddress left, AssetAddress right)
        {
            return !left.Equals(right);
        }
    }
}
