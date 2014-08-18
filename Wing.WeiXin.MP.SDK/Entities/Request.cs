using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Wing.CL.Serialize;
using Wing.CL.StringManager;
using Wing.WeiXin.MP.SDK.Enumeration;

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
        public XElement RootElement { get; protected set; }

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
        private DateTime createTime;

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public DateTime CreateTime 
        {
            get
            {
                if (createTime == default(DateTime))
                {
                    createTime = DateTimeHelper.GetTime(GetPostData("CreateTime"));
                }
                return createTime;
            } 
        }

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
        private WXAccount wxAccount;

        /// <summary>
        /// 账号对象
        /// </summary>
        public WXAccount WXAccount 
        {
            get { return wxAccount ?? (wxAccount = new WXAccount(ToUserName)); }
        }

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
            if (!CheckSignature(nonce)) throw new Exception("验证未通过\nRequest:" + 
                String.Format("[signature]:{0}[timestamp]:{1}[nonce]:{2}[echostr]:{3}[postData]:{4}",
                    Signature, Timestamp, nonce, echostr, PostData));
            Nonce = nonce;
            //首次验证
            if (!String.IsNullOrEmpty(echostr)) throw new Exception(echostr);
            Echostr = echostr;
            PostData = postData;
        } 
        #endregion

        #region 验证signature是否有效 private bool CheckSignature(string nonce)
        /// <summary>
        /// 验证signature是否有效
        /// </summary>
        /// <param name="nonce">随机数</param>
        /// <returns>是否有效</returns>
        private bool CheckSignature(string nonce)
        {
            string[] arr = new[] 
            { 
                GlobalManager.ConfigManager.BaseConfig.Token, 
                Timestamp, 
                nonce
            }.OrderBy(z => z).ToArray();

            return Security.SHA1_Encrypt(string.Join("", arr)).Equals(Signature);
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
            MsgTypeName = GetPostData("event".Equals(MsgTypeName) ? "Event" : "MsgType");
            if ("subscribe".Equals(MsgTypeName) || HasPostData("EventKey"))
            {
                MsgTypeName = "subscribeByQRScene";
            }
            ReceiveEntityType Temp;
            if (!Enum.TryParse(MsgTypeName, out Temp)) throw new Exception("XML格式错误（未知消息类型）");
            MsgType = Temp;
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

        #region 是否存在XML数据 public bool HasPostData(string key)
        /// <summary>
        /// 是否存在XML数据
        /// </summary>
        /// <param name="key">数据名称</param>
        /// <returns>是否存在XML数据</returns>
        public bool HasPostData(string key)
        {
            XElement element = RootElement.Element(key);

            return element != null;
        } 
        #endregion

        #region 获取消息id，64位整型 public string GetMsgId()
        /// <summary>
        /// 获取消息id，64位整型
        /// </summary>
        /// <returns>消息id，64位整型</returns>
        public string GetMsgId()
        {
            return GetPostData("MsgId");
        } 
        #endregion
    }
}
