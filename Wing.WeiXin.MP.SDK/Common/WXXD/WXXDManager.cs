using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店管理类
    /// </summary>
    public abstract class WXXDManager
    {
        #region 获取AccessToken protected String GetAccessToken()
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns>AccessToken</returns>
        protected AccessToken GetAccessToken()
        {
            return GlobalManager.AccessTokenContainer.GetAccessToken(
                GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountFirst(WeixinMPType.Service));
        } 
        #endregion

        #region 获取带有AccessToken的URL protected String GetUrlByAccessToken(string url)
        /// <summary>
        /// 获取带有AccessToken的URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>带有AccessToken的URL</returns>
        protected String GetUrlByAccessToken(string url)
        {
            return url.Replace("{AccessToken}", GetAccessToken().access_token);
        } 
        #endregion

        #region 获取接口数据（POST） protected T GetData<T>(String url, Object obj)
        /// <summary>
        /// 获取接口数据（POST）
        /// </summary>
        /// <typeparam name="T">响应数据类型</typeparam>
        /// <param name="url">接口数据URL</param>
        /// <param name="obj">请求数据</param>
        /// <returns>响应数据</returns>
        protected T GetData<T>(String url, Object obj)
        {
            return JSONHelper.JSONDeserialize<T>(HTTPHelper.Post(
                GetUrlByAccessToken(url),
                JSONHelper.JSONSerialize(obj)));
        } 
        #endregion

        #region 获取接口数据（GET） protected T GetData<T>(String url)
        /// <summary>
        /// 获取接口数据（GET）
        /// </summary>
        /// <typeparam name="T">响应数据类型</typeparam>
        /// <param name="url">接口数据URL</param>
        /// <returns>响应数据</returns>
        protected T GetData<T>(String url)
        {
            return JSONHelper.JSONDeserialize<T>(HTTPHelper.Get(
                GetUrlByAccessToken(url)));
        }
        #endregion

        #region 上传文件（POST） protected T Upload<T>(String url, String path, String filename)
        /// <summary>
        /// 上传文件（POST）
        /// </summary>
        /// <typeparam name="T">响应数据类型</typeparam>
        /// <param name="url">接口数据URL</param>
        /// <param name="path">文件路径</param>
        /// <param name="filename">文件名</param>
        /// <returns>响应数据</returns>
        protected T Upload<T>(String url, String path, String filename)
        {
            return JSONHelper.JSONDeserialize<T>(HTTPHelper.Upload(
                GetUrlByAccessToken(url), path, filename));
        }
        #endregion
    }
}