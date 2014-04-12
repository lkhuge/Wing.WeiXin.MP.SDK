using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ServerDataContainer
{
    /// <summary>
    /// 基于静态消息状态容器
    /// </summary>
    public class StaticServerDataContainer : IServerDataContainer
    {
        /// <summary>
        /// 基于静态消息状态容器
        /// </summary>
        private static StaticServerDataContainer staticServerDataContainer;

        /// <summary>
        /// 静态消息状态容器
        /// </summary>
        private static Dictionary<string, object> dataContainer;

        #region 初始化实例化 private StaticServerDataContainer()
        /// <summary>
        /// 初始化实例化
        /// </summary>
        private StaticServerDataContainer()
        {
            ResetData();
        } 
        #endregion

        #region 获取基于静态消息状态容器 public static StaticServerDataContainer GetStaticServerDataContainer()
        /// <summary>
        /// 获取基于静态消息状态容器
        /// </summary>
        /// <returns>基于静态消息状态容器</returns>
        public static StaticServerDataContainer GetStaticServerDataContainer()
        {
            return staticServerDataContainer ?? (staticServerDataContainer = new StaticServerDataContainer());
        } 
        #endregion

        #region 添加消息 public void AddData(string key, object obj)
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="obj">对象数据</param>
        public void AddData(string key, object obj)
        {
            lock (dataContainer)
            {
                dataContainer[key] = obj;
            }
        } 
        #endregion

        #region 获取数据 public object GetData(string key, bool IsDelete = true)
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="IsDelete">获取的同时是否删除</param>
        /// <returns>数据，不存在则为NULL</returns>
        public object GetData(string key, bool IsDelete = true)
        {
            lock (dataContainer)
            {
                if(!dataContainer.ContainsKey(key)) return null;
                object obj = dataContainer[key];
                if (IsDelete) dataContainer.Remove(key);

                return obj;
            }
        }
        #endregion

        #region 是否存在数据 public bool HasData(string key)
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>是否存在数据</returns>
        public bool HasData(string key)
        {
            lock (dataContainer)
            {
                return dataContainer.ContainsKey(key);
            }
        } 
        #endregion

        #region 删除数据 public void DeleteData(string key)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">Key值</param>
        public void DeleteData(string key)
        {
            lock (dataContainer)
            {
                if(!dataContainer.ContainsKey(key)) return;
                dataContainer.Remove(key);
            }
        } 
        #endregion

        #region 重置全部数据 public void ResetData()
        /// <summary>
        /// 重置全部数据
        /// </summary>
        public void ResetData()
        {
            dataContainer = null;
            staticServerDataContainer = null;
        } 
        #endregion
    }
}
