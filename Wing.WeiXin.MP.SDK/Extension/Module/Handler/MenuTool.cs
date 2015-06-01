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

        /// <summary>
        /// 操作参数名称
        /// </summary>
        public static string OperationName = "Operation";

        /// <summary>
        /// 获取菜单参数名称
        /// </summary>
        public static string GetName = "Get";

        /// <summary>
        /// 保存菜单参数名称
        /// </summary>
        public static string SaveName = "Save";

        /// <summary>
        /// 保存菜单数据参数名称
        /// </summary>
        public static string SaveDataName = "Data";

        /// <summary>
        /// 删除菜单参数名称
        /// </summary>
        public static string DeleteName = "Delete";

        /// <summary>
        /// 获取OAuthUrl参数名称
        /// </summary>
        public static string GetOAuthUrlName = "GetOAuthUrl";

        /// <summary>
        /// OAuth回调地址参数名称
        /// </summary>
        public static string OAuthCallbackName = "callback";

        /// <summary>
        /// OAuth类型参数名称
        /// </summary>
        public static string OAuthTypeName = "type";

        /// <summary>
        /// OAuth参数参数名称
        /// </summary>
        public static string OAuthStateName = "state";

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            string operation = context.Request.QueryString[OperationName];
            object result = new { msg = "未知操作" };
            if (GetName.Equals(operation)) result = Get();
            if (SaveName.Equals(operation)) result = Save(context.Request.Form[SaveDataName]);
            if (DeleteName.Equals(operation)) result = Delete();
            if (GetOAuthUrlName.Equals(operation)) result = GetOAuthURL(
                context.Request.Form[OAuthCallbackName],
                context.Request.Form[OAuthTypeName],
                context.Request.Form[OAuthStateName]);


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
                return GlobalManager.FunctionManager.Menu.GetMenu(Account ?? GlobalManager.GetDefaultAccount());
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
                    msg = GlobalManager.FunctionManager.Menu.CreateMenu(Account ?? GlobalManager.GetDefaultAccount(), m).GetIntroduce()
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
                    msg = GlobalManager.FunctionManager.Menu.DeleteMenu(Account ?? GlobalManager.GetDefaultAccount()).GetIntroduce()
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
                    url = GlobalManager.FunctionManager.OAuth.GetURLForOAuthGetCode(
                        Account ?? GlobalManager.GetDefaultAccount(),
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
