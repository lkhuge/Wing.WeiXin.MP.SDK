using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 只有服务号可以使用的功能发出异常
    /// </summary>
    public class OnlyServiceException : WXException
    {
        #region 根据微信公共账号ID实例化 public OnlyServiceException(string weixinMPID)
        /// <summary>
        /// 根据微信公共账号ID实例化
        /// </summary>
        /// <param name="weixinMPID">微信公共账号ID</param>
        public OnlyServiceException(string weixinMPID)
            : base(String.Format("该账号（{0}）为订阅号， 无法使用服务号的功能", weixinMPID))
        {
        } 
        #endregion
    }
}
