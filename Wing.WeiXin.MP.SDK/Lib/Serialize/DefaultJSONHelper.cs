using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wing.WeiXin.MP.SDK.Lib.Serialize
{
    /// <summary>
    /// 默认JSON工具类（使用JSON.Net）
    /// </summary>
    public class DefaultJSONHelper : IJSONHelper
    {
        /// <summary>
        /// JSON序列化是否需要整理格式
        /// </summary>
        public static bool IsFormat = false;

        #region 将对象转换为Json字符串 public string JSONSerialize(object obj)
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public string JSONSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                IsFormat ? Formatting.Indented : Formatting.None);
        }
        #endregion

        #region 将Json字符串转换为对象 public T JSONDeserialize<T>(string jsonString)
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>对象</returns>
        public T JSONDeserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion

        #region 将Json字符串转换为对象 public static object JSONDeserialize(string jsonString)
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>对象</returns>
        public object JSONDeserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }
        #endregion

        #region 根据Key获取值 public string GetValue(string jsonString, string key)
        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="key">Key</param>
        /// <returns>值</returns>
        public string GetValue(string jsonString, string key)
        {
            JProperty jp = JObject.Parse(jsonString).Property(key);

            return jp != null ? jp.Value.ToString() : null;
        } 
        #endregion

        #region 是否存在该Key public bool HasKey(string jsonString, string key)
        /// <summary>
        /// 是否存在该Key
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="key">Key</param>
        /// <returns>是否存在</returns>
        public bool HasKey(string jsonString, string key)
        {
            return JObject.Parse(jsonString).Property(key) != null;
        }
        #endregion
    }
}
