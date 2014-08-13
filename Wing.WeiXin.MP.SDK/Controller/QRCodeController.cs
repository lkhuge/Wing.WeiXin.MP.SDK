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
            string result = HTTPHelper.Post(URLManager.GetURLForCreateQRCodeTicket(account), 
                JSONHelper.JSONSerialize(qrCodeTicketRequest));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<QRCodeTicket>(result);
        } 
        #endregion

        #region 通过ticket换取二维码 public void GetQRCode(QRCodeTicket qrCodeTicket, string pathName)
        /// <summary>
        /// 通过ticket换取二维码
        /// </summary>
        /// <param name="qrCodeTicket">二维码ticket</param>
        /// <param name="pathName">保存路径</param>
        public void GetQRCode(QRCodeTicket qrCodeTicket, string pathName)
        {
            HTTPHelper.DownloadFile(URLManager.GetURLForGetQRCode(qrCodeTicket), pathName);
        } 
        #endregion
    }
}
