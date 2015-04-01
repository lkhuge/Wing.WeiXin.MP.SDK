using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Common.WXSession
{
    /// <summary>
    /// 静态微信会话
    /// </summary>
    public class StaticWXSession : IWXSession
    {
        /// <summary>
        /// 用于锁定资源的对象
        /// </summary>
        private static readonly object lockSign = new object();

        /// <summary>
        /// AccessToken列表
        /// </summary>
        private readonly Dictionary<string, object> session = new Dictionary<string, object>();

        #region 获取数据 public T Get<T>(string user, string key)
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>数据</returns>
        public T Get<T>(string user, string key)
        {
            lock (lockSign)
            {
                string sessionKey = String.Format("{0}@{1}", user, key);

                return !session.ContainsKey(sessionKey) ? default(T) : (T)session[sessionKey];
            }
        }

        #endregion

        #region 设置对象数据 public void Set<T>(string user, string key, T value)
        /// <summary>
        /// 设置对象数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">对象数据</param>
        public void Set<T>(string user, string key, T value)
        {
            lock (lockSign)
            {
                string sessionKey = String.Format("{0}@{1}", user, key);

                session[sessionKey] = value;
            }
        }
        #endregion

        #region 删除数据 public void Delete(string user, string key)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        public void Delete(string user, string key)
        {
            lock (lockSign)
            {
                string sessionKey = String.Format("{0}@{1}", user, key);

                session.Remove(sessionKey);
            }
        }
        #endregion
    }
}
