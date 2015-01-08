using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.CSMessages;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 客服控制器
    /// </summary>
    public class CSController : WXController
    {
        /// <summary>
        /// 发送客服消息的URL
        /// </summary>
        private const string Url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

        #region 发送客服信息 public ErrorMsg SendCSMessage(WXAccount account, CSMessage csmessage)
        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="csmessage">客服信息</param>
        /// <returns>错误码</returns>
        public ErrorMsg SendCSMessage(WXAccount account, CSMessage csmessage)
        {
            return Action<ErrorMsg>(Url, csmessage, account, true, false);
        } 
        #endregion
    }
}
