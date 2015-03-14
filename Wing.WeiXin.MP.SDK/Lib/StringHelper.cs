using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 随机类
        /// </summary>
        private static readonly Random ran = new Random();

        #region 字符串列表
        /// <summary>
        /// 小写字符列表
        /// </summary>
        private static readonly char[] lowStringList =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 
            'h', 'i', 'j', 'k', 'l', 'm', 'n', 
            'o', 'p', 'q', 'r', 's', 't', 'u', 
            'v', 'w', 'x', 'y', 'z'
        };
        /// <summary>
        /// 大写字符列表
        /// </summary>
        private static readonly char[] upStringList =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 
            'O', 'P', 'Q', 'R', 'S', 'T', 'N', 
            'V', 'W', 'X', 'Y', 'Z'
        };
        /// <summary>
        /// 数字列表
        /// </summary>
        private static readonly char[] numList =
        {
            '0', '1', '2', '3', '4', 
            '5', '6', '7', '8', '9'
        };
        #endregion

        #region 获取随机字符串 public static string GetRamdomString(int length, bool HasNum, bool HasString, StringType stringType = UDStringType.All)
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="HasNum">是否存在数字</param>
        /// <param name="HasString">是否存在字母</param>
        /// <param name="stringType">字符串大小写规范</param>
        /// <returns>随机字符串</returns>
        public static string GetRamdomString(int length, bool HasNum, bool HasString, UDStringType stringType = UDStringType.All)
        {
            StringBuilder sb = new StringBuilder();
            List<char> list = new List<char>();
            if (HasNum) list.AddRange(numList);
            if (HasString)
            {
                if (stringType == UDStringType.All || stringType == UDStringType.Lower) list.AddRange(lowStringList);
                if (stringType == UDStringType.All || stringType == UDStringType.Upper) list.AddRange(upStringList);
            }
            for (int i = 0; i < length; i++) sb.Append(list[ran.Next(list.Count)]);

            return sb.ToString();
        }
        #endregion
    }
}
