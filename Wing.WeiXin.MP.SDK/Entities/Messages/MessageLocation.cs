using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Messages
{
    /// <summary>
    /// 地理位置消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageLocation : AMessage
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public double Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }

        #region 实例化空数据地理位置消息 public MessageLocation()
        /// <summary>
        /// 实例化空数据地理位置消息
        /// </summary>
        public MessageLocation()
        {
            MsgType = "location";
        }
        #endregion
    }
}
