using System;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 二维码控制器
    /// </summary>
    public class QRCodeController : WXController
    {
        /// <summary>
        /// 创建二维码ticket的URL
        /// </summary>
        private const string UrlGetQRCodeTicket = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

        /// <summary>
        /// 通过ticket换取二维码的URL
        /// </summary>
        private const string UrlGetQRCode = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";

        /// <summary>
        /// 获取短域名的URL
        /// </summary>
        private const string UrlGetShortURL = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}";

        #region 创建二维码ticket public QRCodeTicket GetQRCodeTicket(WXAccount account, QRCodeTicketRequest qrCodeTicketRequest)
        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="qrCodeTicketRequest">二维码ticket请求</param>
        /// <returns>二维码ticket</returns>
        public QRCodeTicket GetQRCodeTicket(WXAccount account, QRCodeTicketRequest qrCodeTicketRequest)
        {
            return Action<QRCodeTicket>(UrlGetQRCodeTicket, qrCodeTicketRequest, account);
        } 
        #endregion

        #region 通过ticket换取二维码 public void GetQRCode(string ticket, string pathName)
        /// <summary>
        /// 通过ticket换取二维码
        /// </summary>
        /// <param name="ticket">二维码ticket</param>
        /// <param name="pathName">保存路径</param>
        public void GetQRCode(string ticket, string pathName)
        {
            HTTPHelper.DownloadFile(String.Format(
                UrlGetQRCode,
                HttpUtility.UrlEncode(ticket)), pathName);
        } 
        #endregion

        #region 获取短域名 public string GetShortURL(WXAccount account, string url)
        /// <summary>
        /// 获取短域名
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="url">域名</param>
        /// <returns>短域名</returns>
        public string GetShortURL(WXAccount account, string url)
        {
            return Action<ShortUrl>(
                UrlGetShortURL,
                new { action = "long2short", long_url = url },
                account).short_url;
        }
        #endregion
    }
}
