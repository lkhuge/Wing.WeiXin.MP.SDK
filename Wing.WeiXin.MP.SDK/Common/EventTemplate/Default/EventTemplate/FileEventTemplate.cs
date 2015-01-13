using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Item;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Default.EventTemplate
{
    /// <summary>
    /// 基于文件的事件模板抽象类
    /// </summary>
    public abstract class FileEventTemplate : IEventTemplate
    {
        #region 获取事件项列表 public IEnumerable<EventItem> GetEventList(string path)
        /// <summary>
        /// 获取事件项列表
        /// </summary>
        /// <param name="path">事件模版路径</param>
        /// <returns>事件项列表</returns>
        public IEnumerable<EventItem> GetEventList(string path)
        {
            try
            {
                return GetEventListFromFileText(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                throw WXException.GetInstance(String.Format("读取文件发生错误{0}说明：{1}",
                    Environment.NewLine, e.Message), Settings.Default.SystemUsername);
            }
        } 
        #endregion

        /// <summary>
        /// 从文件文本内容获取事件项列表
        /// </summary>
        /// <param name="text">文件文本内容</param>
        /// <returns>事件项列表</returns>
        protected abstract IEnumerable<EventItem> GetEventListFromFileText(string text);
    }
}
