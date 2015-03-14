using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// HTML工具类
    /// </summary>
    public static class HTMLHelper
    {
        #region 获取超链接Html字符串 public static string GetActionLink(string url, string text)
        /// <summary>
        /// 获取超链接Html字符串
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="text">显示文本</param>
        /// <returns>超链接Html字符串</returns>
        public static string GetActionLink(string url, string text)
        {
            return String.Format("<a href=\"{0}\">{1}</a>", url, text);
        } 
        #endregion
    }
}
