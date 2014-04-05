using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.QRCode
{
    /// <summary>
    /// 场景
    /// </summary>
    public class QRCodeTicketRequestScene
    {
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int scene_id { get; set; }
    }
}
