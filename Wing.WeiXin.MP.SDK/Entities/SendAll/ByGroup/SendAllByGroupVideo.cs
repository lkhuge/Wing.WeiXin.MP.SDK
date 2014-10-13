using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发视频消息
    /// </summary>
    public class SendAllByGroupVideo : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的视频消息
        /// </summary>
        public MPVideo mpvideo { get; set; }

        #region 实例化空的高级群发图文消息 public SendAllByGroupVideo()
        /// <summary>
        /// 实例化空的高级群发视频消息
        /// </summary>
        public SendAllByGroupVideo()
        {
            msgtype = "mpvideo";
        } 
        #endregion

        #region 根据用于群发的消息的media_id实例化高级群发视频消息 public SendAllByGroupVideo(string media_id)
        /// <summary>
        /// 根据用于群发的消息的media_id实例化高级群发视频消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByGroupVideo(string media_id)
        {
            msgtype = "mpvideo";
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
