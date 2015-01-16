using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// 安全工具类
    /// </summary>
    public static class SecurityHelper
    {
        #region 使用SHA1加密 public static string SHA1_Encrypt(string str)
        /// <summary>
        /// 使用SHA1加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后字符串</returns>
        public static string SHA1_Encrypt(string str)
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

        #region 使用MD5加密 public static string MD5(string str)
        /// <summary>
        /// 使用MD5加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5(string str)
        {
            string strUpper = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            if (strUpper == null) throw WXException.GetInstance("生成MD5错误", Settings.Default.SystemUsername);
            return strUpper.ToUpper();
        }
        #endregion
    }
}
