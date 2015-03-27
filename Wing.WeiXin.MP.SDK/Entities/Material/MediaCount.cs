using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Material
{
    /// <summary>
    /// 素材总数
    /// </summary>
    public class MediaCount
    {
        /// <summary>
        /// 语音总数量
        /// </summary>
        public long voice_count { get; set; }

        /// <summary>
        /// 视频总数量
        /// </summary>
        public long video_count { get; set; }

        /// <summary>
        /// 图片总数量
        /// </summary>
        public long image_count { get; set; }

        /// <summary>
        /// 图文总数量
        /// </summary>
        public long news_count { get; set; }
    }
}
