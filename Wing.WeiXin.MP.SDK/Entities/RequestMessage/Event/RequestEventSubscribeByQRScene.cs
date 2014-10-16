using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event
{
    /// <summary>
    /// 带参数二维码关注事件请求
    /// </summary>
    public class RequestEventSubscribeByQRScene : RequestAMessage
    {
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey
        {
            get { return GetPostData("EventKey").Substring(8); }
        }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket
        {
            get { return GetPostData("Ticket"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.subscribeByQRScene; }
        }
    }
}
