using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Enumeration;
using BaseException = System.Exception;
namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public static class LogHelper
    {
        #region 是否需要记录日志 public static bool IsLog { get; set; } 
        /// <summary>
        /// 是否需要记录日志
        /// </summary>
        public static bool IsLog { get; set; } 
        #endregion

        #region 初始化 public static void Init(params IAppender[] appenders)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appenders">多个附加器</param>
        public static void Init(params IAppender[] appenders)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            foreach (IAppender app in appenders)
            {
                hierarchy.Root.AddAppender(app);
            }
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            IsLog = true;
        } 
        #endregion

        #region 记录信息日志 public static void Info(string info)
        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="type"></param>
        public static void Info(string info, Type type)
        {
            if (!IsLog) return;
            ILog log = LogManager.GetLogger(type);
            if (log.IsInfoEnabled)
            {
                log.Info(info);
            }
        }
        #endregion

        #region 记录调试信息日志 public static void Debug(string info)
        /// <summary>
        /// 记录调试信息日志
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="type"></param>
        public static void Debug(string info, Type type)
        {
            if (!IsLog) return;
            ILog log = LogManager.GetLogger(type);
            if (log.IsDebugEnabled)
            {
                log.Debug(info);
            }
        }
        #endregion

        #region 记录警告信息日志 public static void Warn(string info)
        /// <summary>
        /// 记录警告信息日志
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="type"></param>
        public static void Warn(string info, Type type)
        {
            if (!IsLog) return;
            ILog log = LogManager.GetLogger(type);
            if (log.IsWarnEnabled)
            {
                log.Warn(info);
            }
        }
        #endregion

        #region 记录严重错误信息 public static void Fatal(string info, BaseException e, Type type)
        /// <summary>
        /// 记录严重错误信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="e">异常</param>
        /// <param name="type"></param>
        public static void Fatal(string info, BaseException e, Type type)
        {
            if (!IsLog) return;
            ILog log = LogManager.GetLogger(type);
            if (log.IsFatalEnabled)
            {
                log.Fatal(info, e);
            }
        }
        #endregion

        #region 记录错误信息 public static void Error(string info, BaseException e, Type type)
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="e">异常</param>
        /// <param name="type"></param>
        public static void Error(string info, BaseException e, Type type)
        {
            if (!IsLog) return;
            ILog log = LogManager.GetLogger(type);
            if (log.IsErrorEnabled)
            {
                log.Error(info, e);
            }
        }  
        #endregion

        #region 获取回滚文件记录附加器 public static RollingFileAppender GetRollingFileAppender(string path, string pattern, string maximumFileSize = "10MB")
        /// <summary>
        /// 获取回滚文件记录附加器
        /// </summary>
        /// <param name="path">日志路径</param>
        /// <param name="pattern">输出格式</param>
        /// <param name="maximumFileSize">日志单文件最大容量</param>
        /// <returns>回滚文件记录附加器</returns>
        public static RollingFileAppender GetRollingFileAppender(string path, string pattern, string maximumFileSize = "10MB")
        {
            RollingFileAppender appender = new RollingFileAppender
            {
                AppendToFile = true,
                File = path + "WeiXin",
                PreserveLogFileNameExtension = false,
                DatePattern = "yyyyMMdd.lo\\g",
                MaxSizeRollBackups = -1,
                MaximumFileSize = maximumFileSize,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                StaticLogFileName = false
            };
            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = pattern
            };
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;
            appender.ActivateOptions();

            return appender;
        } 
        #endregion

        #region 获取ADO.NET记录附加器 public static AdoNetAppender GetAdoNetAppender(SQLType sqlType, string connectionString, string commandText)
        /// <summary>
        /// 获取ADO.NET记录附加器
        /// </summary>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">插入语句</param>
        /// <returns>回滚文件记录附加器</returns>
        public static AdoNetAppender GetAdoNetAppender(SQLType sqlType, string connectionString, string commandText)
        {
            AdoNetAppender appender = GetAdoNetAppenderBySQLType(sqlType);
            appender.ConnectionString = connectionString;
            appender.CommandText = commandText;
            List<AdoNetAppenderParameter> paramList = GetAdoNetAppenderParameterList();
            foreach (AdoNetAppenderParameter param in paramList)
            {
                appender.AddParameter(param);
            }
            appender.ActivateOptions();
            return appender;
        }
        #endregion

        #region 获取ADO.NET记录配置列表 public static List<AdoNetAppenderParameter> GetAdoNetAppenderParameterList()
        /// <summary>
        /// 获取ADO.NET记录配置列表
        /// </summary>
        /// <returns>ADO.NET记录配置列表</returns>
        public static List<AdoNetAppenderParameter> GetAdoNetAppenderParameterList()
        {
            List<AdoNetAppenderParameter> adoNetAppenderParameterList = new List<AdoNetAppenderParameter>
            {
                new AdoNetAppenderParameter
                {
                    ParameterName = "@log_date",
                    DbType = DbType.DateTime,
                    Layout = new RawTimeStampLayout()
                },
                new AdoNetAppenderParameter
                {
                    ParameterName = "@thread",
                    DbType = DbType.String,
                    Size = 255,
                    Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread"))
                },
                new AdoNetAppenderParameter
                {
                    ParameterName = "@log_level",
                    DbType = DbType.String,
                    Size = 50,
                    Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level"))
                },
                new AdoNetAppenderParameter
                {
                    ParameterName = "@logger",
                    DbType = DbType.String,
                    Size = 255,
                    Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger"))
                },
                new AdoNetAppenderParameter
                {
                    ParameterName = "@message",
                    DbType = DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new PatternLayout("%message"))
                },
                new AdoNetAppenderParameter
                {
                    ParameterName = "@exception",
                    DbType = DbType.String,
                    Size = 2000,
                    Layout = new Layout2RawLayoutAdapter(new ExceptionLayout())
                },
            };
            return adoNetAppenderParameterList;
        } 
        #endregion

        #region 根据数据库类型初步获取ADO.NET记录附加器 private static AdoNetAppender GetAdoNetAppenderBySQLType(SQLType sqlType)
        /// <summary>
        /// 根据数据库类型初步获取ADO.NET记录附加器
        /// </summary>
        /// <param name="sqlType">数据库类型</param>
        /// <returns>初步的ADO.NET记录附加器</returns>
        private static AdoNetAppender GetAdoNetAppenderBySQLType(SQLType sqlType)
        {
            if (SQLType.SQLServer.Equals(sqlType))
            {
                return new AdoNetAppender
                {
                    BufferSize = 1,
                    ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                };
            }

            return null;
        } 
        #endregion
    }
}
