using System;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.Module.Handler
{
    /// <summary>
    /// 菜单操作
    /// </summary>
    public class MenuTool : IHttpHandler
    {
        /// <summary>
        /// 使用的微信账号
        /// </summary>
        public static WXAccount Account;

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            string operation = context.Request.QueryString["Operation"];
            context.Response.ContentType = "application/json";
            object result = new { msg = "未知操作" };
            if ("Get".Equals(operation)) result = Get();
            if ("Save".Equals(operation)) result = Save(context.Request.Form["Data"]);
            if ("Delete".Equals(operation)) result = Delete();
            if ("GetOAuthUrl".Equals(operation)) result = GetOAuthURL(
                context.Request.Form["callback"],
                context.Request.Form["type"],
                context.Request.Form["state"]);


            context.Response.Write(JSONHelper.JSONSerialize(result));
        } 
        #endregion

        #region 获取菜单信息 private object Get()
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns>菜单信息</returns>
        private object Get()
        {
            try
            {
                return GlobalManager.FunctionManager.MenuController.GetMenu(Account ?? GlobalManager.GetFirstAccount());
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        } 
        #endregion

        #region 保存菜单信息 private object Save(string menu)
        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns>结果</returns>
        private object Save(string menu)
        {
            try
            {
                MenuForGet m = JSONHelper.JSONDeserialize<MenuForGet>(HttpUtility.UrlDecode(menu));
                return new
                {
                    msg = GlobalManager.FunctionManager.MenuController.CreateMenu(Account ?? GlobalManager.GetFirstAccount(), m).GetIntroduce()
                };
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        } 
        #endregion

        #region 删除菜单 private object Delete()
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns>结果</returns>
        private object Delete()
        {
            try
            {
                return new
                {
                    msg = GlobalManager.FunctionManager.MenuController.DeleteMenu(Account ?? GlobalManager.GetFirstAccount()).GetIntroduce()
                };
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        } 
        #endregion

        #region 获取OAuthURL private object GetOAuthURL()
        /// <summary>
        /// 获取OAuthURL
        /// </summary>
        /// <param name="callback">回调地址</param>
        /// <param name="type">OAuth类型</param>
        /// <param name="state">OAuth参数</param>
        /// <returns>OAuthURL</returns>
        private object GetOAuthURL(string callback, string type, string state)
        {
            try
            {
                return new
                {
                    url = GlobalManager.FunctionManager.OAuthController.GetURLForOAuthGetCode(
                        Account ?? GlobalManager.GetFirstAccount(),
                        callback,
                        (OAuthScope)Enum.Parse(typeof(OAuthScope), type, true),
                        state)
                };
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
