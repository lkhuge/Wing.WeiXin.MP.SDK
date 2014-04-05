using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.LogConfig
{
    /// <summary>
    /// ADO.NET记录附加器配置节点
    /// </summary>
    public class AdoNetAppenderConfigSection : ConfigurationSection
    {
        #region 数据库类型 public SQLType SQLType
        /// <summary>
        /// 数据库类型
        /// </summary>
        [ConfigurationProperty("SQLType", DefaultValue = "SQLServer")]
        public SQLType SQLType
        {
            get { return (SQLType)this["SQLType"]; }
        }
        #endregion

        #region 连接字符串 public string ConnectionString
        /// <summary>
        /// 连接字符串
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return Convert.ToString(this["ConnectionString"]); }
        }
        #endregion

        #region 插入语句 public string CommandText
        /// <summary>
        /// 插入语句
        /// </summary>
        [ConfigurationProperty("CommandText", IsRequired = true)]
        public string CommandText
        {
            get { return Convert.ToString(this["CommandText"]); }
        }
        #endregion
    }
}
