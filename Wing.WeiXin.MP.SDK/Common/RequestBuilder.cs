using System;
using System.Linq;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 请求创建工具类
    /// </summary>
    public static class RequestBuilder
    {
        /// <summary>
        /// 用于测试的时间戳
        /// </summary>
        public static string TestTimestamp = "TestTimestamp";

        /// <summary>
        /// 用于测试的随机数
        /// </summary>
        public static string TestNonce = "TestNonce";

        /// <summary>
        /// 用于测试的随机字符串
        /// </summary>
        public static string TestEchostr = "TestEchostr";

        /// <summary>
        /// 用于测试的微信账号ID
        /// </summary>
        public static string TestToUserName = "TestToUserName";

        /// <summary>
        /// 用于测试的微信用户ID
        /// </summary>
        public static string TestFromUserName = "TestFromUserName";

        #region 普通消息
        #region 图片请求内容
        /// <summary>
        /// 图片请求内容
        /// </summary>
        private const string MessageImage =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[image]]></MsgType><PicUrl><![CDATA[{0}]]></PicUrl><MediaId><![CDATA[{1}]]></MediaId><MsgId>{MsgId}</MsgId></xml>";
        #endregion

        #region 链接请求内容
        /// <summary>
        /// 链接请求内容
        /// </summary>
        private const string MessageLink =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[link]]></MsgType><Title><![CDATA[{0}]]></Title><Description><![CDATA[{1}]]></Description><Url><![CDATA[{2}]]></Url><MsgId>{MsgId}</MsgId></xml> ";
        #endregion

        #region 地理位置请求内容
        /// <summary>
        /// 地理位置请求内容
        /// </summary>
        private const string MessageLocation =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[location]]></MsgType><Location_X>{0}</Location_X><Location_Y>{1}</Location_Y><Scale>{2}</Scale><Label><![CDATA[{3}]]></Label><MsgId>{MsgId}</MsgId></xml>";
        #endregion

        #region 文本请求内容
        /// <summary>
        /// 文本请求内容
        /// </summary>
        private const string MessageText =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName> <CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content><MsgId>{MsgId}</MsgId></xml>";
        #endregion

        #region 视频请求内容
        /// <summary>
        /// 视频请求内容
        /// </summary>
        private const string MessageVideo =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[video]]></MsgType><MediaId><![CDATA[{0}]]></MediaId><ThumbMediaId><![CDATA[{1}]]></ThumbMediaId><MsgId>{MsgId}</MsgId></xml>";
        #endregion

        #region 语音请求内容
        /// <summary>
        /// 语音请求内容
        /// </summary>
        private const string MessageVoice =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[voice]]></MsgType><MediaId><![CDATA[{0}]]></MediaId><Format><![CDATA[{1}]]></Format><MsgId>{MsgId}</MsgId></xml>";
        #endregion

        #region 获取图片请求对象 public static Request GetMessageImage(string picUrl, string mediaId)
        /// <summary>
        /// 获取图片请求对象
        /// </summary>
        /// <param name="picUrl">图片链接</param>
        /// <param name="mediaId">图片消息媒体id，可以调用多媒体文件下载接口拉取数据。</param>
        /// <returns>请求对象</returns>
        public static Request GetMessageImage(string picUrl, string mediaId)
        {
            return GetRequest(MessageImage, picUrl, mediaId);
        } 
        #endregion

        #region 获取链接请求对象 public static Request GetMessageLink(string title, string description, string url)
        /// <summary>
        /// 获取链接请求对象
        /// </summary>
        /// <param name="title">消息标题</param>
        /// <param name="description">消息描述</param>
        /// <param name="url">消息链接</param>
        /// <returns>链接请求对象</returns>
        public static Request GetMessageLink(string title, string description, string url)
        {
            return GetRequest(MessageLink, title, description, url);
        } 
        #endregion

        #region 获取地理位置请求对象 public static Request GetMessageLocation(string locationX, string locationY, string scale, string label)
        /// <summary>
        /// 获取地理位置请求对象
        /// </summary>
        /// <param name="locationX">地理位置维度</param>
        /// <param name="locationY">地理位置经度</param>
        /// <param name="scale">地图缩放大小</param>
        /// <param name="label">地理位置信息</param>
        /// <returns>地理位置请求对象</returns>
        public static Request GetMessageLocation(string locationX, string locationY, string scale, string label)
        {
            return GetRequest(MessageLocation, locationX, locationY, scale, label);
        } 
        #endregion

        #region 获取文本请求对象 public static Request GetMessageText(string content)
        /// <summary>
        /// 获取文本请求对象
        /// </summary>
        /// <param name="content">文本消息内容</param>
        /// <returns>文本请求对象</returns>
        public static Request GetMessageText(string content)
        {
            return GetRequest(MessageText, content);
        } 
        #endregion

        #region 获取视频请求对象 public static Request GetMessageVideo(string mediaId, string thumbMediaId)
        /// <summary>
        /// 获取视频请求对象
        /// </summary>
        /// <param name="mediaId">视频消息媒体id，可以调用多媒体文件下载接口拉取数据。</param>
        /// <param name="thumbMediaId">视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。</param>
        /// <returns>视频请求对象</returns>
        public static Request GetMessageVideo(string mediaId, string thumbMediaId)
        {
            return GetRequest(MessageVideo, mediaId, thumbMediaId);
        } 
        #endregion

        #region 获取语音请求对象 public static Request GetMessageVoice(string mediaId, string format)
        /// <summary>
        /// 获取语音请求对象
        /// </summary>
        /// <param name="mediaId">语音消息媒体id，可以调用多媒体文件下载接口拉取数据。</param>
        /// <param name="format">语音格式，如amr，speex等</param>
        /// <returns>语音请求对象</returns>
        public static Request GetMessageVoice(string mediaId, string format)
        {
            return GetRequest(MessageVoice, mediaId, format);
        } 
        #endregion
        #endregion

        #region 事件消息
        #region 菜单事件消息
        #region 点击菜单拉取消息时的事件请求内容
        /// <summary>
        /// 点击菜单拉取消息时的事件请求内容
        /// </summary>
        private const string EventClick =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[CLICK]]></Event><EventKey><![CDATA[{0}]]></EventKey></xml>";
        #endregion

        #region 弹出地理位置选择器的事件请求内容
        /// <summary>
        /// 弹出地理位置选择器的事件请求内容
        /// </summary>
        private const string EventLocationSelect =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[location_select]]></Event><EventKey><![CDATA[{0}]]></EventKey><SendLocationInfo><Location_X><![CDATA[{1}]]></Location_X><Location_Y><![CDATA[{2}]]></Location_Y><Scale><![CDATA[{3}]]></Scale><Label><![CDATA[{4}]]></Label><Poiname><![CDATA[{5}]]></Poiname></SendLocationInfo></xml>";
        #endregion

        #region 弹出拍照或者相册发图的事件请求内容
        /// <summary>
        /// 弹出拍照或者相册发图的事件请求内容
        /// </summary>
        private const string EventPicPhotoOrAlbum =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[pic_photo_or_album]]></Event><EventKey><![CDATA[{0}]]></EventKey><SendPicsInfo><Count>{1}</Count><PicList>{2}</PicList></SendPicsInfo></xml>";
        #endregion

        #region 弹出系统拍照发图的事件请求内容
        /// <summary>
        /// 弹出系统拍照发图的事件请求内容
        /// </summary>
        private const string EventPicSysPhoto =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[pic_sysphoto]]></Event><EventKey><![CDATA[{0}]]></EventKey><SendPicsInfo><Count>{1}</Count><PicList>{2}</PicList></SendPicsInfo></xml>";
        #endregion

        #region 弹出微信相册发图器的事件请求内容
        /// <summary>
        /// 弹出微信相册发图器的事件请求内容
        /// </summary>
        private const string EventPicWeixin =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[pic_weixin]]></Event><EventKey><![CDATA[{0}]]></EventKey><SendPicsInfo><Count>{1}</Count><PicList>{2}</PicList></SendPicsInfo></xml>";
        #endregion

        #region 扫码推事件的事件请求内容
        /// <summary>
        /// 扫码推事件的事件请求内容
        /// </summary>
        private const string EventScanCodePush =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[scancode_push]]></Event><EventKey><![CDATA[{0}]]></EventKey><ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType><ScanResult><![CDATA[{1}]]></ScanResult></ScanCodeInfo></xml>";
        #endregion

        #region 扫码推事件且弹出“消息接收中”提示框的事件内容
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件内容
        /// </summary>
        private const string EventScanCodeWaitMsg =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[scancode_waitmsg]]></Event><EventKey><![CDATA[{0}]]></EventKey><ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType><ScanResult><![CDATA[{1}]]></ScanResult></ScanCodeInfo></xml>";
        #endregion

        #region 点击菜单跳转链接时的事件请求内容
        /// <summary>
        /// 点击菜单跳转链接时的事件请求内容
        /// </summary>
        private const string EventView =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[VIEW]]></Event><EventKey><![CDATA[{0}]]></EventKey></xml>";
        #endregion  

        #region 获取点击菜单拉取消息时的事件请求对象 public static Request GetEventClick(string eventKey)
        /// <summary>
        /// 获取点击菜单拉取消息时的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，与自定义菜单接口中KEY值对应</param>
        /// <returns>获取点击菜单拉取消息时的事件请求对象</returns>
        public static Request GetEventClick(string eventKey)
        {
            return GetRequest(EventClick, eventKey);
        } 
        #endregion

        #region 获取弹出地理位置选择器的事件请求对象 public static Request GetEventLocationSelect(string eventKey, float locationX, float locationY, int scale, string label, string poiname)
        /// <summary>
        /// 获取弹出地理位置选择器的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="locationX">X坐标信息</param>
        /// <param name="locationY">Y坐标信息</param>
        /// <param name="scale">精度，可理解为精度或者比例尺、越精细的话 scale越高</param>
        /// <param name="label">地理位置的字符串信息</param>
        /// <param name="poiname">朋友圈POI的名字，可能为空</param>
        /// <returns>弹出地理位置选择器的事件请求对象</returns>
        public static Request GetEventLocationSelect(string eventKey, float locationX, float locationY, int scale, string label, string poiname)
        {
            return GetRequest(EventLocationSelect, eventKey, locationX, locationY, scale, label, poiname);
        } 
        #endregion

        #region 获取弹出拍照或者相册发图的事件请求对象 public static Request GetEventPicPhotoOrAlbum(string eventKey, string[] sendPicsInfo)
        /// <summary>
        /// 获取弹出拍照或者相册发图的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="sendPicsInfo">发送的图片信息</param>
        /// <returns>弹出拍照或者相册发图的事件请求对象</returns>
        public static Request GetEventPicPhotoOrAlbum(string eventKey, string[] sendPicsInfo)
        {
            return GetRequest(EventPicPhotoOrAlbum, eventKey, sendPicsInfo.Length,
                String.Join("", sendPicsInfo.Select(i => String.Format("<item><PicMd5Sum><![CDATA[{0}]]></PicMd5Sum></item>", i))));
        } 
        #endregion

        #region 获取弹出系统拍照发图的事件请求对象 public static Request GetEventPicSysPhoto(string eventKey, string[] sendPicsInfo)
        /// <summary>
        /// 获取弹出系统拍照发图的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="sendPicsInfo">发送的图片信息</param>
        /// <returns>弹出系统拍照发图的事件请求对象</returns>
        public static Request GetEventPicSysPhoto(string eventKey, string[] sendPicsInfo)
        {
            return GetRequest(EventPicSysPhoto, eventKey, sendPicsInfo.Length,
                String.Join("", sendPicsInfo.Select(i => String.Format("<item><PicMd5Sum><![CDATA[{0}]]></PicMd5Sum></item>", i))));
        } 
        #endregion

        #region 获取弹出微信相册发图器的事件请求对象 public static Request GetEventPicWeixin(string eventKey, string[] sendPicsInfo)
        /// <summary>
        /// 获取弹出微信相册发图器的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="sendPicsInfo">发送的图片信息</param>
        /// <returns>弹出微信相册发图器的事件请求对象</returns>
        public static Request GetEventPicWeixin(string eventKey, string[] sendPicsInfo)
        {
            return GetRequest(EventPicWeixin, eventKey, sendPicsInfo.Length,
                String.Join("", sendPicsInfo.Select(i => String.Format("<item><PicMd5Sum><![CDATA[{0}]]></PicMd5Sum></item>", i))));
        } 
        #endregion

        #region 获取扫码推事件的事件请求对象 public static Request GetEventScanCodePush(string eventKey, string scanCodeInfo, string scanType, string scanResult)
        /// <summary>
        /// 获取扫码推事件的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="scanCodeInfo">扫描信息</param>
        /// <param name="scanType">扫描类型，一般是qrcode</param>
        /// <param name="scanResult">扫描结果，即二维码对应的字符串信息</param>
        /// <returns>扫码推事件的事件请求对象</returns>
        public static Request GetEventScanCodePush(string eventKey, string scanCodeInfo, string scanType, string scanResult)
        {
            return GetRequest(EventScanCodePush, eventKey, scanCodeInfo, scanType, scanResult);
        } 
        #endregion

        #region 获取扫码推事件且弹出“消息接收中”提示框的事件对象 public static Request GetEventScanCodeWaitMsg(string eventKey, string scanCodeInfo, string scanType, string scanResult)
        /// <summary>
        /// 获取扫码推事件且弹出“消息接收中”提示框的事件对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，由开发者在创建菜单时设定</param>
        /// <param name="scanCodeInfo">扫描信息</param>
        /// <param name="scanType">扫描类型，一般是qrcode</param>
        /// <param name="scanResult">扫描结果，即二维码对应的字符串信息</param>
        /// <returns>扫码推事件且弹出“消息接收中”提示框的事件对象</returns>
        public static Request GetEventScanCodeWaitMsg(string eventKey, string scanCodeInfo, string scanType, string scanResult)
        {
            return GetRequest(EventScanCodeWaitMsg, eventKey, scanCodeInfo, scanType, scanResult);
        } 
        #endregion

        #region 获取点击菜单跳转链接时的事件请求对象 public static Request GetEventView(string eventKey)
        /// <summary>
        /// 获取点击菜单跳转链接时的事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，设置的跳转URL</param>
        /// <returns>点击菜单跳转链接时的事件请求对象</returns>
        public static Request GetEventView(string eventKey)
        {
            return GetRequest(EventView, eventKey);
        }  
        #endregion
        #endregion

        #region 上报地理位置事件请求内容
        /// <summary>
        /// 上报地理位置事件请求内容
        /// </summary>
        private const string EventLocation =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[LOCATION]]></Event><Latitude>{0}</Latitude><Longitude>{1}</Longitude><Precision>{2}</Precision></xml>";
        #endregion

        #region 推送群发结果事件请求内容
        /// <summary>
        /// 推送群发结果事件请求内容
        /// </summary>
        private const string EventMasssEndJobFinish =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[MASSSENDJOBFINISH]]></Event><MsgID>{0}</MsgID><Status><![CDATA[{1}]]></Status><TotalCount>{2}</TotalCount><FilterCount>{3}</FilterCount><SentCount>{4}</SentCount><ErrorCount>{5}</ErrorCount></xml>";
        #endregion

        #region 带参数二维码事件请求内容
        /// <summary>
        /// 带参数二维码事件请求内容
        /// </summary>
        private const string EventScan =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[SCAN]]></Event><EventKey><![CDATA[{0}]]></EventKey><Ticket><![CDATA[{1}]]></Ticket></xml>";
        #endregion

        #region 关注事件请求内容
        /// <summary>
        /// 关注事件请求内容
        /// </summary>
        private const string EventSubscribe =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[subscribe]]></Event></xml>";
        #endregion

        #region 带参数二维码关注事件请求内容
        /// <summary>
        /// 带参数二维码关注事件请求内容
        /// </summary>
        private const string EventSubscribeByQRScene =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[subscribe]]></Event><EventKey><![CDATA[qrscene_{0}]]></EventKey><Ticket><![CDATA[{1}]]></Ticket></xml>";
        #endregion

        #region 取消关注事件请求内容
        /// <summary>
        /// 取消关注事件请求内容
        /// </summary>
        private const string EventUnsubscribe =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[unsubscribe]]></Event></xml>";
        #endregion

        #region 发送模板消息事件请求内容
        /// <summary>
        /// 发送模板消息事件请求内容
        /// </summary>
        private const string TemplateSendJobFinish =
            "<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[TEMPLATESENDJOBFINISH]]></Event><MsgID>{0}</MsgID><Status><![CDATA[{1}]]></Status></xml>";
        #endregion  

        #region 获取上报地理位置事件请求对象 public static Request GetEventLocation(string latitude, string longitude, string precision)
        /// <summary>
        /// 获取上报地理位置事件请求对象
        /// </summary>
        /// <param name="latitude">地理位置纬度</param>
        /// <param name="longitude">地理位置经度</param>
        /// <param name="precision">地理位置精度</param>
        /// <returns>上报地理位置事件请求对象</returns>
        public static Request GetEventLocation(string latitude, string longitude, string precision)
        {
            return GetRequest(EventLocation, latitude, longitude, precision);
        } 
        #endregion

        #region 获取推送群发结果事件请求对象 public static Request GetEventMasssEndJobFinish(string msgID, string status, int totalCount, int filterCount, int sentCount, int errorCount)
        /// <summary>
        /// 获取推送群发结果事件请求对象
        /// </summary>
        /// <param name="msgID">群发的消息ID</param>
        /// <param name="status">
        /// 群发的结构，为“send success”或“send fail”或“err(num)”。
        /// 但send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。
        /// err(num)是审核失败的具体原因，可能的情况如下：
        /// err(10001), //涉嫌广告 
        /// err(20001), //涉嫌政治 
        /// err(20004), //涉嫌社会 
        /// err(20002), //涉嫌色情 
        /// err(20006), //涉嫌违法犯罪 
        /// err(20008), //涉嫌欺诈 
        /// err(20013), //涉嫌版权 
        /// err(22000), //涉嫌互推(互相宣传) 
        /// err(21000), //涉嫌其他
        /// </param>
        /// <param name="totalCount">group_id下粉丝数；或者openid_list中的粉丝数</param>
        /// <param name="filterCount">
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，
        /// 准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </param>
        /// <param name="sentCount">发送成功的粉丝数</param>
        /// <param name="errorCount">发送失败的粉丝数</param>
        /// <returns>推送群发结果事件请求对象</returns>
        public static Request GetEventMasssEndJobFinish(string msgID, string status, int totalCount, int filterCount, int sentCount, int errorCount)
        {
            return GetRequest(EventMasssEndJobFinish, msgID, status, totalCount, filterCount, sentCount, errorCount);
        } 
        #endregion

        #region 获取带参数二维码事件请求对象 public static Request GetEventScan(string eventKey, string ticket)
        /// <summary>
        /// 获取带参数二维码事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id</param>
        /// <param name="ticket">二维码的ticket，可用来换取二维码图片</param>
        /// <returns>带参数二维码事件请求对象</returns>
        public static Request GetEventScan(string eventKey, string ticket)
        {
            return GetRequest(EventScan, eventKey, ticket);
        } 
        #endregion

        #region 获取关注事件请求对象 public static Request GetEventSubscribe()
        /// <summary>
        /// 获取关注事件请求对象
        /// </summary>
        /// <returns>关注事件请求对象</returns>
        public static Request GetEventSubscribe()
        {
            return GetRequest(EventSubscribe);
        } 
        #endregion

        #region 获取带参数二维码关注事件请求对象 public static Request GetEventSubscribeByQRScene(string eventKey, string ticket)
        /// <summary>
        /// 获取带参数二维码关注事件请求对象
        /// </summary>
        /// <param name="eventKey">事件KEY值，qrscene_为前缀，后面为二维码的参数值</param>
        /// <param name="ticket">	二维码的ticket，可用来换取二维码图片</param>
        /// <returns>带参数二维码关注事件请求对象</returns>
        public static Request GetEventSubscribeByQRScene(string eventKey, string ticket)
        {
            return GetRequest(EventSubscribeByQRScene, eventKey, ticket);
        } 
        #endregion

        #region 获取取消关注事件请求对象 public static Request GetEventUnsubscribe()
        /// <summary>
        /// 获取取消关注事件请求对象
        /// </summary>
        /// <returns>取消关注事件请求对象</returns>
        public static Request GetEventUnsubscribe()
        {
            return GetRequest(EventUnsubscribe);
        } 
        #endregion

        #region 获取发送模板消息事件请求对象 public static Request GetTemplateSendJobFinish(string msgID, string status)
        /// <summary>
        /// 获取发送模板消息事件请求对象
        /// </summary>
        /// <param name="msgID">消息id</param>
        /// <param name="status">发送状态</param>
        /// <returns>发送模板消息事件请求对象</returns>
        public static Request GetTemplateSendJobFinish(string msgID, string status)
        {
            return GetRequest(TemplateSendJobFinish, msgID, status);
        }
        #endregion 
        #endregion

        #region 获取请求 private static Request GetRequest(string content, params object[] paramList)
        /// <summary>
        /// 获取请求
        /// </summary>
        /// <param name="content">消息模板</param>
        /// <param name="paramList">其余参数列表</param>
        /// <returns>请求对象</returns>
        private static Request GetRequest(string content, params object[] paramList)
        {
            return new Request(
                            GetNewSignature(),
                            TestTimestamp, 
                            TestNonce, 
                            null,
                            String.Format(content
                                .Replace("{ToUserName}", TestToUserName)
                                .Replace("{FromUserName}", TestFromUserName)
                                .Replace("{CreateTime}", DateTimeHelper.GetLongTimeByDateTime(DateTime.Now).ToString())
                                .Replace("{MsgId}", Guid.NewGuid().ToString().Replace("-", "")),
                            paramList));
        }
        #endregion

        #region 获取请求（首次验证请求） public static Request GetRequest()
        /// <summary>
        /// 获取请求（首次验证请求）
        /// </summary>
        /// <returns>请求对象（首次验证请求）</returns>
        public static Request GetRequest()
        {
            return new Request(
                            GetNewSignature(),
                            TestTimestamp,
                            TestNonce,
                            TestEchostr,
                            null);
        }
        #endregion

        #region 获取新的Signature private static string GetNewSignature()
        /// <summary>
        /// 获取新的Signature
        /// </summary>
        /// <returns>新的Signature</returns>
        private static string GetNewSignature()
        {
            string[] arr = new[] 
            { 
                GlobalManager.ConfigManager.Config.Base.Token, 
                TestTimestamp, 
                TestNonce
            }.OrderBy(z => z).ToArray();

            return SecurityHelper.SHA1_Encrypt(string.Join("", arr));
        }
        #endregion
    }
}
