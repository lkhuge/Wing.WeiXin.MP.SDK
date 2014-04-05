using System;
using System.Configuration;

namespace Wing.WeiXin.MP.SDK.ConfigSection
{
    /// <summary>
    /// 基本配置节点
    /// </summary>
    public class BaseConfigSection : ConfigurationSection
    {
        #region AppID public string AppID
        /// <summary>
        /// AppID
        /// </summary>
        [ConfigurationProperty("AppID", DefaultValue = "")]
        public string AppID
        {
            get { return Convert.ToString(this["AppID"]); }
        } 
        #endregion

        #region AppSecret public string AppSecret
        /// <summary>
        /// AppSecret
        /// </summary>
        [ConfigurationProperty("AppSecret", DefaultValue="")]
        public string AppSecret
        {
            get { return Convert.ToString(this["AppSecret"]); }
        } 
        #endregion

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
    }
}