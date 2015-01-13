using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Default.Mapping.TransObject
{
    /// <summary>
    /// 事件模板对象
    /// </summary>
    public class EventTemplateObject
    {
        /// <summary>
        /// 非全局事件模板对象项列表
        /// </summary>
        public List<EventTemplateObjectItem> RList { get; set; }

        /// <summary>
        /// 全局事件模板对象项列表
        /// </summary>
        public List<EventTemplateObjectItem> GList { get; set; }

        /// <summary>
        /// 事件模板对象项
        /// </summary>
        public class EventTemplateObjectItem
        {
            /// <summary>
            /// 事件名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 开发者微信号
            /// </summary>
            public string ToUserName { get; set; }

            /// <summary>
            /// 事件字符串
            /// </summary>
            public string Action { get; set; }
        }
    }
}
