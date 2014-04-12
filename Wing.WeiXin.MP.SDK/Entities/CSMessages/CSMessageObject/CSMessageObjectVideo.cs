using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages.CSMessageObject
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class CSMessageObjectVideo
    {
        /// <summary>
        /// 发送的视频的媒体ID
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string description { get; set; }
    }
}
