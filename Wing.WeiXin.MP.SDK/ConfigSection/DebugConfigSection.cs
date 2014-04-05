using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection
{
    /// <summary>
    /// 调试配置节点
    /// </summary>
    public class DebugConfigSection : ConfigurationSection
    {
        #region 是否开启调试 public bool IsDebug
        /// <summary>
        /// 是否开启调试
        /// </summary>
        [ConfigurationProperty("IsDebug", DefaultValue = false)]
        public bool IsDebug
        {
            get { return Convert.ToBoolean(this["IsDebug"]); }
        }
        #endregion
    }
}
