﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.CL.StringManager;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.DKF;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 模板消息控制器
    /// </summary>
    public class TemplateController
    {
        /// <summary>
        /// 发送消息模板的URL
        /// </summary>
        private const string UrlSendMessageTemplate = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        #region 发送消息模板 public ReturnMessage SendMessageTemplate(WXAccount account, MessageTemplate messageTemplate)
        /// <summary>
        /// 发送消息模板
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="messageTemplate">消息模板</param>
        /// <returns></returns>
        public ReturnMessage SendMessageTemplate(WXAccount account, MessageTemplate messageTemplate)
        {
            account.CheckIsService();
            string result = HTTPHelper.Post(String.Format(
                    UrlSendMessageTemplate,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                    JSONHelper.JSONSerialize(messageTemplate));

            return JSONHelper.JSONDeserialize<ReturnMessage>(result);
        } 
        #endregion
    }
}