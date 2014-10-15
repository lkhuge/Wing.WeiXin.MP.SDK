using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 高级群发语音消息（根据OpenID列表）
    /// </summary>
    public class SendAllByOpenIDListVoice : SendAllByOpenIDList
    {
        /// <summary>
        /// 用于设定即将发送的语音消息
        /// </summary>
        public MPVoice voice { get; set; }

        #region 实例化空的高级群发语音消息 public SendAllByOpenIDListVoice()
        /// <summary>
        /// 实例化空的高级群发语音消息
        /// </summary>
        public SendAllByOpenIDListVoice()
        {
            msgtype = "voice";
        } 
        #endregion

        #region 根据用于群发的消息的media_id和OpenID列表实例化高级群发语音消息 public SendAllByOpenIDListVoice(string media_id, List<string> openIDList)
        /// <summary>
        /// 根据用于群发的消息的media_id和OpenID列表实例化高级群发语音消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        /// <param name="openIDList">OpenID列表</param>
        public SendAllByOpenIDListVoice(string media_id, List<string> openIDList)
        {
            msgtype = "voice";
            touser = openIDList;
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
