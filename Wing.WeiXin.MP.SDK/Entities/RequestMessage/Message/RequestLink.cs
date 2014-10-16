using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message
{
    /// <summary>
    /// 链接消息请求
    /// </summary>
    public class RequestLink : RequestAMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return GetPostData("Title"); }
        }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get { return GetPostData("Description"); }
        }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url
        {
            get { return GetPostData("Url"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.link; }
        }
    }
}
