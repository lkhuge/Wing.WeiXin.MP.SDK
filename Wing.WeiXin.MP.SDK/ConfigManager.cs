using System;
using System.Collections.Generic;
using System.Configuration;
using log4net.Appender;
using log4net.Layout;
using Wing.WeiXin.MP.SDK.ConfigSection;
using Wing.WeiXin.MP.SDK.ConfigSection.EventConfig;
using Wing.WeiXin.MP.SDK.ConfigSection.LogConfig;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.StringManager;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public static class ConfigManager
    {
        #region 配置节点
        #region 基础配置节点 public static BaseConfigSection BaseConfig { get; set; }
        /// <summary>
        /// 基础配置节点
        /// </summary>
        public static BaseConfigSection BaseConfig { get; set; }
        #endregion

        #region 事件配置节点 public static EventConfigSection EventConfig { get; set; }
        /// <summary>
        /// 事件配置节点
        /// </summary>
        public static EventConfigSection EventConfig { get; set; }
        #endregion

        #region 调试配置节点 public static DebugConfigSection DebugConfig { get; set; }
        /// <summary>
        /// 调试配置节点
        /// </summary>
        public static DebugConfigSection DebugConfig { get; set; }
        #endregion

        #region 日志配置节点 public static LogConfigSection LogConfig { get; set; }
        /// <summary>
        /// 日志配置节点
        /// </summary>
        public static LogConfigSection LogConfig { get; set; }
        #endregion 
        #endregion

        #region 加载配置节点 static ConfigManager()
        /// <summary>
        /// 加载配置节点
        /// </summary>
        static ConfigManager()
        {
            LoadBaseConfigSection();
            LoadEventConfigSection();
            LoadDebugConfigSection();
            LoadLogConfigSection();
        } 
        #endregion

        #region 加载基础配置 private static void LoadBaseConfigSection()
        /// <summary>
        /// 加载基础配置
        /// </summary>
        private static void LoadBaseConfigSection()
        {
            BaseConfig = GetConfigSection<BaseConfigSection>("Base");
            if (BaseConfig == null) throw new ConfigNotFoundException("Base");
        } 
        #endregion

        #region 加载事件配置 private static void LoadEventConfigSection()
        /// <summary>
        /// 加载事件配置
        /// </summary>
        private static void LoadEventConfigSection()
        {
            EventConfig = GetConfigSection<EventConfigSection>("Event");
            if (EventConfig == null) throw new ConfigNotFoundException("Event");
        }
        #endregion

        #region 加载调试配置 private static void LoadDebugConfigSection()
        /// <summary>
        /// 加载调试配置
        /// </summary>
        private static void LoadDebugConfigSection()
        {
            DebugConfig = GetConfigSection<DebugConfigSection>("Debug");
            if (DebugConfig == null) throw new ConfigNotFoundException("Debug");
        }
        #endregion

        #region 加载日志配置 private static void LoadLogConfigSection()
        /// <summary>
        /// 加载日志配置
        /// </summary>
        private static void LoadLogConfigSection()
        {
            LogConfig = GetConfigSection<LogConfigSection>("Log/Base");
            if (LogConfig == null) throw new ConfigNotFoundException("Log/Base");
            LogHelper.IsLog = LogConfig.IsLog;
            if (!LogHelper.IsLog) return;
            List<IAppender> appenderList = new List<IAppender>();
            RollingFileAppender rollingFileAppender = LoadRollingFileLogConfigSection();
            if (rollingFileAppender != null) appenderList.Add(rollingFileAppender);
            AdoNetAppender adoNetAppender = LoadAdoNetLogConfigSection();
            if (adoNetAppender != null) appenderList.Add(adoNetAppender);

            LogHelper.Init(appenderList.ToArray());
        }
        #endregion

        #region 获取基础配置
        #region 获取AppID public static string GetAppID()
        /// <summary>
        /// 获取AppID
        /// </summary>
        /// <returns>AppID</returns>
        public static string GetAppID()
        {
            string appID;
            try
            {
                appID = BaseConfig.AppID;
            }
            catch (BaseException)
            {
                throw new ConfigNotFoundException("AppID");
            }
            if (String.IsNullOrEmpty(appID)) throw new ConfigNotFoundException("AppID");

            return appID;
        }
        #endregion

        #region 获取AppSecret public static string GetAppSecret()
        /// <summary>
        /// 获取AppSecret
        /// </summary>
        /// <returns>AppSecret</returns>
        public static string GetAppSecret()
        {
            string appSecret;
            try
            {
                appSecret = BaseConfig.AppSecret;
            }
            catch (BaseException)
            {
                throw new ConfigNotFoundException("AppSecret");
            }
            if (String.IsNullOrEmpty(appSecret)) throw new ConfigNotFoundException("AppSecret");

            return appSecret;
        }
        #endregion

        #region 获取Token public static string GetToken()
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns>Token</returns>
        public static string GetToken()
        {
            string token;
            try
            {
                token = BaseConfig.Token;
            }
            catch (BaseException)
            {
                throw new ConfigNotFoundException("Token");
            }
            if (String.IsNullOrEmpty(token)) throw new ConfigNotFoundException("Token");

            return token;
        }
        #endregion 
        #endregion

        #region 获取配置节点对象 private static T GetConfigSection<T>(string configName) where T : ConfigurationSection
        /// <summary>
        /// 获取配置节点对象
        /// </summary>
        /// <typeparam name="T">配置节点</typeparam>
        /// <param name="configName">配置节点名称</param>
        /// <returns>配置节点对象</returns>
        private static T GetConfigSection<T>(string configName) where T : ConfigurationSection
        {
            try
            {
                return ConfigurationManager.GetSection("WeiXinMPSDKConfigGroup/" + configName) as T;
            }
            catch (BaseException e)
            {
                throw new LoadConfigException(e);
            }
        } 
        #endregion

        #region 加载日志回滚文件配置 private static RollingFileAppender LoadRollingFileLogConfigSection()
        /// <summary>
        /// 加载日志回滚文件配置
        /// </summary>
        /// <returns>日志回滚文件记录附加器</returns>
        private static RollingFileAppender LoadRollingFileLogConfigSection()
        {
            RollingFileAppenderConfigSection rollingFileAppenderConfigSection =
                GetConfigSection<RollingFileAppenderConfigSection>("Log/RollingFileAppender");
            if (rollingFileAppenderConfigSection == null) return null;
            RollingFileAppender rollingFileAppender = LogHelper.GetRollingFileAppender(
                rollingFileAppenderConfigSection.Path,
                rollingFileAppenderConfigSection.Pattern,
                rollingFileAppenderConfigSection.MaximumFileSize);

            return rollingFileAppender;
        }
        #endregion

        #region 加载日志ADO.NET配置 private static AdoNetAppender LoadAdoNetLogConfigSection()
        /// <summary>
        /// 加载日志ADO.NET配置
        /// </summary>
        /// <returns>日志回滚文件记录附加器</returns>
        private static AdoNetAppender LoadAdoNetLogConfigSection()
        {
            AdoNetAppenderConfigSection adoNetAppenderConfigSection =
                GetConfigSection<AdoNetAppenderConfigSection>("Log/AdoNetAppender");
            if (adoNetAppenderConfigSection == null) return null;
            AdoNetAppender adoNetAppender = LogHelper.GetAdoNetAppender(
                adoNetAppenderConfigSection.SQLType,
                adoNetAppenderConfigSection.ConnectionString,
                adoNetAppenderConfigSection.CommandText);

            return adoNetAppender;
        }
        #endregion
    }
}
