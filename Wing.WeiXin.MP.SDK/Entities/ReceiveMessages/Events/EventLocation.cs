using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventLocation : BaseEvent
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }

        #region 实例化空数据上报地理位置事件 public EventLocation()
        /// <summary>
        /// 实例化空数据上报地理位置事件
        /// </summary>
        public EventLocation()
        {
            Event = "LOCATION";
            entityType = ReceiveEntityType.EventLocation;
        }
        #endregion
    }
}
