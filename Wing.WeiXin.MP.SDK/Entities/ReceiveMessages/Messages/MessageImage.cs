using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages
{
    /// <summary>
    /// 图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageImage : BaseReceiveMessage
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        #region 实例化空数据图片消息 public MessageImage()
        /// <summary>
        /// 实例化空数据图片消息
        /// </summary>
        public MessageImage()
        {
            MsgType = "image";
        }
        #endregion
    }
}
