using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Material
{
    /// <summary>
    /// 素材列表
    /// </summary>
    public class MediaList
    {
        /// <summary>
        /// 素材的总数
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }

        /// <summary>
        /// 素材列表项列表
        /// </summary>
        public List<MediaListItem> item { get; set; }

        /// <summary>
        /// 素材列表项
        /// </summary>
        public class MediaListItem
        {
            /// <summary>
            /// 素材id
            /// </summary>
            public string media_id { get; set; }

            /// <summary>
            /// 文件名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 消息素材的最后更新时间
            /// </summary>
            public string update_time { get; set; }
        }
    }
}
