using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.User.Group;
using Wing.WeiXin.MP.SDK.Entities.User.User;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class WXUserControllerTest : BaseTest
    {
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

        [TestMethod]
        public WXUserList GetWXUserListTest()
        {
            WXUserList l = new WXUserController().GetWXUserList(account);
            return l;
        } 

        public void GetWXUserTest(string openID)
        {
            Assert.AreEqual(new WXUserController().GetWXUser(account, openID).openid, openID);
        }

        public void AddWXGroupTest(WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().AddWXGroup(account, group).group.name, group.group.name);
        }

        [TestMethod]
        public WXUserGroupList GetWXUserGroupListTest()
        {
            return new WXUserController().GetWXUserGroupList(account);
        } 

        public void ModityGroupNameTest(WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().ModityGroupName(account, group).errcode, "0");
        } 

        public void MoveGroupTest(WXUser user, WXUserGroup group)
        {
            Assert.AreEqual(new WXUserController().MoveGroup(account, user.openid, group.group.id).errcode, "0");
        } 

        public WXUserGroup GetWXGroupByWXUserTest(WXUser user)
        {
            return new WXUserController().GetWXGroupByWXUser(account, user);
        } 

        public WXUser GetWXUserListFromListTest(WXUserList userList)
        {
            return new WXUserController().GetWXUserListFromList(account, userList, 1)[0];
        } 

        [TestMethod]
        public void GetWXGroupByWXUserTest()
        {
            WXUserGroup g = new WXUserController().GetWXGroupByWXUser(account, new WXUser
            {
                openid = "orImOuC33jQiJFrVelQGGTmwPSFE"
            });
        }
    }
}
