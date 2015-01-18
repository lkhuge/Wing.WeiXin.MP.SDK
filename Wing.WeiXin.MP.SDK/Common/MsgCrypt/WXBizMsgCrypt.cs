using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Security.Cryptography;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Common.MsgCrypt
{
    /// <summary>
    /// 微信加解密工具类
    /// </summary>
    internal class WXBizMsgCrypt
    {
        /// <summary>
        /// 公众平台上，开发者设置的Token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 公众平台上，开发者设置的EncodingAESKey
        /// </summary>
        public string encodingAESKey { get; set; }

        /// <summary>
        /// 公众帐号的appid
        /// </summary>
        public string appID { get; set; }

        #region 检验消息的真实性，并且获取解密后的明文 public WXBizMsgCryptErrorCode DecryptMsg(string signature, string timeStamp, string nonce, string postData, ref string msg)
        /// <summary>
        /// 检验消息的真实性，并且获取解密后的明文
        /// </summary>
        /// <param name="signature">签名串，对应URL参数的msg_signature</param>
        /// <param name="timeStamp">时间戳，对应URL参数的timestamp</param>
        /// <param name="nonce">随机串，对应URL参数的nonce</param>
        /// <param name="postData">密文，对应POST请求的数据</param>
        /// <param name="msg">解密后的原文，当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public WXBizMsgCryptErrorCode DecryptMsg(string signature, string timeStamp, string nonce, string postData, ref string msg)
        {
            if (encodingAESKey.Length != 43) return WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            XmlDocument doc = new XmlDocument();
            string sEncryptMsg;
            try
            {
                doc.LoadXml(postData);
                XmlNode root = doc.FirstChild;
                XmlNode en = root["Encrypt"];
                if (en == null) return WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
                sEncryptMsg = en.InnerText;
            }
            catch (Exception)
            {
                return WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
            }
            WXBizMsgCryptErrorCode ret = VerifySignature(timeStamp, nonce, sEncryptMsg, signature);
            if (ret != WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK) return ret;
            string cpid = "";
            try
            {
                msg = Cryptography.AES_decrypt(sEncryptMsg, encodingAESKey, ref cpid);
            }
            catch (FormatException)
            {
                return WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                return WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }

            return cpid != appID
                ? WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateAppid_Error
                : WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK;
        } 
        #endregion

        #region 将企业号回复用户的消息加密打包 public WXBizMsgCryptErrorCode EncryptMsg(string replyMsg, string timeStamp, string nonce, ref string encryptMsg)
        /// <summary>
        /// 将企业号回复用户的消息加密打包
        /// </summary>
        /// <param name="replyMsg">企业号待回复用户的消息，xml格式的字符串</param>
        /// <param name="timeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="encryptMsg">加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public WXBizMsgCryptErrorCode EncryptMsg(string replyMsg, string timeStamp, string nonce, ref string encryptMsg)
        {
            if (encodingAESKey.Length != 43) return WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            string raw;
            try
            {
                raw = Cryptography.AES_encrypt(replyMsg, encodingAESKey, appID);
            }
            catch (Exception)
            {
                return WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
            }
            string MsgSigature = "";
            WXBizMsgCryptErrorCode ret = GenarateSinature(timeStamp, nonce, raw, ref MsgSigature);
            if (WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK != ret) return ret;
            encryptMsg = String.Format("<xml><Encrypt><![CDATA[{0}]]></Encrypt><MsgSignature><![CDATA[{1}]]></MsgSignature><TimeStamp><![CDATA[{2}]]></TimeStamp><Nonce><![CDATA[{3}]]></Nonce></xml>",
                raw,
                MsgSigature,
                timeStamp,
                nonce);

            return WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK;
        } 
        #endregion

        #region 验证签名 private WXBizMsgCryptErrorCode VerifySignature(string timeStamp, string nonce, string msgEncrypt, string sigture)
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="nonce">随机串</param>
        /// <param name="msgEncrypt">密文</param>
        /// <param name="sigture">签名</param>
        /// <returns>验证结果</returns>
        private WXBizMsgCryptErrorCode VerifySignature(string timeStamp, string nonce, string msgEncrypt, string sigture)
        {
            string hash = "";
            WXBizMsgCryptErrorCode ret = GenarateSinature(timeStamp, nonce, msgEncrypt, ref hash);
            if (ret != 0) return ret;
            if (hash == sigture) return WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK;

            return WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateSignature_Error;
        } 
        #endregion

        #region 生成签名 private WXBizMsgCryptErrorCode GenarateSinature(string timeStamp, string nonce, string msgEncrypt, ref string msgSignature)
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="nonce">随机串</param>
        /// <param name="msgEncrypt">密文</param>
        /// <param name="msgSignature">签名</param>
        /// <returns>生成结果</returns>
        private WXBizMsgCryptErrorCode GenarateSinature(string timeStamp, string nonce, string msgEncrypt, ref string msgSignature)
        {
            ArrayList AL = new ArrayList { token, timeStamp, nonce, msgEncrypt };
            AL.Sort(new DictionarySort());
            string raw = AL.Cast<string>().Aggregate("", (current, t) => current + t);
            string hash;
            try
            {
                SHA1 sha = new SHA1CryptoServiceProvider();
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return WXBizMsgCryptErrorCode.WXBizMsgCrypt_ComputeSignature_Error;
            }
            msgSignature = hash;

            return WXBizMsgCryptErrorCode.WXBizMsgCrypt_OK;
        } 
        #endregion

        /// <summary>
        /// 字典排序
        /// </summary>
        public class DictionarySort : IComparer
        {
            /// <summary>
            /// 排序
            /// </summary>
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft.ToString();
                string sRight = oRight.ToString();
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    if (sLeft[index] > sRight[index])
                        return 1;
                    index++;
                }
                return iLeftLength - iRightLength;

            }
        }
    }
}
