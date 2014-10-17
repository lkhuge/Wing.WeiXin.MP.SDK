using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Common.WXSession
{
    /// <summary>
    /// 微信用户会话管理类
    /// </summary>
    public class WXSessionManager
    {
        /// <summary>
        /// 微信会话类
        /// </summary>
        private readonly IWXSession wxSession;

        #region 根据微信会话类实例化 public WXSessionManager(IWXSession wxSession)
        /// <summary>
        /// 根据微信会话类实例化
        /// </summary>
        /// <param name="wxSession">微信会话类</param>
        public WXSessionManager(IWXSession wxSession)
        {
            this.wxSession = wxSession;
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
            return wxSession.Get(user, key);
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
            wxSession.Set(user, key, value);
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
            wxSession.Set(user, key, value);
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
            wxSession.Delete(user, key);
        } 
        #endregion
    }
}
