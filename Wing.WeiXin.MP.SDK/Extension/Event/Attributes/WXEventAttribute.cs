using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;
using Wing.WeiXin.MP.SDK.Enumeration;

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

        /// <summary>
        /// 是否只运行在调试模式
        /// </summary>
        public bool OnlyDebug { get; set; }

        /// <summary>
        /// 限定类型
        /// 为事件自动添加对于类型的限制
        /// 
        /// PS: 仅在全局类型事件（Response Event(Reuqet request)）有效
        /// </summary>
        public ReceiveEntityType? LimitType { get; set; }

        /// <summary>
        /// 限定关键字
        /// 为事件自动添加对于关键字的限制
        /// 
        /// PS:
        /// 1.仅在实际请求类型为Click和Text有效（后续可能还会添加支持类型）
        /// 2.当限定类型不为空时 优先判断类型
        /// </summary>
        public string LimitKey { get; set; }

        #region 根据事件名和开发者微信号实例化微信事件特性 public WXEventAttribute(string eventName, string toUserName)
        /// <summary>
        /// 根据事件名和开发者微信号实例化微信事件特性
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        public WXEventAttribute(string eventName, string toUserName)
        {
            EventName = eventName;
            ToUserName = toUserName;
        } 
        #endregion

        #region 根据事件名和开发者微信号实例化微信事件特性 public WXEventAttribute(string eventName, string toUserName)
        /// <summary>
        /// 根据事件名开发者微信号和限定类型实例化微信事件特性
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="type">限定类型</param>
        public WXEventAttribute(string eventName, string toUserName, ReceiveEntityType type)
            : this(eventName, toUserName)
        {
            LimitType = type;
        }
        #endregion

        #region 根据限定类型包装事件 internal Func<Request, Response> PackageEventByLimitType(Func<Request, Response> receiveEvent)
        /// <summary>
        /// 根据限定类型包装事件
        /// </summary>
        /// <param name="receiveEvent">原事件</param>
        /// <returns>包装后的事件</returns>
        internal Func<Request, Response> PackageEventByLimitType(Func<Request, Response> receiveEvent)
        {
            return LimitType != null
                ? (request => request.MsgType != LimitType ? null : receiveEvent(request))
                : receiveEvent;
        } 
        #endregion

        #region 根据限定关键字包装事件 internal Func<Request, Response> PackageEventByLimitKey(Func<Request, Response> receiveEvent)
        /// <summary>
        /// 根据限定关键字包装事件
        /// </summary>
        /// <param name="receiveEvent">原事件</param>
        /// <returns>包装后的事件</returns>
        internal Func<Request, Response> PackageEventByLimitKey(Func<Request, Response> receiveEvent)
        {
            if (String.IsNullOrEmpty(LimitKey)) return receiveEvent;
            return request =>
            {
                if (request.MsgType == ReceiveEntityType.CLICK)
                {
                    return !RequestAMessage.GetRequestAMessage<RequestEventClick>(request).EventKey.Equals(LimitKey)
                        ? null : receiveEvent(request);
                }
                if (request.MsgType == ReceiveEntityType.text)
                {
                    return !RequestAMessage.GetRequestAMessage<RequestText>(request).Content.Equals(LimitKey)
                        ? null : receiveEvent(request);
                }
                return null;
            };
        } 
        #endregion

        #region 根据限定关键字包装已知类型事件 internal Func<Request, Response> PackageEventByLimitKey(Func<Request, Response> receiveEvent, ReceiveEntityType type)
        /// <summary>
        /// 根据限定关键字包装已知类型事件
        /// </summary>
        /// <param name="receiveEvent">原事件</param>
        /// <param name="type">事件类型</param>
        /// <returns>包装后的事件</returns>
        internal Func<Request, Response> PackageEventByLimitKey(Func<Request, Response> receiveEvent, ReceiveEntityType type)
        {
            if (String.IsNullOrEmpty(LimitKey)) return receiveEvent;
            if (type == ReceiveEntityType.CLICK)
            {
                return request => !RequestAMessage.GetRequestAMessage<RequestEventClick>(request).EventKey.Equals(LimitKey)
                    ? null
                    : receiveEvent(request);
            }
            if (type == ReceiveEntityType.text)
            {
                return request => !RequestAMessage.GetRequestAMessage<RequestText>(request).Content.Equals(LimitKey)
                    ? null
                    : receiveEvent(request);
            }
            return receiveEvent;
        }
        #endregion
    }
}
