using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 微信异常
    /// </summary>
    public class WXException : Exception
    {
        /// <summary>
        /// 是否为消息
        /// </summary>
        public bool IsMessage { get; protected set; }

        /// <summary>
        /// 发送异常的用户
        /// </summary>
        public string ExceptionUser { get; private set; }

        /// <summary>
        /// 异常信息标签
        /// </summary>
        public object ExceptionTag { get; private set; }

        #region 根据消息实例化消息类异常 protected WXException(string message)
        /// <summary>
        /// 根据消息实例化消息类异常
        /// </summary>
        /// <param name="message">消息</param>
        protected WXException(string message)
            : base(message)
        {
            IsMessage = true;
        }
        #endregion

        #region 根据消息和发送异常的用户实例化异常 protected WXException(string message, string user)
        /// <summary>
        /// 根据消息和发送异常的用户实例化异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="user">发送异常的用户</param>
        protected WXException(string message, string user)
            : base(message)
        {
            ExceptionUser = user;
        } 
        #endregion

        #region 根据消息发送异常的用户和异常信息标签实例化异常 protected WXException(string message, string user, object exceptionTag)
        /// <summary>
        /// 根据消息发送异常的用户和异常信息标签实例化异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="user">发送异常的用户</param>
        /// <param name="exceptionTag">异常信息标签</param>
        protected WXException(string message, string user, object exceptionTag)
            : base(message)
        {
            ExceptionUser = user;
            ExceptionTag = exceptionTag;
        }
        #endregion

        #region 获取异常 public static WXException GetInstance(string message, string user, object exceptionTag = null)
        /// <summary>
        /// 获取异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="user">发送异常的用户</param>
        /// <param name="exceptionTag">异常信息标签</param>
        /// <returns>异常</returns>
        public static WXException GetInstance(string message, string user, object exceptionTag = null)
        {
            WXException e = exceptionTag == null
                ? new WXException(message, user)
                : new WXException(message, user, exceptionTag);
            LogManager.WriteError(
                String.Format("微信公众平台服务器发送异常：[{0}]{1}异常信息标签：{2}{1}详细信息：{1}{3}", 
                    e.Message, 
                    Environment.NewLine, 
                    exceptionTag == null ? "" : exceptionTag.ToString(),
                    e.StackTrace),
                e,
                user);

            return e;
        }
        #endregion

        #region 获取异常 public static WXException GetInstance(ErrorMsg errorMsg, string user)
        /// <summary>
        /// 获取异常
        /// </summary>
        /// <param name="errorMsg">错误码</param>
        /// <param name="user">发送异常的用户</param>
        /// <returns>异常</returns>
        public static WXException GetInstance(ErrorMsg errorMsg, string user)
        {
            return GetInstance(errorMsg.GetIntroduce(), user, errorMsg);
        }
        #endregion

        #region 获取异常 public static WXException GetInstance(string message)
        /// <summary>
        /// 获取消息类异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>异常</returns>
        public static WXException GetInstance(string message)
        {
            return new WXException(message);
        }
        #endregion
    }
}
