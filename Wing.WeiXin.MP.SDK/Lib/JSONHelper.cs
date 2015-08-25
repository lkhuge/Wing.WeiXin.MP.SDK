using Newtonsoft.Json;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public static class JSONHelper
    {
        #region 将对象转换为Json字符串 public static string JSONSerialize(object obj)
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string JSONSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
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
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion
    }
}
