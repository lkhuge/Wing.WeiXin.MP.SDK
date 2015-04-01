﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Common.WXSession
{
    /// <summary>
    /// 微信会话接口
    /// </summary>
    public interface IWXSession
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>数据</returns>
        T Get<T>(string user, string key);

        /// <summary>
        /// 设置对象数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">对象数据</param>
        void Set<T>(string user, string key, T value);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        void Delete(string user, string key);
    }
}
