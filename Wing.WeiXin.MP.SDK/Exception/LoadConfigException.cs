using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 加载配置异常
    /// </summary>
    public class LoadConfigException : WXException
    {
        #region 根据加载配置异常实例化异常 public LoadConfigException(BaseException e)
        /// <summary>
        /// 根据加载配置异常实例化异常
        /// </summary>
        /// <param name="e">加载配置异常</param>
        public LoadConfigException(BaseException e)
            : base("加载配置异常", e)
        {
        } 
        #endregion
    }
}
