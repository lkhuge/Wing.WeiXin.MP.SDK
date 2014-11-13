using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 二维码控制器
    /// </summary>
    public class QRCodeController
    {
        /// <summary>
        /// 创建二维码ticket的URL
        /// </summary>
        private const string UrlGetQRCodeTicket = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

        /// <summary>
        /// 通过ticket换取二维码的URL
        /// </summary>
        private const string UrlGetQRCode = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";

        #region 创建二维码ticket public QRCodeTicket GetQRCodeTicket(WXAccount account, QRCodeTicketRequest qrCodeTicketRequest)
        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="qrCodeTicketRequest">二维码ticket请求</param>
        /// <returns>二维码ticket</returns>
        public QRCodeTicket GetQRCodeTicket(WXAccount account, QRCodeTicketRequest qrCodeTicketRequest)
        {
            account.CheckIsService();
            string result = HTTPHelper.Post(
                String.Format(
                    UrlGetQRCodeTicket,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), 
                JSONHelper.JSONSerialize(qrCodeTicketRequest));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<QRCodeTicket>(result);
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
    }
}
