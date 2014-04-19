using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 二维码控制器
    /// </summary>
    public static class QRCodeController
    {
        #region 创建二维码ticket public static QRCodeTicket GetQRCodeTicket(string weixinMPID, QRCodeTicketRequest qrCodeTicketRequest)
        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="qrCodeTicketRequest">二维码ticket请求</param>
        /// <returns>二维码ticket</returns>
        public static QRCodeTicket GetQRCodeTicket(string weixinMPID, QRCodeTicketRequest qrCodeTicketRequest)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID); 
            string result = HTTPHelper.Post(URLManager.GetURLForCreateQRCodeTicket(weixinMPID), 
                JSONHelper.JSONSerialize(qrCodeTicketRequest));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<QRCodeTicket>(result);
        } 
        #endregion

        #region 通过ticket换取二维码 public static void GetQRCode(QRCodeTicket qrCodeTicket, string pathName)
        /// <summary>
        /// 通过ticket换取二维码
        /// </summary>
        /// <param name="qrCodeTicket">二维码ticket</param>
        /// <param name="pathName">保存路径</param>
        public static void GetQRCode(QRCodeTicket qrCodeTicket, string pathName)
        {
            HTTPHelper.DownloadFile(URLManager.GetURLForGetQRCode(qrCodeTicket), pathName);
        } 
        #endregion
    }
}
