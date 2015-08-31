using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Semantic;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 语义控制器
    /// </summary>
    public class SemanticController : WXController
    {
        /// <summary>
        /// 获取语义的URL
        /// </summary>
        private const string Url = "https://api.weixin.qq.com/semantic/semproxy/search?access_token=[AT]";

        #region 根据AccessToken容器初始化 public SemanticController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public SemanticController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

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
            return Action<T>(Url, request, account);
        } 
        #endregion
    }
}
