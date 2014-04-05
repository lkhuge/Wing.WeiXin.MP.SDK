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
    /// 带参数二维码关注事件事件处理
    /// </summary>
    public class EventSubscribeByQRSceneEventHandler : EntityEventHandler<EventSubscribeByQRScene>
    {
        #region 基础事件处理 protected override IReturn BaseEntityEvent(EventSubscribeByQRScene entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(EventSubscribeByQRScene entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = "事件:带参数二维码关注事件"
            };
        } 
        #endregion
    }
}
