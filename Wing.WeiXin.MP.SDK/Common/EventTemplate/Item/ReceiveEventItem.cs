using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Item
{
    /// <summary>
    /// 非全局事件项
    /// </summary>
    public class ReceiveEventItem : EventItem
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        public ReceiveEntityType Type { get; set; }
    }
}
