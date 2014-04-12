using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessages.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服语音消息
    /// </summary>
    public class CSMessageVoice : CSMessage
    {
        /// <summary>
        /// 语音消息
        /// </summary>
        public CSMessageObjectVoice voice { get; set; }

        #region 实例化空数据客服语音消息 public CSMessageVoice()
        /// <summary>
        /// 实例化空数据客服语音消息
        /// </summary>
        public CSMessageVoice()
        {
            msgtype = "voice";
        }
        #endregion
    }
}
