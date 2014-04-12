using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 事件处理异常
    /// </summary>
    public class EventHandlerException : WXException
    {
        #region 根据事件处理异常消息实例化异常 public EventHandlerException(string message)
        /// <summary>
        /// 根据事件处理异常消息实例化异常
        /// </summary>
        /// <param name="message">事件处理异常消息</param>
        public EventHandlerException(string message)
            : base(String.Format("事件处理异常({0})", message))
        {
        } 
        #endregion
    }
}
