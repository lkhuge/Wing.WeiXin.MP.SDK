using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// 默认安全工具类
    /// </summary>
    public class DefaultSecurityHelper : ISecurityHelper
    {
        #region 使用SHA1加密 public string SHA1_Encrypt(string str)
        /// <summary>
        /// 使用SHA1加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后字符串</returns>
        public string SHA1_Encrypt(string str)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in sha1Arr)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
        #endregion
    }
}
