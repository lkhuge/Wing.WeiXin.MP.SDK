using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageImage : BaseReturnMessage
    {
        /// <summary>
        /// 图片对象
        /// </summary>
        public image Image { get; set; }

        #region 实例化空数据回复图片消息 public ReturnMessageImage()
        /// <summary>
        /// 实例化空数据回复图片消息
        /// </summary>
        public ReturnMessageImage()
        {
            MsgType = "image";
        }
        #endregion

        #region 根据多媒体Id和接收的实体实例化 public ReturnMessageImage(string MediaId, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据多媒体Id和接收的实体实例化
        /// </summary>
        /// <param name="MediaId">多媒体Id</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageImage(string MediaId, BaseEntity entity)
            : base(entity)
        {
            if (String.IsNullOrEmpty(MediaId)) throw new ArgumentNullException("MediaId");
            MsgType = "image";
            Image = new image
            {
                MediaId = MediaId
            };
        }
        #endregion

        #region 图片对象 public class image
        /// <summary>
        /// 图片对象
        /// </summary>
        public class image
        {
            /// <summary>
            /// 通过上传多媒体文件，得到的id。
            /// </summary>
            public string MediaId { get; set; }
        } 
        #endregion
    }
}
