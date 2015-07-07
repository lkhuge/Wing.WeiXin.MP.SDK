using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// 解码Unicode规则
        /// </summary>
        private static readonly Regex reUnicode
            = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

        #region 将对象转换为Json字符串 public static string JSONSerialize(object obj)
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string JSONSerialize(object obj)
        {
            return DecodereUnicode(javaScriptSerializer.Serialize(obj));
        }
        #endregion

        #region 解码Unicode private static string DecodereUnicode(string str)
        /// <summary>
        /// 解码Unicode
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>解码后的字符串</returns>
        private static string DecodereUnicode(string str)
        {
            return reUnicode.Replace(str, m =>
            {
                short c;
                return short.TryParse(
                    m.Groups[1].Value,
                    NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture,
                    out c) ? c.ToString() : m.Value;
            });
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
