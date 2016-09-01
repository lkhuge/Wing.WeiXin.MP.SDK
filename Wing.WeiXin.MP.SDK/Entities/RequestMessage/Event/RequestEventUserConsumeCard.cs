using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event
{
    /// <summary>
    /// 买单通知事件请求
    /// </summary>
    public class RequestEventUserConsumeCard : RequestAMessage
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId
        {
            get { return GetPostData("CardId"); }
        }

        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string UserCardCode
        {
            get { return GetPostData("UserCardCode"); }
        }

        /// <summary>
        /// 核销来源
        /// 支持开发者统计
        ///     API核销（FROM_API）
        ///     公众平台核销（FROM_MP）
        ///     卡券商户助手核销（FROM_MOBILE_HELPER）（核销员微信号）
        /// </summary>
        public string ConsumeSource
        {
            get { return GetPostData("ConsumeSource"); }
        }

        /// <summary>
        /// 微信支付交易订单号
        /// </summary>
        public string TransId
        {
            get { return GetPostData("TransId"); }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo
        {
            get { return GetPostData("OutTradeNo"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType {
            get { return ReceiveEntityType.user_consume_card; } 
        }
    }
}
