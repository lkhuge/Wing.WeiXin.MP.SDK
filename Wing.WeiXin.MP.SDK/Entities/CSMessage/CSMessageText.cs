using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessage.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessage
{
    /// <summary>
    /// 客服文本消息
    /// </summary>
    public class CSMessageText : CSMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public CSMessageObjectText text { get; set; }

        #region 实例化空数据客服文本消息 public CSMessageText()
        /// <summary>
        /// 实例化空数据客服文本消息
        /// </summary>
        public CSMessageText()
        {
            msgtype = "text";
        }
        #endregion
    }
}
