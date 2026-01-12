//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using EasyGameFramework.Core;
using EasyGameFramework.Core.Event;
using EasyGameFramework.Core.Network;

namespace EasyGameFramework
{
    /// <summary>
    /// 网络连接关闭事件。
    /// </summary>
    public sealed class NetworkClosedEventArgs : GameEventArgs
    {
        /// <summary>
        /// 初始化网络连接关闭事件的新实例。
        /// </summary>
        public NetworkClosedEventArgs()
        {
            NetworkChannel = null;
        }

        /// <summary>
        /// 获取网络频道。
        /// </summary>
        public INetworkChannel NetworkChannel
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建网络连接关闭事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的网络连接关闭事件。</returns>
        public static NetworkClosedEventArgs Create(EasyGameFramework.Core.Network.NetworkClosedEventArgs e)
        {
            NetworkClosedEventArgs networkClosedEventArgs = ReferencePool.Acquire<NetworkClosedEventArgs>();
            networkClosedEventArgs.NetworkChannel = e.NetworkChannel;
            return networkClosedEventArgs;
        }

        /// <summary>
        /// 清理网络连接关闭事件。
        /// </summary>
        public override void Clear()
        {
            NetworkChannel = null;
        }
    }
}
