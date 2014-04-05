using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.Interface;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler
{
    /// <summary>
    /// 地理位置消息事件处理
    /// </summary>
    public class MessageLocationEventHandler : EntityEventHandler<MessageLocation>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageLocation entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageLocation entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = String.Format("地理位置维度:{0}\n 地理位置经度:{1}\n 地图缩放大小:{2}\n 地理位置信息:{3}\n",
                entity.Location_X, entity.Location_Y, entity.Scale, entity.Label)
            };
        } 
        #endregion
    }
}
