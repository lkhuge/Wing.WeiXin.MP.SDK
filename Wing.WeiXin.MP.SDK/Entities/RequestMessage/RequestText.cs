using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage
{
    /// <summary>
    /// 文本消息请求
    /// </summary>
    public class RequestText : RequestAMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return GetPostData("Content"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.text; }
        }
    }
}
