using System.Linq;
using Wing.WeiXin.MP.SDK.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.Test.Controller
{
    
    
    /// <summary>
    ///这是 WXUserControllerTest 的测试类，旨在
    ///包含所有 WXUserControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class WXUserControllerTest : BaseTest
    {
        #region 微信用户测试 public void WXUserAllTest()
        /// <summary>
        /// 微信用户测试
        /// </summary>
        [TestMethod]
        public void WXUserAllTest()
        {
            WXUserList useList = GetWXUserListTest();
            Assert.IsNotNull(GetWXUserListFromListTest(useList));
            if (useList.count > 0)
            {
                GetWXUserTest(useList.data.openid[0]);
            }
            WXUser newUser = new WXUser {openid = useList.data.openid[0]};
            WXUserGroup group = new WXUserGroup
            {
                group = new WXGroup
                {
                    name = "test234"
                }
            };
            AddWXGroupTest(group);
            WXUserGroupList list = GetWXUserGroupListTest();
            WXGroup groupTemp = list.groups.SingleOrDefault(g => g.name.Equals(group.group.name));
            Assert.IsNotNull(groupTemp);
            WXUserGroup groupNew = new WXUserGroup
            {
                group = new WXGroup
                {
                    id = groupTemp.id,
                    name = "test2"
                }
            };
            ModityGroupNameTest(groupNew);
            MoveGroupTest(newUser, groupNew);
            Assert.AreEqual(GetWXGroupByWXUserTest(newUser).group.id, groupNew.group.id);
        } 
        #endregion

        #region GetWXUserList 的测试 public void GetWXUserListTest()
        /// <summary>
        /// GetWXUserList 的测试
        ///</summary>
        public WXUserList GetWXUserListTest()
        {
            return WXUserController.GetWXUserList("gh_7f215c8b1c91");
        } 
        #endregion

        #region GetWXUser 的测试 public void GetWXUserTest()
        /// <summary>
        /// GetWXUser 的测试
        ///</summary>
        public void GetWXUserTest(string openID)
        {
            Assert.AreEqual(WXUserController.GetWXUser("gh_7f215c8b1c91", openID).openid, openID);
        }
        #endregion

        #region AddWXGroup 的测试 public void AddWXGroupTest()
        /// <summary>
        /// AddWXGroup 的测试
        ///</summary>
        public void AddWXGroupTest(WXUserGroup group)
        {
            Assert.AreEqual(WXUserController.AddWXGroup("gh_7f215c8b1c91", group).group.name, group.group.name);
        }
        #endregion

        #region GetWXUserGroupList 的测试 public WXUserGroupList GetWXUserGroupListTest()
        /// <summary>
        /// GetWXUserGroupList 的测试
        ///</summary>
        public WXUserGroupList GetWXUserGroupListTest()
        {
            return WXUserController.GetWXUserGroupList("gh_7f215c8b1c91");
        } 
        #endregion

        #region ModityGroupName 的测试 public void ModityGroupNameTest(WXUserGroup group)
        /// <summary>
        /// ModityGroupName 的测试
        ///</summary>
        public void ModityGroupNameTest(WXUserGroup group)
        {
            Assert.AreEqual(WXUserController.ModityGroupName("gh_7f215c8b1c91", group).errcode, "0");
        } 
        #endregion

        #region MoveGroup 的测试 public void MoveGroupTest(WXUser user, WXUserGroup group)
        /// <summary>
        /// MoveGroup 的测试
        ///</summary>
        public void MoveGroupTest(WXUser user, WXUserGroup group)
        {
            Assert.AreEqual(WXUserController.MoveGroup("gh_7f215c8b1c91", user, group).errcode, "0");
        } 
        #endregion

        #region GetWXGroupByWXUser 的测试 public WXUserGroup GetWXGroupByWXUserTest(WXUser user)
        /// <summary>
        /// GetWXGroupByWXUser 的测试
        ///</summary>
        public WXUserGroup GetWXGroupByWXUserTest(WXUser user)
        {
            return WXUserController.GetWXGroupByWXUser("gh_7f215c8b1c91", user);
        } 
        #endregion

        #region GetWXUserListFromList 的测试 public WXUser GetWXUserListFromListTest(WXUserList userList)
        /// <summary>
        /// GetWXUserListFromList 的测试
        ///</summary>
        public WXUser GetWXUserListFromListTest(WXUserList userList)
        {
            return WXUserController.GetWXUserListFromList("gh_7f215c8b1c91", userList, 1)[0];
        } 
        #endregion
    }
}
