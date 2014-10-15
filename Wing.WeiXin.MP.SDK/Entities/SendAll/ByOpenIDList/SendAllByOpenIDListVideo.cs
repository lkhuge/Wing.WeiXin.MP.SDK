using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 高级群发视频消息（根据OpenID列表）
    /// </summary>
    public class SendAllByOpenIDListVideo : SendAllByOpenIDList
    {
        /// <summary>
        /// 用于设定即将发送的视频消息
        /// </summary>
        public MPVideo mpvideo { get; set; }

        #region 实例化空的高级群发图文消息 public SendAllByOpenIDListVideo()
        /// <summary>
        /// 实例化空的高级群发视频消息
        /// </summary>
        public SendAllByOpenIDListVideo()
        {
            msgtype = "mpvideo";
        } 
        #endregion

        #region 根据用于群发的消息的media_id和OpenID列表实例化高级群发视频消息 public SendAllByOpenIDListVideo(string media_id, List<string> openIDList)
        /// <summary>
        /// 根据用于群发的消息的media_id和OpenID列表实例化高级群发视频消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        /// <param name="openIDList">OpenID列表</param>
        public SendAllByOpenIDListVideo(string media_id, List<string> openIDList)
        {
            msgtype = "mpvideo";
            touser = openIDList;
            mpvideo = new MPVideo
            {
                media_id = media_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的视频消息
        /// </summary>
        public class MPVideo
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
