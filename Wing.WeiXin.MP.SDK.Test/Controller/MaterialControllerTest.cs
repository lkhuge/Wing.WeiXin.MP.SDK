using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Material;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class MaterialControllerTest : BaseTest
    {
        private readonly MaterialController controller = GlobalManager.FunctionManager.Material;

        private readonly string[] mediaIDList =
        {
            "jUtUo6nQzY0Z2FQ3Ay-UA2gy4yfpL-dhOjVRw-P752Ks7Niq4r0jsZIWrZr-xzz_",
            "8794195636"
        };

        [TestMethod]
        public void AddTempTest()
        {
            WXAccount testAccount = account;
            const UploadMediaType type = UploadMediaType.thumb;
            const string path = "E:\\";
            const string name = "2.JPG";

            Media media = controller.AddTemp(testAccount, type, path, name);

            Assert.IsNotNull(media);
        }

        [TestMethod]
        public void GetTempTest()
        {
            WXAccount testAccount = account;
            string media_id = mediaIDList[0];
            const string pathName = "E:\\2.JPG";

            controller.GetTemp(testAccount, media_id, pathName);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddNewsTest()
        {
            WXAccount testAccount = account;
            MediaNews mediaNews = new MediaNews
            {
                articles = new List<NewsArticles>
                {
                    new NewsArticles
                    {
                        author = "author",
                        content = "content",
                        content_source_url = "content_source_url",
                        digest = "digest",
                        show_cover_pic = "0",
                        thumb_media_id = mediaIDList[1],
                        title = "title"
                    }
                }
            };

            Media media = controller.AddNews(testAccount, mediaNews);

            Assert.IsNotNull(media);
        }

        [TestMethod]
        public void AddTest()
        {
            WXAccount testAccount = account;
            const string path = "E:\\";
            const string name = "1.JPG";

            Media media = controller.Add(testAccount, path, name);

            Assert.IsNotNull(media);
        }

        [TestMethod]
        public void GetNewsTest()
        {
            WXAccount testAccount = account;
            const string media_id = "17384130244";

            MediaNews media = controller.GetNews(testAccount, media_id);

            Assert.IsNotNull(media);
        }

        [TestMethod]
        public void GetTest()
        {
            WXAccount testAccount = account;
            string media_id = mediaIDList[1];
            const string pathName = "E:\\3.JPG";

            controller.Get(testAccount, media_id, pathName);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DeleteTest()
        {
            WXAccount testAccount = account;
            const string media_id = "8794195860";

            ErrorMsg msg = controller.Delete(testAccount, media_id);

            Assert.IsTrue(msg.errcode.Equals("0"));
        }

        [TestMethod]
        public void UpdateTest()
        {
            WXAccount testAccount = account;
            const string media_id = "17384130244";
            List<NewsArticles> articles = new List<NewsArticles>
            {
                new NewsArticles
                {
                    author = "author",
                    content = "content",
                    content_source_url = "content_source_url",
                    digest = "digest",
                    show_cover_pic = "0",
                    thumb_media_id = mediaIDList[1],
                    title = "title"
                }
            };
            const int index = 1;

            ErrorMsg msg = controller.Update(testAccount, articles, media_id, index);

            Assert.IsTrue(msg.errcode.Equals("0"));
        }

        [TestMethod]
        public void GetCountTest()
        {
            WXAccount testAccount = account;

            MediaCount msg = controller.GetCount(testAccount);

            Assert.IsNotNull(msg);
        }

        [TestMethod]
        public void GetNewsListTest()
        {
            WXAccount testAccount = account;
            const int offset = 0;
            const int count = 10;

            MediaNewsList msg = controller.GetNewsList(testAccount, offset, count);

            Assert.IsNotNull(msg);
        }

        [TestMethod]
        public void GetListTest()
        {
            WXAccount testAccount = account;
            const UploadMediaType type = UploadMediaType.image;
            const int offset = 0;
            const int count = 10;

            MediaList msg = controller.GetList(testAccount, type, offset, count);

            Assert.IsNotNull(msg);
        }
    }
}
