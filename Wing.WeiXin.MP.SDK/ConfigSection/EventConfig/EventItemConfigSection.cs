using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件项目配置节点
    /// </summary>
    public class EventItemConfigSection : ConfigurationElement
    {
        #region 事件项目名称 public string Name
        /// <summary>
        /// 事件项目名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired=true)]
        public string Name
        {
            get { return Convert.ToString(this["Name"]); }
        }
        #endregion

        #region 微信公共平台ID public string WeixinMPID
        /// <summary>
        /// 微信公共平台ID
        /// </summary>
        [ConfigurationProperty("WeixinMPID", IsRequired = true)]
        public string WeixinMPID
        {
            get { return Convert.ToString(this["WeixinMPID"]); }
        }
        #endregion

        #region 事件项目是否生效 public bool IsAction
        /// <summary>
        /// 事件项目是否生效
        /// </summary>
        [ConfigurationProperty("IsAction", IsRequired = true)]
        public bool IsAction
        {
            get { return Convert.ToBoolean(this["IsAction"]); }
        }
        #endregion
    }
}
