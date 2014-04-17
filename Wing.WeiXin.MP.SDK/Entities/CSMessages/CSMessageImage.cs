using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服图片消息
    /// </summary>
    public class CSMessageImage : CSMessage
    {
        /// <summary>
        /// 图片消息
        /// </summary>
        public Image image { get; set; }

        #region 实例化空数据客服图片消息 public CSMessageImage()
        /// <summary>
        /// 实例化空数据客服图片消息
        /// </summary>
        public CSMessageImage()
        {
            msgtype = "image";
        } 
        #endregion

        #region 根据多媒体ID和普通用户openid实例化 public CSMessageImage(string media_id, string touser)
        /// <summary>
        /// 根据多媒体ID和普通用户openid实例化
        /// </summary>
        /// <param name="media_id">多媒体ID</param>
        /// <param name="touser">普通用户openid</param>
        public CSMessageImage(string media_id, string touser)
            : base(touser)
        {
            msgtype = "image";
            image = new Image
            {
                media_id = media_id
            };
        }
        #endregion

        #region 图片消息 public class Image
        /// <summary>
        /// 图片消息
        /// </summary>
        public class Image
        {
            /// <summary>
            /// 发送的图片的媒体ID
            /// </summary>
            public string media_id { get; set; }
        } 
        #endregion
    }
}
