using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服文本消息
    /// </summary>
    public class CSMessageText : CSMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public Text text { get; set; }

        #region 实例化空数据客服文本消息 public CSMessageText()
        /// <summary>
        /// 实例化空数据客服文本消息
        /// </summary>
        public CSMessageText()
        {
            msgtype = "text";
        }
        #endregion

        #region 根据文本消息内容和普通用户openid实例化 public CSMessageText(string content, string touser)
        /// <summary>
        /// 根据文本消息内容和普通用户openid实例化
        /// </summary>
        /// <param name="content">文本消息内容</param>
        /// <param name="touser">普通用户openid</param>
        public CSMessageText(string content, string touser)
            : base(touser)
        {
            msgtype = "text";
            text = new Text
            {
                content = content
            };
        }
        #endregion

        #region 文本消息 public class Text
        /// <summary>
        /// 文本消息
        /// </summary>
        public class Text
        {
            /// <summary>
            /// 文本消息内容
            /// </summary>
            public string content { get; set; }
        } 
        #endregion
    }
}
