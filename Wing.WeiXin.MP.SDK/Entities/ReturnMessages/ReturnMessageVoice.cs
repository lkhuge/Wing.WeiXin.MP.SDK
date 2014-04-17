using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复语音消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageVoice : BaseReturnMessage
    {
        /// <summary>
        /// 语音对象
        /// </summary>
        public voice Voice { get; set; }

        #region 实例化空数据回复语音消息 public ReturnMessageVoice()
        /// <summary>
        /// 实例化空数据回复语音消息
        /// </summary>
        public ReturnMessageVoice()
        {
            MsgType = "voice";
        }
        #endregion

        #region 根据多媒体Id和接收的实体实例化 public ReturnMessageVoice(string MediaId, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据多媒体Id和接收的实体实例化
        /// </summary>
        /// <param name="MediaId">多媒体Id</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageVoice(string MediaId, BaseEntity entity)
            : base(entity)
        {
            if (String.IsNullOrEmpty(MediaId)) throw new ArgumentNullException("MediaId");
            MsgType = "voice";
            Voice = new voice
            {
                MediaId = MediaId
            };
        }
        #endregion

        #region 语音对象 public class voice
        /// <summary>
        /// 语音对象
        /// </summary>
        public class voice
        {
            /// <summary>
            /// 通过上传多媒体文件，得到的id。
            /// </summary>
            public string MediaId { get; set; }
        } 
        #endregion
    }
}
