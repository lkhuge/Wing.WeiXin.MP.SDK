using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件模板配置节点
    /// </summary>
    public class EventTemplateConfigSection : ConfigurationElement
    {
        #region 事件模板名称 public string Name
        /// <summary>
        /// 事件模板名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return Convert.ToString(this["Name"]); }
        }
        #endregion

        #region 事件模板类型 public string Type
        /// <summary>
        /// 事件模板类型
        /// </summary>
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get { return Convert.ToString(this["Type"]); }
        }
        #endregion

        #region 事件模板路径 public string Path
        /// <summary>
        /// 事件模板路径
        /// </summary>
        [ConfigurationProperty("Path", IsRequired = true)]
        public string Path
        {
            get { return Convert.ToString(this["Path"]); }
        }
        #endregion
    }
}
