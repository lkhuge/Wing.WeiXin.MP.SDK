using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 获取菜单失败异常
    /// </summary>
    public class FailGetMenuException : WXException
    {
        #region 根据失败原因实例化 public FailGetMenuException(string result)
        /// <summary>
        /// 根据失败原因实例化
        /// </summary>
        /// <param name="result">失败原因</param>
        public FailGetMenuException(string result)
            : base(String.Format("获取菜单失败(原因:{0})", result)) { } 
        #endregion
    }
}
