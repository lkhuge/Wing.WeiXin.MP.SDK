using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 带参数二维码事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventWithQRScene : BaseEvent
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        #region 实例化空数据带参数二维码事件 public EventWithQRScene()
        /// <summary>
        /// 实例化空数据带参数二维码事件
        /// </summary>
        public EventWithQRScene()
        {
            Event = "SCAN";
            entityType = ReceiveEntityType.EventWithQRScene;
        }
        #endregion
    }
}
