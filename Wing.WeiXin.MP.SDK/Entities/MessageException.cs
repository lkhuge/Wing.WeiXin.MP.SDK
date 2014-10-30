using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 消息类异常
    /// 不会引发ReceiveException事件
    /// </summary>
    public class MessageException : Exception
    {
        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 自身对象
        /// </summary>
        private static MessageException messageException;

        #region 根据消息实例化异常 public MessageException(string message)
        /// <summary>
        /// 根据消息实例化异常
        /// 不推荐直接实例化
        /// 建议使用GetInstance方法
        /// </summary>
        /// <param name="message">消息</param>
        public MessageException(string message)
        {
            ExceptionMessage = message;
        } 
        #endregion

        #region 获取引用 public static MessageException GetInstance(string message)
        /// <summary>
        /// 获取引用
        /// 推荐使用此方法获取异常
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static MessageException GetInstance(string message)
        {
            if (messageException == null)
            {
                messageException = new MessageException(message);
            }
            else
            {
                messageException.ExceptionMessage = message;
            }

            return messageException;
        } 
        #endregion
    }
}
