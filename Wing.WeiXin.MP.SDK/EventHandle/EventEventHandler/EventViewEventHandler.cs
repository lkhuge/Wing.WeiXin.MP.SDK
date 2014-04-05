using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Events;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.Interface;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.EventEventHandler
{
    /// <summary>
    /// 自定义菜单事件（点击菜单跳转链接时的事件推送）事件处理
    /// </summary>
    public class EventViewEventHandler : EntityEventHandler<EventView>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(EventView entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(EventView entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = String.Format("事件:自定义菜单事件（点击菜单跳转链接时的事件推送）/n事件KEY值：{0}", entity.EventKey)
            };
        }
        #endregion
    }
}
