using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.CSMessages;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Test.Controller;

namespace Wing.WeiXin.MP.SDK.Test
{
    /// <summary>
    /// 基础测试
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }

        #region 请求头部
        /// <summary>
        /// 正确请求头部
        /// </summary>
        protected Request requestRight = new Request
        (
            "d054e317b56cc26c457981cc3d615f96c72ec230",
            "1395058361",
            "929810330",
            "",
            "echostr"
        );

        /// <summary>
        /// 错误请求头部
        /// </summary>
        protected Request requestError = new Request
        (
            "d054e317b56cc26c457981cc3d615f96c72ec230Error",
            "1395058361",
            "929810330",
            "",
            "echostr"
        ); 
        #endregion

        #region 服务号账号
        /// <summary>
        /// 服务号账号
        /// </summary>
        protected WXAccount account; 
        #endregion

        #region 被动接受消息
        /// <summary>
        /// 文本消息
        /// </summary>
        protected Request messageText = new Request
        (
            "d054e317b56cc26c457981cc3d615f96c72ec230",
            "1395058361",
            "929810330",
            "",
             @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <xml>
                            <ToUserName><![CDATA[gh_7f215c8b1c91]]></ToUserName>
                            <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
                            <CreateTime>1357986928</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[test]]></Content>
                            <MsgId>5832509444155992350</MsgId>
                        </xml>
                        ")
        ; 
        #endregion

        #region 客服文本消息
        /// <summary>
        /// 客服文本消息
        /// </summary>
        protected CSMessageText csMessageText = new CSMessageText("Test", "orImOuC33jQiJFrVelQGGTmwPSFE");
        #endregion

        #region 菜单
        /// <summary>
        /// 菜单
        /// </summary>
        protected Menu menu = new Menu
        {
            button = new List<AMenuItem>
                {
                    new MenuList
                    {
                        name = "test",
                        sub_button = new List<AMenuItem>
                        {
                            new MenuButtonView {name = "百度", url = "http://www.baidu.com"},
                            new MenuButtonView {name = "sxd", url = "http://sxd.xd.com/"},
                        }
                    },
                    new MenuButtonClick {name = "tbtttt", key = "test1"}
                }
        };

        /// <summary>
        /// 获取的菜单
        /// </summary>
        protected MenuForGet menuForGet = new MenuForGet
        {
            menu = new MenuMainForGet
            {
                button = new List<MenuButtonForGet>
                {
                    new MenuButtonForGet
                    {
                        name = "menu1",
                        sub_button = new List<MenuButtonForGet>
                        {
                            new MenuButtonForGet
                            {
                                name = "menu11",
                                type = "click",
                                key = "menu2"
                            }
                        }
                    },
                    new MenuButtonForGet
                    {
                        name = "menu2",
                        type = "click",
                        key = "menu2"
                    },
                    new MenuButtonForGet
                    {
                        name = "menu3",
                        type = "view",
                        url = "baidu.com"
                    }
                }
            }
        }; 
        #endregion

        #region 临时二维码
        /// <summary>
        /// 临时二维码
        /// </summary>
        protected QRCodeTicketRequest qrCodeTemp = new QRCodeTicketRequest
        {
            action_info = new QRCodeTicketRequest.ActionInfo
            {
                scene = new QRCodeTicketRequest.ActionInfo.Scene
                {
                    scene_id = 0
                }
            },
            action_name = QRCodeType.QR_SCENE.ToString(),
            expire_seconds = 100
        }; 
        #endregion

        #region 初始化实例化 protected BaseTest()
        /// <summary>
        /// 初始化实例化
        /// </summary>
        protected BaseTest()
        {
            LoadConfig();
        } 
        #endregion

        #region 载入配置 private void LoadConfig()
        /// <summary>
        /// 载入配置
        /// </summary>
        private void LoadConfig()
        {
            GlobalManager.InitConfig(new ConfigManager());
            account = GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountFirst(WeixinMPType.Service); 
        } 
        #endregion
    }
}
