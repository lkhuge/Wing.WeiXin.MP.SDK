using System;
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
        #region 获取数据 object Get(string user, string key);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>数据</returns>
        object Get(string user, string key);
        #endregion

        #region 设置数据 void Set(string user, string key, object value);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <param name="value">数据</param>
        void Set(string user, string key, object value);
        #endregion

        #region 删除数据 void Delete(string user, string key);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        void Delete(string user, string key);
        #endregion

        #region 是否存在Key值 bool HasKey(string user, string key);
        /// <summary>
        /// 是否存在Key值
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="key">Key值</param>
        /// <returns>是否存在Key值</returns>
        bool HasKey(string user, string key);
        #endregion

        #region 是否存在数据 bool HasValue(string user, object value);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="user">用户编号</param>
        /// <param name="value">数据</param>
        /// <returns>是否存在数据</returns>
        bool HasValue(string user, object value);
        #endregion
    }
}
