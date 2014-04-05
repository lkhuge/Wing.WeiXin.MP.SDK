using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 微信公共平台服务格式发送改变
    /// </summary>
    public class WXFormationException : WXException
    {
        #region 根据消息实例化 public WXFormationException(string message, BaseException e)
        /// <summary>
        /// 根据消息实例化
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="e">异常</param>
        public WXFormationException(string message, BaseException e)
            : base(String.Format("微信公共平台服务格式发送改变({0})", message), e)
        { } 
        #endregion
    }
}
