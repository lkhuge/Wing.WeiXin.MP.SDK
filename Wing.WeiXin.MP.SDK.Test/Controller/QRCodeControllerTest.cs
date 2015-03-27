using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.QRCode;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class QRCodeControllerTest : BaseTest
    {
        [TestMethod]
        public void GetDownLoadTest()
        {
            GetQRCodeTest(GetQRCodeTicketTest());
        } 

        public QRCodeTicket GetQRCodeTicketTest()
        {
            try
            {
                return new QRCodeController().GetQRCodeTicket(account, qrCodeTemp);
            }
            catch (Exception)
            {
                Assert.Fail("创建二维码错误");
            }
            return null;
        } 

        public void GetQRCodeTest(QRCodeTicket qrCodeTicket)
        {
            const string pathName = "E:\\Test\\test.jpg";
            new QRCodeController().GetQRCode(qrCodeTicket.ticket, pathName);
            Assert.IsTrue(File.Exists(pathName));
        } 

        [TestMethod]
        public void GetShortURLTest()
        {
            string url = new QRCodeController().GetShortURL(account, "http://www.baidu.com");
            Assert.IsNotNull(url);
        } 
    }
}
