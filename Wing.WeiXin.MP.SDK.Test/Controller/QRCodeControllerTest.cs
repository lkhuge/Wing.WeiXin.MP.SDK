using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.QRCode;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    
    
    /// <summary>
    ///这是 QRCodeControllerTest 的测试类，旨在
    ///包含所有 QRCodeControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class QRCodeControllerTest : BaseTest
    {
        #region 获取下载二维码测试 public void GetDownLoadTest()
        /// <summary>
        /// 获取下载二维码测试
        /// </summary>
        [TestMethod]
        public void GetDownLoadTest()
        {
            GetQRCodeTest(GetQRCodeTicketTest());
        } 
        #endregion

        #region GetQRCodeTicket 的测试 public QRCodeTicket GetQRCodeTicketTest()
        /// <summary>
        /// GetQRCodeTicket 的测试
        ///</summary>
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
        #endregion

        #region GetQRCode 的测试 public void GetQRCodeTest(QRCodeTicket qrCodeTicket)
        /// <summary>
        /// GetQRCode 的测试
        ///</summary>
        public void GetQRCodeTest(QRCodeTicket qrCodeTicket)
        {
            const string pathName = "E:\\Test\\test.jpg";
            new QRCodeController().GetQRCode(qrCodeTicket, pathName);
            Assert.IsTrue(File.Exists(pathName));
        } 
        #endregion
    }
}
