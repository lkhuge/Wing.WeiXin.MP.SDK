using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复音乐消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageMusic : BaseReturnMessage
    {
        /// <summary>
        /// 音乐对象
        /// </summary>
        public Music music { get; set; }

        #region 实例化空数据回复音乐消息 public ReturnMessageMusic()
        /// <summary>
        /// 实例化空数据回复音乐消息
        /// </summary>
        public ReturnMessageMusic()
        {
            MsgType = "music";
        }
        #endregion

        #region 根据音乐标题,音乐描述,音乐链接,高质量音乐链接,缩略图的媒体id和接收的实体实例化 public ReturnMessageMusic(string title, string description, string musicURL, string hqMusicUrl, string thumbMediaId, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据音乐标题,音乐描述,音乐链接,高质量音乐链接,缩略图的媒体id和接收的实体实例化
        /// </summary>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicURL">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接</param>
        /// <param name="thumbMediaId">缩略图的媒体id</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageMusic(string title, string description, string musicURL,
            string hqMusicUrl, string thumbMediaId, BaseEntity entity)
            : base(entity)
        {
            if (String.IsNullOrEmpty(thumbMediaId)) throw new ArgumentNullException("thumbMediaId");
            MsgType = "music";
            music = new Music
            {
                Title = title,
                Description = description,
                MusicURL = musicURL,
                HQMusicUrl = hqMusicUrl,
                ThumbMediaId = thumbMediaId
            };
        }
        #endregion

        #region 音乐对象 public class Music
        /// <summary>
        /// 音乐对象
        /// </summary>
        public class Music
        {
            /// <summary>
            /// 音乐标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 音乐描述
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 音乐链接
            /// </summary>
            public string MusicURL { get; set; }

            /// <summary>
            /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
            /// </summary>
            public string HQMusicUrl { get; set; }

            /// <summary>
            /// 缩略图的媒体id，通过上传多媒体文件，得到的id
            /// </summary>
            public string ThumbMediaId { get; set; }
        } 
        #endregion
    }
}
