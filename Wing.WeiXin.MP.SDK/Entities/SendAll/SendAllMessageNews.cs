using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 群发图文消息
    /// </summary>
    public class SendAllMessageNews
    {
        /// <summary>
        /// 图文消息，一个图文消息支持1到10条图文
        /// </summary>
        public List<Articles> articles { get; set; }

        /// <summary>
        /// 群发图文消息对象
        /// </summary>
        public class Articles
        {
            /// <summary>
            /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
            /// </summary>
            public string thumb_media_id { get; set; }

            /// <summary>
            /// 图文消息的作者
            /// </summary>
            public string author { get; set; }

            /// <summary>
            /// 图文消息的标题
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 在图文消息页面点击“阅读原文”后的页面
            /// </summary>
            public string content_source_url { get; set; }

            /// <summary>
            /// 图文消息页面的内容，支持HTML标签
            /// </summary>
            public string content { get; set; }

            /// <summary>
            /// 图文消息的描述
            /// </summary>
            public string digest { get; set; }
        }
    }
}
