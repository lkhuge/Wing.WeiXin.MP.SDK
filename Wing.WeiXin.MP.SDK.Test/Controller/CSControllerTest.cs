using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class CSControllerTest : BaseTest
    {
        [TestMethod]
        public void SendCSMessageTest()
        {
            GlobalManager.AccessTokenContainer.NewAccessToken += r => Console.WriteLine("new---------------------------------");

            ErrorMsg e = new CSController().SendCSMessage(account, csMessageText);
            ErrorMsg e2 = new CSController().SendCSMessage(account, csMessageText);
            Assert.AreEqual(e.errcode, "0");
        } 
    }
}
