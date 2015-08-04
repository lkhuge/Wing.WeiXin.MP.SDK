using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Material
{
    /// <summary>
    /// 图片URL
    /// </summary>
    public class MediaImgURL
    {
        /// <summary>
        /// 上传图片的URL，可用于后续群发中，放置到图文消息中
        /// </summary>
        public string url { get; set; }
    }
}
