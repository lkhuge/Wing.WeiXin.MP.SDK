using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Lib.Serialize
{
    /// <summary>
    /// JSON工具类接口
    /// </summary>
    public interface IJSONHelper
    {
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        string JSONSerialize(object obj);

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>对象</returns>
        T JSONDeserialize<T>(string jsonString);

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>对象</returns>
        object JSONDeserialize(string jsonString);

        /// <summary>
        /// 是否存在该Key
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="key">Key</param>
        /// <returns>是否存在</returns>
        string GetValue(string jsonString, string key);

        /// <summary>
        /// 是否存在该Key
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="key">Key</param>
        /// <returns>是否存在</returns>
        bool HasKey(string jsonString, string key);
    }
}
