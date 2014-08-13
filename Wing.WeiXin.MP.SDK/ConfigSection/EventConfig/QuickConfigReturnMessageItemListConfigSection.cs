using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Wing.CL.FileManager;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

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

        #region 获取快速配置回复消息配置对象 public Response GetQuickConfigReturnMessage(Request request)
        /// <summary>
        /// 获取快速配置回复消息配置对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public Response GetQuickConfigReturnMessage(Request request)
        {
            if (request.MsgType == ReceiveEntityType.text) return GetQuickConfigReturnMessage(
                "Text",
                request.ToUserName,
                request.GetPostData("Content"),
                request);
            if (request.MsgType == ReceiveEntityType.CLICK) return GetQuickConfigReturnMessage(
                "Click",
                request.ToUserName,
                request.GetPostData("EventKey"),
                request);

            return null;
        }
        #endregion

        #region 获取快速配置回复消息配置对象 private Response GetQuickConfigReturnMessage(string type, string weixinMPID, string key, Request request)
        /// <summary>
        /// 获取快速配置回复消息配置对象
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="key">菜单点击事件</param>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response GetQuickConfigReturnMessage(string type, string weixinMPID, string key, Request request)
        {
            QuickConfigReturnMessageItemConfigSection[] list = this
                .Cast<QuickConfigReturnMessageItemConfigSection>().ToArray();
            string name = String.Format("{0}:{1}:{2}", weixinMPID, type, key);
            QuickConfigReturnMessageItemConfigSection item = list
                .FirstOrDefault(q => q.Key.Equals(name));
            if (item != null) return QuickConfigReturnMessageManager.GetReturnMessage(
                    ReadOfKeyValueData(item.Path),
                    request);
            string keyPath = String.Format("{0}:{1}:", weixinMPID, type);
            QuickConfigReturnMessageItemConfigSection listItem = list
                .FirstOrDefault(q => q.Key.Equals(keyPath));
            if (listItem == null) return null;
            string path = String.Format("{0}{1}.wx.txt", listItem.Path, key);
            return File.Exists(path)
                ? QuickConfigReturnMessageManager.GetReturnMessage(
                    ReadOfKeyValueData(path), request)
                : null;
        }
        #endregion

        #region 从文件中读取KeyValue数据 public static Dictionary<string, string> ReadOfKeyValueData(string fileName)
        /// <summary>
        /// 从文件中读取KeyValue数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>KeyValue数据</returns>
        public static Dictionary<string, string> ReadOfKeyValueData(string fileName)
        {
            return FileHelper.ReadLine(fileName)
                .Where(r => !String.IsNullOrEmpty(r) && !String.IsNullOrEmpty(r.Trim()) && r.IndexOf(':') != -1)
                .ToDictionary(
                    k => k.Substring(0, k.IndexOf(':')).Trim(), 
                    v => v.Substring(v.IndexOf(':') + 1).Trim()
                            .Replace("{LF}", "\n")
                            .Replace("{NowDate}", DateTime.Now.ToString("yyyy年MM月dd日"))
                            .Replace("{NowTime}", DateTime.Now.ToString("hh:mm:ss")));
        }
        #endregion
    }
}
