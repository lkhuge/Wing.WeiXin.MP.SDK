﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 微信账号
    /// </summary>
    public class WXAccount
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public WeixinMPType Type { get; private set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; private set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; private set; }

        #region 根据微信账号ID实例化 public WXAccount(string id)
        /// <summary>
        /// 根据微信账号ID实例化
        /// </summary>
        /// <param name="id">微信账号ID</param>
        public WXAccount(string id)
        {
            ID = id;
            Token = GlobalManager.ConfigManager.BaseConfig.Token;
            AccountItemConfigSection config = GlobalManager.ConfigManager.BaseConfig
                .AccountList.GetAccountItemConfigSection(ID);
            Type = config.WeixinMPType;
            AppID = config.AppID;
            AppSecret = config.AppSecret;
        } 
        #endregion

        #region 根据配置节点实例化 public WXAccount(AccountItemConfigSection config)
        /// <summary>
        /// 根据配置节点实例化
        /// </summary>
        /// <param name="config">配置节点</param>
        public WXAccount(AccountItemConfigSection config)
        {
            ID = config.WeixinMPID;
            Token = GlobalManager.ConfigManager.BaseConfig.Token;
            Type = config.WeixinMPType;
            AppID = config.AppID;
            AppSecret = config.AppSecret;
        }
        #endregion

        #region 检测是否为服务号 public void CheckIsService()
        /// <summary>
        /// 检测是否为服务号
        /// </summary>
        public void CheckIsService()
        {
            if (Type == WeixinMPType.Service) return;
            throw new Exception("只有服务号支持此操作");
        } 
        #endregion
    }
}
