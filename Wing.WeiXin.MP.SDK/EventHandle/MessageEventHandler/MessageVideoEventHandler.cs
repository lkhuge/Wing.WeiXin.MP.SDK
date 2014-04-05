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
    /// 视频消息事件处理
    /// </summary>
    public class MessageVideoEventHandler : EntityEventHandler<MessageVideo>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageVideo entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageVideo entity)
        {
            return new ReturnMessageVideo
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                Video = new Video
                {
                    MediaId = entity.MediaId,
                    Title = "测试视频",
                    Description = "测试视频描述"
                }
            };
        } 
        #endregion
    }
}
