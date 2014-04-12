using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 取消关注事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventUnsubscribe : BaseEvent
    {
        #region 实例化空数据取消关注事件 public EventUnsubscribe()
        /// <summary>
        /// 实例化空数据取消关注事件
        /// </summary>
        public EventUnsubscribe()
        {
            Event = "unsubscribe";
        }
        #endregion
    }
}
