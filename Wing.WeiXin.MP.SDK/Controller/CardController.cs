using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Card;
using Wing.WeiXin.MP.SDK.Entities.Card.Type;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 卡券控制器
    /// </summary>
    public class CardController : WXController
    {
        /// <summary>
        /// 创建卡券的URL
        /// </summary>
        private const string UrlCreateCard = "https://api.weixin.qq.com/card/create?access_token=[AT]";

        /// <summary>
        /// 开通买单的URL
        /// </summary>
        private const string UrlOpenPayCard = "https://api.weixin.qq.com/card/paycell/set?access_token=[AT]";

        #region 根据AccessToken容器初始化
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public CardController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 创建卡券 public CardInfo CreateCard(WXAccount account, CardTypeBaseInfo info)
        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="info">卡券类型信息</param>
        /// <returns>卡券信息</returns>
        public CardInfo CreateCard(WXAccount account, CardTypeBaseInfo info)
        {
            return Action<CardInfo>(UrlCreateCard, new CardTypeInfo
            {
                card = info
            }, account);
        } 
        #endregion

        #region 开通买单 public ErrorMsg PayCard(WXAccount account, string cardID, bool isOpen)
        /// <summary>
        /// 开通买单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="cardID">卡券ID</param>
        /// <param name="isOpen">是否开启买单功能</param>
        /// <returns>卡券信息</returns>
        public ErrorMsg PayCard(WXAccount account, string cardID, bool isOpen)
        {
            return Action<ErrorMsg>(UrlOpenPayCard, new 
            {
                card_id = cardID,
                is_open = isOpen
            }, account);
        }
        #endregion
    }
}
