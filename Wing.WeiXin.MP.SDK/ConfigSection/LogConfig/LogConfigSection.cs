using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.LogConfig
{
    /// <summary>
    /// 日志配置节点
    /// </summary>
    public class LogConfigSection : ConfigurationSection
    {
        #region 是否开启日志记录 public bool IsLog
        /// <summary>
        /// 是否开启日志记录
        /// </summary>
        [ConfigurationProperty("IsLog", DefaultValue = false)]
        public bool IsLog
        {
            get { return Convert.ToBoolean(this["IsLog"]); }
        }
        #endregion
    }
}
