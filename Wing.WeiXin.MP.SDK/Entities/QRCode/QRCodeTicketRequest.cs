using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.Interface;

namespace Wing.WeiXin.MP.SDK.Entities.QRCode
{
    /// <summary>
    /// 临时二维码ticket请求
    /// </summary>
    public class QRCodeTicketRequest : IEntity
    {
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过1800。
        /// </summary>
        public int expire_seconds { get; set; }

        /// <summary>
        /// 二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久
        /// </summary>
        public string action_name { get; set; }

        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public QRCodeTicketRequestActionInfo action_info { get; set; }
    }
}
