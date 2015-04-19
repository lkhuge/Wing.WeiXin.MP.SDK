using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.Menu;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class MenuControllerTest : BaseTest
    {
        private readonly MenuController MenuController = GlobalManager.FunctionManager.MenuController;

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
            Assert.AreEqual(MenuController.CreateMenu(account, menu).errcode, "0");
        } 

        [TestMethod]
        public void DeleteMenuTest()
        {
            Assert.AreEqual(MenuController.DeleteMenu(account).errcode, "0");
        } 

        [TestMethod]
        public void GetMenuTest()
        {
            Menu m = MenuController.GetMenu(account);
            Assert.AreEqual(m.button.Count, menu.button.Count);
        } 
    }
}
