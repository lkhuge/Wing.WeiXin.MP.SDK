using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发语音消息
    /// </summary>
    public class SendAllByGroupVoice : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的语音消息
        /// </summary>
        public MPVoice voice { get; set; }

        #region 实例化空的高级群发语音消息 public SendAllByGroupVoice()
        /// <summary>
        /// 实例化空的高级群发语音消息
        /// </summary>
        public SendAllByGroupVoice()
        {
            msgtype = "voice";
        } 
        #endregion

        #region 根据用于群发的消息的media_id实例化高级群发语音消息 public SendAllByGroupVoice(string media_id)
        /// <summary>
        /// 根据用于群发的消息的media_id实例化高级群发语音消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByGroupVoice(string media_id)
        {
            msgtype = "voice";
            voice = new MPVoice
            {
                media_id = media_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的语音消息
        /// </summary>
        public class MPVoice
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
