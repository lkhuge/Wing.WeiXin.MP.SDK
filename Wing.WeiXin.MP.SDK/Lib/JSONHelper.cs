using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public static class JSONHelper
    {
        /// <summary>
        /// 序列化工具类
        /// </summary>
        private static readonly JavaScriptSerializer javaScriptSerializer
            = new JavaScriptSerializer();

        #region 将对象转换为Json字符串 public static string JSONSerialize(object obj)
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string JSONSerialize(object obj)
        {
            return javaScriptSerializer.Serialize(obj);
        }
        #endregion

        #region 将Json字符串转换为对象 public static T JSONDeserialize<T>(string jsonString)
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>对象</returns>
        public static T JSONDeserialize<T>(string jsonString)
        {
            return javaScriptSerializer.Deserialize<T>(jsonString);
        }
        #endregion

        #region 是否存在该Key public static bool HasKey(string jsonString, string key)
        /// <summary>
        /// 是否存在该Key
        /// </summary>
        /// <param name="jsonString">Json字符串</param>
        /// <param name="key">Key</param>
        /// <returns>是否存在</returns>
        public static bool HasKey(string jsonString, string key)
        {
            return ((Dictionary<string, object>)javaScriptSerializer
                .DeserializeObject(jsonString)).ContainsKey(key);
        }
        #endregion
    }
}
