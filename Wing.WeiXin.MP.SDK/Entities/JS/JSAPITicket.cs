using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.JS
{
    /// <summary>
    /// JS接口票据
    /// jsapi_ticket是公众号用于调用微信JS接口的临时票据
    /// </summary>
    public class JSAPITicket : ErrorMsg
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
