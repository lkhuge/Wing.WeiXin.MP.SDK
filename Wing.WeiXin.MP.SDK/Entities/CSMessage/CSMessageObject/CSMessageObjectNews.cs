using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessage.CSMessageObject
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class CSMessageObjectNews
    {
        /// <summary>
        /// 图文列表
        /// </summary>
        public List<CSMessageObjectArticles> articles { get; set; }
    }
}
