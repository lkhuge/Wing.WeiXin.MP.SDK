using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.CL.WebManager;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Extension
{
    /// <summary>
    /// 接收事件处理（不需要检查请求）（一般处理程序扩展类）
    /// 用于测试
    /// </summary>
    public class AshxReceiveWithoutCheckHandler : AshxExtension
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

        #region 初始化 public AshxReceiveWithoutCheckHandler()
        /// <summary>
        /// 初始化
        /// </summary>
        public AshxReceiveWithoutCheckHandler()
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
            Response response = receiveController.Action(
                new Request(context.GetPostStream()), 
                false);

            return response == null ? "" : response.Text;
        } 
        #endregion
    }
}
