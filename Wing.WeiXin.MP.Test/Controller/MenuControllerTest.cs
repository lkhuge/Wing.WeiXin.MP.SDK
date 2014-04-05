using Wing.WeiXin.MP.SDK.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.Test.Controller
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
        public void CreateMenuTest()
        {
            Assert.AreEqual(MenuController.CreateMenu(menu).errcode, "0");
        } 
        #endregion

        #region DeleteMenu 的测试 public void DeleteMenuTest()
        /// <summary>
        /// DeleteMenu 的测试
        ///</summary>
        public void DeleteMenuTest()
        {
            Assert.AreEqual(MenuController.DeleteMenu().errcode, "0");
        } 
        #endregion

        #region GetMenu 的测试 public void GetMenuTest()
        /// <summary>
        /// GetMenu 的测试
        ///</summary>
        public void GetMenuTest()
        {
            Assert.AreEqual(MenuController.GetMenu().button.Count, menu.button.Count);
        } 
        #endregion
    }
}
