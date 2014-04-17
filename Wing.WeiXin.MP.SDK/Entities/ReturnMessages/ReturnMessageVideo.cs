using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复视频消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageVideo : BaseReturnMessage
    {
        /// <summary>
        /// 视频对象
        /// </summary>
        public video Video { get; set; }

        #region 实例化空数据回复视频消息 public ReturnMessageVideo()
        /// <summary>
        /// 实例化空数据回复视频消息
        /// </summary>
        public ReturnMessageVideo()
        {
            MsgType = "video";
        }
        #endregion

        #region 根据多媒体Id,视频消息的标题,视频消息的描述和接收的实体实例化 public ReturnMessageVideo(string MediaId, string title, string description, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据多媒体Id,视频消息的标题,视频消息的描述和接收的实体实例化
        /// </summary>
        /// <param name="MediaId">多媒体Id</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageVideo(string MediaId, string title, string description, BaseEntity entity)
            : base(entity)
        {
            if (String.IsNullOrEmpty(MediaId)) throw new ArgumentNullException("MediaId");
            MsgType = "video";
            Video = new video
            {
                MediaId = MediaId,
                Title = title,
                Description = description
            };
        }
        #endregion

        #region 视频对象 public class video
        /// <summary>
        /// 视频对象
        /// </summary>
        public class video
        {
            /// <summary>
            /// 通过上传多媒体文件，得到的id
            /// </summary>
            public string MediaId { get; set; }

            /// <summary>
            /// 视频消息的标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 视频消息的描述 
            /// </summary>
            public string Description { get; set; }
        } 
        #endregion
    }
}
