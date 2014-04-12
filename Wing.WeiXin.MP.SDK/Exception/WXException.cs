using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib.StringManager;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 内置异常
    /// </summary>
    public class WXException : BaseException
    {
        #region 成员变量
        /// <summary>
        /// 是否为错误
        /// </summary>
        private readonly bool IsError;

        /// <summary>
        /// 异常介绍
        /// </summary>
        private readonly string IntroduceMessage;

        /// <summary>
        /// 异常信息
        /// </summary>
        private readonly string ExceptionMessage;

        /// <summary>
        /// 异常调试信息
        /// </summary>
        private readonly string ExceptionStackTrace; 
        #endregion

        #region 实例化系统异常 public WXException(string message, BaseException e)
        /// <summary>
        /// 实例化系统异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">系统异常</param>
        public WXException(string message, BaseException e)
        {
            IsError = true;
            IntroduceMessage = message;
            ExceptionMessage = e.Message;
            ExceptionStackTrace = e.StackTrace;
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Error(message, e, GetType());
        } 
        #endregion

        #region 实例化非系统异常 public WXException(string message)
        /// <summary>
        /// 实例化非系统异常
        /// </summary>
        /// <param name="message">逻辑异常</param>
        public WXException(string message)
        {
            IsError = false;
            IntroduceMessage = message;
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Warn(message, GetType());
        } 
        #endregion

        #region 获取消息 public Note GetNote()
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        public Note GetNote()
        {
            string message = IsError
                ? String.Format("异常介绍:{0} \n 异常信息:{1} \n 异常调试信息:{2}", IntroduceMessage, ExceptionMessage, ExceptionStackTrace)
                : IntroduceMessage;
            return new Note { Message = message };
        } 
        #endregion
    }
}
