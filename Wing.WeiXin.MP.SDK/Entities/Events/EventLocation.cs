using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Events
{
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventLocation : AEvent
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
        }
        #endregion
    }
}
