using System;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// DateTime工具类
    /// </summary>
    public static class DateTimeHelper
    {
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

        #region 时间戳转为C#格式时间 public static DateTime GetDateTimeByLongTime(string timeStamp)
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetDateTimeByLongTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }
        #endregion
    }
}
