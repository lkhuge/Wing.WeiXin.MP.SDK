using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 非法消息异常
    /// </summary>
    public class InvalidMessageException : WXException
    {
        /// <summary>
        /// 请求对象
        /// </summary>
        public Request request { get; private set; }

        #region 根据请求对象实例化 public InvalidMessageException(Request request)
        /// <summary>
        /// 根据请求对象实例化
        /// </summary>
        /// <param name="request">请求对象</param>
        public InvalidMessageException(Request request)
            : base(GetErrMsg(request))
        {
            this.request = request;
        } 
        #endregion

        #region 获取错误信息 private static string GetErrMsg(Request requestObj)
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        private static string GetErrMsg(Request requestObj)
        {
            const string ErrMsg = "非法消息异常（Request:{0}）";
            string requestStr = requestObj == null ? "为空" : String.Format(
                "[signature]:{0}[timestamp]:{1}[nonce]:{2}", 
                requestObj.signature, requestObj.timestamp, requestObj.nonce);

            return String.Format(ErrMsg, requestStr);
        }
        #endregion
    }
}
