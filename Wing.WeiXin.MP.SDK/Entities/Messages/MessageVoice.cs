﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Messages
{
    /// <summary>
    /// 语音消息
    /// </summary>
    [XmlRoot("xml")]
    public class MessageVoice : AMessage
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }

        #region 实例化空数据语音消息 public MessageVoice()
        /// <summary>
        /// 实例化空数据语音消息
        /// </summary>
        public MessageVoice()
        {
            MsgType = "voice";
        }
        #endregion
    }
}
