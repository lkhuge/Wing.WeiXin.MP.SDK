using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.DKF;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 模板消息控制器
    /// </summary>
    public class TemplateController : WXController
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
            return Action<ReturnMessage>(UrlSendMessageTemplate, messageTemplate, account);
        } 
        #endregion
    }
}
