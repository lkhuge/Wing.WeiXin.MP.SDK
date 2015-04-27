﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.QRCode
{
    /// <summary>
    /// 临时二维码ticket请求
    /// </summary>
    public class QRCodeTicketRequest
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
        public ActionInfo action_info { get; set; }

        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public class ActionInfo
        {
            /// <summary>
            /// 场景
            /// </summary>
            public Scene scene { get; set; }

            /// <summary>
            /// 场景
            /// </summary>
            public class Scene
            {
                /// <summary>
                /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
                /// </summary>
                public int scene_id { get; set; }

                /// <summary>
                /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段
                /// </summary>
                public string scene_str { get; set; }
            }
        }
    }
}
