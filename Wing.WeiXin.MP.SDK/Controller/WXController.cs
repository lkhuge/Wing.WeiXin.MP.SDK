using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    public abstract class WXController
    {
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
            return ActionWithoutAccessToken<T>(
                String.Format(url, GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                account);
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
            if (typeof(T) != typeof(ErrorMsg) && JSONHelper.HasKey(result, "errcode"))
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
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                    JSONHelper.JSONSerialize(messageObj));
            if (typeof(T) != typeof(ErrorMsg) && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        } 
        #endregion
    }
}
