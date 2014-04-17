using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服图文消息
    /// </summary>
    public class CSMessageNews : CSMessage
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        public News news { get; set; }

        #region 实例化空数据客服图文消息 public CSMessageNews()
        /// <summary>
        /// 实例化空数据客服图文消息
        /// </summary>
        public CSMessageNews()
        {
            msgtype = "news";
        }
        #endregion

        #region 根据图文列表和普通用户openid实例化 public CSMessageNews(List<News.Articles> articles, string touser)
        /// <summary>
        /// 根据图文列表和普通用户openid实例化
        /// </summary>
        /// <param name="articles">图文列表</param>
        /// <param name="touser">普通用户openid</param>
        public CSMessageNews(List<News.Articles> articles, string touser)
            : base(touser)
        {
            msgtype = "news";
            news = new News
            {
                articles = articles
            };
        }
        #endregion

        #region 图文消息 public class News
        /// <summary>
        /// 图文消息
        /// </summary>
        public class News
        {
            /// <summary>
            /// 图文列表
            /// </summary>
            public List<Articles> articles { get; set; }

            #region 图文消息 public class Articles
            /// <summary>
            /// 图文消息
            /// </summary>
            public class Articles
            {
                /// <summary>
                /// 标题
                /// </summary>
                public string title { get; set; }

                /// <summary>
                /// 描述
                /// </summary>
                public string description { get; set; }

                /// <summary>
                /// 点击后跳转的链接
                /// </summary>
                public string url { get; set; }

                /// <summary>
                /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                /// </summary>
                public string picurl { get; set; }
            } 
            #endregion
        } 
        #endregion
    }
}
