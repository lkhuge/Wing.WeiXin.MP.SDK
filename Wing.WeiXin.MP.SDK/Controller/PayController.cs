using Wing.WeiXin.MP.SDK.Common;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public class PayController : WXController
    {
        #region 根据AccessToken容器初始化
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public PayController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion
    }
}
