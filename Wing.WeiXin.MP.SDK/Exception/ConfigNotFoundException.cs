using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 没有找到配置异常
    /// </summary>
    public class ConfigNotFoundException : WXException
    {
        #region 根据配置参数名实例化异常 public ConfigNotFoundException(string configID)
        /// <summary>
        /// 根据配置参数名实例化异常
        /// </summary>
        /// <param name="configID">配置参数名</param>
        public ConfigNotFoundException(string configID) 
            : base(String.Format("没有找到配置（{0}）", configID))
        {
        } 
        #endregion
    }
}
