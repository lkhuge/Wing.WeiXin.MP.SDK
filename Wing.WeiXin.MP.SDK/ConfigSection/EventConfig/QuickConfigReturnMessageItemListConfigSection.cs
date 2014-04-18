using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Lib.FileManager;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 快速配置回复消息列表配置节点
    /// </summary>
    public class QuickConfigReturnMessageItemListConfigSection : ConfigurationElementCollection
    {
        #region 创建新的快速配置回复消息配置节点 protected override ConfigurationElement CreateNewElement()
        /// <summary>
        /// 创建新的快速配置回复消息配置节点
        /// </summary>
        /// <returns>快速配置回复消息配置节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new QuickConfigReturnMessageItemConfigSection();
        } 
        #endregion

        #region 获取快速配置回复消息配置节点名称 protected override object GetElementKey(ConfigurationElement element)
        /// <summary>
        /// 获取快速配置回复消息配置节点名称
        /// </summary>
        /// <param name="element">快速配置回复消息配置节点</param>
        /// <returns>快速配置回复消息配置节点名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            QuickConfigReturnMessageItemConfigSection config = element as QuickConfigReturnMessageItemConfigSection;
            if (config == null) throw new ArgumentException();

            return config.Key;
        } 
        #endregion

        #region 获取快速配置回复消息配置对象 public IReturn GetQuickConfigReturnMessage(MessageText message)
        /// <summary>
        /// 获取快速配置回复消息配置对象
        /// </summary>
        /// <param name="message">文本消息对象</param>
        /// <returns>回复消息</returns>
        public IReturn GetQuickConfigReturnMessage(MessageText message)
        {
            List<QuickConfigReturnMessageItemConfigSection> list =
                this.OfType<QuickConfigReturnMessageItemConfigSection>().Where(q => q.Key.Equals(message.Content)).ToList();

            return list.Count != 1 
                ? null 
                : QuickConfigReturnMessageManager.GetReturnMessage(FileHelper.ReadOfKeyValueData(list[0].Path), message);
        } 
        #endregion
    }
}
