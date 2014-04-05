﻿using System;
using System.IO;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.HTTP;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 菜单工具类
    /// </summary>
    public static class MenuController
    {
        #region 创建菜单 public static ErrorMsg CreateMenu(Menu menu)
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>错误码</returns>
        public static ErrorMsg CreateMenu(Menu menu)
        {
            return JSONHelper.JSONDeserialize<ErrorMsg>(
                HTTPHelper.Post(URLManager.GetURLForCreateMenu(), JSONHelper.JSONSerialize(menu)));
        } 
        #endregion

        #region 获取菜单 public static MenuForGet GetMenu()
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns>菜单</returns>
        public static Menu GetMenu()
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetMenu());
            ErrorMsg errorMsg = Authentication.CheckHaveErrorMsg(result);
            if (errorMsg != null) throw new FailGetMenuException(errorMsg.GetIntroduce());

            return MenuHelper.GetMenu(JSONHelper.JSONDeserialize<MenuForGet>(result));
        } 
        #endregion

        #region 删除菜单 public static ErrorMsg DeleteMenu()
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns>错误码</returns>
        public static ErrorMsg DeleteMenu()
        {
            return Authentication.CheckHaveErrorMsg(HTTPHelper.Get(URLManager.GetURLForDeleteMenu()));
        } 
        #endregion
    }
}
