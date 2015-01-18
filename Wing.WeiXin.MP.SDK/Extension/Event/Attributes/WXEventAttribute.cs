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

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        #region 根据事件名和开发者微信号实例化微信事件特性 public WXEventAttribute(string EventName, string ToUserName)
        /// <summary>
        /// 根据事件名和开发者微信号实例化微信事件特性
        /// </summary>
        /// <param name="EventName">事件名</param>
        /// <param name="ToUserName">开发者微信号</param>
        public WXEventAttribute(string EventName, string ToUserName)
        {
            this.EventName = EventName;
            this.ToUserName = ToUserName;
        } 
        #endregion
    }
}
