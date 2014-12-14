using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities.WXXD.Function;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店功能管理
    /// </summary>
    public class WXFunctionManager : WXXDManager
    {
        /// <summary>
        /// 上传图片的URL
        /// </summary>
        private const String URLUploadPic =
            "https://api.weixin.qq.com/merchant/common/upload_img?access_token={AccessToken}&filename={Filename}";

        #region 上传图片 public UploadPicResponse UploadPic(String picPath, String filename)
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="picPath">图片路径</param>
        /// <param name="filename">文件名</param>
        /// <returns>上传图片响应</returns>
        public UploadPicResponse UploadPic(String picPath, String filename)
        {
            return Upload<UploadPicResponse>(
                URLUploadPic.Replace("{Filename}", filename), 
                picPath, 
                filename);
        } 
        #endregion
    }
}