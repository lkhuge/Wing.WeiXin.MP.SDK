using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.LogConfig
{
    /// <summary>
    /// 回滚文件记录附加器配置节点
    /// </summary>
    public class RollingFileAppenderConfigSection : ConfigurationSection
    {
        #region 日志格式 public string Pattern
        /// <summary>
        /// 日志格式
        /// </summary>
        [ConfigurationProperty("Pattern", DefaultValue = "记录时间：%d %n日志级别：%-5level %n类：%logger%n描述：%n%m%n%n")]
        public string Pattern
        {
            get { return Convert.ToString(this["Pattern"]); }
        }
        #endregion

        #region 日志文件路径 public string Path
        /// <summary>
        /// 日志文件路径
        /// </summary>
        [ConfigurationProperty("Path", DefaultValue = "C;\\")]
        public string Path
        {
            get { return Convert.ToString(this["Path"]); }
        }
        #endregion

        #region 日志单文件最大容量 public string MaximumFileSize
        /// <summary>
        /// 日志单文件最大容量
        /// </summary>
        [ConfigurationProperty("MaximumFileSize", DefaultValue = "10MB")]
        public string MaximumFileSize
        {
            get { return Convert.ToString(this["MaximumFileSize"]); }
        }
        #endregion
    }
}
