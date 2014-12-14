using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// 安全工具类接口
    /// </summary>
    public interface ISecurityHelper
    {
        /// <summary>
        /// 使用SHA1加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后字符串</returns>
        string SHA1_Encrypt(string str);
    }
}
