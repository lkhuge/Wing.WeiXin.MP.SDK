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
        #region 执行请求 响应类型为T的对象 protected T Action<T>(string url, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        /// <summary>
        /// 执行请求 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信账号</param>
        /// <param name="isNeedCheckService">是否需要检测必须为服务号</param>
        /// <param name="IsNeedCheckErrorMsg">是否需要检测是否返回错误信息</param>
        /// <returns>类型为T的对象</returns>
        protected T Action<T>(string url, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        {
            return ActionWithoutAccessToken<T>(
                String.Format(url, GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                account,
                isNeedCheckService,
                IsNeedCheckErrorMsg);
        } 
        #endregion

        #region 执行请求（无需AccessToken） 响应类型为T的对象 protected T ActionWithoutAccessToken<T>(string url, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        /// <summary>
        /// 执行请求（无需AccessToken） 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="account">微信账号</param>
        /// <param name="isNeedCheckService">是否需要检测必须为服务号</param>
        /// <param name="IsNeedCheckErrorMsg">是否需要检测是否返回错误信息</param>
        /// <returns>类型为T的对象</returns>
        protected T ActionWithoutAccessToken<T>(string url, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        {
            if (isNeedCheckService) account.CheckIsService();
            string result = HTTPHelper.Get(url);
            if (IsNeedCheckErrorMsg && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        }
        #endregion

        #region 执行带有消息对象的请求 响应类型为T的对象 protected T Action<T>(string url, object messageObj, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        /// <summary>
        /// 执行带有消息对象的请求 响应类型为T的对象
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="url">请求接口地址</param>
        /// <param name="messageObj">消息对象</param>
        /// <param name="account">微信账号</param>
        /// <param name="isNeedCheckService">是否需要检测必须为服务号</param>
        /// <param name="IsNeedCheckErrorMsg">是否需要检测是否返回错误信息</param>
        /// <returns>类型为T的对象</returns>
        protected T Action<T>(string url, object messageObj, WXAccount account, bool isNeedCheckService = false, bool IsNeedCheckErrorMsg = true)
        {
            if (isNeedCheckService) account.CheckIsService();
            string result = HTTPHelper.Post(String.Format(
                    url,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                    JSONHelper.JSONSerialize(messageObj));
            if (IsNeedCheckErrorMsg && JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return JSONHelper.JSONDeserialize<T>(result);
        } 
        #endregion
    }
}
