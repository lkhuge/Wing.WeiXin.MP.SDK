﻿using System;
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
