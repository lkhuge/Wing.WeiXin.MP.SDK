using System;
using System.Configuration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.HandlerConfig
{
    /// <summary>
    /// Handler项目配置节点
    /// </summary>
    public class HandlerItemConfigSection : ConfigurationElement
    {
        #region Handler项目名称 public string Name
        /// <summary>
        /// Handler项目名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired=true)]
        public string Name
        {
            get { return Convert.ToString(this["Name"]); }
        }
        #endregion

        #region Handler项目别名 public string Alias
        /// <summary>
        /// Handler项目别名
        /// </summary>
        [ConfigurationProperty("Alias")]
        public string Alias
        {
            get { return Convert.ToString(this["Alias"]); }
        }
        #endregion

        #region Handler项目是否生效 public bool IsAction
        /// <summary>
        /// Handler项目是否生效
        /// </summary>
        [ConfigurationProperty("IsAction", IsRequired = true)]
        public bool IsAction
        {
            get { return Convert.ToBoolean(this["IsAction"]); }
        }
        #endregion
    }
}
