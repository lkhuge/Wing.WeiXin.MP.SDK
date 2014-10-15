using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 高级群发文本消息（根据OpenID列表）
    /// </summary>
    public class SendAllByOpenIDListText : SendAllByOpenIDList
    {
        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public MPText text { get; set; }

        #region 实例化空的高级群发文本消息 public SendAllByOpenIDListText()
        /// <summary>
        /// 实例化空的高级群发文本消息
        /// </summary>
        public SendAllByOpenIDListText()
        {
            msgtype = "text";
        } 
        #endregion

        #region 根据文本消息和OpenID列表实例化高级群发文本消息 public SendAllByOpenIDListText(string content, List<string> openIDList)
        /// <summary>
        /// 根据文本消息和OpenID列表实例化高级群发文本消息
        /// </summary>
        /// <param name="content">文本消息</param>
        /// <param name="openIDList">OpenID列表</param>
        public SendAllByOpenIDListText(string content, List<string> openIDList)
        {
            msgtype = "text";
            touser = openIDList;
            text = new MPText
            {
                content = content
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public class MPText
        {
            /// <summary>
            /// 文本消息
            /// </summary>
            public string content { get; set; }
        }
    }
}
