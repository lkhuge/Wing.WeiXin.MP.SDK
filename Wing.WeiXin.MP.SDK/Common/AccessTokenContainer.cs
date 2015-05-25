using System;
using System.Linq;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// AccessToken容器
    /// </summary>
    public class AccessTokenContainer
    {
        /// <summary>
        /// 获取AccessToken的URL
        /// </summary>
        private const string Url =
            "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 获取新的AccessToken事件
        /// </summary>
        public event Action<WXAccount> NewAccessToken;

        /// <summary>
        /// 微信会话接口
        /// </summary>
        private readonly IWXSession wxSession;

        /// <summary>
        /// 需要刷新AccessToken的错误信息
        /// </summary>
        private readonly string[] reflushAccessTokenCode =
        {
            "40001"
        };

        #region 根据微信会话接口实例化 public AccessTokenContainer(IWXSession wxSession)
        /// <summary>
        /// 根据微信会话接口实例化
        /// </summary>
        /// <param name="wxSession">微信会话接口</param>
        public AccessTokenContainer(IWXSession wxSession)
        {
            if (wxSession == null) throw WXException.GetInstance("微信会话接口不能为空", Settings.Default.SystemUsername);
            this.wxSession = wxSession;
        }
        #endregion

        #region 获取AccessToken private AccessToken GetAccessToken(WXAccount account)
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        private AccessToken GetAccessToken(WXAccount account)
        {
            LogManager.WriteSystem("获取AccessToken-开始");
            AccessToken accessToken = wxSession.Get<AccessToken>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID);
            DateTime accessTokenExpDT = wxSession.Get<DateTime>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID);
            if (accessToken != null && accessTokenExpDT != default(DateTime)
                && DateTime.Now < accessTokenExpDT)
            {
                LogManager.WriteSystem("获取AccessToken-缓存-结束" + Environment.NewLine + accessToken);
                return accessToken;
            }
            AccessToken result = GetNewAccessToken(account);
            LogManager.WriteSystem("获取AccessToken-新-结束" + Environment.NewLine + result);
            return result;
        }
        #endregion

        #region 使用AccessToken public string UseAccessToken(Func<string, string> action, WXAccount account)
        /// <summary>
        /// 使用AccessToken
        /// </summary>
        /// <param name="action">执行操作(result Action(accessToken))</param>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>结果</returns>
        public string UseAccessToken(Func<string, string> action, WXAccount account)
        {
            string result = action(GetAccessToken(account).access_token);
            ErrorMsg errorMsg = JSONHelper.JSONDeserialize<ErrorMsg>(result);
            if (!String.IsNullOrEmpty(errorMsg.errcode) && reflushAccessTokenCode.Contains(errorMsg.errcode))
            {
                LogManager.WriteSystem("发现无效AccessToken 正在重新获取AccessToken");
                return action(GetNewAccessToken(account).access_token);
            }
            return result;
        }
        #endregion

        #region 获取新的AccessToken private AccessToken GetNewAccessToken(WXAccount account)
        /// <summary>
        /// 获取新的AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        private AccessToken GetNewAccessToken(WXAccount account)
        {
            if (NewAccessToken != null) NewAccessToken(account);
            string result = HTTPHelper.Get(String.Format(Url, account.AppID, account.AppSecret));
            ErrorMsg errorMsg = JSONHelper.JSONDeserialize<ErrorMsg>(result);
            if (!String.IsNullOrEmpty(errorMsg.errcode))
            {
                throw WXException.GetInstance(errorMsg, account.ID);
            }
            AccessToken accessTokenNew = JSONHelper.JSONDeserialize<AccessToken>(result);
            wxSession.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID,
                accessTokenNew);
            wxSession.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID,
                DateTime.Now + new TimeSpan(0, 0, accessTokenNew.expires_in));

            return accessTokenNew;
        }
        #endregion
    }
}
