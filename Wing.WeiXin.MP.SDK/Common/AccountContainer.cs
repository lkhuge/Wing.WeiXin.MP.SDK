using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 账号容器
    /// </summary>
    public static class AccountContainer
    {
        /// <summary>
        /// 账号列表
        /// </summary>
        private readonly static List<WXAccount> WXAccountList;

        #region 初始化账户列表 static AccountContainer()
        /// <summary>
        /// 初始化账户列表
        /// </summary>
        static AccountContainer()
        {
            WXAccountList =
                ConfigManager.BaseConfig.AccountList.OfType<AccountItemConfigSection>()
                    .Select(a => new WXAccount(a.WeixinMPID))
                    .ToList();
        } 
        #endregion

        #region 根据账号类型获取账号列表 public static List<WXAccount> GetWXAccountList(WeixinMPType type)
        /// <summary>
        /// 根据账号类型获取账号列表
        /// </summary>
        /// <param name="type">账号类型</param>
        /// <returns>账号列表</returns>
        public static List<WXAccount> GetWXAccountList(WeixinMPType type)
        {
            return WXAccountList.Where(a => a.Type == type).ToList();
        } 
        #endregion

        #region 根据账号类型获取账号列表中的第一个账号 public static WXAccount GetWXAccountFirst(WeixinMPType type)
        /// <summary>
        /// 根据账号类型获取账号列表中的第一个账号
        /// </summary>
        /// <param name="type">账号类型</param>
        /// <returns>第一个账号</returns>
        public static WXAccount GetWXAccountFirst(WeixinMPType type)
        {
            return WXAccountList.FirstOrDefault(a => a.Type == type);
        } 
        #endregion

        #region 获取账号列表中的第一个服务账号 public static WXAccount GetWXAccountFirstService()
        /// <summary>
        /// 获取账号列表中的第一个服务账号
        /// </summary>
        /// <returns>第一个服务账号</returns>
        public static WXAccount GetWXAccountFirstService()
        {
            return GetWXAccountFirst(WeixinMPType.Service);
        }
        #endregion

        #region 获取账号列表中的第一个订阅账号 public static WXAccount GetWXAccountFirst()
        /// <summary>
        /// 获取账号列表中的第一个订阅账号
        /// </summary>
        /// <returns>第一个订阅账号</returns>
        public static WXAccount GetWXAccountFirstSubscription()
        {
            return GetWXAccountFirst(WeixinMPType.Subscription);
        }
        #endregion
    }
}
