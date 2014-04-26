using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public static class WXUserController
    {
        #region 获取用户基本信息 public static WXUser GetWXUser(WXAccount account, string openID)
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="openID">普通用户的标识</param>
        /// <returns>用户基本信息</returns>
        public static WXUser GetWXUser(WXAccount account, string openID)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUser(account, openID));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUser>(result);
        } 
        #endregion

        #region 获取用户列表 public static WXUserList GetWXUserList(WXAccount account)
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>用户列表</returns>
        public static WXUserList GetWXUserList(WXAccount account)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserList(account));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 根据用户列表对象获取指定数量用户列表 public static List<WXUser> GetWXUserListFromList(WXAccount account, WXUserList userList, int num = 0)
        /// <summary>
        /// 根据用户列表对象获取指定数量用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="userList">用户列表对象</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public static List<WXUser> GetWXUserListFromList(WXAccount account, WXUserList userList, int num = 0)
        {
            if (num < 0 || num > userList.total) throw new ArgumentOutOfRangeException();
            if (userList.total == 0) return null;
            num = num == 0 ? userList.total : num;
            List<WXUser> wxUserList = new List<WXUser>();
            for (int i = 0; i < num; i++)
            {
                if (userList.data.openid.Count <= i)
                {
                    WXUserList userListNext = GetWXUserListNext(account, userList);
                    userList.next_openid = userListNext.next_openid;
                    userList.data.openid.AddRange(userListNext.data.openid);
                }
                wxUserList.Add(GetWXUser(account, userList.data.openid[i]));
            }

            return wxUserList;
        } 
        #endregion

        #region 获取后续用户列表 public static WXUserList GetWXUserListNext(WXAccount account, WXUserList userList)
        /// <summary>
        /// 获取后续用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="userList">用户列表</param>
        /// <returns>后续用户列表</returns>
        public static WXUserList GetWXUserListNext(WXAccount account, WXUserList userList)
        {
            if (userList.total == userList.count || String.IsNullOrEmpty(userList.next_openid)) return userList;
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserListNext(account, userList));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 添加组 public static WXUserGroup AddWXGroup(WXAccount account, WXUserGroup group)
        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">组</param>
        /// <returns>组</returns>
        public static WXUserGroup AddWXGroup(WXAccount account, WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForCreateWXUserGroup(account), JSONHelper.JSONSerialize(group));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserGroup>(result);
        } 
        #endregion

        #region 获取用户组列表 public static WXUserGroupList GetWXUserGroupList(WXAccount account)
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>用户组列表</returns>
        public static WXUserGroupList GetWXUserGroupList(WXAccount account)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserGroupList(account));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserGroupList>(result);
        } 
        #endregion

        #region 根据用户获取组 public static WXUserGroup GetWXGroupByWXUser(WXAccount account, WXUser user)
        /// <summary>
        /// 根据用户获取组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="user">用户</param>
        /// <returns>组</returns>
        public static WXUserGroup GetWXGroupByWXUser(WXAccount account, WXUser user)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForGetWXUserGroupByWXUser(account), JSONHelper.JSONSerialize(user));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return new WXUserGroup { group = new WXGroup
            {
                id = JSONHelper.JSONDeserialize<WXGroupForGet>(result).groupid
            }};
        }
        #endregion

        #region 修改组名 public static ErrorMsg ModityGroupName(WXAccount account, WXUserGroup group)
        /// <summary>
        /// 修改组名
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">组</param>
        /// <returns></returns>
        public static ErrorMsg ModityGroupName(WXAccount account, WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForModityWXUserGroup(account), JSONHelper.JSONSerialize(group));

            return JSONHelper.JSONDeserialize<ErrorMsg>(result);
        } 
        #endregion

        #region 移动用户分组 public static ErrorMsg MoveGroup(WXAccount account, WXUser user, WXUserGroup group)
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="user">用户</param>
        /// <param name="group">组</param>
        /// <returns></returns>
        public static ErrorMsg MoveGroup(WXAccount account, WXUser user, WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForMoveWXUserGroup(account), JSONHelper.JSONSerialize(new
            {
                user.openid,
                to_groupid = group.group.id
            }));

            return JSONHelper.JSONDeserialize<ErrorMsg>(result);
        }
        #endregion
    }
}
