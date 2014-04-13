using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 关注事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventSubscribe : BaseEvent
    {
        #region 实例化空数据关注事件 public EventSubscribe()
        /// <summary>
        /// 实例化空数据关注事件
        /// </summary>
        public EventSubscribe()
        {
            Event = "subscribe";
            entityType = ReceiveEntityType.EventSubscribe;
        }
        #endregion
    }
}
