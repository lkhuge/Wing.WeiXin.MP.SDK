using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.PostageTemp
{
    /// <summary>
    /// 查询邮费模板响应
    /// </summary>
    public class WXPostageTempQueryResponse : ErrorMsg
    {
        /// <summary>
        /// 邮费模板信息(字段说明详见增加邮费模板)
        /// </summary>
        public WXPostageTemp template_info { get; set; }
    }
}