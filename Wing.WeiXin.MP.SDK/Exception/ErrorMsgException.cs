using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 错误信息异常
    /// </summary>
    public class ErrorMsgException : WXException
    {
        #region 根据错误码对象实例化异常 public ErrorMsgException(ErrorMsg errMsg)
        /// <summary>
        /// 根据错误码对象实例化异常
        /// </summary>
        /// <param name="errMsg">错误码对象</param>
        public ErrorMsgException(ErrorMsg errMsg)
            : base(String.Format("[返回码]：{0}[说明]：{1}", errMsg.errcode, errMsg.errmsg))
        {
        } 
        #endregion
    }
}
