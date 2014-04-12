using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessages.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服图文消息
    /// </summary>
    public class CSMessageNews : CSMessage
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        public CSMessageObjectNews news { get; set; }

        #region 实例化空数据客服图文消息 public CSMessageNews()
        /// <summary>
        /// 实例化空数据客服图文消息
        /// </summary>
        public CSMessageNews()
        {
            msgtype = "news";
        }
        #endregion
    }
}
