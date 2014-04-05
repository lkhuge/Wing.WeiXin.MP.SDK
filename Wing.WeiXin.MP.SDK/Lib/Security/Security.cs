using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Wing.WeiXin.MP.SDK.Lib.Security
{
    /// <summary>
    /// 安全工具类
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static readonly byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };

        #region DES加密字符串 public static string EncryptDES(string str, string key)
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <returns>加密结果字符串</returns>
        public static string EncryptDES(string str, string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region DES解密字符串 public static string DecryptDES(string str, string key)
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密后字符串</returns>
        public static string DecryptDES(string str, string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(str);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
        #endregion

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
            if (strUpper == null) throw new NullReferenceException("生成MD5错误");
            return strUpper.ToUpper();
        } 
        #endregion
    }
}
