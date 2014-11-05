﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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
    public class AshxReceiveWithoutCheckHandler : IHttpHandler
    {
        /// <summary>
        /// 接收消息控制器
        /// </summary>
        private readonly ReceiveController receiveController;

        #region 初始化 public AshxReceiveWithoutCheckHandler()
        /// <summary>
        /// 初始化
        /// </summary>
        public AshxReceiveWithoutCheckHandler()
        {
            receiveController = new ReceiveController();
        } 
        #endregion

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            Response response = receiveController.Action(
                new Request(GetPostStream(context)), 
                false);

            context.Response.Write(response == null ? "" : response.Text);
        } 
        #endregion

        #region 获取Post请求流 private string GetPostStream(HttpContext context)
        /// <summary>
        /// 获取Post请求流
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>Post请求流字符串</returns>
        private string GetPostStream(HttpContext context)
        {
            try
            {
                return new StreamReader(
                    context.Request.InputStream,
                    Encoding.UTF8).ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public bool IsReusable { get; private set; }
    }
}