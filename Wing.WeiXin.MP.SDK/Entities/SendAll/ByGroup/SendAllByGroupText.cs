using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发文本消息
    /// </summary>
    public class SendAllByGroupText : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public MPText text { get; set; }

        #region 实例化空的高级群发文本消息 public SendAllByGroupText()
        /// <summary>
        /// 实例化空的高级群发文本消息
        /// </summary>
        public SendAllByGroupText()
        {
            msgtype = "text";
        } 
        #endregion

        #region 根据用于群发的消息的media_id实例化高级群发文本消息 public SendAllByGroupText(string media_id)
        /// <summary>
        /// 根据用于群发的消息的media_id实例化高级群发文本消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByGroupText(string media_id)
        {
            msgtype = "text";
            text = new MPText
            {
                media_id = media_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public class MPText
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
