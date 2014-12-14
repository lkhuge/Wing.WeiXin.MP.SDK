using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Semantic;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 语义控制器
    /// </summary>
    public class SemanticController
    {
        /// <summary>
        /// 获取语义的URL
        /// </summary>
        private const string Url = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";

        #region 获取语义 public T GetSemantic<T>(WXAccount account, SemanticRequest request) where T : SemanticResponse
        /// <summary>
        /// 获取语义
        /// </summary>
        /// <typeparam name="T">语义响应类型</typeparam>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="request">语义请求</param>
        /// <returns>语义</returns>
        public T GetSemantic<T>(WXAccount account, SemanticRequest request) where T : SemanticResponse
        {
            request.appid = account.AppID;
            string result = LibManager.HTTPHelper.Post(String.Format(
                    Url,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                    LibManager.JSONHelper.JSONSerialize(request));

            return LibManager.JSONHelper.JSONDeserialize<T>(result);
        } 
        #endregion
    }
}
