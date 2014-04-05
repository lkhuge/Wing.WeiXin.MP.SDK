using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 无响应消息异常
    /// </summary>
    public class NoResponseException : WXException
    {
        #region 根据无响应消息原因实例化异常 public NoResponseException(string result)
        /// <summary>
        /// 根据无响应消息原因实例化异常
        /// </summary>
        /// <param name="result">无响应消息原因</param>
        public NoResponseException(string result)
            : base(String.Format("无响应消息[原因]：{0}", result))
        {
        } 
        #endregion
    }
}
