using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        public static event Action<string, string> Info;

        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        public static event Action<string> System;

        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        public static event Action<string, string> Warn;

        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        public static event Action<string, WXException, string> Error;

        #region 记录日志（信息） public static void WriteInfo(string message, string user)
        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="user">发起用户</param>
        public static void WriteInfo(string message, string user)
        {
            if (Info != null) Info(message, user);
        }
        #endregion

        #region 记录系统日志 public static void WriteSystem(string message)
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void WriteSystem(string message)
        {
            if (System != null) System(message);
        }
        #endregion

        #region 记录警告日志 public static void WriteWarn(string message, string user)
        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="user">发起用户</param>
        public static void WriteWarn(string message, string user)
        {
            if (Warn != null) Warn(message, user);
        }
        #endregion

        #region 记录错误日志 public static void WriteError(string message, WXException e, string user)
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="e">异常信息</param>
        /// <param name="user">发起用户</param>
        public static void WriteError(string message, WXException e, string user)
        {
            if (Error != null) Error(message, e, user);
        }
        #endregion

        #region 添加写入方法 public static void AddWriteCallback(Action<string> callback)
        /// <summary>
        /// 添加写入方法
        /// </summary>
        /// <param name="callback">写入方法</param>
        public static void AddWriteCallback(Action<string> callback)
        {
            Info += (m, u) => callback(String.Format("[{0}]({1}){2}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), u, m));
            System += m => callback(String.Format("[{0}]{1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), m));
            Warn += (m, u) => callback(String.Format("[{0}]({1}){2}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), u, m));
            Error += (m, e, u) => callback(String.Format("[{0}]({1}){2}{3}{4}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), u, m, Environment.NewLine, e.Message));
        }
        #endregion
    }
}
