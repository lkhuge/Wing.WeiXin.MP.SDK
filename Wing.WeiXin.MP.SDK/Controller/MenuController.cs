using System;
using System.IO;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 菜单工具类
    /// </summary>
    public class MenuController
    {
        #region 创建菜单 public ErrorMsg CreateMenu(WXAccount account, Menu menu)
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>错误码</returns>
        public ErrorMsg CreateMenu(WXAccount account, Menu menu)
        {
            return JSONHelper.JSONDeserialize<ErrorMsg>(
                HTTPHelper.Post(URLManager.GetURLForCreateMenu(account), JSONHelper.JSONSerialize(menu)));
        } 
        #endregion

        #region 获取菜单 public MenuForGet GetMenu(WXAccount account)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>菜单</returns>
        public MenuForGet GetMenu(WXAccount account)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetMenu(account));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<MenuForGet>(result);
        } 
        #endregion

        #region 删除菜单 public ErrorMsg DeleteMenu(WXAccount account)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>错误码</returns>
        public ErrorMsg DeleteMenu(WXAccount account)
        {
            return JSONHelper.JSONDeserialize<ErrorMsg>(HTTPHelper.Get(URLManager.GetURLForDeleteMenu(account)));
        } 
        #endregion
    }
}
