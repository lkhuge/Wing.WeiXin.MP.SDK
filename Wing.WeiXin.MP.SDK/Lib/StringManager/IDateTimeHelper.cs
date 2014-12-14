using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// DateTime工具类接口
    /// </summary>
    public interface IDateTimeHelper
    {
        /// <summary>
        /// 根据DateTime时间获取长整型数据
        /// </summary>
        /// <param name="dt">DateTime时间</param>
        /// <returns>长整型数据</returns>
        long GetLongTimeByDateTime(DateTime dt);

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        DateTime GetDateTimeByLongTime(string timeStamp);
    }
}
