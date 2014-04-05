using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageText : AMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        #region 实例化空数据文本消息 public MessageText()
        /// <summary>
        /// 实例化空数据文本消息
        /// </summary>
        public MessageText()
        {
            MsgType = "text";
        }
        #endregion
    }
}
