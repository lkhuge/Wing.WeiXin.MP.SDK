using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 群发OpenID列表
    /// </summary>
    public class SendAllByOpenIDList
    {
        /// <summary>
        /// 填写图文消息的接收者，一串OpenID列表，OpenID最少1个，最多10000个
        /// </summary>
        public List<string> touser { get; set; }

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public MPNews mpnews { get; set; }

        /// <summary>
        /// 群发的消息类型，图文消息为mpnews
        /// </summary>
        public string msgtype { get; set; }

        #region 根据图文消息的接收者和media_id实例化 public SendAllByOpenIDList(List<string> touser, string media_id)
        /// <summary>
        /// 根据图文消息的接收者和media_id实例化
        /// </summary>
        /// <param name="touser">图文消息的接收者</param>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByOpenIDList(List<string> touser, string media_id)
        {
            this.touser = touser;
            mpnews = new MPNews { media_id = media_id };
            msgtype = "mpnews";
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public class MPNews
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
