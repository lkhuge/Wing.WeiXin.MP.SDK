﻿using System;
using System.Text;
using System.Linq;
using BaseException = System.Exception;
namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// 消息工具类
    /// </summary>
    public static class Message
    {
        #region 对字符串进行编码 public static string StringEncoding(string str, Encoding encoding = null)
        /// <summary>
        /// 对字符串进行编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">指定编码</param>
        /// <returns>编码后的字符串</returns>
        public static string StringEncoding(string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            byte[] strByte;
            try
            {
                strByte = encoding.GetBytes(str);
            }
            catch (BaseException)
            {
                return "";
            }

            return encoding.GetString(strByte);
        }
        #endregion

        #region 时间戳转为C#格式时间 public static DateTime GetTime(string timeStamp)
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }
        #endregion

        #region DateTime时间格式转换为Unix时间戳格式 public static int ConvertDateTimeInt(DateTime time)
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;
        }
        #endregion

        #region 获取当前时间长整型数据 public static long GetLongTimeNow()
        /// <summary>
        /// 获取当前时间长整型数据
        /// </summary>
        /// <returns>当前时间长整型数据</returns>
        public static long GetLongTimeNow()
        {
            return GetLongTimeByDateTime(DateTime.Now);
        }
        #endregion

        #region 根据DateTime时间获取长整型数据 public static long GetLongTimeByDateTime(DateTime dt)
        /// <summary>
        /// 根据DateTime时间获取长整型数据
        /// </summary>
        /// <param name="dt">DateTime时间</param>
        /// <returns>长整型数据</returns>
        public static long GetLongTimeByDateTime(DateTime dt)
        {
            return (dt.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000 - 8 * 60 * 60;
        }
        #endregion

        #region 根据长整型数据获取DateTime时间 public static DateTime GetDateTimeByLongTime(long longTime)
        /// <summary>
        /// 根据长整型数据获取DateTime时间
        /// </summary>
        /// <param name="longTime">长整型数据</param>
        /// <returns>DateTime时间</returns>
        public static DateTime GetDateTimeByLongTime(long longTime)
        {
            return new DateTime(1970, 1, 1).AddTicks((longTime + 8 * 60 * 60) * 10000000);
        }
        #endregion

        #region 获取两个字符串的相似度（Levenshtein算法） public static float SimilarityByLevenshtein(string str1, string str2)
        /// <summary>
        /// 获取两个字符串的相似度（Levenshtein算法）
        /// </summary>
        /// <param name="str1">字符串1</param>
        /// <param name="str2">字符串2</param>
        /// <returns>相似度</returns>
        public static float SimilarityByLevenshtein(string str1, string str2)
        {
            int len1 = str1.Length;
            int len2 = str2.Length;
            int[,] dif = new int[len1 + 1, len2 + 1];
            for (int a = 0; a <= len1; a++) dif[a, 0] = a;
            for (int a = 0; a <= len2; a++) dif[0, a] = a;
            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int temp = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    dif[i, j] = new[] {
                        dif[i - 1, j - 1] + temp, 
                        dif[i, j - 1] + 1,
                        dif[i - 1, j] + 1}.Min();
                }
            }
            return 1 - (float)dif[len1, len2] / Math.Max(str1.Length, str2.Length);
        }  
        #endregion 
    }
}
