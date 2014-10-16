using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.PostageTemp
{
    /// <summary>
    /// 微信小店邮费模板响应
    /// </summary>
    public class WXPostageTempResponse : ErrorMsg
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public int template_id { get; set; }
    }
}