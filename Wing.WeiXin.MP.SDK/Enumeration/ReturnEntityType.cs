using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Enumeration
{
    /// <summary>
    /// 回复消息实体类型
    /// </summary>
    public enum ReturnEntityType
    {
        /// <summary>
        /// 回复图片消息
        /// </summary>
        ReturnMessageImage,

        /// <summary>
        /// 回复音乐消息
        /// </summary>
        ReturnMessageMusic,

        /// <summary>
        /// 回复图文消息
        /// </summary>
        ReturnMessageNews,

        /// <summary>
        /// 回复文本消息
        /// </summary>
        ReturnMessageText,

        /// <summary>
        /// 回复视频消息
        /// </summary>
        ReturnMessageVideo,

        /// <summary>
        /// 回复语音消息
        /// </summary>
        ReturnMessageVoice
    }
}
