using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 根据Code获取AccessToken失败
    /// </summary>
    public class FailGetGetAccessTokenByCode : WXException
    {
        #region 根据失败原因实例化 public FailGetGetAccessTokenByCode(string result)
        /// <summary>
        /// 根据失败原因实例化
        /// </summary>
        /// <param name="result">失败原因</param>
        public FailGetGetAccessTokenByCode(string result)
            : base(String.Format("根据Code获取AccessToken失败(原因:{0})", result)) { }  
        #endregion
    }
}
