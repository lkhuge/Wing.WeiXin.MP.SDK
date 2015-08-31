using System;

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
        public static event Action<string> Info;

        /// <summary>
        /// 记录日志（系统）
        /// </summary>
        public static event Action<string> System;

        /// <summary>
        /// 记录日志（错误）
        /// </summary>
        public static event Action<string, Exception> Error;

        #region 记录日志（信息） public static void WriteInfo(string message)
        /// <summary>
        /// 记录日志（信息）
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void WriteInfo(string message)
        {
            if (Info != null) Info(message);
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

        #region 记录错误日志 public static void WriteError(string message, Exception e)
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="e">异常信息</param>
        public static void WriteError(string message, Exception e)
        {
            if (Error != null) Error(message, e);
        }
        #endregion

        #region 添加写入方法 public static void AddWriteCallback(Action<string> callback)
        /// <summary>
        /// 添加写入方法
        /// </summary>
        /// <param name="callback">写入方法</param>
        public static void AddWriteCallback(Action<string> callback)
        {
            Info += m => callback(String.Format("[Info][{0}]{1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), m));
            System += m => callback(String.Format("[System][{0}]{1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), m));
            Error += (m, e) => callback(String.Format("[Error][{0}]{1}{2}{3}{2}{4}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"), m, Environment.NewLine, e.Message, e.StackTrace));
        }
        #endregion
    }
}
