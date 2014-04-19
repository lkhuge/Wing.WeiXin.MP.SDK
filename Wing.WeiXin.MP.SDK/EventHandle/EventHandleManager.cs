using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public static class EventHandleManager
    {
        /// <summary>
        /// 实体处理对象列表
        /// </summary>
        private static Dictionary<string, EntityHandler> EntityHandlerList;

        #region 加载实体处理对象 public static void Init(Dictionary<string,EntityHandler> entityHandler)
        /// <summary>
        /// 加载实体处理对象
        /// </summary>
        /// <param name="entityHandlerList">实体处理对象列表</param>
        public static void Init(Dictionary<string, EntityHandler> entityHandlerList)
        {
            if (EntityHandlerList != null) return;
            EntityHandlerList = entityHandlerList;
        } 
        #endregion

        #region 根据创建委托加载实体处理对象 public static void Init(Func<EntityHandler> entityHandlerFunc)
        /// <summary>
        /// 根据创建委托加载实体处理对象
        /// </summary>
        /// <param name="entityHandlerFunc">实体处理对象列表创建委托</param>
        public static void Init(Func<Dictionary<string, EntityHandler>> entityHandlerFunc)
        {
            if (EntityHandlerList != null) return;
            EntityHandlerList = entityHandlerFunc();
        }
        #endregion

        #region 执行处理 public static IReturn Action(BaseReceiveMessage message)
        /// <summary>
        /// 执行处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        public static IReturn Action(BaseReceiveMessage message)
        {
            if (EntityHandlerList == null) throw new EventHandlerException("未加载实体处理对象");
            if (!EntityHandlerList.ContainsKey(message.ToUserName)) throw new NoResponseException("未注册事件处理对象");
            IReturn globalEntity = GlobalAction(message);
            if (globalEntity != null) return globalEntity;
            IReturn wxUserEntity = WXUserBaseAction(message);
            if (wxUserEntity != null) return wxUserEntity;
            IReturn wxUserGroupEntity = WXUserGroupBaseAction(message);
            if (wxUserGroupEntity != null) return wxUserGroupEntity;
            IReturn quickConfigReturnMessage = QuickConfigReturnMessageAction(message);
            if (quickConfigReturnMessage != null) return quickConfigReturnMessage;
            IReturn customEntity = CustomAction(message);
            if (customEntity == null) throw new NoResponseException("无事件处理");

            return customEntity;
        } 
        #endregion

        #region 全局事件处理 private static IReturn GlobalAction(BaseReceiveMessage message)
        /// <summary>
        /// 全局事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn GlobalAction(BaseReceiveMessage message)
        {
            if (!ConfigManager.EventConfig.UseGlobalEventHandler || EntityHandlerList[message.ToUserName].GlobalHandler == null) return null;

            return EntityHandlerList[message.ToUserName].GlobalHandler.Select(handle => handle(message))
                .FirstOrDefault(globalEntity => globalEntity != null);
        }
        #endregion

        #region 基于微信用户事件处理 private static IReturn WXUserBaseAction(BaseReceiveMessage message)
        /// <summary>
        /// 基于微信用户事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn WXUserBaseAction(BaseReceiveMessage message)
        {
            if (!ConfigManager.EventConfig.UseWXUserBaseEventHandler 
                || EntityHandlerList[message.ToUserName].WXUserBaseHandler == null) return null;
            if (!EntityHandlerList[message.ToUserName].WXUserBaseHandler.ContainsKey(message.FromUserName)) return null;
            if (!ConfigManager.EventConfig.EventList.CheckEventForWXUserBase(message.FromUserName)) return null;
            IReturn wxUserEntity = EntityHandlerList[message.ToUserName].WXUserBaseHandler[message.FromUserName](message);

            return wxUserEntity;
        }
        #endregion

        #region 微信用户所属分组信息 private static readonly Dictionary<string, int> WXUserGroupList
        /// <summary>
        /// 微信用户所属分组信息
        /// </summary>
        private static readonly Dictionary<string, int> WXUserGroupList = new Dictionary<string, int>(); 
        #endregion

        #region 基于微信用户分组事件处理 private static IReturn WXUserGroupBaseAction(BaseReceiveMessage message)
        /// <summary>
        /// 基于微信用户分组事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn WXUserGroupBaseAction(BaseReceiveMessage message)
        {
            if (!ConfigManager.EventConfig.UseWXUserGroupBaseEventHandler
                || EntityHandlerList[message.ToUserName].WXUserGroupBaseHandler == null) return null;
            try
            {
                if (!WXUserGroupList.ContainsKey(message.FromUserName))
                {
                    WXUserGroupList[message.FromUserName] = 
                        WXUserController.GetWXGroupByWXUser(message.FromUserName, 
                            new WXUser { openid = message.FromUserName }).group.id;
                }
                if (!ConfigManager.EventConfig.EventList
                    .CheckEventForWXUserGroupBase(WXUserGroupList[message.FromUserName])) return null;
                if (!EntityHandlerList[message.ToUserName]
                    .WXUserGroupBaseHandler.ContainsKey(WXUserGroupList[message.FromUserName])) return null;
                IReturn wxUserGroupEntity = EntityHandlerList[message.ToUserName]
                    .WXUserGroupBaseHandler[WXUserGroupList[message.FromUserName]](message);

                return wxUserGroupEntity;
            }
            catch (ErrorMsgException)
            {
                return null;
            }
        }
        #endregion

        #region 基于快速配置回复消息事件处理 private static IReturn QuickConfigReturnMessageAction(BaseReceiveMessage message)
        /// <summary>
        /// 基于快速配置回复消息事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn QuickConfigReturnMessageAction(BaseReceiveMessage message)
        {
            if (message.entityType != ReceiveEntityType.MessageText) return null;
            MessageText messageText = message as MessageText;
            if (messageText == null) return null;
            if (ConfigManager.EventConfig.QuickConfigReturnMessageList == null
                || ConfigManager.EventConfig.QuickConfigReturnMessageList.Count != 0) return null;

            return ConfigManager.EventConfig.QuickConfigReturnMessageList.GetQuickConfigReturnMessage(messageText);
        } 
        #endregion

        #region 自定义事件处理 private static IReturn CustomAction(BaseReceiveMessage message)
        /// <summary>
        /// 自定义事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn CustomAction(BaseReceiveMessage message)
        {
            if (!ConfigManager.EventConfig.UseCustomEventHandler) return null;

            return !CustomHandlerList.ContainsKey(message.entityType) 
                ? null 
                : CustomHandlerList[message.entityType](message);
        } 
        #endregion

        #region 自定义事件处理列表 private static readonly Dictionary<ReceiveEntityType, Func<BaseReceiveMessage, IReturn>> CustomHandlerList
        /// <summary>
        /// 自定义事件处理列表
        /// </summary>
        private static readonly Dictionary<ReceiveEntityType, Func<BaseReceiveMessage, IReturn>> CustomHandlerList =
            new Dictionary<ReceiveEntityType, Func<BaseReceiveMessage, IReturn>>
            {
                {ReceiveEntityType.MessageImage, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageImageHandler, message as MessageImage)},
                {ReceiveEntityType.MessageLink, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageLinkHandler, message as MessageLink)},
                {ReceiveEntityType.MessageLocation, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageLocationHandler, message as MessageLocation)},
                {ReceiveEntityType.MessageText, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageTextHandler, message as MessageText)},
                {ReceiveEntityType.MessageVideo, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageVideoHandler, message as MessageVideo)},
                {ReceiveEntityType.MessageVoice, message => CustomListAction(EntityHandlerList[message.ToUserName].MessageVoiceHandler, message as MessageVoice)},
                {ReceiveEntityType.EventClick, message => CustomListAction(EntityHandlerList[message.ToUserName].EventClickHandler, message as EventClick)},
                {ReceiveEntityType.EventLocation, message => CustomListAction(EntityHandlerList[message.ToUserName].EventLocationHandler, message as EventLocation)},
                {ReceiveEntityType.EventSubscribeByQRScene, message => CustomListAction(EntityHandlerList[message.ToUserName].EventSubscribeByQRSceneHandler, message as EventSubscribeByQRScene)},
                {ReceiveEntityType.EventSubscribe, message => CustomListAction(EntityHandlerList[message.ToUserName].EventSubscribeHandler, message as EventSubscribe)},
                {ReceiveEntityType.EventUnsubscribe, message => CustomListAction(EntityHandlerList[message.ToUserName].EventUnsubscribeHandler, message as EventUnsubscribe)},
                {ReceiveEntityType.EventView, message => CustomListAction(EntityHandlerList[message.ToUserName].EventViewHandler, message as EventView)},
                {ReceiveEntityType.EventWithQRScene, message => CustomListAction(EntityHandlerList[message.ToUserName].EventWithQRSceneHandler, message as EventWithQRScene)}
            }; 
        #endregion

        #region 自定义事件列表处理 private static IReturn CustomListAction<T>(EntityHandler.CustomEntityHandler<T>[] handler, T message) where T : BaseReceiveMessage
        /// <summary>
        /// 自定义事件列表处理
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="handler">自定义事件列表</param>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn CustomListAction<T>(EntityHandler.CustomEntityHandler<T>[] handler, T message)
            where T : BaseReceiveMessage
        {
            if (handler == null) return null;
            return handler.Select(handle => handle(message))
                .FirstOrDefault(entity => entity != null);
        } 
        #endregion
    }
}
