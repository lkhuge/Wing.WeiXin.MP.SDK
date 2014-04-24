using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Exception;
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

        #region 根据文本消息获取快速配置回复消息配置对象 public IReturn GetQuickConfigReturnMessageFromMessageText(string weixinMPID, MessageText message)
        /// <summary>
        /// 根据文本消息获取快速配置回复消息配置对象
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="message">文本消息对象</param>
        /// <returns>回复消息</returns>
        public IReturn GetQuickConfigReturnMessageFromMessageText(string weixinMPID, MessageText message)
        {
            string key = String.Format("{0}:Text:{1}", weixinMPID, message.Content);
            List<QuickConfigReturnMessageItemConfigSection> list =
                this.OfType<QuickConfigReturnMessageItemConfigSection>().Where(q => q.Key.Equals(key)).ToList();
            if (list.Count == 1)
            {
                try
                {
                    return QuickConfigReturnMessageManager.GetReturnMessage(FileHelper.ReadOfKeyValueData(list[0].Path), message);
                }
                catch (WXException)
                {
                    return null;
                }
            }
            string keyPath = String.Format("{0}:Text:", weixinMPID);
            List<QuickConfigReturnMessageItemConfigSection> listPath =
                this.OfType<QuickConfigReturnMessageItemConfigSection>().Where(q => q.Key.Equals(keyPath)).ToList();
            if (listPath.Count != 1) return null;
            string path = String.Format("{0}{1}.wx.txt", listPath[0].Path, message.Content);
            try
            {
                return File.Exists(path)
                ? QuickConfigReturnMessageManager.GetReturnMessage(FileHelper.ReadOfKeyValueData(path), message)
                : null;
            }
            catch (WXException)
            {
                return null;
            }
        } 
        #endregion

        #region 根据菜单点击事件获取快速配置回复消息配置对象 public IReturn GetQuickConfigReturnMessageFromEventClick(string weixinMPID, EventClick click)
        /// <summary>
        /// 根据菜单点击事件获取快速配置回复消息配置对象
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="click">菜单点击事件</param>
        /// <returns>回复消息</returns>
        public IReturn GetQuickConfigReturnMessageFromEventClick(string weixinMPID, EventClick click)
        {
            string key = String.Format("{0}:Click:{1}", weixinMPID, click.EventKey);
            List<QuickConfigReturnMessageItemConfigSection> list =
                this.OfType<QuickConfigReturnMessageItemConfigSection>().Where(q => q.Key.Equals(key)).ToList();
            if (list.Count == 1)
            {
                try
                {
                    return QuickConfigReturnMessageManager.GetReturnMessage(FileHelper.ReadOfKeyValueData(list[0].Path), click);
                }
                catch (WXException)
                {
                    return null;
                }
            }
            string keyPath = String.Format("{0}:Click:", weixinMPID);
            List<QuickConfigReturnMessageItemConfigSection> listPath =
                this.OfType<QuickConfigReturnMessageItemConfigSection>().Where(q => q.Key.Equals(keyPath)).ToList();
            if (listPath.Count != 1) return null;
            string path = String.Format("{0}{1}.wx.txt", listPath[0].Path, click.EventKey);
            try
            {
                return File.Exists(path)
                    ? QuickConfigReturnMessageManager.GetReturnMessage(FileHelper.ReadOfKeyValueData(path), click)
                    : null;
            }
            catch (WXException)
            {
                return null;
            }
        }
        #endregion
    }
}
