using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;

namespace Wing.WeiXin.MP.SDK.Test.Controller
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
                @group = new WXGroup
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
                @group = new WXGroup
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
        [TestMethod]
        public WXUserList GetWXUserListTest()
        {
            WXUserList l = new WXUserController().GetWXUserList(account);
            return l;
        } 
        #endregion

        #region GetWXUser 的测试 public void GetWXUserTest()
        /// <summary>
        /// GetWXUser 的测试
        ///</summary>
        public void GetWXUserTest(string openID)
        {
            Assert.AreEqual(new WXUserController().GetWXUser(account, openID).openid, openID);
        }
        #endregion

        #region AddWXGroup 的测试 public void AddWXGroupTest()
        /// <summary>
        /// AddWXGroup 的测试
        ///</summary>
        public void AddWXGroupTest(WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().AddWXGroup(account, group).group.name, group.group.name);
        }
        #endregion

        #region GetWXUserGroupList 的测试 public WXUserGroupList GetWXUserGroupListTest()
        /// <summary>
        /// GetWXUserGroupList 的测试
        ///</summary>
        [TestMethod]
        public WXUserGroupList GetWXUserGroupListTest()
        {
            return new WXUserController().GetWXUserGroupList(account);
        } 
        #endregion

        #region ModityGroupName 的测试 public void ModityGroupNameTest(WXUserGroup group)
        /// <summary>
        /// ModityGroupName 的测试
        ///</summary>
        public void ModityGroupNameTest(WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().ModityGroupName(account, group).errcode, "0");
        } 
        #endregion

        #region MoveGroup 的测试 public void MoveGroupTest(WXUser user, WXUserGroup group)
        /// <summary>
        /// MoveGroup 的测试
        ///</summary>
        public void MoveGroupTest(WXUser user, WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().MoveGroup(account, user, group).errcode, "0");
        } 
        #endregion

        #region GetWXGroupByWXUser 的测试 public WXUserGroup GetWXGroupByWXUserTest(WXUser user)
        /// <summary>
        /// GetWXGroupByWXUser 的测试
        ///</summary>
        public WXUserGroup GetWXGroupByWXUserTest(WXUser user)
        {
            return new WXUserController().GetWXGroupByWXUser(account, user);
        } 
        #endregion

        #region GetWXUserListFromList 的测试 public WXUser GetWXUserListFromListTest(WXUserList userList)
        /// <summary>
        /// GetWXUserListFromList 的测试
        ///</summary>
        public WXUser GetWXUserListFromListTest(WXUserList userList)
        {
            return new WXUserController().GetWXUserListFromList(account, userList, 1)[0];
        } 
        #endregion

        #region GetWXGroupByWXUser 的测试 public void GetWXGroupByWXUserTest()
        /// <summary>
        /// GetWXGroupByWXUser 的测试
        ///</summary>
        [TestMethod]
        public void GetWXGroupByWXUserTest()
        {
            WXUserGroup g = new WXUserController().GetWXGroupByWXUser(account, new WXUser
            {
                openid = "orImOuC33jQiJFrVelQGGTmwPSFE"
            });
        }
        #endregion
    }
}
