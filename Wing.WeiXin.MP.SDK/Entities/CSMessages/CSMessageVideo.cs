using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服视频消息
    /// </summary>
    public class CSMessageVideo : CSMessage
    {
        /// <summary>
        /// 视频消息
        /// </summary>
        public Video video { get; set; }

        #region 实例化空数据客服视频消息 public CSMessageVideo()
        /// <summary>
        /// 实例化空数据客服视频消息
        /// </summary>
        public CSMessageVideo()
        {
            msgtype = "video";
        }
        #endregion

        #region 根据多媒体ID,视频消息的标题,视频消息的描述和普通用户openid实例化 public CSMessageVideo(string media_id, string title, string description, string touser)
        /// <summary>
        /// 根据多媒体ID,视频消息的标题,视频消息的描述和普通用户openid实例化
        /// </summary>
        /// <param name="media_id">多媒体ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="touser">普通用户openid</param>
        public CSMessageVideo(string media_id, string title, string description, string touser)
            : base(touser)
        {
            msgtype = "video";
            video = new Video
            {
                media_id = media_id,
                title = title,
                description = description
            };
        }
        #endregion

        #region 视频消息 public class Video
        /// <summary>
        /// 视频消息
        /// </summary>
        public class Video
        {
            /// <summary>
            /// 发送的视频的媒体ID
            /// </summary>
            public string media_id { get; set; }

            /// <summary>
            /// 视频消息的标题
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 视频消息的描述
            /// </summary>
            public string description { get; set; }
        } 
        #endregion
    }
}
