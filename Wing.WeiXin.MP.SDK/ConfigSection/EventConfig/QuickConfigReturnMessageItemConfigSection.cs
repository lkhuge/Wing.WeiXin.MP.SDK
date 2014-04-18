using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 快速配置回复消息配置节点
    /// </summary>
    public class QuickConfigReturnMessageItemConfigSection : ConfigurationElement
    {
        #region 快速配置回复消息事件项目关键字 public string Key
        /// <summary>
        /// 快速配置回复消息事件项目关键字
        /// </summary>
        [ConfigurationProperty("Key", IsRequired = true)]
        public string Key
        {
            get { return Convert.ToString(this["Key"]); }
        }
        #endregion

        #region 快速配置回复消息事件项目回复消息文件路径 public string Path
        /// <summary>
        /// 快速配置回复消息事件项目回复消息文件路径
        /// </summary>
        [ConfigurationProperty("Path", IsRequired = true)]
        public string Path
        {
            get { return Convert.ToString(this["Path"]); }
        }
        #endregion
    }
}
