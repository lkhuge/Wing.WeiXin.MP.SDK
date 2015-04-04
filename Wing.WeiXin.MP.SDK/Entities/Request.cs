using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 请求对象
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 请求者服务器IP
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }

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
                    createTime = DateTimeHelper.GetDateTimeByLongTime(GetPostData("CreateTime"));
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
            get { return wxAccount ?? (wxAccount = GlobalManager.ConfigManager.GetWXAccountByID(ToUserName)); }
            set { wxAccount = value; }
        }

        /// <summary>
        /// 请求标记对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 是否计算请求响应时长
        /// </summary>
        public static bool IsSumRunTime = false;

        /// <summary>
        /// 运行时长计时器
        /// </summary>
        private static Stopwatch Stopwatch;

        #region 实例化请求对象 public Request(string token, string signature, string timestamp, string nonce, string echostr, string postData, string ip = null)
        /// <summary>
        /// 实例化请求对象
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="postData">POST数据</param>
        /// <param name="ip">请求者服务器IP</param>
        public Request(string token, string signature, string timestamp, string nonce, string echostr, string postData, string ip = null)
            : this(postData, ip)
        {
            Token = token;
            Signature = signature;
            Timestamp = timestamp;
            Nonce = nonce;
            Echostr = echostr;
        }
        #endregion

        #region 实例化请求对象 public Request(string postData, string ip = null)
        /// <summary>
        /// 实例化请求对象
        /// </summary>
        /// <param name="postData">POST数据</param>
        /// <param name="ip">请求者服务器IP</param>
        public Request(string postData, string ip = null)
        {
            PostData = postData;
            IP = ip;
            if (!IsSumRunTime) return;
            if (Stopwatch == null) Stopwatch = new Stopwatch();
            Stopwatch.Restart();
        }
        #endregion

        #region 验证数据 internal void Check()
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>结果（空则验证通过）</returns>
        internal void Check()
        {
            LogManager.WriteSystem("验证请求头部");
            if (!CheckSignature(Nonce))
            {
                LogManager.WriteSystem("验证请求头部未通过");
                throw WXException.GetInstance("验证未通过\nRequest:" +
                String.Format("[signature]:{0}[timestamp]:{1}[nonce]:{2}[echostr]:{3}[postData]:{4}",
                    Signature, Timestamp, Nonce, Echostr, PostData), Settings.Default.SystemUsername);
            }
            LogManager.WriteSystem("验证请求头部通过");
            //首次验证
            if (!String.IsNullOrEmpty(Echostr))
            {
                LogManager.WriteSystem("首次验证");
                throw WXException.GetInstance(Echostr);
            }
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
                Token, 
                Timestamp, 
                nonce
            }.OrderBy(z => z).ToArray();

            return SecurityHelper.SHA1_Encrypt(string.Join("", arr)).Equals(Signature);
        }
        #endregion

        #region 解析POST数据 internal void ParsePostData()
        /// <summary>
        /// 解析POST数据
        /// </summary>
        internal void ParsePostData()
        {
            LogManager.WriteSystem("解析POST数据");
            RootElement = EncodingData();
            ToUserName = GetPostData("ToUserName");
            FromUserName = GetPostData("FromUserName");
            MsgTypeName = GetPostData("MsgType");
            MsgTypeName = "event".Equals(MsgTypeName) ? GetPostData("Event") : MsgTypeName;
            if ("subscribe".Equals(MsgTypeName) && HasPostData("EventKey"))
            {
                MsgTypeName = "subscribeByQRScene";
            }
            ReceiveEntityType Temp;
            if (!Enum.TryParse(MsgTypeName, out Temp)) throw WXException.GetInstance("XML格式错误（未知消息类型）", Settings.Default.SystemUsername, MsgTypeName);
            MsgType = Temp;
            LogManager.WriteSystem("确认为消息类型为：" + MsgType);
        }
        #endregion

        #region 解密数据 private XElement EncodingData()
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <returns>root节点数据</returns>
        private XElement EncodingData()
        {
            XElement root = XDocument.Parse(PostData).Element("xml");
            if (root == null) throw WXException.GetInstance("XML格式错误（未发现xml根节点）", Settings.Default.SystemUsername);
            XElement enElement = root.Element("Encrypt");
            if (enElement == null) return root;
            LogManager.WriteSystem("解密POST数据");
            string weixinID = GetPostData("ToUserName");
            if (wxAccount.WXBizMsgCrypt == null) throw WXException.GetInstance("消息需要解密，可没有提供解密密钥", weixinID);
            string outMsg = null;
            if (wxAccount.WXBizMsgCrypt.DecryptMsg(Signature, Timestamp, Nonce, PostData, ref outMsg) != 0)
                throw WXException.GetInstance(String.Format("消息解密失败，原文：{0}", PostData), weixinID);

            return XDocument.Parse(outMsg).Element("xml");
        }
        #endregion

        #region 获取XML数据 internal string GetPostData(string key)
        /// <summary>
        /// 获取XML数据
        /// </summary>
        /// <param name="key">数据名称</param>
        /// <returns>XML数据</returns>
        internal string GetPostData(string key)
        {
            XElement element = RootElement.Element(key);
            if (element == null) throw WXException.GetInstance(String.Format("XML格式错误（未发现{0}节点）", key), Settings.Default.SystemUsername);

            return element.Value;
        }
        #endregion

        #region 获取XML数据（二级数据） internal string GetPostData(string key1, string key2)
        /// <summary>
        /// 获取XML数据（二级数据）
        /// </summary>
        /// <param name="key1">一级数据名称</param>
        /// <param name="key2">二级数据名称</param>
        /// <returns>XML数据</returns>
        internal string GetPostData(string key1, string key2)
        {
            XElement element = RootElement.Element(key1);
            if (element == null) throw WXException.GetInstance(String.Format("XML格式错误（未发现{0}节点）", key1), Settings.Default.SystemUsername);
            XElement element2 = element.Element(key2);
            if (element2 == null) throw WXException.GetInstance(String.Format("XML格式错误（未发现{0}节点）", key2), Settings.Default.SystemUsername);

            return element2.Value;
        }
        #endregion

        #region 是否存在XML数据 internal bool HasPostData(string key)
        /// <summary>
        /// 是否存在XML数据
        /// </summary>
        /// <param name="key">数据名称</param>
        /// <returns>是否存在XML数据</returns>
        internal bool HasPostData(string key)
        {
            XElement element = RootElement.Element(key);

            return element != null;
        }
        #endregion

        #region 获取消息id，64位整型 internal string GetMsgId()
        /// <summary>
        /// 获取消息id，64位整型
        /// </summary>
        /// <returns>消息id，64位整型</returns>
        internal string GetMsgId()
        {
            return GetPostData("MsgId");
        }
        #endregion

        #region 获取运行时长 internal long GetRunTime()
        /// <summary>
        /// 获取运行时长
        /// </summary>
        /// <returns>运行时长</returns>
        internal long GetRunTime()
        {
            if (!IsSumRunTime) return 0;
            Stopwatch.Stop();
            return Stopwatch.ElapsedMilliseconds;
        }
        #endregion
    }
}
