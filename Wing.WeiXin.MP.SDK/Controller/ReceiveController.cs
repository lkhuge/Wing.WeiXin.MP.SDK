using System;
using System.Configuration;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 接收消息控制器
    /// </summary>
    public class ReceiveController
    {
        /// <summary>
        /// 微信服务器IP
        /// </summary>
        private static WXServerIPList WXServerIPList;

        /// <summary>
        /// 接收开始事件
        /// </summary>
        public static event Action<Request> ReceiveStart;

        /// <summary>
        /// 接收结束事件
        /// </summary>
        public static event Action<Request, Response> ReceiveEnd;

        /// <summary>
        /// 接收异常事件
        /// </summary>
        public static event Action<Request, Exception> ReceiveException;

        #region 执行操作 public Response Action(Request request, bool needCheck = true)
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="needCheck">是否需要检查请求</param>
        /// <returns>响应对象</returns>
        public Response Action(Request request, bool needCheck = true)
        {
            GlobalManager.CheckInit();
            
            if (ReceiveStart != null)
            {
                LogManager.WriteSystem("触发接收开始事件");
                ReceiveStart(request);
                LogManager.WriteSystem("接收开始事件结束");
            }
            try
            {
                if (needCheck) request.Check();
                request.ParsePostData();
                if (needCheck)
                {
                    CheckRequestIP(request);
                    if(CheckMsgID(request)) return null;
                }
                Response response = GlobalManager.EventManager.ActionEvent(request);
                if (ReceiveEnd != null)
                {
                    LogManager.WriteSystem("触发接收结束事件");
                    ReceiveEnd(request, response);
                    LogManager.WriteSystem("接收结束事件结束");
                }
                return response;
            }
            catch (WXException e)
            {
                if (!e.IsMessage && ReceiveException != null)
                {
                    LogManager.WriteSystem("触发接收异常事件");
                    ReceiveException(request, e);
                    LogManager.WriteSystem("接收异常事件结束");
                }
                return new Response(e);
            }
            catch (Exception e)
            {
                LogManager.WriteError("ReceiveController发生不可预料异常", e, Settings.Default.SystemUsername);
                throw e;
            }
        }
        #endregion

        #region 检测MsgID防止消息重复 private bool CheckMsgID(Request request)
        /// <summary>
        /// 检测MsgID防止消息重复
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否息重复</returns>
        private bool CheckMsgID(Request request)
        {
            LogManager.WriteSystem("检测MsgID");
            if (!request.HasPostData("MsgId")) return false;
            if (GlobalManager.WXSession == null) return false;
            string msgID = request.GetMsgId();
            string msgIDTemp = GlobalManager.WXSession.Get<string>(request.FromUserName, Settings.Default.LastMsgIDKey);
            if (msgIDTemp != null)
            {
                LogManager.WriteSystem("检测MsgID通过");
                string lastMsgID = msgIDTemp;
                if (msgID.Equals(lastMsgID)) return true;
            }
            GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.LastMsgIDKey, msgID);
            LogManager.WriteSystem("检测MsgID未通过");
            return false;
        }
        #endregion

        #region 检测请求IP private void CheckRequestIP(Request request)
        /// <summary>
        /// 检测请求IP
        /// </summary>
        /// <param name="request">请求对象</param>
        private void CheckRequestIP(Request request)
        {
            if (WXServerIPList == null || String.IsNullOrEmpty(request.IP)) return;
            LogManager.WriteSystem(String.Format("触发检查微信服务器IP（请求者IP:{0}）", request.IP));
            bool result = WXServerIPList.ip_list.Contains(request.IP);
            LogManager.WriteSystem("检查微信服务器IP" + (result ? "通过" : "不通过"));
            if (!result) throw WXException.GetInstance(
                String.Format("检查微信服务器IP不通过（请求者IP:{0}）", request.IP), request.ToUserName);
        } 
        #endregion

        #region 检查微信服务器IP public static void CheckWXServerIP(string toUserName)
        /// <summary>
        /// 检查微信服务器IP
        /// 由于部分服务器的外层存在代理，因此该方法不一定能够获取到真正请求IP
        /// </summary>
        /// <param name="toUserName">微信公共平台账号ID</param>
        public static void CheckWXServerIP(string toUserName)
        {
            WXServerIPList = new SecurityController().GetWXServerIPList(
                Settings.Default.FirstAccountToUserNameSign.Equals(toUserName) 
                ? GlobalManager.GetFirstAccount()
                : GlobalManager.ConfigManager.GetWXAccountByID(toUserName));
            LogManager.WriteSystem(String.Format("微信服务器IP列表：【{0}】", 
                String.Join(";", WXServerIPList.ip_list)));
        } 
        #endregion
    }
}
