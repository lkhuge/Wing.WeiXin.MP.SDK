using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.ReceiveHandler.Ashx
{
    /// <summary>
    /// 菜单操作
    /// </summary>
    public class AshxMenuHandler : IHttpHandler
    {
        /// <summary>
        /// 使用的微信账号
        /// </summary>
        public static WXAccount Account;

        /// <summary>
        /// 菜单控制器
        /// </summary>
        private readonly MenuController controller;

        #region 初始化 public AshxMenuHandler()
        /// <summary>
        /// 初始化
        /// </summary>
        public AshxMenuHandler()
        {
            controller = new MenuController();
            if (Account == null) Account = GlobalManager.GetFirstAccount();
        } 
        #endregion

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
            return controller.GetMenu(Account);
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
            MenuForGet m = JSONHelper.JSONDeserialize<MenuForGet>(HttpUtility.UrlDecode(menu));
            return new
            {
                msg = controller.CreateMenu(Account, m).GetIntroduce()
            };
        } 
        #endregion

        #region 删除菜单 private object Delete()
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns>结果</returns>
        private object Delete()
        {
            return new
            {
                msg = controller.DeleteMenu(Account).GetIntroduce()
            };
        } 
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
