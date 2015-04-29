using System;
using System.Collections.Generic;
using System.Web;
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

        /// <summary>
        /// URL选择规则
        /// string UrlSelectHandler(string state, string openid)
        /// </summary>
        public static Func<string, string, string> UrlSelectHandler = (state, openid) => UrlList[state];

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            OAuthAccessToken result = GlobalManager.FunctionManager.OAuthController.GetAccessTokenByCode(
                Account ?? GlobalManager.GetDefaultAccount(), context.Request.QueryString["code"]);

            string state = context.Request.QueryString["state"];
            string openID = result.openid;
            string urlKey = UrlSelectHandler(state, openID);

            context.Response.Redirect(UrlList.ContainsKey(urlKey)
                ? String.Format(UrlList[urlKey], result.openid)
                : ErrorUrl);
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
