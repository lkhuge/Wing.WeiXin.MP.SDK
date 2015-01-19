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
        MASSSENDJOBFINISH,

        /// <summary>
        /// 扫码推事件的事件推送
        /// </summary>
        scancode_push,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件推送
        /// </summary>
        scancode_waitmsg,

        /// <summary>
        /// 弹出系统拍照发图的事件推送
        /// </summary>
        pic_sysphoto,

        /// <summary>
        /// 弹出拍照或者相册发图的事件推送
        /// </summary>
        pic_photo_or_album,

        /// <summary>
        /// 弹出微信相册发图器的事件推送
        /// </summary>
        pic_weixin,

        /// <summary>
        /// 弹出地理位置选择器的事件推送
        /// </summary>
        location_select,

        /// <summary>
        /// 发送模板消息事件
        /// </summary>
        TEMPLATESENDJOBFINISH
    }
}
