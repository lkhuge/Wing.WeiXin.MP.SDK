﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Extension
{
    /// <summary>
    /// 实体处理扩展
    /// </summary>
    public static class EntityHandlerExtension
    {
        #region 获取自动回复委托 public static EntityHandler.CustomEntityHandler<MessageText> GetEventHandlerAutoReturnMessageText(Func<string, string> messageHandler, EntityHandler.CustomEntityHandler<MessageText> nullMessageCallBack = null)
        /// <summary>
        /// 获取自动回复委托
        /// </summary>
        /// <param name="messageHandler">消息处理</param>
        /// <param name="nullMessageCallBack">没有消息的回调方法</param>
        /// <returns>自动回复委托</returns>
        public static EntityHandler.CustomEntityHandler<MessageText> GetEventHandlerAutoReturnMessageText(
            Func<string, string> messageHandler,
            EntityHandler.CustomEntityHandler<MessageText> nullMessageCallBack = null)
        {
            return message =>
            {
                string returnMessage = messageHandler(message.Content);
                if (String.IsNullOrEmpty(returnMessage))
                {
                    if (nullMessageCallBack == null)
                    {
                        throw new NoResponseException("无自动回复消息");
                    }
                    return nullMessageCallBack(message);
                }
                
                return new ReturnMessageText
                {
                    FromUserName = message.ToUserName,
                    ToUserName = message.FromUserName,
                    content = returnMessage
                };
            };
        } 
        #endregion

        #region 获取根据Key回复委托 public static EntityHandler.CustomEntityHandler<EventClick> GetEventHandlerReturnByKey(Dictionary<string, EntityHandler.CustomEntityHandler<EventClick>> keyHandlerList, EntityHandler.CustomEntityHandler<EventClick> nullMessageCallBack = null)
        /// <summary>
        /// 获取根据Key回复委托
        /// </summary>
        /// <param name="keyHandlerList">根据Key回复委托列表</param>
        /// <param name="nullMessageCallBack">没有消息的回调方法</param>
        /// <returns>根据Key回复委托</returns>
        public static EntityHandler.CustomEntityHandler<EventClick> GetEventHandlerReturnByKey(
            Dictionary<string, EntityHandler.CustomEntityHandler<EventClick>> keyHandlerList,
            EntityHandler.CustomEntityHandler<EventClick> nullMessageCallBack = null)
        {
            return message =>
            {
                string key = message.EventKey;
                if (keyHandlerList.ContainsKey(key))
                {
                    IReturn returnMessage = keyHandlerList[key](message);
                    if (returnMessage != null) return returnMessage;
                }
                if (nullMessageCallBack == null)
                {
                    throw new NoResponseException("无根据Key回复消息");
                }
                return nullMessageCallBack(message);
            };
        } 
        #endregion
    }
}
