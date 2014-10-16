using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message
{
    /// <summary>
    /// 地理位置请求
    /// </summary>
    public class RequestLocation : RequestAMessage
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X
        {
            get { return GetPostData("Location_X"); }
        }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            get { return GetPostData("Location_Y"); }
        }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale
        {
            get { return GetPostData("Scale"); }
        }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return GetPostData("Label"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.location; }
        }
    }
}
