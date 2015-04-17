using System;
using System.Collections.Generic;
using System.Web;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.OAuth;

namespace Wing.WeiXin.MP.SDK.Extension.Module.Handler
{
    /// <summary>
    /// OAuth接口处理
    /// </summary>
    public class OAuth : IHttpHandler
    {
        /// <summary>
        /// 使用的账号（如果为空则默认为第一个账号）
        /// </summary>
        public static WXAccount Account;

        /// <summary>
        /// URL列表
        /// </summary>
        public static Dictionary<string, string> UrlList = new Dictionary<string, string>();

        /// <summary>
        /// 参数不存在时跳转的URL
        /// </summary>
        public static string ErrorUrl;

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            OAuthAccessToken result = new OAuthController().GetAccessTokenByCode(
                Account ?? GlobalManager.GetFirstAccount(), context.Request.QueryString["code"]);

            string state = context.Request.QueryString["state"];

            context.Response.Redirect(UrlList.ContainsKey(state)
                ? String.Format(UrlList[state], result.openid)
                : ErrorUrl);
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
