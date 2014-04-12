using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageText : BaseReceiveMessage
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
            entityType = ReceiveEntityType.MessageText;
        }
        #endregion
    }
}
