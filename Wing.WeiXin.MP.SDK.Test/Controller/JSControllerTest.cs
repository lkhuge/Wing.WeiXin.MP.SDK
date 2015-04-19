using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.JS;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class JSControllerTest : BaseTest
    {
        [TestMethod]
        public void GetJSWeixinConfigTest()
        {
            JSController jsController = GlobalManager.FunctionManager.JSController;
            WXAccount accountT = account;
            const string url = "http://127.0.0.1/test.html";
            string[] jsApiList =  {"hideMenuItems"};

            JSWeixinConfig config = jsController.GetJSWeixinConfig(accountT, url, jsApiList);
            Assert.IsNotNull(config);
        }
    }
}
