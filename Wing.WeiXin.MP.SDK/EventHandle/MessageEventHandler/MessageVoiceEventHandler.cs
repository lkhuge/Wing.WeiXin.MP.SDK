using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.Interface;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler
{
    /// <summary>
    /// 语音消息事件处理
    /// </summary>
    public class MessageVoiceEventHandler : EntityEventHandler<MessageVoice>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageVoice entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageVoice entity)
        {
            return new ReturnMessageVoice
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                Voice = new Voice
                {
                    MediaId = entity.MediaId
                }
            };
        } 
        #endregion
    }
}
