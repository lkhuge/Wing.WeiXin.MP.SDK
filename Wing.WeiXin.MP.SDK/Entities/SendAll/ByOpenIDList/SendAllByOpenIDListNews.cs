using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 高级群发图文消息（根据OpenID列表）
    /// </summary>
    public class SendAllByOpenIDListNews : SendAllByOpenIDList
    {
        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public MPNews mpnews { get; set; }

        #region 实例化空的高级群发图文消息 public SendAllByOpenIDListNews()
        /// <summary>
        /// 实例化空的高级群发图文消息
        /// </summary>
        public SendAllByOpenIDListNews()
        {
            msgtype = "mpnews";
        } 
        #endregion

        #region 根据用于群发的消息的media_id和OpenID列表实例化高级群发图文消息 public SendAllByOpenIDListNews(string media_id, List<string> openIDList)
        /// <summary>
        /// 根据用于群发的消息的media_id和OpenID列表实例化高级群发图文消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        /// <param name="openIDList">OpenID列表</param>
        public SendAllByOpenIDListNews(string media_id, List<string> openIDList)
        {
            msgtype = "mpnews";
            touser = openIDList;
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
