using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 功能管理器
    /// </summary>
    public class FunctionManager
    {
        /// <summary>
        /// AccessToken容器
        /// </summary>
        private readonly AccessTokenContainer accessTokenContainer;

        /// <summary>
        /// 客服控制器
        /// </summary>
        private CSController csController;

        /// <summary>
        /// 多客服控制器
        /// </summary>
        private DKFController dkfController;

        /// <summary>
        /// JSSDK控制器
        /// </summary>
        private JSController jsController;

        /// <summary>
        /// 素材控制器
        /// </summary>
        private MaterialController materialController;

        /// <summary>
        /// 菜单工具类
        /// </summary>
        private MenuController menuController;

        /// <summary>
        /// OAuth控制器
        /// </summary>
        private OAuthController oauthController;

        /// <summary>
        /// 二维码控制器
        /// </summary>
        private QRCodeController qrCodeController;

        /// <summary>
        /// 安全控制器
        /// </summary>
        private SecurityController securityController;

        /// <summary>
        /// 语义控制器
        /// </summary>
        private SemanticController semanticController;

        /// <summary>
        /// 群发控制器
        /// </summary>
        private SendAllController sendAllController;

        /// <summary>
        /// 统计数据控制器
        /// </summary>
        private StatisticsController statisticsController;

        /// <summary>
        /// 模板消息控制器
        /// </summary>
        private TemplateController templateController;

        /// <summary>
        /// 用户控制器
        /// </summary>
        private WXUserController wxUserController;

        /// <summary>
        /// 客服控制器
        /// </summary>
        public CSController CS
        {
            get { return csController ?? (csController = new CSController(accessTokenContainer)); }
        }

        /// <summary>
        /// 多客服控制器
        /// </summary>
        public DKFController DKF
        {
            get { return dkfController ?? (dkfController = new DKFController(accessTokenContainer)); }
        }

        /// <summary>
        /// JSSDK控制器
        /// </summary>
        public JSController JS
        {
            get { return jsController ?? (jsController = new JSController(accessTokenContainer)); }
        }

        /// <summary>
        /// 素材控制器
        /// </summary>
        public MaterialController Material
        {
            get { return materialController ?? (materialController = new MaterialController(accessTokenContainer)); }
        }

        /// <summary>
        /// 菜单工具类
        /// </summary>
        public MenuController Menu
        {
            get { return menuController ?? (menuController = new MenuController(accessTokenContainer)); }
        }

        /// <summary>
        /// OAuth控制器
        /// </summary>
        public OAuthController OAuth
        {
            get { return oauthController ?? (oauthController = new OAuthController(accessTokenContainer)); }
        }

        /// <summary>
        /// 二维码控制器
        /// </summary>
        public QRCodeController QRCode
        {
            get { return qrCodeController ?? (qrCodeController = new QRCodeController(accessTokenContainer)); }
        }

        /// <summary>
        /// 安全控制器
        /// </summary>
        public SecurityController Security
        {
            get { return securityController ?? (securityController = new SecurityController(accessTokenContainer)); }
        }

        /// <summary>
        /// 语义控制器
        /// </summary>
        public SemanticController Semantic
        {
            get { return semanticController ?? (semanticController = new SemanticController(accessTokenContainer)); }
        }

        /// <summary>
        /// 群发控制器
        /// </summary>
        public SendAllController SendAll
        {
            get { return sendAllController ?? (sendAllController = new SendAllController(accessTokenContainer)); }
        }

        /// <summary>
        /// 统计数据控制器
        /// </summary>
        public StatisticsController Statistics
        {
            get { return statisticsController ?? (statisticsController = new StatisticsController(accessTokenContainer)); }
        }

        /// <summary>
        /// 模板消息控制器
        /// </summary>
        public TemplateController Template
        {
            get { return templateController ?? (templateController = new TemplateController(accessTokenContainer)); }
        }

        /// <summary>
        /// 用户控制器
        /// </summary>
        public WXUserController WXUser
        {
            get { return wxUserController ?? (wxUserController = new WXUserController(accessTokenContainer)); }
        }

        #region 根据AccessToken容器初始化 public FunctionManager(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public FunctionManager(AccessTokenContainer accessTokenContainer)
        {
            if (accessTokenContainer == null)
                throw WXException.GetInstance("未初始化AccessToken容器", Settings.Default.SystemUsername);
            this.accessTokenContainer = accessTokenContainer;
        } 
        #endregion
    }
}
