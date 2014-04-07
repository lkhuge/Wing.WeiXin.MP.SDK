using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler
{
    /// <summary>
    /// 链接消息事件处理
    /// </summary>
    public class MessageLinkEventHandler : EntityEventHandler<MessageLink>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageLink entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageLink entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = String.Format("消息标题:{0}\n 消息描述:{1}\n 消息链接:{2}",
                entity.Title, entity.Description, entity.Url)
            };
        } 
        #endregion
    }
}
