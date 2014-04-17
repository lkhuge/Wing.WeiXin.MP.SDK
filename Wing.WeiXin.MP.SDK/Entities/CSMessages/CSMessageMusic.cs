using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服音乐消息
    /// </summary>
    public class CSMessageMusic : CSMessage
    {
        /// <summary>
        /// 音乐消息
        /// </summary>
        public Music music { get; set; }

        #region 实例化空数据客服音乐消息 public CSMessageMusic()
        /// <summary>
        /// 实例化空数据客服音乐消息
        /// </summary>
        public CSMessageMusic()
        {
            msgtype = "music";
        } 
        #endregion

        #region 根据普通用户openid和多媒体ID实例化 public CSMessageMusic(string title, string description, string musicurl, string hqmusicurl, string thumb_media_id, string touser)
        /// <summary>
        /// 根据音乐标题,音乐描述,音乐链接,高品质音乐链接,缩略图的媒体ID和普通用户openid实例化
        /// </summary>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高品质音乐链接</param>
        /// <param name="thumb_media_id">缩略图的媒体ID</param>
        /// <param name="touser">普通用户openidD</param>
        public CSMessageMusic(string title, string description, string musicurl, string hqmusicurl, string thumb_media_id, string touser)
            : base(touser)
        {
            msgtype = "music";
            music = new Music
            {
                title = title,
                description = description,
                musicurl = musicurl,
                hqmusicurl = hqmusicurl,
                thumb_media_id = thumb_media_id
            };
        }
        #endregion

        #region 音乐消息 public class Music
        /// <summary>
        /// 音乐消息
        /// </summary>
        public class Music
        {
            /// <summary>
            /// 音乐标题
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 音乐描述
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 音乐链接
            /// </summary>
            public string musicurl { get; set; }

            /// <summary>
            /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
            /// </summary>
            public string hqmusicurl { get; set; }

            /// <summary>
            /// 缩略图的媒体ID
            /// </summary>
            public string thumb_media_id { get; set; }
        } 
        #endregion
    }
}
