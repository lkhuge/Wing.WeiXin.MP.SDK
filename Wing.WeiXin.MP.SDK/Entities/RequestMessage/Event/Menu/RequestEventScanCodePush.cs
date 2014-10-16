using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu
{
    /// <summary>
    /// 扫码推事件的事件请求
    /// </summary>
    public class RequestEventScanCodePush : RequestAMessage
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey
        {
            get { return GetPostData("EventKey"); }
        }

        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult
        {
            get { return GetPostData("ScanCodeInfo", "ScanResult"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.scancode_push; }
        }
    }
}
