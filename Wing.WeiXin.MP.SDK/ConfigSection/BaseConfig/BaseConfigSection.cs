using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig
{
    /// <summary>
    /// 基础配置节点
    /// </summary>
    public class BaseConfigSection : ConfigurationSection
    {
        #region Token public string Token
        /// <summary>
        /// Token
        /// </summary>
        [ConfigurationProperty("Token", IsRequired = true)]
        public string Token
        {
            get { return Convert.ToString(this["Token"]); }
        }
        #endregion

        #region 是否为Debug模式 public bool Debug
        /// <summary>
        /// 是否为Debug模式
        /// </summary>
        [ConfigurationProperty("Debug", DefaultValue = false)]
        public bool Debug
        {
            get { return Convert.ToBoolean(this["Debug"]); }
        }
        #endregion

        #region 日志路径 public string Log
        /// <summary>
        /// 日志路径
        /// </summary>
        [ConfigurationProperty("Log")]
        public string Log
        {
            get { return Convert.ToString(this["Log"]); }
        }
        #endregion

        #region 公共平台账号项目列表 public AccountItemListConfigSection AccountList
        /// <summary>
        /// 公共平台账号项目列表
        /// </summary>
        [ConfigurationProperty("AccountList", IsRequired = true)]
        public AccountItemListConfigSection AccountList
        {
            get { return this["AccountList"] as AccountItemListConfigSection; }
        }
        #endregion
    }
}
