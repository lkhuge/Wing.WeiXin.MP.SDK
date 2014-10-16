using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu
{
    /// <summary>
    /// 弹出地理位置选择器的事件请求
    /// </summary>
    public class RequestEventLocationSelect : RequestAMessage
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey
        {
            get { return GetPostData("EventKey"); }
        }

        /// <summary>
        /// X坐标信息
        /// </summary>
        public string Location_X
        {
            get { return GetPostData("SendLocationInfo", "Location_X"); }
        }

        /// <summary>
        /// Y坐标信息
        /// </summary>
        public string Location_Y
        {
            get { return GetPostData("SendLocationInfo", "Location_Y"); }
        }

        /// <summary>
        /// 精度，可理解为精度或者比例尺、越精细的话 scale越高
        /// </summary>
        public string Scale
        {
            get { return GetPostData("SendLocationInfo", "Scale"); }
        }

        /// <summary>
        /// 地理位置的字符串信息
        /// </summary>
        public string Label
        {
            get { return GetPostData("SendLocationInfo", "Label"); }
        }

        /// <summary>
        /// 朋友圈POI的名字，可能为空
        /// </summary>
        public string Poiname
        {
            get { return GetPostData("SendLocationInfo", "Poiname"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.location_select; }
        }
    }
}
