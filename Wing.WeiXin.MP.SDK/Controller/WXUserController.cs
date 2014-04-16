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
        #region 获取用户基本信息 public static WXUser GetWXUser(string openID)
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="openID">普通用户的标识</param>
        /// <returns>用户基本信息</returns>
        public static WXUser GetWXUser(string openID)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUser(openID));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUser>(result);
        } 
        #endregion

        #region 获取用户列表 public static WXUserList GetWXUserList()
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static WXUserList GetWXUserList()
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserList());
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 根据用户列表对象获取指定数量用户列表 public static List<WXUser> GetWXUserListFromList(WXUserList userList, int num = 0)
        /// <summary>
        /// 根据用户列表对象获取指定数量用户列表
        /// </summary>
        /// <param name="userList">用户列表对象</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public static List<WXUser> GetWXUserListFromList(WXUserList userList, int num = 0)
        {
            if (num < 0 || num > userList.total) throw new ArgumentOutOfRangeException();
            if (userList.total == 0) return null;
            num = num == 0 ? userList.total : num;
            List<WXUser> wxUserList = new List<WXUser>();
            for (int i = 0; i < num; i++)
            {
                if (userList.data.openid.Count <= i)
                {
                    WXUserList userListNext = GetWXUserListNext(userList);
                    userList.next_openid = userListNext.next_openid;
                    userList.data.openid.AddRange(userListNext.data.openid);
                }
                wxUserList.Add(GetWXUser(userList.data.openid[i]));
            }

            return wxUserList;
        } 
        #endregion

        #region 获取后续用户列表 public static WXUserList GetWXUserListNext(WXUserList userList)
        /// <summary>
        /// 获取后续用户列表
        /// </summary>
        /// <param name="userList">用户列表</param>
        /// <returns>后续用户列表</returns>
        public static WXUserList GetWXUserListNext(WXUserList userList)
        {
            if (userList.total == userList.count || String.IsNullOrEmpty(userList.next_openid)) return userList;
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserListNext(userList));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 添加组 public static WXUserGroup AddWXGroup(WXUserGroup group)
        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="group">组</param>
        /// <returns>组</returns>
        public static WXUserGroup AddWXGroup(WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForCreateWXUserGroup(), JSONHelper.JSONSerialize(group));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserGroup>(result);
        } 
        #endregion

        #region 获取用户组列表 public static WXUserGroupList GetWXUserGroupList()
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns>用户组列表</returns>
        public static WXUserGroupList GetWXUserGroupList()
        {
            string result = HTTPHelper.Get(URLManager.GetURLForGetWXUserGroupList());
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<WXUserGroupList>(result);
        } 
        #endregion

        #region 根据用户获取组 public static WXUserGroup GetWXGroupByWXUser(WXUser user)
        /// <summary>
        /// 根据用户获取组
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>组</returns>
        public static WXUserGroup GetWXGroupByWXUser(WXUser user)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForGetWXUserGroupByWXUser(), JSONHelper.JSONSerialize(user));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return new WXUserGroup { group = new WXGroup
            {
                id = JSONHelper.JSONDeserialize<WXGroupForGet>(result).groupid
            }};
        }
        #endregion

        #region 修改组名 public static ErrorMsg ModityGroupName(WXUserGroup group)
        /// <summary>
        /// 修改组名
        /// </summary>
        /// <param name="group">组</param>
        /// <returns></returns>
        public static ErrorMsg ModityGroupName(WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForModityWXUserGroup(), JSONHelper.JSONSerialize(group));

            return JSONHelper.JSONDeserialize<ErrorMsg>(result);
        } 
        #endregion

        #region 移动用户分组 public static ErrorMsg MoveGroup(WXUser user, WXUserGroup group)
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="group">组</param>
        /// <returns></returns>
        public static ErrorMsg MoveGroup(WXUser user, WXUserGroup group)
        {
            string result = HTTPHelper.Post(URLManager.GetURLForMoveWXUserGroup(), JSONHelper.JSONSerialize(new
            {
                user.openid,
                to_groupid = group.group.id
            }));

            return JSONHelper.JSONDeserialize<ErrorMsg>(result);
        }
        #endregion
    }
}
