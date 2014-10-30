using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// URL控制器
    /// </summary>
    public class URLController
    {
        /// <summary>
        /// 获取短域名的URL
        /// </summary>
        private const string UrlGetShortURL = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}";

        #region 获取短域名 public string GetShortURL(WXAccount account, string url)
        /// <summary>
        /// 获取短域名
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="url">域名</param>
        /// <returns>短域名</returns>
        public string GetShortURL(WXAccount account, string url)
        {
            string result = HTTPHelper.Post(
                String.Format(UrlGetShortURL, GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), 
                JSONHelper.JSONSerialize(new
                {
                    action = "long2short",
                    long_url = url
                }));
            JProperty jp = JObject.Parse(result).Property("short_url");
            if (jp != null)
            {
                return jp.Value.ToString();
            }
            throw MessageException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
        } 
        #endregion
    }
}
