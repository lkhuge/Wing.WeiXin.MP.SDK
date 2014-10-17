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
        /// AccessToken列表
        /// </summary>
        private readonly ConcurrentDictionary<string, Dictionary<string, object>> session;

        #region 初始化 public StaticWXSession()
        /// <summary>
        /// 初始化
        /// </summary>
        public StaticWXSession()
        {
            session = new ConcurrentDictionary<string, Dictionary<string, object>>();
        } 
        #endregion

        #region 获取数据 public object Get(string user, string key)
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>数据</returns>
        public object Get(string user, string key)
        {
            return !HasKey(user, key) ? null : session.GetOrAdd(user, new Dictionary<string,object>())[key];
        }

        #endregion

        #region 设置对象数据 public void Set(string user, string key, object value)
        /// <summary>
        /// 设置对象数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">对象数据</param>
        public void Set(string user, string key, object value)
        {
            session.GetOrAdd(user, new Dictionary<string, object>())[key] = value;
        }
        #endregion

        #region 设置字符串数据 public void Set(string user, string key, string value)
        /// <summary>
        /// 设置字符串数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">字符串数据</param>
        public void Set(string user, string key, string value)
        {
            session.GetOrAdd(user, new Dictionary<string, object>())[key] = value;
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
            if (!HasKey(user, key)) return;
            session.GetOrAdd(user, new Dictionary<string, object>()).Remove(key);
        }
        #endregion

        #region 是否存在Key值 public bool HasKey(string user, string key)
        /// <summary>
        /// 是否存在Key值
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>是否存在Key值</returns>
        public bool HasKey(string user, string key)
        {
            return session.GetOrAdd(user, new Dictionary<string, object>()).ContainsKey(key);
        }
        #endregion
    }
}
