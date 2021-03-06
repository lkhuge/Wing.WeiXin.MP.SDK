﻿using Newtonsoft.Json;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.QRCode
{
    /// <summary>
    /// 二维码ticket
    /// </summary>
    public class QRCodeTicket : IEntity
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 二维码的有效时间，以秒为单位。最大不超过1800。
        /// </summary>
        public int expire_seconds { get; set; }
    }
}
