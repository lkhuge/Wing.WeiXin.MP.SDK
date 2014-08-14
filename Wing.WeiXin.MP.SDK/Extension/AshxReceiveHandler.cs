using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.CL.WebManager;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Extension
{
    /// <summary>
    /// 接收事件处理（一般处理程序扩展类）
    /// </summary>
    public class AshxReceiveHandler : AshxExtension
    {
        /// <summary>
        /// 接收消息控制器
        /// </summary>
        private readonly ReceiveController receiveController;

        /// <summary>
        /// 主要为了获取Get参数
        /// </summary>
        protected override bool IsGet
        {
            get { return true; }
        }

        /// <summary>
        /// 由于控制器已经将请求解析为字符串,因此响应对象使用文本类型
        /// </summary>
        protected override ResponseContentType ContentType
        {
            get { return ResponseContentType.TEXT; }
        }

        #region 初始化 public AshxReceiveHandler()
        /// <summary>
        /// 初始化
        /// </summary>
        public AshxReceiveHandler()
        {
            receiveController = new ReceiveController();
        } 
        #endregion

        #region 响应事件 protected override object Action(HttpContextExtension context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        protected override object Action(HttpContextExtension context)
        {
            return receiveController.Action(new Request(
                context.GetString("signature"),
                context.GetString("timestamp"),
                context.GetString("nonce"),
                context.GetString("echostr"),
                context.GetPostStream())).Text;
        } 
        #endregion
    }
}
