﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 返回信息
    /// </summary>
    public class ReturnMessage : ErrorMsg
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        public long msg_id { get; set; }

        /// <summary>
        /// 消息的数据ID，该字段只有在群发图文消息时，才会出现。
        /// 可以用于在图文分析数据接口中，获取到对应的图文消息的数据，
        /// 是图文分析数据接口中的msgid字段中的前半部分，
        /// 详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>
        public long msg_data_id { get; set; }
    }
}
