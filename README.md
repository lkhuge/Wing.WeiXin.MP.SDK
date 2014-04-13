微信公共平台SDK(c# .Net4.0)
==================
Wing.WeiXin.MP.SDK
==================
```
目前已经初步支持微信公共平台的全部API。
今后可能会进一步的改进优化这个SDK，欢迎大家交流
QQ群：203230922
```

快速上手
----------------
###配置文件
1.首先在配置文件（Web.config）中添加节点配置
```
<sectionGroup name="WeiXinMPSDKConfigGroup">
     <section name="Base" type="Wing.WeiXin.MP.SDK.ConfigSection.BaseConfigSection, Wing.WeiXin.MP.SDK" />
     <section name="Event" type="Wing.WeiXin.MP.SDK.ConfigSection.EventConfig.EventConfigSection, Wing.WeiXin.MP.SDK" />
    <section name="Debug" type="Wing.WeiXin.MP.SDK.ConfigSection.DebugConfigSection, Wing.WeiXin.MP.SDK" />
    <sectionGroup name="Log">
        <section name="Base" type="Wing.WeiXin.MP.SDK.ConfigSection.LogConfig.LogConfigSection, Wing.WeiXin.MP.SDK" />
        <section name="RollingFileAppender" type="Wing.WeiXin.MP.SDK.ConfigSection.LogConfig.RollingFileAppenderConfigSection, Wing.WeiXin.MP.SDK" />
        <section name="AdoNetAppender" type="Wing.WeiXin.MP.SDK.ConfigSection.LogConfig.AdoNetAppenderConfigSection, Wing.WeiXin.MP.SDK" />
    </sectionGroup>
</sectionGroup>
```

2.添加配置节点
```
<WeiXinMPSDKConfigGroup>
    <Base AppID="" AppSecret="" Token="" />
    <Debug IsDebug="True" />
    <Event>
        <EventList>
            <add Name="Event1" IsAction="True" />
            <add Name="Event2" IsAction="True" />
        </EventList>
    </Event>
    <Log>
        <Base IsLog="True" />
        <RollingFileAppender Path="C:\\" MaximumFileSize="50MB" />
    </Log>
</WeiXinMPSDKConfigGroup>
```

###一般处理程序（.ashx）调用
```C#
public void ProcessRequest(HttpContext context)
{
    ReceiveController.ActionForHttpContext(context);
}
```

事件处理
----------------
###事件处理对象
```
MessageImageEventHandler               (图片消息事件处理)
MessageLinkEventHandler                (链接消息事件处理)
MessageLocationEventHandler            (地理位置消息事件处理)
MessageTextEventHandler                (文本消息事件处理)
MessageVideoEventHandler               (视频消息事件处理)
MessageVoiceEventHandler               (语音消息事件处理)
EventClickEventHandler                 (点击菜单拉取消息时的事件处理)
EventLocationEventHandler              (上报地理位置事件事件处理)
EventSubscribeByQRSceneEventHandler    (带参数二维码关注事件事件处理)
EventSubscribeEventHandler             (关注事件事件处理)
EventUnsubscribeEventHandler           (取消关注事件事件处理)
EventViewEventHandler                  (点击菜单跳转链接时的事件处理)
EventWithQRSceneEventHandler           (带参数二维码事件事件处理)
```
###事件处理优先级
```
全局事件 > 基于微信用户事件 > 基于微信用户分组事件 > 自定义事件
```
###添加实体处理对象
```C#
//初始化实体处理对象
EventHandleManager.Init(new EntityHandler
{
    //添加全局事件
    GlobalHandler = new Func<BaseReceiveMessage,IReturn>[]{globalEntityEvent}, 

    //添加基于微信用户事件
    WXUserBaseHandler = new Dictionary<string, Func<BaseReceiveMessage, IReturn>>
    {
        {"xxxxxxxx", globalEntityEvent}
    },

    //添加基于微信用户分组事件
    WXUserGroupBaseHandler = new Dictionary<int, Func<BaseReceiveMessage, IReturn>>
    {
        {0, globalEntityEvent}
    }

    //添加自定义事件
    CustomEntityHandler = new Dictionary<ReceiveEntityType,Func<BaseReceiveMessage,IReturn>>
    {
        {ReceiveEntityType.MessageText, globalEntityEvent}
    }
});
```

```C#
public IReturn globalEntityEvent(IEvent c)
{
    MessageText text = c as MessageText;
    if (text == null) return null;
    if (String.IsNullOrEmpty(text.Content))
    {
        return new ReturnMessageText
        {
            FromUserName = text.ToUserName,
            ToUserName = text.FromUserName,
            CreateTime = Message.GetLongTimeNow(),
            content = "Hello World"
        };
    }

    return null;
}
```

配置说明
----------------
###全部配置
```
<WeiXinMPSDKConfigGroup>
    <Base AppID="xxxx" AppSecret="xxxx" Token="xxxx" />
    <Debug IsDebug="True" />
    <Event UseGlobalEventHandler="true" 
           UseWXUserGroupBaseEventHandler="true" 
           UseWXUserBaseEventHandler="true" 
           UseCustomEventHandler="true" >
        <EventList>
            <add Name="Event1" IsAction="True" />
            <add Name="Event2" IsAction="True" />
        </EventList>
    </Event>
    <Log>
        <Base IsLog="True" />
        <RollingFileAppender Pattern="记录时间：%d %n日志级别：%-5level %n类：%logger%n描述：%n%m%n%n" 
                             Path="C:\\"
                             MaximumFileSize="50MB" />
        <AdoNetAppender SQLType="SQLServer"
                        ConnectionString="xxxxxxxxxxxx"
                        CommandText="INSERT INTO Log ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception)" />
    </Log>
</WeiXinMPSDKConfigGroup>
```
###基础配置说明（Base）
```
AppID        AppID
AppSecret    AppSecret
Token        Token
```
###调试配置说明（Debug）
```
IsDebug     是否启动调试
```
###事件配置说明（Event）
```
UseGlobalEventHandler              是否开启全局事件处理
UseWXUserGroupBaseEventHandler     是否开启基于微信用户事件处理
UseWXUserBaseEventHandler          是否开启基于微信用户分组事件处理
UseCustomEventHandler              是否开启自定义事件处理
EventList                          事件处理列表
Name                               事件名称
IsAction                           是否开启该事件
```
###日志配置说明（Log）
```
IsLog                     是否记录日志
RollingFileAppender       记录到文件
Pattern                   记录格式
Path                      文件路径
MaximumFileSize           单文件最大容量
AdoNetAppender            记录到数据库
SQLType                   数据库类型
ConnectionString          连接字符串
CommandText               记录到数据库的执行语句
```