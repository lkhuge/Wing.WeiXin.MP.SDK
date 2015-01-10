using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Attributes
{
    /// <summary>
    /// 微信事件特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WXEventAttribute : Attribute
    {
        /// <summary>
        /// 事件名
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
    }
}
