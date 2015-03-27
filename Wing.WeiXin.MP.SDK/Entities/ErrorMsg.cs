using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 错误码
    /// </summary>
    public class ErrorMsg
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        #region 获取错误码枚举对象 public ReturnCode GetReturnCode()
        /// <summary>
        /// 获取错误码枚举对象
        /// </summary>
        /// <returns>错误码枚举对象</returns>
        public ReturnCode GetReturnCode()
        {
            return (ReturnCode)Enum.Parse(typeof(ReturnCode), errcode);
        } 
        #endregion

        #region 获取错误说明 public string GetIntroduce()
        /// <summary>
        /// 获取错误说明
        /// </summary>
        public string GetIntroduce()
        {
            return Enum.GetName(typeof(ReturnCode), Convert.ToInt32(errcode));
        } 
        #endregion

        #region 获取错误码信息 public override string ToString()
        /// <summary>
        /// 获取错误码信息
        /// </summary>
        /// <returns>错误码信息</returns>
        public override string ToString()
        {
            return String.Format("Errcode:{1}{0}Errmsg:{2}{0}Introduce:{3}{0}",
                Environment.NewLine,
                errcode,
                errmsg,
                GetIntroduce());
        } 
        #endregion
    }
}
