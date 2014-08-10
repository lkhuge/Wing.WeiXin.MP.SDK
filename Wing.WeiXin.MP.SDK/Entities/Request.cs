using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 请求对象
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        /// </summary>
        public string Signature { get; private set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; private set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; private set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string Echostr { get; private set; }

        /// <summary>
        /// POST数据
        /// </summary>
        public string PostData { get; private set; }

        /// <summary>
        /// 请求XML数据
        /// </summary>
        public XElement RootElement { get; private set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; private set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; private set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        public string MsgTypeName { get; private set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public ReceiveEntityType MsgType { get; private set; }

        /// <summary>
        /// 账号对象
        /// </summary>
        public WXAccount WXAccount { get; private set; }

        #region 实例化请求对象 public Request(string signature, string timestamp, string nonce, string echostr, string postData)
        /// <summary>
        /// 实例化请求对象
        /// </summary>
        /// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="postData">POST数据</param>
        public Request(string signature, string timestamp, string nonce, string echostr, string postData)
        {
            Signature = signature;
            Timestamp = timestamp;
            Nonce = nonce;
            Echostr = echostr;
            PostData = postData;
        } 
        #endregion

        #region 解析POST数据 public void ParsePostData()
        /// <summary>
        /// 解析POST数据
        /// </summary>
        public void ParsePostData()
        {
            XDocument doc = XDocument.Parse(PostData);
            RootElement = doc.Element("xml");
            if (RootElement == null) throw new Exception("XML格式错误（未发现xml根节点）");
            ToUserName = GetPostData("ToUserName");
            FromUserName = GetPostData("FromUserName");
            CreateTime = Message.GetTime(GetPostData("CreateTime"));
            MsgTypeName = GetPostData("MsgType");
            ReceiveEntityType Temp;
            if ("event".Equals(MsgTypeName))
            {
                MsgTypeName = GetPostData("Event");
            }
            if (!Enum.TryParse(MsgTypeName, out Temp)) throw new Exception("XML格式错误（未知消息类型）");
            MsgType = Temp;
            WXAccount = new WXAccount(ToUserName);
        } 
        #endregion

        #region 获取XML数据 public string GetPostData(string key)
        /// <summary>
        /// 获取XML数据
        /// </summary>
        /// <param name="key">数据名称</param>
        /// <returns>XML数据</returns>
        public string GetPostData(string key)
        {
            XElement element = RootElement.Element(key);
            if (element == null) throw new Exception(String.Format("XML格式错误（未发现{0}节点）", key));

            return element.Value;
        } 
        #endregion

        #region 获取请求全部信息 public override string ToString()
        /// <summary>
        /// 获取请求全部信息
        /// </summary>
        /// <returns>请求全部信息</returns>
        public override string ToString()
        {
            return string.Format("[signature]:{0}[timestamp]:{1}[nonce]:{2}[echostr]:{3}[postData]:{4}",
                Signature, Timestamp, Nonce, Echostr, PostData);
        }
        #endregion
    }
}
