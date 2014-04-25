using System;
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
        #region 获取自动回复委托 public static EntityHandler.CustomEntityHandler<MessageText> GetEventHandlerAutoReturnMessageText(Func<string, string> messageHandler)
        /// <summary>
        /// 获取自动回复委托
        /// </summary>
        /// <param name="messageHandler">消息处理</param>
        /// <returns>自动回复委托</returns>
        public static EntityHandler.CustomEntityHandler<MessageText> GetEventHandlerAutoReturnMessageText(Func<string, string> messageHandler)
        {
            return message =>
            {
                string returnMessage = messageHandler(message.Content);

                return String.IsNullOrEmpty(returnMessage) ? null : new ReturnMessageText(returnMessage, message);
            };
        } 
        #endregion

        #region 获取根据Key回复委托 public static EntityHandler.CustomEntityHandler<EventClick> GetEventHandlerReturnByKey(string key, EntityHandler.CustomEntityHandler<EventClick> Handler)
        /// <summary>
        /// 获取根据Key回复委托
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="Handler">根据Key回复委托</param>
        /// <returns>根据Key回复委托</returns>
        public static EntityHandler.CustomEntityHandler<EventClick> GetEventHandlerReturnByKey(
            string key, EntityHandler.CustomEntityHandler<EventClick> Handler)
        {
            return message =>
            {
                if (message.EventKey.Equals(key))
                {
                    IReturn returnMessage = Handler(message);
                    if (returnMessage != null) return returnMessage;
                }

                return null;
            };
        } 
        #endregion
    }
}
