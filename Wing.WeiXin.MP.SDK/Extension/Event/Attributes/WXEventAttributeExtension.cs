using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Attributes
{
    /// <summary>
    /// 微信事件特性扩展类
    /// </summary>
    public static class WXEventAttributeExtension
    {
        #region 添加接收事件 public static void AddReceiveEvent(this EventManager eventManager, object receiveEvent)
        /// <summary>
        /// 添加接收事件
        /// </summary>
        /// <param name="eventManager">事件管理类</param>
        /// <param name="receiveEvent">接收事件对象</param>
        public static void AddReceiveEvent(this EventManager eventManager, object receiveEvent)
        {
            foreach (MethodInfo methodInfo in receiveEvent.GetType().GetMethods())
            {
                WXEventAttribute[] attList = (WXEventAttribute[])
                    methodInfo.GetCustomAttributes(typeof(WXEventAttribute), false);
                if (attList.Length != 1) continue;
                ParameterInfo arg = GetParameterInfo(methodInfo, receiveEvent.GetType(), attList[0]);
                if (arg.ParameterType == typeof(Request))
                {
                    MethodInfo info = methodInfo;
                    eventManager.AddGloablReceiveEvent(
                        attList[0].EventName,
                        attList[0].ToUserName,
                        request => (Response)info.Invoke(receiveEvent, new object[] { request }),
                        attList[0].Priority);
                    continue;
                }
                if (typeof(RequestAMessage).IsAssignableFrom(arg.ParameterType))
                {
                    ConstructorInfo constructorInfo = arg.ParameterType.GetConstructor(Type.EmptyTypes);
                    if (constructorInfo == null)
                        throw WXException.GetInstance(String.Format("无法获取{0}构造方法", arg.ParameterType.Name), Settings.Default.SystemUsername);
                    MethodInfo methodInfoGen = typeof(RequestAMessage).GetMethod("GetRequestAMessage").MakeGenericMethod(arg.ParameterType);
                    MethodInfo info = methodInfo;
                    eventManager.AddReceiveEvent(
                        ((RequestAMessage)constructorInfo.Invoke(null)).ReceiveEntityType,
                        attList[0].EventName,
                        attList[0].ToUserName,
                        request => (Response)info.Invoke(receiveEvent, new[] { methodInfoGen.Invoke(null, new object[] { request }) }),
                        attList[0].Priority);
                }
            }
        }
        #endregion

        #region 获取参数信息 private static ParameterInfo GetParameterInfo(MethodInfo methodInfo, Type type, WXEventAttribute attr)
        /// <summary>
        /// 获取参数信息
        /// </summary>
        /// <param name="methodInfo">方法对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="attr">方法特性</param>
        /// <returns>参数信息</returns>
        private static ParameterInfo GetParameterInfo(MethodInfo methodInfo, Type type, WXEventAttribute attr)
        {
            if (methodInfo.IsStatic || !methodInfo.IsPublic)
                throw WXException.GetInstance(String.Format("接收事件对象（{0}）方法（{1}）必须为非静态且公开类型（public）",
                    type.Name, methodInfo.Name), attr.ToUserName);

            ParameterInfo[] argList = methodInfo.GetParameters();
            if (argList.Length != 1 || (!typeof(RequestAMessage).IsAssignableFrom(argList[0].ParameterType)
                && argList[0].ParameterType != typeof(Request)))
                throw WXException.GetInstance(String.Format("接收事件对象（{0}）方法（{1}）参数仅有一个且类型必须为Request或者RequestAMessage及其子类",
                    type.Name, methodInfo.Name), attr.ToUserName);

            ParameterInfo returnParam = methodInfo.ReturnParameter;
            if (returnParam == null || returnParam.ParameterType != typeof(Response))
                throw WXException.GetInstance(String.Format("接收事件对象（{0}）方法（{1}）必须有返回值且类型为Response",
                    type.Name, methodInfo.Name), attr.ToUserName);

            return argList[0];
        } 
        #endregion

        #region 自动添加接收事件 public static void AutoAddReceiveEvent(this EventManager eventManager, string namespaceName)
        /// <summary>
        /// 自动添加接收事件
        /// </summary>
        /// <param name="eventManager">事件管理类</param>
        /// <param name="namespaceName">命名空间</param>
        public static void AutoAddReceiveEvent(this EventManager eventManager, string namespaceName)
        {
            namespaceName = namespaceName
                .Replace("%Namespace%", Assembly.GetCallingAssembly().FullName.Split(',')[0]);
            Assembly.GetCallingAssembly().GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(namespaceName))
                .Select(s => s.GetConstructors().FirstOrDefault(c => c.IsPublic && !c.GetParameters().Any()))
                .Where(c => c != null)
                .Select(c => c.Invoke(null))
                .ToList()
                .ForEach(eventManager.AddReceiveEvent);
        } 
        #endregion
    }
}
