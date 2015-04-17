using System.Web;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.Module.Handler
{
    /// <summary>
    /// 微信配置接口
    /// </summary>
    public class WeixinConfig : IHttpHandler
    {
        /// <summary>
        /// 微信账号
        /// 如果为空则默认为第一个账号
        /// </summary>
        public static WXAccount Account;

        /// <summary>
        /// 是否使用调试模式
        /// </summary>
        public static bool IsDebug;
        
        /// <summary>
        /// URL参数名称
        /// </summary>
        public static string URL = "url";

        /// <summary>
        /// 接口列表参数名称
        /// </summary>
        public static string APIList = "apiList";

        /// <summary>
        /// JSSDK控制器
        /// </summary>
        private readonly JSController jsController = new JSController();

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(JSONHelper.JSONSerialize(jsController.GetJSWeixinConfig(
                Account ?? GlobalManager.GetFirstAccount(),
                HttpUtility.UrlDecode(context.Request.QueryString[URL]),
                context.Request.QueryString[APIList].Split(','),
                IsDebug)));
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
