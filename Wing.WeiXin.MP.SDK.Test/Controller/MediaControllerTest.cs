using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    /// <summary>
    ///这是 MediaControllerTest 的测试类，旨在
    ///包含所有 MediaControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class MediaControllerTest : BaseTest
    {
        #region 上传下载同步测试 public void UploadAndDownLoadTest()
        /// <summary>
        /// 上传下载同步测试
        /// </summary>
        [TestMethod]
        public void UploadAndDownLoadTest()
        {
            DownLoadTest(UploadTest());
        } 
        #endregion

        #region Upload 的测试 public void UploadTest()
        /// <summary>
        /// Upload 的测试
        ///</summary>
        public string UploadTest()
        {
            const UploadMediaType type = UploadMediaType.image;
            const string path = "E:\\";
            const string name = "test.jpg";
            try
            {
                return new MediaController().Upload(account, type, path, name).media_id;
            }
            catch (Exception)
            {
                Assert.Fail("上传多媒体发送错误" + path + name);
            }
            return null;
        } 
        #endregion

        #region DownLoad 的测试 public void DownLoadTest(string media_id)
        /// <summary>
        /// DownLoad 的测试
        ///</summary>
        public void DownLoadTest(string media_id)
        {
            const string path = "E:\\Test\\test.jpg";
            try
            {
                new MediaController().DownLoad(account, media_id, path);
            }
            catch (Exception)
            {
                Assert.Fail("下载多媒体发送错误" + media_id);
            }
        } 
        #endregion
    }
}
