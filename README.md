微信公共平台SDK(c# .Net4.0 Mono)
==================
Wing.WeiXin.MP.SDK
==================
```
目前已经初步支持微信公共平台的全部API。
今后可能会进一步的改进优化这个SDK，欢迎大家交流
```

```
Nuget安装命令：
PM> Install-Package Wing.WeiXin.MP.SDK
```

```
V2 : 基于V1重新开发，保留了事件委托的开发方式，大幅度提高性能（实验证明提高35倍）

PS: V2 与 V1 基本不兼容， 请谨慎切换
```

快速上手
----------------
###配置文件
1.首先在配置文件（Web.config）中添加节点(configuration->configSections)
```
<sectionGroup name="WeiXinMPSDKConfigGroup">
    <section name="Base" type="Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig.BaseConfigSection, Wing.WeiXin.MP.SDK" />
    <section name="Event" type="Wing.WeiXin.MP.SDK.ConfigSection.EventConfig.EventConfigSection, Wing.WeiXin.MP.SDK" />
    <section name="Handler" type="Wing.WeiXin.MP.SDK.ConfigSection.HandlerConfig.HandlerConfigSection, Wing.WeiXin.MP.SDK" />
</sectionGroup>
```

2.添加配置节点(configuration)
```
<WeiXinMPSDKConfigGroup>
    <Base Token="xxxxxxxx">
      <AccountList>
        <add WeixinMPID="xxxxxx" AppID="xxxxx" AppSecret="xxxxx" EncodingAESKey="xxxxxxxxx" />
      </AccountList>
    </Base>
    <Event>
        <EventList>
            <add Name="Event1" IsAction="True" />
            <add Name="Event2" IsAction="True" />
        </EventList>
    </Event>
	<Handler>
		<HandlerList>
            <add Name="Receive" IsAction="True" />
        </HandlerList>
	</Handler>
</WeiXinMPSDKConfigGroup>
```

3.添加接收事件处理配置(configuration)
IIS 6.0 Or Mono(jexus)
```
<system.web>
    <httpModules>
        <add name="WeixinModule" type="Wing.WeiXin.MP.SDK.Extension.Module.WeixinModule"/>
    </httpModules>
</system.web>
```

IIS 7.0 （集成模式）(configuration)
```
<system.webServer>
    <modules>
        <add name="WeixinModule" type="Wing.WeiXin.MP.SDK.Extension.Module.WeixinModule" />
    </modules>
</system.webServer>
```

4.添加接收事件
（建议放在Global.asax）
```
GlobalManager.Init();
GlobalManager.EventManager.AddGloablReceiveEvent("Event0", E0);
GlobalManager.EventManager.AddReceiveEvent<RequestText>("Event1", "gh_7f215c8b1c91", E1);
GlobalManager.EventManager.AddReceiveEvent<RequestEventClick>("Event2", "gh_7f215c8b1c91", E2);

private Response E0(Request request)
{
    //......
}

private Response E1(RequestText request)
{
    //......
}

private Response E2(RequestEventClick request)
{
    //......
}
```


配置说明
----------------
###全部配置
```
<WeiXinMPSDKConfigGroup>
    <!-- Token：微信后台网站设置的参数，用于验证用户 -->
    <!-- Debug：是否为Debug模式 -->
    <!-- Log：日志路径 -->
    <Base Token="xxxxxxxx" Debug="True" Log="">
        <!-- 微信公众平台帐号列表，支持多帐号配置开发 -->
        <!-- WeixinMPID：微信公众平台帐号ID，可在微信后台网站查询 -->
        <!-- AppID：微信公众平台应用ID，认证用户或者服务号才可以获取，可在微信后台网站查询 -->
        <!-- AppSecret：微信公众平台应用密钥，认证用户或者服务号才可以获取，可在微信后台网站查询 -->
        <!-- EncodingAESKey：加解密消息的密钥 -->
        <AccountList>
            <add WeixinMPID="xxxxxx" AppID="xxxxx" AppSecret="xxxxx" EncodingAESKey="xxxxxxxxx" />
            <add WeixinMPID="xxxxxx" AppID="xxxxx" AppSecret="xxxxxx" EncodingAESKey="xxxxxxxxx" />
        </AccountList>
    </Base>
    <Event>
        <!-- 事件列表 -->
        <!-- Name：事件名称，添加事件后自定义的事件名称 -->
        <!-- IsAction：是否启用该事件 -->
        <!-- PS: 如果没有填写，则默认为不启用 -->
        <EventList>
            <add Name="Event1" IsAction="True" />
            <add Name="Event2" IsAction="True" />
        </EventList>
    </Event>
	<!-- Sign：标记 添加后当第一级路径为该标记时才去从第二级路径寻找Handler -->
	<!-- Default：默认Handler  可以理解为"默认主页" -->
	<Handler Sign="WX" Default="">
		<!-- Handler列表 -->
        <!-- Name：Handler名称(用于定位Handler) -->
		<!-- Alias：Handler别名(用于更换名称) -->
        <!-- IsAction：是否启用该事件 -->
        <!-- RunInRelease：是否能够运行在正式环境 -->
        <!-- PS: 如果没有填写，则默认为不添加 -->
		<HandlerList>
            <!-- 接受消息主入口 -->
            <add Name="Receive" IsAction="True" RunInRelease="True" />
            <!-- 微信检测入口 -->
            <add Name="Check" IsAction="True" RunInRelease="False" />
            <!-- OAuth入口 -->
            <add Name="OAuth" IsAction="True" RunInRelease="True" />
            <!-- JS配置入口 -->
            <add Name="WeixinConfig" IsAction="True" RunInRelease="True" />
            <!-- 菜单工具入口 -->
            <add Name="MenuTool" IsAction="True" RunInRelease="False" />
            <!-- 调试工具入口 -->
            <add Name="DebugTool" IsAction="True" RunInRelease="False" />
        </HandlerList>
	</Handler>
</WeiXinMPSDKConfigGroup>
```