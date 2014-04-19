using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 获取账号异常
    /// </summary>
    public class FailGetAccountException : WXException
    {
        #region 根据微信公共账号ID实例化 public FailGetAccount(string weixinMPDI)
        /// <summary>
        /// 根据微信公共账号ID实例化
        /// </summary>
        /// <param name="weixinMPDI">微信公共账号ID</param>
        public FailGetAccountException(string weixinMPDI)
            : base(String.Format("获取账号（{0}）发生异常", weixinMPDI))
        { } 
        #endregion
    }
}
