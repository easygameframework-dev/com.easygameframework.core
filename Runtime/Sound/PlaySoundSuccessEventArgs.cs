//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using EasyGameFramework.Core;
using EasyGameFramework.Core.Event;
using EasyGameFramework.Core.Resource;
using EasyGameFramework.Core.Sound;

namespace EasyGameFramework
{
    /// <summary>
    /// 播放声音成功事件。
    /// </summary>
    public sealed class PlaySoundSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 初始化播放声音成功事件的新实例。
        /// </summary>
        public PlaySoundSuccessEventArgs()
        {
            SerialId = 0;
            SoundAssetAddress = AssetAddress.Empty;
            SoundAgent = null;
            Duration = 0f;
            BindingEntity = null;
            UserData = null;
        }

        /// <summary>
        /// 获取声音的序列编号。
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取声音资源地址。
        /// </summary>
        public AssetAddress SoundAssetAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取用于播放的声音代理。
        /// </summary>
        public ISoundAgent SoundAgent
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取加载持续时间。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取声音绑定的实体。
        /// </summary>
        public Entity BindingEntity
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
        /// 创建播放声音成功事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的播放声音成功事件。</returns>
        public static PlaySoundSuccessEventArgs Create(EasyGameFramework.Core.Sound.PlaySoundSuccessEventArgs e)
        {
            PlaySoundInfo playSoundInfo = (PlaySoundInfo)e.UserData;
            PlaySoundSuccessEventArgs playSoundSuccessEventArgs = ReferencePool.Acquire<PlaySoundSuccessEventArgs>();
            playSoundSuccessEventArgs.SerialId = e.SerialId;
            playSoundSuccessEventArgs.SoundAssetAddress = e.SoundAssetAddress;
            playSoundSuccessEventArgs.SoundAgent = e.SoundAgent;
            playSoundSuccessEventArgs.Duration = e.Duration;
            playSoundSuccessEventArgs.BindingEntity = playSoundInfo.BindingEntity;
            playSoundSuccessEventArgs.UserData = playSoundInfo.UserData;
            ReferencePool.Release(playSoundInfo);
            return playSoundSuccessEventArgs;
        }

        /// <summary>
        /// 清理播放声音成功事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetAddress = AssetAddress.Empty;
            SoundAgent = null;
            Duration = 0f;
            BindingEntity = null;
            UserData = null;
        }
    }
}
