using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.JS
{
    /// <summary>
    /// JS微信配置对象
    /// </summary>
    public class JSWeixinConfig
    {
        /// <summary>
        /// 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，
        /// 可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        /// </summary>
        public bool debug { get; set; }

        /// <summary>
        /// [必填]公众号的唯一标识
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// [必填]生成签名的时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// [必填]生成签名的随机串
        /// </summary>
        public string nonceStr { get; set; }

        /// <summary>
        /// [必填]签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// [必填]需要使用的JS接口列表
        /// </summary>
        public string[] jsApiList { get; set; }
    }
}
