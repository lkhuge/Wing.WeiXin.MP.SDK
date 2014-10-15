using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 高级群发图片消息（根据OpenID列表）
    /// </summary>
    public class SendAllByOpenIDListImage : SendAllByOpenIDList
    {
        /// <summary>
        /// 用于设定即将发送的图片消息
        /// </summary>
        public MPImage image { get; set; }

        #region 实例化空的高级群发图片消息 public SendAllByOpenIDListImage()
        /// <summary>
        /// 实例化空的高级群发图片消息
        /// </summary>
        public SendAllByOpenIDListImage()
        {
            msgtype = "image";
        } 
        #endregion

        #region 根据用于群发的消息的media_id和OpenID列表实例化高级群发图片消息 public SendAllByOpenIDListImage(string media_id, List<string> openIDList)
        /// <summary>
        /// 根据用于群发的消息的media_id和OpenID列表实例化高级群发图片消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        /// <param name="openIDList">OpenID列表</param>
        public SendAllByOpenIDListImage(string media_id, List<string> openIDList)
        {
            msgtype = "image";
            touser = openIDList;
            image = new MPImage
            {
                media_id = media_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public class MPImage
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
