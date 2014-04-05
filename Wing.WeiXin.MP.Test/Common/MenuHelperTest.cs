using Wing.WeiXin.MP.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Entities.Menu;

namespace Wing.WeiXin.MP.Test.Common
{
    
    
    /// <summary>
    ///这是 MenuHelperTest 的测试类，旨在
    ///包含所有 MenuHelperTest 单元测试
    ///</summary>
    [TestClass]
    public class MenuHelperTest : BaseTest
    {
        #region GetMenu 的测试
        /// <summary>
        /// GetMenu 的测试
        ///</summary>
        [TestMethod]
        public void GetMenuTest()
        {
            Assert.AreEqual(MenuHelper.GetMenu(menuForGet).button.Count, 3);
        } 
        #endregion
    }
}
