using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu
{
    /// <summary>
    /// 点击菜单跳转链接时的事件请求
    /// </summary>
    public class RequestEventPicWeixin : RequestAMessage
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey
        {
            get { return GetPostData("EventKey"); }
        }

        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public int Count
        {
            get { return Convert.ToInt32(GetPostData("SendPicsInfo", "Count")); }
        }

        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片
        /// </summary>
        public List<string> PicMd5SumList
        {
            get
            {
                XElement element = Request.RootElement.Element("SendPicsInfo");
                if (element == null) throw new Exception("XML格式错误（未发现SendPicsInfo节点）");
                XElement element2 = element.Element("PicList");
                if (element2 == null) throw new Exception("XML格式错误（未发现PicList节点）");

                return element2.Elements().Select(e =>
                {
                    XElement eleTemp = e.Element("item");
                    if (eleTemp == null) throw new Exception("XML格式错误（未发现item节点）");
                    XElement eleTemp2 = eleTemp.Element("PicMd5Sum");
                    if (eleTemp2 == null) throw new Exception("XML格式错误（未发现PicMd5Sum节点）");
                    return eleTemp2.Value;
                }).ToList();
            }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.pic_weixin; }
        }
    }
}
