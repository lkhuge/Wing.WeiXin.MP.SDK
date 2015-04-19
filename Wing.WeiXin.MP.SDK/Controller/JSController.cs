using System;
using System.Collections.Generic;
using System.Linq;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.JS;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// JSSDK控制器
    /// </summary>
    public class JSController : WXController
    {
        /// <summary>
        /// 获取JS接口票据的URL
        /// </summary>
        private const string UrlGetJSAPITicket = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

        #region 根据AccessToken容器初始化 public JSController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public JSController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 获取JS接口票据 public JSAPITicket GetJSAPITicket(WXAccount account)
        /// <summary>
        /// 获取JS接口票据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>JS接口票据</returns>
        public JSAPITicket GetJSAPITicket(WXAccount account)
        {
            JSAPITicket result = GlobalManager.WXSession.Get<JSAPITicket>(
                Settings.Default.SystemUsername, 
                Settings.Default.JSAPITicketHead + account.ID);
            DateTime resultDatetime = GlobalManager.WXSession.Get<DateTime>(
                Settings.Default.SystemUsername, 
                Settings.Default.JSAPITicketTimeHead + account.ID);
            if (result == null || resultDatetime == default(DateTime) || resultDatetime.AddSeconds(result.expires_in) > DateTime.Now)
            {
                JSAPITicket jsAPITicket = Action<JSAPITicket>(UrlGetJSAPITicket, account);
                GlobalManager.WXSession.Set(
                    Settings.Default.SystemUsername, 
                    Settings.Default.JSAPITicketHead + account.ID, 
                    jsAPITicket);
                GlobalManager.WXSession.Set(
                    Settings.Default.SystemUsername, 
                    Settings.Default.JSAPITicketTimeHead + account.ID, 
                    DateTime.Now);
                return jsAPITicket;
            }

            return result;
        }
        #endregion

        #region 获取JS微信配置对象 public JSWeixinConfig GetJSWeixinConfig(WXAccount account, string url, string[] jsApiList, bool debug = false)
        /// <summary>
        /// 获取JS微信配置对象
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="url">当前网页的URL</param>
        /// <param name="jsApiList">需要使用的JS接口列表</param>
        /// <param name="debug">是否为调试模式</param>
        /// <returns>JS微信配置对象</returns>
        public JSWeixinConfig GetJSWeixinConfig(WXAccount account, string url, string[] jsApiList, bool debug = false)
        {
            string[] tmp = url.Split('#');
            url = tmp.Length > 0 ? tmp[0] : url;
            string timestamp = DateTimeHelper.GetLongTimeByDateTime(DateTime.Now).ToString();
            string nonce = StringHelper.GetRamdomString(16, true, true);

            return new JSWeixinConfig
            {
                debug = debug,
                appId = account.AppID,
                timestamp = timestamp,
                nonceStr = nonce,
                signature = SecurityHelper.SHA1_Encrypt(String.Join("&", new Dictionary<string, string>
		                    {
		                        {"noncestr", nonce},
		                        {"jsapi_ticket", GetJSAPITicket(account).ticket},
		                        {"timestamp", timestamp},
		                        {"url", url}
		                    }
                            .OrderBy(z => z.Key)
                            .Select(z => String.Format("{0}={1}", z.Key, z.Value)))),
                jsApiList = jsApiList
            };
        } 
        #endregion
    }
}
