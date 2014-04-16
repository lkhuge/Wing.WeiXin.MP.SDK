using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.FileManager;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 快速配置回复消息管理类
    /// </summary>
    public static class QuickConfigReturnMessageManager
    {
        #region 获取回复文本消息的快速配置事件 public static EntityHandler.CustomEntityHandler<MessageText> GetQuickConfigEntityHandler(string fileName, string str)
        /// <summary>
        /// 获取回复文本消息的快速配置事件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="str">匹配字符串</param>
        /// <returns>回复文本消息的快速配置事件</returns>
        public static EntityHandler.CustomEntityHandler<MessageText>
            GetQuickConfigEntityHandler(string fileName, string str)
        {
            Dictionary<string, string> quickConfig = new Dictionary<string, string>
            {
                {"Content", ""},
            };
            return messageText =>
            {
                FileHelper.ReadOfKeyValueData(fileName, quickConfig);
                return str.Equals(messageText.Content)
                    ? new ReturnMessageText(quickConfig["Content"], messageText)
                    : null;
            };
        }
        #endregion
    }
}
