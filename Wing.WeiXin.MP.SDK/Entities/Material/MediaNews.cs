using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Material
{
    /// <summary>
    /// 图文素材
    /// </summary>
    public class MediaNews
    {
        /// <summary>
        /// 图文消息列表
        /// </summary>
        public List<NewsArticles> articles { get; set; }

        /// <summary>
        /// 图文消息列表
        /// </summary>
        public List<NewsArticles> news_item { get; set; }
    }
}
