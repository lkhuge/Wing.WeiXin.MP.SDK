using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig
{
    /// <summary>
    /// 公共平台账号项目配置节点
    /// </summary>
    public class AccountItemConfigSection : ConfigurationElement
    {
        #region 微信公共平台ID public string WeixinMPID
        /// <summary>
        /// 微信公共平台ID
        /// </summary>
        [ConfigurationProperty("WeixinMPID")]
        public string WeixinMPID
        {
            get { return Convert.ToString(this["WeixinMPID"]); }
        }
        #endregion

        #region 微信公共平台类型 public WeixinMPType WeixinMPType
        /// <summary>
        /// 微信公共平台类型
        /// </summary>
        [ConfigurationProperty("WeixinMPType")]
        public WeixinMPType WeixinMPType
        {
            get { return (WeixinMPType)this["WeixinMPType"]; }
        }
        #endregion

        #region AppID public string AppID
        /// <summary>
        /// AppID
        /// </summary>
        [ConfigurationProperty("AppID")]
        public string AppID
        {
            get { return Convert.ToString(this["AppID"]); }
        }
        #endregion

        #region AppSecret public string AppSecret
        /// <summary>
        /// AppSecret
        /// </summary>
        [ConfigurationProperty("AppSecret")]
        public string AppSecret
        {
            get { return Convert.ToString(this["AppSecret"]); }
        }
        #endregion
    }
}
