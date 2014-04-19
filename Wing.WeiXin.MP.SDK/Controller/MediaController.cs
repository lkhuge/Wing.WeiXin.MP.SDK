using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 多媒体控制器
    /// </summary>
    public static class MediaController
    {
        #region 上传多媒体 public static Media Upload(string weixinMPID, UploadMediaType type, string path)
        /// <summary>
        /// 上传多媒体
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="type">多媒体类型</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>多媒体对象</returns>
        public static Media Upload(string weixinMPID, UploadMediaType type, string path, string name)
        {
            string result = HTTPHelper.Upload(URLManager.GetURLForUploadMedia(weixinMPID, type), path, name);
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<Media>(result);
        } 
        #endregion

        #region 下载多媒体 public static void DownLoad(string weixinMPID, string media_id, string pathName)
        /// <summary>
        /// 下载多媒体
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="media_id">多媒体编号</param>
        /// <param name="pathName">下载路径加文件名</param>
        public static void DownLoad(string weixinMPID, string media_id, string pathName)
        {
            string result = HTTPHelper.DownloadFile(URLManager.GetURLForDownloadMedia(weixinMPID, media_id), pathName);
            if (!String.IsNullOrEmpty(result)) throw new ErrorMsgException(
                JSONHelper.JSONDeserialize<ErrorMsg>(result));
        } 
        #endregion
    }
}
