using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Material
{
    /// <summary>
    /// 图文素材列表
    /// </summary>
    public class MediaNewsList
    {
        /// <summary>
        /// 素材的总数
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public long item_count { get; set; }

        /// <summary>
        /// 图文素材列表项列表
        /// </summary>
        public List<MediaNewsListItem> item { get; set; }

        /// <summary>
        /// 图文素材列表项
        /// </summary>
        public class MediaNewsListItem
        {
            /// <summary>
            /// 图文素材id
            /// </summary>
            public string media_id { get; set; }

            /// <summary>
            /// 图文消息
            /// </summary>
            public MediaNews content { get; set; }

            /// <summary>
            /// 这篇图文消息素材的最后更新时间
            /// </summary>
            public string update_time { get; set; }
        }
    }
}
