using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Messages
{
    /// <summary>
    /// 图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageImage : AMessage
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
