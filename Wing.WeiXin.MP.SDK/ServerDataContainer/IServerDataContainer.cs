using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.ServerDataContainer
{
    /// <summary>
    /// 消息状态容器接口
    /// </summary>
    public interface IServerDataContainer
    {
        #region 添加消息 void AddData(string key, object obj); 
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="obj">对象数据</param>
        void AddData(string key, object obj); 
        #endregion

        #region 获取数据 object GetData(string key, bool IsDelete = false);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="IsDelete">获取的同时是否删除</param>
        /// <returns>数据，不存在则为NULL</returns>
        object GetData(string key, bool IsDelete = false); 
        #endregion

        #region 是否存在数据 bool HasData(string key); 
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>是否存在数据</returns>
        bool HasData(string key); 
        #endregion

        #region 删除数据 void DeleteData(string key); 
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">Key值</param>
        void DeleteData(string key); 
        #endregion

        #region 重置全部数据 void ResetData(); 
        /// <summary>
        /// 重置全部数据
        /// </summary>
        void ResetData(); 
        #endregion
    }
}
