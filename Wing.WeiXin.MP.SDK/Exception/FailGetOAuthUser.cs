using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 获取获取认证用户信息失败
    /// </summary>
    public class FailGetOAuthUser : WXException
    {
        #region 根据失败原因实例化 public FailGetOAuthUser(string result)
        /// <summary>
        /// 根据失败原因实例化
        /// </summary>
        /// <param name="result">失败原因</param>
        public FailGetOAuthUser(string result)
            : base(String.Format("获取获取认证用户信息失败失败(原因:{0})", result)) { }  
        #endregion
    }
}
