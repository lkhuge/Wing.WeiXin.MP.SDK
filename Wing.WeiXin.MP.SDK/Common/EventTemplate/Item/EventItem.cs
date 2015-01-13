using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Item
{
    /// <summary>
    /// 事件项
    /// </summary>
    public abstract class EventItem
    {
        /// <summary>
        /// 事件名
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 执行事件
        /// </summary>
        public Func<Request, Response> Action { get; set; }
    }
}
