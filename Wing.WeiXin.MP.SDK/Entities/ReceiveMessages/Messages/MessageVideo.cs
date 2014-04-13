using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages
{
    /// <summary>
    /// 视频消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageVideo : BaseReceiveMessage
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }

        #region 实例化空数据视频消息 public MessageVideo()
        /// <summary>
        /// 实例化空数据视频消息
        /// </summary>
        public MessageVideo()
        {
            MsgType = "video";
            entityType = ReceiveEntityType.MessageVideo;
        }
        #endregion
    }
}
