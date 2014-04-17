using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public Voice voice { get; set; }

        #region 实例化空数据客服语音消息 public CSMessageVoice()
        /// <summary>
        /// 实例化空数据客服语音消息
        /// </summary>
        public CSMessageVoice()
        {
            msgtype = "voice";
        }
        #endregion

        #region 根据多媒体ID和普通用户openid实例化 public CSMessageVoice(string media_id, string touser)
        /// <summary>
        /// 根据多媒体ID和普通用户openid实例化
        /// </summary>
        /// <param name="media_id">多媒体ID</param>
        /// <param name="touser">普通用户openid</param>
        public CSMessageVoice(string media_id, string touser)
            : base(touser)
        {
            msgtype = "voice";
            voice = new Voice
            {
                media_id = media_id
            };
        }
        #endregion

        #region 语音消息 public class Voice
        /// <summary>
        /// 语音消息
        /// </summary>
        public class Voice
        {
            /// <summary>
            /// 发送的语音的媒体ID
            /// </summary>
            public string media_id { get; set; }
        } 
        #endregion
    }
}
