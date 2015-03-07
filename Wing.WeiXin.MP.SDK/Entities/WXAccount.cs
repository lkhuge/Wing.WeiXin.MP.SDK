using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Properties;

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
            AccountItemConfigSection config = GlobalManager.ConfigManager.BaseConfig
                .AccountList.GetAccountItemConfigSection(ID);
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
            AppID = config.AppID;
            AppSecret = config.AppSecret;
        }
        #endregion

        #region 获取账户信息 public override string ToString()
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns>账户信息</returns>
        public override string ToString()
        {
            return String.Format("ID:{1}{0}{0}{0}AppID:{2}{0}AppSecret:{3}",
                Environment.NewLine,
                ID,
                AppID,
                AppSecret);
        } 
        #endregion
    }
}
