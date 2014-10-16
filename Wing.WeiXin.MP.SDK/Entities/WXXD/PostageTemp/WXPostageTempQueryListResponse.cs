using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.PostageTemp
{
    /// <summary>
    /// 查询邮费模板列表响应
    /// </summary>
    public class WXPostageTempQueryListResponse : ErrorMsg
    {
        /// <summary>
        /// 所有邮费模板集合(字段说明详见增加邮费模板)
        /// </summary>
        public List<WXPostageTemp> templates_info { get; set; }
    }
}