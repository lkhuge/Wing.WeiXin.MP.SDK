using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// HTML工具类
    /// </summary>
    public static class HTMLHelper
    {
        #region 获取超链接标记 public static string GetHyperLink(string url, string linkName)
        /// <summary>
        /// 获取超链接标记
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="linkName">连接文字</param>
        /// <returns></returns>
        public static string GetHyperLink(string url, string linkName)
        {
            return String.Format("<a href=\"{0}\">{1}</a>", url, linkName);
        } 
        #endregion
    }
}
