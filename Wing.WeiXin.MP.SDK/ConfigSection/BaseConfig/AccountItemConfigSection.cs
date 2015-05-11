using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

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
        [ConfigurationProperty("WeixinMPID", IsRequired = true)]
        public string WeixinMPID
        {
            get { return Convert.ToString(this["WeixinMPID"]); }
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

        #region 加密密钥 public string EncodingAESKey
        /// <summary>
        /// 加密密钥
        /// </summary>
        [ConfigurationProperty("EncodingAESKey")]
        public string EncodingAESKey
        {
            get { return Convert.ToString(this["EncodingAESKey"]); }
        }
        #endregion

        #region 是否启动重复消息过滤 public bool RMFilter
        /// <summary>
        /// 是否启动重复消息过滤
        /// </summary>
        [ConfigurationProperty("RMFilter", DefaultValue = false)]
        public bool RMFilter
        {
            get { return Convert.ToBoolean(this["RMFilter"]); }
        }
        #endregion
    }
}
