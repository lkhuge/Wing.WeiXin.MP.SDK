using System;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Material;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    public abstract class WXController
    {
        /// <summary>
        /// AccessToken容器
        /// </summary>
        internal static AccessTokenContainer AccessTokenContainer;

        #region 初始化微信控制器 protected WXController()
        /// <summary>
        /// 初始化微信控制器
        /// </summary>
        protected WXController()
        {
            if (AccessTokenContainer == null)
                throw WXException.GetInstance("未初始化AccessToken容器", Settings.Default.SystemUsername);
        } 
        #endregion

        #region 执行请求 响应类型为T的对象 protected T Action<T>(string url, WXAccount account)
        /// <summary>
        /// 执行请求 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信账号</param>
        /// <returns>类型为T的对象</returns>
        protected T Action<T>(string url, WXAccount account)
        {
            string result = HTTPHelper.Get(String.Format(
                url,
                AccessTokenContainer.GetAccessToken(account).access_token));
            if (!typeof(ErrorMsg).IsAssignableFrom(typeof(T)) && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        } 
        #endregion

        #region 执行请求（无需AccessToken） 响应类型为T的对象 protected T ActionWithoutAccessToken<T>(string url, WXAccount account)
        /// <summary>
        /// 执行请求（无需AccessToken） 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信账号</param>
        /// <returns>类型为T的对象</returns>
        protected T ActionWithoutAccessToken<T>(string url, WXAccount account)
        {
            string result = HTTPHelper.Get(url);
            if (!typeof(ErrorMsg).IsAssignableFrom(typeof(T)) && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        }
        #endregion

        #region 执行带有消息对象的请求 响应类型为T的对象 protected T Action<T>(string url, object messageObj, WXAccount account)
        /// <summary>
        /// 执行带有消息对象的请求 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="messageObj">消息对象</param>
        /// <param name="account">微信账号</param>
        /// <returns>类型为T的对象</returns>
        protected T Action<T>(string url, object messageObj, WXAccount account)
        {
            string result = HTTPHelper.Post(String.Format(
                    url,
                    AccessTokenContainer.GetAccessToken(account).access_token),
                    JSONHelper.JSONSerialize(messageObj));
            if (!typeof(ErrorMsg).IsAssignableFrom(typeof(T)) && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        } 
        #endregion

        #region 上传文件 protected Media Upload(string url, WXAccount account, UploadMediaType type, string path, string name)
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="type">多媒体类型</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>多媒体对象</returns>
        protected Media Upload(string url, WXAccount account, UploadMediaType type, string path, string name)
        {
            string result = HTTPHelper.Upload(String.Format(
                url,
                AccessTokenContainer.GetAccessToken(account).access_token,
                type), path, name);
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<Media>(result);
        } 
        #endregion

        #region 下载文件 protected void Download(string url, WXAccount account, string media_id, string pathName, string postData = null)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">多媒体编号</param>
        /// <param name="pathName">下载路径加文件名</param>
        /// <param name="postData">POST参数（如果该参数不为空则使用POST方式下载）</param>
        protected void Download(string url, WXAccount account, string media_id, string pathName, string postData = null)
        {
            string result = HTTPHelper.DownloadFile(String.Format(
                url,
                AccessTokenContainer.GetAccessToken(account).access_token,
                media_id), pathName, postData);
            if (!String.IsNullOrEmpty(result)) 
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce(), account.ID);
            }
        } 
        #endregion
    }
}
