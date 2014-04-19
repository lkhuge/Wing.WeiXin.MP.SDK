using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 实体处理对象
    /// </summary>
    public class EntityHandler
    {
        #region 全局实体事件处理 public delegate IReturn GlobalEntityHandler(BaseReceiveMessage message); 
        /// <summary>
        /// 全局实体事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复消息</returns>
        public delegate IReturn GlobalEntityHandler(BaseReceiveMessage message); 
        #endregion

        #region 自定义实体事件处理 public delegate IReturn CustomEntityHandler<in T>(T message) where T : BaseReceiveMessage;
        /// <summary>
        /// 自定义实体事件处理
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="message">接收消息</param>
        /// <returns>回复消息</returns>
        public delegate IReturn CustomEntityHandler<in T>(T message) where T : BaseReceiveMessage;
        #endregion

        #region 全局事件处理 public GlobalEntityHandler[] GlobalHandlerList { get; set; }
        /// <summary>
        /// 全局事件处理
        /// </summary>
        public GlobalEntityHandler[] GlobalHandlerList { get; set; }  
        #endregion

        #region 基于微信用户事件处理 public Dictionary<string, GlobalEntityHandler> WXUserBaseHandlerList { get; set; }
        /// <summary>
        /// 基于微信用户事件处理
        /// </summary>
        public Dictionary<string, GlobalEntityHandler> WXUserBaseHandlerList { get; set; }
        #endregion

        #region 基于微信用户分组事件处理 public Dictionary<int, GlobalEntityHandler> WXUserGroupBaseHandlerList { get; set; }
        /// <summary>
        /// 基于微信用户分组事件处理
        /// </summary>
        public Dictionary<int, GlobalEntityHandler> WXUserGroupBaseHandlerList { get; set; }
        #endregion

        #region 图片消息实体事件处理列表 public CustomEntityHandler<MessageImage>[] MessageImageHandlerList { get; set; }
        /// <summary>
        /// 图片消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageImage>[] MessageImageHandlerList { get; set; }
        #endregion

        #region 链接消息实体事件处理列表 public CustomEntityHandler<MessageLink>[] MessageLinkHandlerList { get; set; }
        /// <summary>
        /// 链接消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageLink>[] MessageLinkHandlerList { get; set; }
        #endregion

        #region 地理位置消息实体事件处理列表 public CustomEntityHandler<MessageLocation>[] MessageLocationHandlerList { get; set; }
        /// <summary>
        /// 地理位置消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageLocation>[] MessageLocationHandlerList { get; set; }
        #endregion

        #region 文本消息实体事件处理列表 public CustomEntityHandler<MessageText>[] MessageTextHandlerList { get; set; }
        /// <summary>
        /// 文本消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageText>[] MessageTextHandlerList { get; set; }
        #endregion

        #region 视频消息实体事件处理列表 public CustomEntityHandler<MessageVideo>[] MessageVideoHandlerList { get; set; }
        /// <summary>
        /// 视频消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageVideo>[] MessageVideoHandlerList { get; set; }
        #endregion

        #region 语音消息实体事件处理列表 public CustomEntityHandler<MessageVoice>[] MessageVoiceHandlerList { get; set; }
        /// <summary>
        /// 语音消息实体事件处理列表
        /// </summary>
        public CustomEntityHandler<MessageVoice>[] MessageVoiceHandlerList { get; set; }
        #endregion

        #region 自定义菜单事件（点击菜单拉取消息时的事件推送）处理列表 public CustomEntityHandler<EventClick>[] EventClickHandlerList { get; set; }
        /// <summary>
        /// 自定义菜单事件（点击菜单拉取消息时的事件推送）处理列表
        /// </summary>
        public CustomEntityHandler<EventClick>[] EventClickHandlerList { get; set; } 
        #endregion

        #region 上报地理位置事件处理列表 public CustomEntityHandler<EventLocation>[] EventLocationHandlerList { get; set; }
        /// <summary>
        /// 上报地理位置事件处理列表
        /// </summary>
        public CustomEntityHandler<EventLocation>[] EventLocationHandlerList { get; set; }
        #endregion

        #region 关注事件处理列表 public CustomEntityHandler<EventSubscribe>[] EventSubscribeHandlerList { get; set; }
        /// <summary>
        /// 关注事件处理列表
        /// </summary>
        public CustomEntityHandler<EventSubscribe>[] EventSubscribeHandlerList { get; set; }
        #endregion

        #region 带参数二维码关注事件处理列表 public CustomEntityHandler<EventSubscribeByQRScene>[] EventSubscribeByQRSceneHandlerList { get; set; }
        /// <summary>
        /// 带参数二维码关注事件处理列表
        /// </summary>
        public CustomEntityHandler<EventSubscribeByQRScene>[] EventSubscribeByQRSceneHandlerList { get; set; }
        #endregion

        #region 取消关注事件处理列表 public CustomEntityHandler<EventUnsubscribe>[] EventUnsubscribeHandlerList { get; set; }
        /// <summary>
        /// 取消关注事件处理列表
        /// </summary>
        public CustomEntityHandler<EventUnsubscribe>[] EventUnsubscribeHandlerList { get; set; }
        #endregion

        #region 自定义菜单事件（点击菜单跳转链接时的事件推送）处理列表 public CustomEntityHandler<EventView>[] EventViewHandlerList { get; set; }
        /// <summary>
        /// 自定义菜单事件（点击菜单跳转链接时的事件推送）处理列表
        /// </summary>
        public CustomEntityHandler<EventView>[] EventViewHandlerList { get; set; }
        #endregion

        #region 带参数二维码事件处理列表 public CustomEntityHandler<EventWithQRScene>[] EventWithQRSceneHandlerList { get; set; }
        /// <summary>
        /// 带参数二维码事件处理列表
        /// </summary>
        public CustomEntityHandler<EventWithQRScene>[] EventWithQRSceneHandlerList { get; set; }
        #endregion

        #region 推送群发结果事件处理列表 public CustomEntityHandler<EventMessageSendAllFinish>[] EventMessageSendAllFinishHandlerList { get; set; }
        /// <summary>
        /// 推送群发结果事件处理列表
        /// </summary>
        public CustomEntityHandler<EventMessageSendAllFinish>[] EventMessageSendAllFinishHandlerList { get; set; }
        #endregion
    }
}
