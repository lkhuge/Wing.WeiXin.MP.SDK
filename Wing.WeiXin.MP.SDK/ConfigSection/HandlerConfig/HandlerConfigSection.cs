using System;
using System.Configuration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.HandlerConfig
{
    /// <summary>
    /// Handler配置节点
    /// </summary>
    public class HandlerConfigSection : ConfigurationSection
    {
        #region Handler项目标记 public string Sign
        /// <summary>
        /// Handler项目标记
        /// </summary>
        [ConfigurationProperty("Sign")]
        public string Sign
        {
            get { return Convert.ToString(this["Sign"]); }
        }
        #endregion

        #region Handler项目默认Handler public string Default
        /// <summary>
        /// Handler项目默认Handler
        /// </summary>
        [ConfigurationProperty("Default")]
        public string Default
        {
            get { return Convert.ToString(this["Default"]); }
        }
        #endregion

        #region Handler列表 public HandlerItemListConfigSection HandlerList
        /// <summary>
        /// Handler列表
        /// </summary>
        [ConfigurationProperty("HandlerList")]
        public HandlerItemListConfigSection HandlerList
        {
            get { return this["HandlerList"] as HandlerItemListConfigSection; }
        }
        #endregion
    }
}
