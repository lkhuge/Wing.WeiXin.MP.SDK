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
        /// 任意消息
        /// </summary>
        Any,

        /// <summary>
        /// 图片消息
        /// </summary>
        image,

        /// <summary>
        /// 链接消息
        /// </summary>
        link,

        /// <summary>
        /// 地理位置
        /// </summary>
        location,

        /// <summary>
        /// 文本消息
        /// </summary>
        text,

        /// <summary>
        /// 视频消息
        /// </summary>
        video,

        /// <summary>
        /// 语音消息
        /// </summary>
        voice,

        /// <summary>
        /// 将消息转发到多客服
        /// </summary>
        transfer_customer_service,

        /// <summary>
        /// 点击菜单拉取消息时的事件
        /// </summary>
        CLICK,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        LOCATION,

        /// <summary>
        /// 带参数二维码关注事件
        /// </summary>
        subscribeByQRScene,

        /// <summary>
        /// 关注事件
        /// </summary>
        subscribe,

        /// <summary>
        /// 取消关注事件
        /// </summary>
        unsubscribe,

        /// <summary>
        /// 点击菜单跳转链接时的事件
        /// </summary>
        VIEW,

        /// <summary>
        /// 带参数二维码事件
        /// </summary>
        SCAN,

        /// <summary>
        /// 推送群发结果事件
        /// </summary>
        MASSSENDJOBFINISH
    }
}
