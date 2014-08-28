using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wing.WeiXin.MP.SDK.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class URLControllerTests : BaseTest
    {
        [TestMethod]
        public void GetShortURLTest()
        {
            string url = new URLController().GetShortURL(account, "http://www.baidu.com");
            Assert.IsNotNull(url);
        }
    }
}
