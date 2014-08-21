﻿using System;
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

        #region 设置数据 public void Set(string user, string key, object value)
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">数据</param>
        public void Set(string user, string key, object value)
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

        #region 是否存在Key值 public bool HasKey(string user, string key)
        /// <summary>
        /// 是否存在Key值
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>是否存在Key值</returns>
        public bool HasKey(string user, string key)
        {
            return wxSession.HasKey(user, key);
        } 
        #endregion

        #region 是否存在数据 public bool HasValue(string user, object value)
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="value">数据</param>
        /// <returns>是否存在数据</returns>
        public bool HasValue(string user, object value)
        {
            return wxSession.HasValue(user, value);
        } 
        #endregion
    }
}