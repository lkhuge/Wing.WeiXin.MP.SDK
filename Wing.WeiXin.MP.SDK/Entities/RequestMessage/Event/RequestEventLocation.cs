using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event
{
    /// <summary>
    /// 上报地理位置事件请求
    /// </summary>
    public class RequestEventLocation : RequestAMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude
        {
            get { return Double.Parse(GetPostData("Latitude")); }
        }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude
        {
            get { return Double.Parse(GetPostData("Longitude")); }
        }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision
        {
            get { return Double.Parse(GetPostData("Precision")); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.LOCATION; }
        }
    }
}
