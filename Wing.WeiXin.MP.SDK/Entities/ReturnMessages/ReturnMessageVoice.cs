using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复语音消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageVoice : BaseReturnMessage
    {
        /// <summary>
        /// 语音对象
        /// </summary>
        public Voice Voice { get; set; }

        #region 实例化空数据回复语音消息 public ReturnMessageVoice()
        /// <summary>
        /// 实例化空数据回复语音消息
        /// </summary>
        public ReturnMessageVoice()
        {
            MsgType = "voice";
        }
        #endregion
    }
}
