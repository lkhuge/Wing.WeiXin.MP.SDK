using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Events;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.EventEventHandler
{
    /// <summary>
    /// 上报地理位置事件事件处理
    /// </summary>
    public class EventLocationEventHandler : EntityEventHandler<EventLocation>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(EventLocation entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(EventLocation entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = String.Format("事件:上报地理位置事件/n地理位置纬度：{0}/n地理位置经度：{1}/n地理位置精度：{2}",
                    entity.Latitude, entity.Longitude, entity.Precision)
            };
        } 
        #endregion
    }
}
