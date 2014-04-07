﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 全局实体事件处理
    /// </summary>
    public static class GlobalEntityEventHandler
    {
        #region 全局实体事件处理 public readonly static EntityHandler<Entity> GlobalEntityEvent
        /// <summary>
        /// 全局实体事件处理
        /// </summary>
        public readonly static EntityHandler<Entity> GlobalEntityEvent = new EntityHandler<Entity>(); 
        #endregion

        #region 基于微信用户实体处理事件 private static readonly Dictionary<string, EntityHandler<Entity>.Handler> WXUserBaseEntityEvent;
        /// <summary>
        /// 基于微信用户实体处理事件
        /// </summary>
        private static readonly Dictionary<string, EntityHandler<Entity>.Handler> WXUserBaseEntityEvent
            = new Dictionary<string, EntityHandler<Entity>.Handler>();
        #endregion

        #region 基于微信用户分组实体处理事件 private static readonly Dictionary<int, EntityHandler<Entity>.Handler> WXUserGroupBaseEntityEvent;
        /// <summary>
        /// 基于微信用户分组实体处理事件
        /// </summary>
        private static readonly Dictionary<int, EntityHandler<Entity>.Handler> WXUserGroupBaseEntityEvent
            = new Dictionary<int, EntityHandler<Entity>.Handler>();
        #endregion

        #region 添加基于微信用户实体处理事件 public static void AddWXUserBaseEntityEvent(WXUser user, EntityHandler<Entity>.Handler eventHandler)
        /// <summary>
        /// 添加基于微信用户实体处理事件
        /// </summary>
        /// <param name="user">微信用户</param>
        /// <param name="eventHandler">实体处理事件</param>
        public static void AddWXUserBaseEntityEvent(WXUser user, EntityHandler<Entity>.Handler eventHandler)
        {
            WXUserBaseEntityEvent[user.openid] = eventHandler;
        } 
        #endregion

        #region 添加基于微信用户分组实体处理事件 public static void AddWXUserGroupBaseEntityEvent(WXGroup group, EntityHandler<Entity>.Handler eventHandler)
        /// <summary>
        /// 添加基于微信用户分组实体处理事件
        /// </summary>
        /// <param name="group">微信用户分组</param>
        /// <param name="eventHandler">实体处理事件</param>
        public static void AddWXUserGroupBaseEntityEvent(WXGroup group, EntityHandler<Entity>.Handler eventHandler)
        {
            WXUserGroupBaseEntityEvent[group.id] = eventHandler;
        } 
        #endregion

        #region 判断执行全局处理 public static IReturn Action(Entity eventEntity)
        /// <summary>
        /// 判断执行全局处理
        /// </summary>
        /// <param name="eventEntity">有事件的实体</param>
        /// <returns>回复实体</returns>
        public static IReturn Action(Entity eventEntity)
        {
            IReturn globalEntityEvent = GlobalAction(eventEntity);
            if (globalEntityEvent != null) return globalEntityEvent;
            IReturn wxUserEntityEvent = WXUserBaseAction(eventEntity);
            if (wxUserEntityEvent != null) return wxUserEntityEvent;
            IReturn wxUserGroupEntityEvent = WXUserGroupBaseAction(eventEntity);

            return wxUserGroupEntityEvent;
        } 
        #endregion

        #region 全局事件处理 private static IReturn GlobalAction(Entity eventEntity)
        /// <summary>
        /// 全局事件处理
        /// </summary>
        /// <param name="eventEntity">有事件的实体</param>
        /// <returns>回复实体</returns>
        private static IReturn GlobalAction(Entity eventEntity)
        {
            if (!ConfigManager.EventConfig.UseGlobalEventHandler || GlobalEntityEvent.EntityEvent == null) return null;
            IReturn globalEntityEvent = GlobalEntityEvent.EntityEvent(eventEntity);

            return globalEntityEvent;
        } 
        #endregion

        #region 基于微信用户事件处理 private static IReturn WXUserBaseAction(Entity entity)
        /// <summary>
        /// 基于微信用户事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>回复实体</returns>
        private static IReturn WXUserBaseAction(Entity entity)
        {
            if (!ConfigManager.EventConfig.UseWXUserBaseEventHandler ||
                !WXUserBaseEntityEvent.ContainsKey(entity.FromUserName)) return null;
            if (!ConfigManager.EventConfig.EventList.CheckEventForWXUserBase(entity.FromUserName)) return null;
            IReturn wxUserEntityEvent = WXUserBaseEntityEvent[entity.FromUserName](entity);

            return wxUserEntityEvent;
        } 
        #endregion

        #region 基于微信用户分组事件处理 private static IReturn WXUserGroupBaseAction(Entity entity)
        /// <summary>
        /// 基于微信用户分组事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>回复实体</returns>
        private static IReturn WXUserGroupBaseAction(Entity entity)
        {
            if (!ConfigManager.EventConfig.UseWXUserGroupBaseEventHandler) return null;
            try
            {
                WXUserGroup group = WXUserController.GetWXGroupByWXUser(new WXUser { openid = entity.FromUserName });
                if (!ConfigManager.EventConfig.EventList.CheckEventForWXUserGroupBase(group.group.id)) return null;
                if (!WXUserGroupBaseEntityEvent.ContainsKey(group.group.id)) return null;
                IReturn wxUserGroupEntityEvent = WXUserGroupBaseEntityEvent[group.group.id](entity);
                return wxUserGroupEntityEvent;
            }
            catch (ErrorMsgException)
            {
                return null;
            }
        } 
        #endregion
    }
}
