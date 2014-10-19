using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Enumeration
{
    /// <summary>
    /// 微信加解密工具错误返回码
    /// </summary>
    public enum WXBizMsgCryptErrorCode
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        WXBizMsgCrypt_OK = 0,

        /// <summary>
        /// 校验签名失败
        /// </summary>
        WXBizMsgCrypt_ValidateSignature_Error = -40001,

        /// <summary>
        /// 解析xml失败
        /// </summary>
        WXBizMsgCrypt_ParseXml_Error = -40002,

        /// <summary>
        /// 计算签名失败
        /// </summary>
        WXBizMsgCrypt_ComputeSignature_Error = -40003,

        /// <summary>
        /// 不合法的AESKey
        /// </summary>
        WXBizMsgCrypt_IllegalAesKey = -40004,

        /// <summary>
        /// 校验AppID失败
        /// </summary>
        WXBizMsgCrypt_ValidateAppid_Error = -40005,

        /// <summary>
        /// AES加密失败
        /// </summary>
        WXBizMsgCrypt_EncryptAES_Error = -40006,

        /// <summary>
        /// AES解密失败
        /// </summary>
        WXBizMsgCrypt_DecryptAES_Error = -40007,

        /// <summary>
        /// 公众平台发送的xml不合法
        /// </summary>
        WXBizMsgCrypt_IllegalBuffer = -40008,

        /// <summary>
        /// Base64编码失败
        /// </summary>
        WXBizMsgCrypt_EncodeBase64_Error = -40009,

        /// <summary>
        /// Base64解码失败
        /// </summary>
        WXBizMsgCrypt_DecodeBase64_Error = -40010
    };
}
