using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler
{
    /// <summary>
    /// 图片消息事件处理
    /// </summary>
    public class MessageImageEventHandler : EntityEventHandler<MessageImage>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageImage entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageImage entity)
        {
            return new ReturnMessageImage
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                Image = new Image
                {
                    MediaId = entity.MediaId
                }
            };
        } 
        #endregion
    }
}
