using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发图文消息
    /// </summary>
    public class SendAllByGroupNews : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public MPNews mpnews { get; set; }

        #region 实例化空的高级群发图文消息 public SendAllByGroupNews()
        /// <summary>
        /// 实例化空的高级群发图文消息
        /// </summary>
        public SendAllByGroupNews()
        {
            msgtype = "mpnews";
        } 
        #endregion

        #region 根据用于群发的消息的media_id实例化高级群发图文消息 public SendAllByGroupNews(string media_id)
        /// <summary>
        /// 根据用于群发的消息的media_id实例化高级群发图文消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByGroupNews(string media_id)
        {
            msgtype = "mpnews";
            mpnews = new MPNews
            {
                media_id = media_id
            };
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
