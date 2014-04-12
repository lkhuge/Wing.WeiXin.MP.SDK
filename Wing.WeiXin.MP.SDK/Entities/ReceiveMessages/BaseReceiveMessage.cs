using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages
{
    /// <summary>
    /// 接收消息抽象类
    /// </summary>
    public abstract class BaseReceiveMessage : BaseEntity, IReceive
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        [XmlIgnore]
        public ReceiveEntityType entityType { get; protected set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }
}
