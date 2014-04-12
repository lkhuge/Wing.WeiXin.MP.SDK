using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Enumeration
{
    /// <summary>
    /// 接收消息实体类型
    /// </summary>
    public enum ReceiveEntityType
    {
        /// <summary>
        /// 图片消息
        /// </summary>
        MessageImage,

        /// <summary>
        /// 链接消息
        /// </summary>
        MessageLink,

        /// <summary>
        /// 地理位置
        /// </summary>
        MessageLocation,

        /// <summary>
        /// 文本消息
        /// </summary>
        MessageText,

        /// <summary>
        /// 视频消息
        /// </summary>
        MessageVideo,

        /// <summary>
        /// 语音消息
        /// </summary>
        MessageVoice,

        /// <summary>
        /// 点击菜单拉取消息时的事件
        /// </summary>
        EventClick,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        EventLocation,

        /// <summary>
        /// 带参数二维码关注事件
        /// </summary>
        EventSubscribeByQRScene,

        /// <summary>
        /// 关注事件
        /// </summary>
        EventSubscribe,

        /// <summary>
        /// 取消关注事件
        /// </summary>
        EventUnsubscribe,

        /// <summary>
        /// 点击菜单跳转链接时的事件
        /// </summary>
        EventView,

        /// <summary>
        /// 带参数二维码事件
        /// </summary>
        EventWithQRScene,
    }
}
