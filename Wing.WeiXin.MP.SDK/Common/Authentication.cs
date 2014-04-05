using System;
using System.Linq;
using Wing.WeiXin.MP.SDK;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.HTTP;
using Wing.WeiXin.MP.SDK.Lib.Security;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 验证工具类
    /// </summary>
    public static class Authentication
    {
        #region 验证signature是否有效 public static bool CheckSignature(Request request)
        /// <summary>
        /// 验证signature是否有效
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否有效</returns>
        public static bool CheckSignature(Request request)
        {
            string[] arr = new[] 
            { 
                ConfigManager.GetToken(), 
                request.timestamp, 
                request.nonce
            }.OrderBy(z => z).ToArray();

            return Security.SHA1_Encrypt(string.Join("", arr)).Equals(request.signature);
        }
        #endregion

        #region 验证消息真实性 public static bool CheckMessage(Request request)
        /// <summary>
        /// 验证消息真实性
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否有效</returns>
        public static bool CheckMessage(Request request)
        {
            return CheckSignature(request);
        } 
        #endregion

        #region 检测是否存在错误信息并输出 public static ErrorMsg CheckHaveErrorMsg(string jsonValue)
        /// <summary>
        /// 检测是否存在错误信息并输出
        /// </summary>
        /// <param name="jsonValue">Json信息</param>
        /// <returns>错误码</returns>
        public static ErrorMsg CheckHaveErrorMsg(string jsonValue)
        {
            if (jsonValue.IndexOf("{\"errcode", StringComparison.Ordinal) != 0) return null;
            try
            {
                return JSONHelper.JSONDeserialize<ErrorMsg>(jsonValue);
            }
            catch (BaseException e)
            {
                throw new WXFormationException(jsonValue, e);
            }
        }
        #endregion
    }
}
