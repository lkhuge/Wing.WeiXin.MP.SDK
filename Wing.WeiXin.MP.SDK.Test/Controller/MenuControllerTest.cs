using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    /// <summary>
    ///这是 MenuControllerTest 的测试类，旨在
    ///包含所有 MenuControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class MenuControllerTest : BaseTest
    {
        #region 对菜单进行创建获取删除测试 public void CGDMenuTest()
        /// <summary>
        /// 对菜单进行创建获取删除测试
        /// </summary>
        [TestMethod]
        public void CGDMenuTest()
        {
            CreateMenuTest();
            GetMenuTest();
            DeleteMenuTest();
        } 
        #endregion

        #region CreateMenu 的测试 public void CreateMenuTest()
        /// <summary>
        /// CreateMenu 的测试
        ///</summary>
        [TestMethod]
        public void CreateMenuTest()
        {
            Assert.AreEqual(new MenuController().CreateMenu(account, menu).errcode, "0");
        } 
        #endregion

        #region DeleteMenu 的测试 public void DeleteMenuTest()
        /// <summary>
        /// DeleteMenu 的测试
        ///</summary>
        [TestMethod]
        public void DeleteMenuTest()
        {
            Assert.AreEqual(new MenuController().DeleteMenu(account).errcode, "0");
        } 
        #endregion

        #region GetMenu 的测试 public void GetMenuTest()
        /// <summary>
        /// GetMenu 的测试
        ///</summary>
        [TestMethod]
        public void GetMenuTest()
        {
            Menu m = new MenuController().GetMenu(account);
            Assert.AreEqual(m.button.Count, menu.button.Count);
        } 
        #endregion
    }
}
