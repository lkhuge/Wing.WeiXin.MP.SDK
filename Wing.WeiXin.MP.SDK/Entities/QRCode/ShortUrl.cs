using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.QRCode
{
    /// <summary>
    /// 短链接
    /// 主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，
    /// 将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
    /// </summary>
    public class ShortUrl : ErrorMsg
    {
        /// <summary>
        /// 短链接
        /// </summary>
        public string short_url { get; set; }
    }
}
