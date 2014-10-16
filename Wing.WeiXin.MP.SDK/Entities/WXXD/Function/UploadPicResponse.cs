using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Function
{
    /// <summary>
    /// 上传图片响应
    /// </summary>
    public class UploadPicResponse : ErrorMsg
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public String image_url { get; set; }
    }
}