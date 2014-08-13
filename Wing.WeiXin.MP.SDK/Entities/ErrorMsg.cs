﻿using System;
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

        #region 获取错误说明 public string GetIntroduce()
        /// <summary>
        /// 获取错误说明
        /// </summary>
        public string GetIntroduce()
        {
            return Enum.GetName(typeof(ReturnCode), Convert.ToInt32(errcode));
        } 
        #endregion
    }
}
