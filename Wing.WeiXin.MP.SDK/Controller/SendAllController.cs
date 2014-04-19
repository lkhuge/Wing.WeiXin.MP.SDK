using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 群发控制器
    /// </summary>
    public static class SendAllController
    {
        #region 上传图文消息素材 public static Media UploadNews(string weixinMPID, SendAllMessageNews news)
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="news">群发图文消息</param>
        /// <returns>多媒体对象</returns>
        public static Media UploadNews(string weixinMPID, SendAllMessageNews news)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID);
            string result = HTTPHelper.Post(URLManager.GetURLForSendAllUploadNews(weixinMPID), JSONHelper.JSONSerialize(news));
            ErrorMsg errMsg = Authentication.CheckHaveErrorMsg(result);
            if (errMsg != null) throw new ErrorMsgException(errMsg);

            return JSONHelper.JSONDeserialize<Media>(result);
        } 
        #endregion

        #region 根据分组进行群发 public static SendAllReturnMessage SendAllByGroup(string weixinMPID, SendAllByGroup group)
        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="group">群发组</param>
        /// <returns>群发回复消息</returns>
        public static SendAllReturnMessage SendAllByGroup(string weixinMPID, SendAllByGroup group)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID);

            return JSONHelper.JSONDeserialize<SendAllReturnMessage>(HTTPHelper.Post(URLManager.GetURLForSendAllByGroup(weixinMPID), JSONHelper.JSONSerialize(group)));
        } 
        #endregion

        #region 根据OpenID列表群发 public static SendAllReturnMessage SendAllByOpenIDList(string weixinMPID, SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="openIDList">OpenID列表</param>
        /// <returns>群发回复消息</returns>
        public static SendAllReturnMessage SendAllByOpenIDList(string weixinMPID, SendAllByOpenIDList openIDList)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID);

            return JSONHelper.JSONDeserialize<SendAllReturnMessage>(HTTPHelper.Post(URLManager.GetURLForSendAllByOpenIDList(weixinMPID), JSONHelper.JSONSerialize(openIDList)));
        }
        #endregion

        #region 删除群发 public static ErrorMsg DeleteSendAll(string weixinMPID, SendAllDelete delete)
        /// <summary>
        /// 删除群发
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="delete">删除群发对象</param>
        /// <returns>返回码对象</returns>
        public static ErrorMsg DeleteSendAll(string weixinMPID, SendAllDelete delete)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (account.WeixinMPType == WeixinMPType.Subscription) throw new OnlyServiceException(weixinMPID);

            return JSONHelper.JSONDeserialize<ErrorMsg>(HTTPHelper.Post(URLManager.GetURLForSendAllDelete(weixinMPID), JSONHelper.JSONSerialize(delete)));
        } 
        #endregion
    }
}
