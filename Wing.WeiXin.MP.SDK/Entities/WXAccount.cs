using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 微信账号
    /// </summary>
    public class WXAccount
    {
        /// <summary>
        /// 微信公共平台ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 是否需要加密
        /// </summary>
        public bool NeedEncoding { get; set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }

        #region 获取账户信息 public override string ToString()
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns>账户信息</returns>
        public override string ToString()
        {
            return String.Format("ID:{1}{0}{0}{0}AppID:{2}{0}AppSecret:{3}",
                Environment.NewLine,
                ID,
                AppID,
                AppSecret);
        } 
        #endregion
    }
}
