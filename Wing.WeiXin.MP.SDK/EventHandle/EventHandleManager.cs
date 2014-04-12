using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public static class EventHandleManager
    {
        /// <summary>
        /// 实体处理对象
        /// </summary>
        private static EntityHandler EntityHandler;

        #region 加载实体处理对象 public static void Init(EntityHandler entityHandler)
        /// <summary>
        /// 加载实体处理对象
        /// </summary>
        /// <param name="entityHandler">实体处理对象</param>
        public static void Init(EntityHandler entityHandler)
        {
            if (EntityHandler != null) return;
            EntityHandler = entityHandler;
        } 
        #endregion

        #region 根据创建委托加载实体处理对象 public static void Init(Func<EntityHandler> entityHandlerFunc)
        /// <summary>
        /// 根据创建委托加载实体处理对象
        /// </summary>
        /// <param name="entityHandlerFunc">实体处理对象创建委托</param>
        public static void Init(Func<EntityHandler> entityHandlerFunc)
        {
            if (EntityHandler != null) return;
            EntityHandler = entityHandlerFunc();
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
            if (EntityHandler == null) throw new EventHandlerException("未加载实体处理对象");
            IReturn globalEntity = GlobalAction(message);
            if (globalEntity != null) return globalEntity;
            IReturn wxUserEntity = WXUserBaseAction(message);
            if (wxUserEntity != null) return wxUserEntity;
            IReturn wxUserGroupEntity = WXUserGroupBaseAction(message);
            if (wxUserGroupEntity != null) return wxUserGroupEntity;
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
            if (!ConfigManager.EventConfig.UseGlobalEventHandler || EntityHandler.GlobalHandler == null) return null;

            return EntityHandler.GlobalHandler.Select(handle => handle(message))
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
            if (!ConfigManager.EventConfig.UseWXUserBaseEventHandler || EntityHandler.WXUserBaseHandler == null) return null;
            if (!EntityHandler.WXUserBaseHandler.ContainsKey(message.FromUserName)) return null;
            if (!ConfigManager.EventConfig.EventList.CheckEventForWXUserBase(message.FromUserName)) return null;
            IReturn wxUserEntity = EntityHandler.WXUserBaseHandler[message.FromUserName](message);

            return wxUserEntity;
        }
        #endregion

        #region 基于微信用户分组事件处理 private static IReturn WXUserGroupBaseAction(BaseReceiveMessage message)
        /// <summary>
        /// 基于微信用户分组事件处理
        /// </summary>
        /// <param name="message">接收消息</param>
        /// <returns>回复实体</returns>
        private static IReturn WXUserGroupBaseAction(BaseReceiveMessage message)
        {
            if (!ConfigManager.EventConfig.UseWXUserGroupBaseEventHandler || EntityHandler.WXUserGroupBaseHandler == null) return null;
            try
            {
                WXUserGroup group = WXUserController.GetWXGroupByWXUser(new WXUser { openid = message.FromUserName });
                if (!ConfigManager.EventConfig.EventList.CheckEventForWXUserGroupBase(group.group.id)) return null;
                if (!EntityHandler.WXUserGroupBaseHandler.ContainsKey(group.group.id)) return null;
                IReturn wxUserGroupEntity = EntityHandler.WXUserGroupBaseHandler[group.group.id](message);

                return wxUserGroupEntity;
            }
            catch (ErrorMsgException)
            {
                return null;
            }
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
            if (!ConfigManager.EventConfig.UseCustomEventHandler || EntityHandler.CustomEntityHandler == null) return null;
            if (!EntityHandler.CustomEntityHandler.ContainsKey(message.entityType)) return null;
            IReturn customEntity = EntityHandler.CustomEntityHandler[message.entityType](message);

            return customEntity;
        } 
        #endregion
    }
}
