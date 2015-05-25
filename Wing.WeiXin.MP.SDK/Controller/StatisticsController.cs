using System;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Statistics.Interface;
using Wing.WeiXin.MP.SDK.Entities.Statistics.Message;
using Wing.WeiXin.MP.SDK.Entities.Statistics.News;
using Wing.WeiXin.MP.SDK.Entities.Statistics.User;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 统计数据控制器
    /// </summary>
    public class StatisticsController : WXController
    {
        /// <summary>
        /// 获取用户增减数据的URL
        /// </summary>
        private const string UrlGetUserSummary = "https://api.weixin.qq.com/datacube/getusersummary?access_token=[AT]";

        /// <summary>
        /// 获取用户增减数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserSummary = 7;

        /// <summary>
        /// 获取累计用户数据的URL
        /// </summary>
        private const string UrlGetUserCumulate = "https://api.weixin.qq.com/datacube/getusercumulate?access_token=[AT]";

        /// <summary>
        /// 获取累计用户数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserCumulate = 7;

        /// <summary>
        /// 获取图文群发每日数据的URL
        /// </summary>
        private const string UrlGetArticleSummary = "https://api.weixin.qq.com/datacube/getarticlesummary?access_token=[AT]";

        /// <summary>
        /// 获取图文群发每日数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetArticleSummary = 1;

        /// <summary>
        /// 获取图文群发总数据的URL
        /// </summary>
        private const string UrlGetArticleTotal = "https://api.weixin.qq.com/datacube/getarticletotal?access_token=[AT]";

        /// <summary>
        /// 获取图文群发总数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetArticleTotal = 1;

        /// <summary>
        /// 获取图文统计数据的URL
        /// </summary>
        private const string UrlGetUserRead = "https://api.weixin.qq.com/datacube/getuserread?access_token=[AT]";

        /// <summary>
        /// 获取图文统计数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserRead = 3;

        /// <summary>
        /// 获取图文统计分时数据的URL
        /// </summary>
        private const string UrlGetUserReadHour = "https://api.weixin.qq.com/datacube/getuserreadhour?access_token=[AT]";

        /// <summary>
        /// 获取图文统计分时数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserReadHour = 1;

        /// <summary>
        /// 获取图文分享转发数据的URL
        /// </summary>
        private const string UrlGetUserShare = "https://api.weixin.qq.com/datacube/getusershare?access_token=[AT]";

        /// <summary>
        /// 获取图文分享转发数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserShare = 7;

        /// <summary>
        /// 获取图文分享转发分时数据的URL
        /// </summary>
        private const string UrlGetUserShareHour = "https://api.weixin.qq.com/datacube/getusersharehour?access_token=[AT]";

        /// <summary>
        /// 获取图文分享转发分时数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUserShareHour = 1;

        /// <summary>
        /// 获取消息发送概况数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsg = "https://api.weixin.qq.com/datacube/getupstreammsg?access_token=[AT]";

        /// <summary>
        /// 获取消息发送概况数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsg = 7;

        /// <summary>
        /// 获取消息分送分时数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgHour = "https://api.weixin.qq.com/datacube/getupstreammsghour?access_token=[AT]";

        /// <summary>
        /// 获取消息分送分时数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgHour = 1;

        /// <summary>
        /// 获取消息发送周数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgWeek = "https://api.weixin.qq.com/datacube/getupstreammsgweek?access_token=[AT]";

        /// <summary>
        /// 获取消息发送周数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgWeek = 30;

        /// <summary>
        /// 获取消息发送月数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgMonth = "https://api.weixin.qq.com/datacube/getupstreammsgmonth?access_token=[AT]";

        /// <summary>
        /// 获取消息发送月数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgMonth = 30;

        /// <summary>
        /// 获取消息发送分布数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgDist = "https://api.weixin.qq.com/datacube/getupstreammsgdist?access_token=[AT]";

        /// <summary>
        /// 获取消息发送分布数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgDist = 15;

        /// <summary>
        /// 获取消息发送分布周数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgDistWeek = "https://api.weixin.qq.com/datacube/getupstreammsgdistweek?access_token=[AT]";

        /// <summary>
        /// 获取消息发送分布周数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgDistWeek = 30;

        /// <summary>
        /// 获取消息发送分布月数据的URL
        /// </summary>
        private const string UrlGetUpstreamMsgDistMonth = "https://api.weixin.qq.com/datacube/getupstreammsgdistmonth?access_token=[AT]";

        /// <summary>
        /// 获取消息发送分布月数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetUpstreamMsgDistMonth = 30;

        /// <summary>
        /// 获取接口分析数据的URL
        /// </summary>
        private const string UrlGetInterfaceSummary = "https://api.weixin.qq.com/datacube/getinterfacesummary?access_token=[AT]";

        /// <summary>
        /// 获取接口分析数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetInterfaceSummary = 30;

        /// <summary>
        /// 获取接口分析分时数据的URL
        /// </summary>
        private const string UrlGetInterfaceSummaryHour = "https://api.weixin.qq.com/datacube/getinterfacesummaryhour?access_token=[AT]";

        /// <summary>
        /// 获取接口分析分时数据的最大时间跨度
        /// </summary>
        private const int MaxDayGetInterfaceSummaryHour = 1;

        #region 根据AccessToken容器初始化 public StatisticsController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public StatisticsController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 获取用户增减数据 public UserSummary GetUserSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取用户增减数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserSummary GetUserSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserSummary)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserSummary>(
                UrlGetUserSummary,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取累计用户数据 public UserCumulate GetUserCumulate(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取累计用户数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserCumulate GetUserCumulate(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserCumulate)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserCumulate>(
                UrlGetUserCumulate,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文群发每日数据 public ArticleSummary GetArticleSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文群发每日数据
        /// 
        /// 某天所有被阅读过的文章（仅包括群发的文章）在当天的阅读次数等数据。
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public ArticleSummary GetArticleSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetArticleSummary)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<ArticleSummary>(
                UrlGetArticleSummary,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文群发总数据 public ArticleTotal GetArticleTotal(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文群发总数据
        /// 
        /// 某天群发的文章，从群发日起到接口调用日（但最多统计发表日后7天数据），每天的到当天的总等数据。
        /// 例如某篇文章是12月1日发出的，发出后在1日、2日、3日的阅读次数分别为1万，则getarticletotal获取到的数据为，
        /// 距发出到12月1日24时的总阅读量为1万，距发出到12月2日24时的总阅读量为2万，距发出到12月1日24时的总阅读量为3万。
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public ArticleTotal GetArticleTotal(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetArticleTotal)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<ArticleTotal>(
                UrlGetArticleTotal,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文统计数据 public UserRead GetUserRead(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文统计数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserRead GetUserRead(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserRead)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserRead>(
                UrlGetUserRead,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文统计分时数据 public UserReadHour GetUserReadHour(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文统计分时数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserReadHour GetUserReadHour(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserReadHour)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserReadHour>(
                UrlGetUserReadHour,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文分享转发数据 public UserShare GetUserShare(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文分享转发数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserShare GetUserShare(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserShare)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserShare>(
                UrlGetUserShare,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取图文分享转发分时数据 public UserShareHour GetUserShareHour(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取图文分享转发分时数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UserShareHour GetUserShareHour(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUserShareHour)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UserShareHour>(
                UrlGetUserShareHour,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送概况数据 public UpstreamMsg GetUpstreamMsg(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送概况数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsg GetUpstreamMsg(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsg)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsg>(
                UrlGetUpstreamMsg,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息分送分时数据 public UpstreamMsgHour GetUpstreamMsgHour(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息分送分时数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgHour GetUpstreamMsgHour(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgHour)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgHour>(
                UrlGetUpstreamMsgHour,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送周数据 public UpstreamMsgWeek GetUpstreamMsgWeek(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送周数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgWeek GetUpstreamMsgWeek(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgWeek)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgWeek>(
                UrlGetUpstreamMsgWeek,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送月数据 public UpstreamMsgMonth GetUpstreamMsgMonth(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送月数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgMonth GetUpstreamMsgMonth(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgMonth)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgMonth>(
                UrlGetUpstreamMsgMonth,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送分布数据 public UpstreamMsgDist GetUpstreamMsgDist(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送分布数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgDist GetUpstreamMsgDist(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgDist)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgDist>(
                UrlGetUpstreamMsgDist,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送分布周数据 public UpstreamMsgDistWeek GetUpstreamMsgDistWeek(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送分布周数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgDistWeek GetUpstreamMsgDistWeek(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgDistWeek)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgDistWeek>(
                UrlGetUpstreamMsgDistWeek,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取消息发送分布月数据 public UpstreamMsgDistMonth GetUpstreamMsgDistMonth(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取消息发送分布月数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public UpstreamMsgDistMonth GetUpstreamMsgDistMonth(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetUpstreamMsgDistMonth)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<UpstreamMsgDistMonth>(
                UrlGetUpstreamMsgDistMonth,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取接口分析数据 public InterfaceSummary GetInterfaceSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取接口分析数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public InterfaceSummary GetInterfaceSummary(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetInterfaceSummary)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<InterfaceSummary>(
                UrlGetInterfaceSummary,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion

        #region 获取接口分析分时数据 public InterfaceSummaryHour GetInterfaceSummaryHour(WXAccount account, DateTime begin_date, DateTime end_date)
        /// <summary>
        /// 获取接口分析分时数据
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="begin_date">
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”
        /// （比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        public InterfaceSummaryHour GetInterfaceSummaryHour(WXAccount account, DateTime begin_date, DateTime end_date)
        {
            if ((end_date.Date - begin_date.Date).Days > MaxDayGetInterfaceSummaryHour)
                throw WXException.GetInstance("时间跨度过大", account.ID);

            return Action<InterfaceSummaryHour>(
                UrlGetInterfaceSummaryHour,
                new { begin_date = begin_date.ToString("yyyy-MM-dd"), end_date = end_date.ToString("yyyy-MM-dd") },
                account);
        } 
        #endregion
    }
}
