微信公共平台SDK(c# .Net4.0)
==================
Wing.WeiXin.MP.SDK
==================
```
目前已经初步支持微信公共平台的全部API。
今后可能会进一步的改进优化这个SDK，欢迎大家交流
QQ群：203230922
```

```
PS: V2 与 V1 不兼容， 请谨慎切换
```

快速上手
----------------
###配置文件
1.首先在配置文件（Web.config）中添加节点配置
```
<sectionGroup name="WeiXinMPSDKConfigGroup">
     <section name="Base" type="Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig.BaseConfigSection, Wing.WeiXin.MP.SDK" />
     <section name="Event" type="Wing.WeiXin.MP.SDK.ConfigSection.EventConfig.EventConfigSection, Wing.WeiXin.MP.SDK" />
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
    <Event>
    </Event>
</WeiXinMPSDKConfigGroup>
```

###一般处理程序（.ashx）调用 （新的配置信息后续将会更新）
```C#

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
</WeiXinMPSDKConfigGroup>
```
###基础配置说明（Base）
```
Token        Token
AccountList  账号列表
AppID        AppID
AppSecret    AppSecret
```
###事件配置说明（Event）
```
EventList                          事件处理列表
Name                               事件名称
IsAction                           是否开启该事件
QuickConfigReturnMessageList       快速配置回复消息列表
Key                                快速配置回复消息关键字(事件从属的微信公共平台账号ID:关键字)
Path                               快速配置回复消息路径