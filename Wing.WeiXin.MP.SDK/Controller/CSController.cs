using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.CSMessages;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 客服控制器
    /// </summary>
    public static class CSController
    {
        #region 发送客服信息 public static ErrorMsg SendCSMessage(string weixinMPID, CSMessage csmessage)
        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="csmessage">客服信息</param>
        /// <returns>错误码</returns>
        public static ErrorMsg SendCSMessage(string weixinMPID, CSMessage csmessage)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID); 
            return JSONHelper.JSONDeserialize<ErrorMsg>(
                HTTPHelper.Post(URLManager.GetURLForSendCSMessage(weixinMPID), JSONHelper.JSONSerialize(csmessage)));
        } 
        #endregion
    }
}
