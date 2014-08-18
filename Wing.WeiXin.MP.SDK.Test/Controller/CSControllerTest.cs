using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    /// <summary>
    ///这是 CSControllerTest 的测试类，旨在
    ///包含所有 CSControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class CSControllerTest : BaseTest
    {
        #region SendCSMessage 的测试 public void SendCSMessageTest()
        /// <summary>
        /// SendCSMessage 的测试
        ///</summary>
        [TestMethod]
        public void SendCSMessageTest()
        {
            GlobalManager.AccessTokenContainer.NewAccessToken += r => Console.WriteLine("new---------------------------------");

            ErrorMsg e = new CSController().SendCSMessage(account, csMessageText);
            ErrorMsg e2 = new CSController().SendCSMessage(account, csMessageText);
            Assert.AreEqual(e.errcode, "0");
        } 
        #endregion
    }
}
