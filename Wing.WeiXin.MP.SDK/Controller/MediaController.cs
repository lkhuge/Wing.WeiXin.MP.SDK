using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 多媒体控制器
    /// </summary>
    public class MediaController
    {
        /// <summary>
        /// 上传多媒体的URL
        /// </summary>
        private const string UrlUpload = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";

        /// <summary>
        /// 上传视频（高级群发）的URL
        /// </summary>
        private const string UrlUploadVideoForSendAll = "https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}";

        /// <summary>
        /// 下载多媒体的URL
        /// </summary>
        private const string UrlDownLoad = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";

        #region 上传多媒体 public Media Upload(WXAccount account, UploadMediaType type, string path, string name)
        /// <summary>
        /// 上传多媒体
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="type">多媒体类型</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>多媒体对象</returns>
        public Media Upload(WXAccount account, UploadMediaType type, string path, string name)
        {
            string result = LibManager.HTTPHelper.Upload(String.Format(
                UrlUpload,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token, 
                type), path, name);
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return LibManager.JSONHelper.JSONDeserialize<Media>(result);
        } 
        #endregion

        #region 上传视频（高级群发） public Media UploadVideoForSendAll(WXAccount account, string path, string name, string title, string description)
        /// <summary>
        /// 上传视频（高级群发）
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <param name="title">视频的标题</param>
        /// <param name="description">视频的描述</param>
        /// <returns>多媒体对象</returns>
        public Media UploadVideoForSendAll(WXAccount account, string path, string name, string title, string description)
        {
            Media media = Upload(account, UploadMediaType.video, path, name);
            string result = LibManager.HTTPHelper.Post(String.Format(
                UrlUploadVideoForSendAll,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), LibManager.JSONHelper.JSONSerialize(new
                {
                    media.media_id, title, description
                }));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return LibManager.JSONHelper.JSONDeserialize<Media>(result);
        }
        #endregion

        #region 下载多媒体 public void DownLoad(WXAccount account, string media_id, string pathName)
        /// <summary>
        /// 下载多媒体
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">多媒体编号</param>
        /// <param name="pathName">下载路径加文件名</param>
        public void DownLoad(WXAccount account, string media_id, string pathName)
        {
            string result = LibManager.HTTPHelper.DownloadFile(String.Format(
                UrlDownLoad,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token,
                media_id), pathName);
            if (!String.IsNullOrEmpty(result)) throw MessageException.GetInstance(
                LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
        } 
        #endregion
    }
}
