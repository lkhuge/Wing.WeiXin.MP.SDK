using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Extension.Event.Attributes;

namespace $rootnamespace$
{
    public class _Global
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //初始化
            GlobalManager.Init();
            
            //范例1： 添加一个事件（通过添加委托的方式）
            //参数说明：
            //    1.事件名：用于标示事件 不能重复不能为空，且需要配置，
            //      详情请看Web.config（configuration->WeiXinMPSDKConfigGroup->Event->EventList）
            //    2.微信公众账号ID: 用于指明该事件由哪个账号处理,
            //      详情请看Web.config（configuration->WeiXinMPSDKConfigGroup->Base->AccountList）
            //      可以使用“&”来表示的默认账号
            //      可以使用“$”来表示列表中的第一个账号
            //    3.事件委托对象
            //    4.事件优先级： 当存在多个相同类型的事件 则按照优先级大小进行顺序执行
            //      值越大 优先级越高  不填默认为0
            GlobalManager.EventManager.AddReceiveEvent<RequestText>(
                "Event1",
                "xxxx",
                TestResponse1,
                1);

            //范例2： 添加一个事件（通过添加特性类的方式）
            //对象说明：
            //    该方法将遍历该对象中所有带WXEvent特性的方法（公共非静态且返回值类型为Response 参数只有1个）
            //    如果参数类型为Request 则添加为全局事件
            //    如果参数类型为RequestAMessage的子类 则添加为普通事件
            GlobalManager.EventManager.AddReceiveEvent(this);
            //如果有多个类且都在一个命名空间树下 也可以使用以下这个方法
            //“%Namespace%”用于代表当前程序集下的根命名空间
            //GlobalManager.EventManager.AutoAddReceiveEvent("%Namespace%.Event");
        }

        /// <summary>
        /// 范例事件1
        /// 事件作用： 
        ///     如果请求是文本类型
        ///     将用户发送的文本加上“Test”响应给用户
        /// </summary>
        /// <param name="request">请求对象（类型为text）</param>
        /// <returns>响应对象</returns>
        private Response TestResponse1(RequestText request)
        {
            //获取请求的文本内容（Content）
            string content = request.Content;

            //根据需求在请求的文本内容后加上“Test”
            string responseContent = content + "Test";

            //使用ResponseBuilder类生成响应对象
            //也可以使用扩展方法简化代码：
            //return request.Request.GetTextResponse(responseContent);
            return ResponseBuilder.GetMessageText(
                request.Request,
                responseContent);

            //请求对象解释：
            //1.服务器接收到用户的请求后将把请求内容映射为Request对象
            //  Request对象中可以读取到请求内容以及其每个字段的值
            //2.为了方便 使用RequestAMessage类包装了Request对象
            //  RequestAMessage对象中Request属性就是Request对象
            //  RequestAMessage是抽象类 而RequestText就是其其中的一个子类
            //  通过调用Content属性 就相当于从Request中获取对应的文本数据
            //3.生成响应的时候就需要从Request中读取请求源来生成响应对象
            //  因此就需要使用将Request作为参数生成响应
            //  如果事件的参数是RequestAMessage的子类（就像本例）
            //  就需要把RequestAMessage的Request属性作为参数
        }

        /// <summary>
        /// 范例事件2-1
        /// 事件作用： 
        ///     如果请求是文本类型
        ///     将用户发送的文本响应给用户
        /// </summary>
        /// <param name="request">请求对象（类型为text）</param>
        /// <returns>响应对象</returns>
        [WXEvent("Event2", "xxxx", Priority = 2)]
        public Response TestResponse21(RequestText request)
        {
            return request.Request.GetTextResponse(request.Content);
        }

        /// <summary>
        /// 范例事件2-2
        /// 事件作用： 
        ///     如果请求是文本类型
        ///     将用户发送的文本响应给用户
        ///     否则跳过
        /// </summary>
        /// <param name="request">请求对象（类型为text）</param>
        /// <returns>响应对象</returns>
        [WXEvent("Event3", "xxxx", Priority = 3)]
        public Response TestResponse22(Request request)
        {
            //判断请求对象是否为文本 如果不是则跳过
            //事件处理说明:
            //    1.除了普通事件和全局事件  还有临时事件和系统事件(内部属性不对外开发)  其优先级如下：
            //        临时事件 > 系统事件 > 普通事件 > 全局事件    
            //    2.如果事件返回值为NULL则跳过进入下一个事件
            //    3.同类型事件允许添加多个 根据事件优先级执行
            if (request.MsgType != ReceiveEntityType.text) return null;

            //将Request请求包装成RequestAMessage请求
            RequestText requestText = RequestAMessage.GetRequestAMessage<RequestText>(request);

            //响应对象
            return request.GetTextResponse(requestText.Content);
        }
    }
}