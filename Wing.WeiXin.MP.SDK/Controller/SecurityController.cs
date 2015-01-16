using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 安全控制权
    /// </summary>
    public class SecurityController : WXController
    {
        /// <summary>
        /// 获取微信服务器IP列表的URL
        /// </summary>
        private const string UrlGetWXServerIPList = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";

        #region 获取微信服务器IP列表 public WXServerIPList GetWXServerIPList(WXAccount account)
        /// <summary>
        /// 获取微信服务器IP列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>微信服务器IP列表</returns>
        public WXServerIPList GetWXServerIPList(WXAccount account)
        {
            return Action<WXServerIPList>(UrlGetWXServerIPList, account);
        }
        #endregion
    }
}
