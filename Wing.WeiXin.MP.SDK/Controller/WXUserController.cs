using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class WXUserController
    {
        /// <summary>
        /// 获取用户基本信息的URL
        /// </summary>
        private const string UrlGetWXUser = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}";

        /// <summary>
        /// 获取用户列表的URL
        /// </summary>
        private const string UrlGetWXUserList = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}";

        /// <summary>
        /// 获取后续用户列表的URL
        /// </summary>
        private const string UrlGetWXUserListNext = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";

        /// <summary>
        /// 添加组的URL
        /// </summary>
        private const string UrlAddWXGroup = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";

        /// <summary>
        /// 获取用户组列表的URL
        /// </summary>
        private const string UrlGetWXUserGroupList = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";

        /// <summary>
        /// 根据用户获取组的URL
        /// </summary>
        private const string UrlGetWXGroupByWXUser = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";

        /// <summary>
        /// 修改组名的URL
        /// </summary>
        private const string UrlModityGroupName = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";

        /// <summary>
        /// 移动用户分组的URL
        /// </summary>
        private const string UrlMoveGroup = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";

        /// <summary>
        /// 修改备注名的URL
        /// </summary>
        private const string UrlModityRemark = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}";

        #region 获取用户基本信息 public WXUser GetWXUser(WXAccount account, string openID, WXLanguageType lang = WXLanguageType.zh_CN)
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="openID">普通用户的标识</param>
        /// <param name="lang">语言</param>
        /// <returns>用户基本信息</returns>
        public WXUser GetWXUser(WXAccount account, string openID, WXLanguageType lang = WXLanguageType.zh_CN)
        {
            string result = LibManager.HTTPHelper.Get(String.Format(
                UrlGetWXUser,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token,
                openID,
                lang));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<WXUser>(result);
        } 
        #endregion

        #region 获取用户列表 public WXUserList GetWXUserList(WXAccount account)
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>用户列表</returns>
        public WXUserList GetWXUserList(WXAccount account)
        {
            string result = LibManager.HTTPHelper.Get(String.Format(
                UrlGetWXUserList,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 根据用户列表对象获取指定数量用户列表 public List<WXUser> GetWXUserListFromList(WXAccount account, WXUserList userList, int num = 0)
        /// <summary>
        /// 根据用户列表对象获取指定数量用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="userList">用户列表对象</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public List<WXUser> GetWXUserListFromList(WXAccount account, WXUserList userList, int num = 0)
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

        #region 获取后续用户列表 public WXUserList GetWXUserListNext(WXAccount account, WXUserList userList)
        /// <summary>
        /// 获取后续用户列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="userList">用户列表</param>
        /// <returns>后续用户列表</returns>
        public WXUserList GetWXUserListNext(WXAccount account, WXUserList userList)
        {
            if (userList.total == userList.count || String.IsNullOrEmpty(userList.next_openid)) return userList;
            string result = LibManager.HTTPHelper.Get(String.Format(
                UrlGetWXUserListNext,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token,
                userList.next_openid));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<WXUserList>(result);
        }
        #endregion

        #region 添加组 public WXUserGroup AddWXGroup(WXAccount account, WXUserGroup group)
        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">组</param>
        /// <returns>组</returns>
        public WXUserGroup AddWXGroup(WXAccount account, WXUserGroup group)
        {
            string result = LibManager.HTTPHelper.Post(String.Format(
                UrlAddWXGroup,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), LibManager.JSONHelper.JSONSerialize(group));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<WXUserGroup>(result);
        } 
        #endregion

        #region 获取用户组列表 public WXUserGroupList GetWXUserGroupList(WXAccount account)
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>用户组列表</returns>
        public WXUserGroupList GetWXUserGroupList(WXAccount account)
        {
            string result = LibManager.HTTPHelper.Get(String.Format(
                UrlGetWXUserGroupList,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<WXUserGroupList>(result);
        } 
        #endregion

        #region 根据用户获取组 public WXUserGroup GetWXGroupByWXUser(WXAccount account, WXUser user)
        /// <summary>
        /// 根据用户获取组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="user">用户</param>
        /// <returns>组</returns>
        public WXUserGroup GetWXGroupByWXUser(WXAccount account, WXUser user)
        {
            string result = LibManager.HTTPHelper.Post(String.Format(
                UrlGetWXGroupByWXUser,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), LibManager.JSONHelper.JSONSerialize(user));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return new WXUserGroup { group = new WXGroup
            {
                id = LibManager.JSONHelper.JSONDeserialize<WXGroupForGet>(result).groupid
            }};
        }
        #endregion

        #region 修改组名 public ErrorMsg ModityGroupName(WXAccount account, WXUserGroup group)
        /// <summary>
        /// 修改组名
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">组</param>
        /// <returns>结果</returns>
        public ErrorMsg ModityGroupName(WXAccount account, WXUserGroup group)
        {
            string result = LibManager.HTTPHelper.Post(String.Format(
                UrlModityGroupName,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), LibManager.JSONHelper.JSONSerialize(group));

            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result);
        } 
        #endregion

        #region 移动用户分组 public ErrorMsg MoveGroup(WXAccount account, WXUser user, int to_groupid)
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="user">用户</param>
        /// <param name="to_groupid">组ID</param>
        /// <returns>结果</returns>
        public ErrorMsg MoveGroup(WXAccount account, WXUser user, int to_groupid)
        {
            string result = LibManager.HTTPHelper.Post(
                String.Format(
                    UrlMoveGroup,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                LibManager.JSONHelper.JSONSerialize(new
                {
                    user.openid,
                    to_groupid
                }));

            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result);
        }
        #endregion

        #region 修改备注名 public ErrorMsg ModityRemark(WXAccount account, string openid, string remark)
        /// <summary>
        /// 修改备注名
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="openid">普通用户的标识</param>
        /// <param name="remark">备注名</param>
        /// <returns>结果</returns>
        public ErrorMsg ModityRemark(WXAccount account, string openid, string remark)
        {
            string result = LibManager.HTTPHelper.Post(
                String.Format(
                    UrlModityRemark,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                LibManager.JSONHelper.JSONSerialize(new
                {
                    openid,
                    remark
                }));

            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result);
        } 
        #endregion
    }
}
