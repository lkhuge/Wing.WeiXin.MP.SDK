using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages
{
    /// <summary>
    /// 链接消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageLink : BaseReceiveMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        #region 实例化空数据链接消息 public MessageLink()
        /// <summary>
        /// 实例化空数据链接消息
        /// </summary>
        public MessageLink()
        {
            MsgType = "link";
            entityType = ReceiveEntityType.MessageLink;
        }
        #endregion
    }
}
