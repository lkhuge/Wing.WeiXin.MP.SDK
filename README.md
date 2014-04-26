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
     <section name="Base" type="Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig.BaseConfigSection, Wing.WeiXin.MP.SDK" />
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
    <Base Token="xxxxxxxx">
      <AccountList>
        <add WeixinMPID="xxxxxx" WeixinMPType="Service" AppID="xxxxx" AppSecret="xxxxx" />
      </AccountList>
    </Base>
    <Debug IsDebug="True" />
    <Event>
    </Event>
    <Log>
        <Base IsLog="True" />
        <RollingFileAppender Path="C:\\" />
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
```
###事件处理优先级
```
全局事件 > 快速配置回复事件 > 自定义事件
```
###添加实体处理对象
```C#
//初始化实体处理对象
EventHandleManager.Init(
    new Dictionary<string, EntityHandler>
    {
        {"xxx(微信公共账号ID)", 
            new EntityHandler
            {
                //添加全局事件
                GlobalHandlerList = new Dictionary<string, EntityHandler.GlobalEntityHandler>
                {
                    {"global1", globalEntityEvent}
                }, 

                //添加自定义事件
                MessageTextHandlerList = new Dictionary<string, EntityHandler.CustomEntityHandler<MessageText>>
                {
                    {"textEvent1", MessageTextEntityEvent}
                }
            }}
    }
);
```

```C#
public IReturn globalEntityEvent(BaseReceiveMessage message)
{
    return new ReturnMessageText
    {
        FromUserName = message.ToUserName,
        ToUserName = message.FromUserName,
        CreateTime = Message.GetLongTimeNow(),
        content = "Hello World"
    };
}
```

```C#
public IReturn MessageTextEntityEvent(MessageText message)
{
    return new ReturnMessageText
    {
        FromUserName = message.ToUserName,
        ToUserName = message.FromUserName,
        CreateTime = Message.GetLongTimeNow(),
        content = "Hello World"
    };
}
```

配置说明
----------------
###全部配置
```
<WeiXinMPSDKConfigGroup>
    <Base Token="xxxxxxxx">
      <AccountList>
        <add WeixinMPID="xxxxxx" WeixinMPType="Service" AppID="xxxxx" AppSecret="xxxxx" />
        <add WeixinMPID="xxxxxx" WeixinMPType="Subscription" AppID="xxxxx" AppSecret="xxxxxx" />
      </AccountList>
    </Base>
    <Debug IsDebug="True" />
    <Event>
        <EventList>
            <add Name="Global:Event1" IsAction="True" />
            <add Name="Custom:Event2" IsAction="True" />
        </EventList>
        <QuickConfigReturnMessageList>
            <add Key="xxxxxx:111" Path="C:\" />
            <add Key="xxxxxx:222" Path="C:\" />
        </QuickConfigReturnMessageList>
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
Token        Token
AccountList  账号列表
AppID        AppID
AppSecret    AppSecret

```
###调试配置说明（Debug）
```
IsDebug     是否启动调试
```
###事件配置说明（Event）
```
EventList                          事件处理列表
Name                               事件名称
IsAction                           是否开启该事件
QuickConfigReturnMessageList       快速配置回复消息列表
Key                                快速配置回复消息关键字(事件从属的微信公共平台账号ID:关键字)
Path                               快速配置回复消息路径
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