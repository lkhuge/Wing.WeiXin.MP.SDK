using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class MenuControllerTest : BaseTest
    {
        [TestMethod]
        public void CGDMenuTest()
        {
            CreateMenuTest();
            GetMenuTest();
            DeleteMenuTest();
        } 

        [TestMethod]
        public void CreateMenuTest()
        {
            Assert.AreEqual(new MenuController().CreateMenu(account, menu).errcode, "0");
        } 

        [TestMethod]
        public void DeleteMenuTest()
        {
            Assert.AreEqual(new MenuController().DeleteMenu(account).errcode, "0");
        } 

        [TestMethod]
        public void GetMenuTest()
        {
            MenuForGet m = new MenuController().GetMenu(account);
            Assert.AreEqual(m.menu.button.Count, menu.button.Count);
        } 
    }
}
