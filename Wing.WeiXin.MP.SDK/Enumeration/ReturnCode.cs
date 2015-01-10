using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Enumeration
{
    /// <summary>
    /// 全局返回码
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// 系统繁忙
        /// </summary>
        系统繁忙 = -1,

        /// <summary>
        /// 请求成功
        /// </summary>
        请求成功 = 0,

        /// <summary>
        /// 验证失败
        /// </summary>
        验证失败 = 40001,

        /// <summary>
        /// 不合法的凭证类型
        /// </summary>
        不合法的凭证类型 = 40002,

        /// <summary>
        /// 不合法的OpenID
        /// </summary>
        不合法的OpenID = 40003,

        /// <summary>
        /// 不合法的媒体文件类型
        /// </summary>
        不合法的媒体文件类型 = 40004,

        /// <summary>
        /// 不合法的文件类型
        /// </summary>
        不合法的文件类型 = 40005,

        /// <summary>
        /// 不合法的文件大小
        /// </summary>
        不合法的文件大小 = 40006,

        /// <summary>
        /// 不合法的媒体文件id
        /// </summary>
        不合法的媒体文件id = 40007,

        /// <summary>
        /// 不合法的消息类型
        /// </summary>
        不合法的消息类型 = 40008,

        /// <summary>
        /// 不合法的图片文件大小
        /// </summary>
        不合法的图片文件大小 = 40009,

        /// <summary>
        /// 不合法的语音文件大小
        /// </summary>
        不合法的语音文件大小 = 40010,

        /// <summary>
        /// 不合法的视频文件大小
        /// </summary>
        不合法的视频文件大小 = 40011,

        /// <summary>
        /// 不合法的缩略图文件大小
        /// </summary>
        不合法的缩略图文件大小 = 40012,

        /// <summary>
        /// 不合法的APPID
        /// </summary>
        不合法的APPID = 40013,

        /// <summary>
        /// 不合法的access_token
        /// </summary>
        不合法的access_token = 40014,

        /// <summary>
        /// 不合法的菜单类型
        /// </summary>
        不合法的菜单类型 = 40015,

        /// <summary>
        /// 不合法的按钮个数1
        /// </summary>
        不合法的按钮个数1 = 40016,

        /// <summary>
        /// 不合法的按钮个数2
        /// </summary>
        不合法的按钮个数2 = 40017,

        /// <summary>
        /// 不合法的按钮名字长度
        /// </summary>
        不合法的按钮名字长度 = 40018,

        /// <summary>
        /// 不合法的按钮KEY长度
        /// </summary>
        不合法的按钮KEY长度 = 40019,

        /// <summary>
        /// 不合法的按钮URL长度
        /// </summary>
        不合法的按钮URL长度 = 40020,

        /// <summary>
        /// 不合法的菜单版本号
        /// </summary>
        不合法的菜单版本号 = 40021,

        /// <summary>
        /// 不合法的子菜单级数
        /// </summary>
        不合法的子菜单级数 = 40022,

        /// <summary>
        /// 不合法的子菜单按钮个数
        /// </summary>
        不合法的子菜单按钮个数 = 40023,

        /// <summary>
        /// 不合法的子菜单按钮类型
        /// </summary>
        不合法的子菜单按钮类型 = 40024,

        /// <summary>
        /// 不合法的子菜单按钮名字长度
        /// </summary>
        不合法的子菜单按钮名字长度 = 40025,

        /// <summary>
        /// 不合法的子菜单按钮KEY长度
        /// </summary>
        不合法的子菜单按钮KEY长度 = 40026,

        /// <summary>
        /// 不合法的子菜单按钮URL长度
        /// </summary>
        不合法的子菜单按钮URL长度 = 40027,

        /// <summary>
        /// 不合法的自定义菜单使用用户
        /// </summary>
        不合法的自定义菜单使用用户 = 40028,

        /// <summary>
        /// 不合法的oauth_code
        /// </summary>
        不合法的oauth_code = 40029,

        /// <summary>
        /// 不合法的refresh_token
        /// </summary>
	    不合法的refresh_token = 40030,

        /// <summary>
        /// 不合法的openid列表
        /// </summary>
	    不合法的openid列表 = 40031,

        /// <summary>
        /// 不合法的openid列表长度
        /// </summary>
	    不合法的openid列表长度 = 40032,

        /// <summary>
        /// 不合法的请求字符，不能包含\uxxxx格式的字符
        /// </summary>
	    不合法的请求字符 = 40033,

        /// <summary>
        /// 不合法的参数
        /// </summary>
	    不合法的参数 = 40035,

        /// <summary>
        /// 不合法的请求格式
        /// </summary>
	    不合法的请求格式 = 40038,

        /// <summary>
        /// 不合法的URL长度
        /// </summary>
	    不合法的URL长度 = 40039,

        /// <summary>
        /// 不合法的分组id
        /// </summary>
	    不合法的分组id = 40050,

        /// <summary>
        /// 分组名字不合法
        /// </summary>
	    分组名字不合法 = 40051,

        /// <summary>
        /// 缺少access_token参数
        /// </summary>
        缺少access_token参数 = 41001,

        /// <summary>
        /// 缺少appid参数
        /// </summary>
        缺少appid参数 = 41002,

        /// <summary>
        /// 缺少refresh_token参数
        /// </summary>
        缺少refresh_token参数 = 41003,

        /// <summary>
        /// 缺少secret参数
        /// </summary>
        缺少secret参数 = 41004,

        /// <summary>
        /// 缺少多媒体文件数据
        /// </summary>
        缺少多媒体文件数据 = 41005,

        /// <summary>
        /// 缺少media_id参数
        /// </summary>
        缺少media_id参数 = 41006,

        /// <summary>
        /// 缺少子菜单数据
        /// </summary>
        缺少子菜单数据 = 41007,

        /// <summary>
        /// 缺少oauth_code
        /// </summary>
        缺少oauth_code = 41008,

        /// <summary>
        /// 缺少openid
        /// </summary>
	    缺少openid  = 41009,

        /// <summary>
        /// access_token超时
        /// </summary>
        access_token超时 = 42001,

        /// <summary>
        /// refresh_token超时
        /// </summary>
    	refresh_token超时 = 42002,

        /// <summary>
        /// oauth_code超时
        /// </summary>
	    oauth_code超时 = 42003,

        /// <summary>
        /// 需要GET请求
        /// </summary>
        需要GET请求 = 43001,

        /// <summary>
        /// 需要POST请求
        /// </summary>
        需要POST请求 = 43002,

        /// <summary>
        /// 需要HTTPS请求
        /// </summary>
        需要HTTPS请求 = 43003,

        /// <summary>
        /// 需要接收者关注
        /// </summary>
    	需要接收者关注 = 43004,

        /// <summary>
        /// 需要好友关系
        /// </summary>
	    需要好友关系 = 43005,

        /// <summary>
        /// 多媒体文件为空
        /// </summary>
        多媒体文件为空 = 44001,

        /// <summary>
        /// POST的数据包为空
        /// </summary>
        POST的数据包为空 = 44002,

        /// <summary>
        /// 图文消息内容为空
        /// </summary>
        图文消息内容为空 = 44003,

        /// <summary>
        /// 文本消息内容为空
        /// </summary>
    	文本消息内容为空 = 44004,

        /// <summary>
        /// 多媒体文件大小超过限制
        /// </summary>
        多媒体文件大小超过限制 = 45001,

        /// <summary>
        /// 消息内容超过限制
        /// </summary>
        消息内容超过限制 = 45002,

        /// <summary>
        /// 标题字段超过限制
        /// </summary>
        标题字段超过限制 = 45003,

        /// <summary>
        /// 描述字段超过限制
        /// </summary>
        描述字段超过限制 = 45004,

        /// <summary>
        /// 链接字段超过限制
        /// </summary>
        链接字段超过限制 = 45005,

        /// <summary>
        /// 图片链接字段超过限制
        /// </summary>
        图片链接字段超过限制 = 45006,

        /// <summary>
        /// 语音播放时间超过限制
        /// </summary>
        语音播放时间超过限制 = 45007,

        /// <summary>
        /// 图文消息超过限制
        /// </summary>
        图文消息超过限制 = 45008,

        /// <summary>
        /// 接口调用超过限制
        /// </summary>
        接口调用超过限制 = 45009,

        /// <summary>
        /// 创建菜单个数超过限制
        /// </summary>
        创建菜单个数超过限制 = 45010,

        /// <summary>
        /// 回复时间超过限制
        /// </summary>
    	回复时间超过限制 = 45015,

        /// <summary>
        /// 系统分组不允许修改
        /// </summary>
	    系统分组不允许修改 = 45016,

        /// <summary>
        /// 分组名字过长
        /// </summary>
	    分组名字过长 = 45017,

        /// <summary>
        /// 分组数量超过上限
        /// </summary>
	    分组数量超过上限 = 45018,

        /// <summary>
        /// 不存在媒体数据
        /// </summary>
        不存在媒体数据 = 46001,

        /// <summary>
        /// 不存在的菜单版本
        /// </summary>
        不存在的菜单版本 = 46002,

        /// <summary>
        /// 不存在的菜单数据
        /// </summary>
        不存在的菜单数据 = 46003,

        /// <summary>
        /// 不存在的用户
        /// </summary>
    	不存在的用户 = 46004,

        /// <summary>
        /// 解析JSON_XML内容错误
        /// </summary>
        解析JSON_XML内容错误 = 47001,

        /// <summary>
        /// api功能未授权
        /// </summary>
        api功能未授权 = 48001,

        /// <summary>
        /// 用户未授权该api
        /// </summary>
        用户未授权该api = 50001,

        /// <summary>
        /// 参数错误(invalid parameter)
        /// </summary>
        参数错误 = 61451,

        /// <summary>
        /// 无效客服账号(invalid kf_account)
        /// </summary>
	    无效客服账号 = 61452,

        /// <summary>
        /// 账号已存在(kf_account exsited)
        /// </summary>
	    账号已存在 = 61453,

        /// <summary>
        /// 账号名长度超过限制(前缀10个英文字符)(invalid kf_acount length)
        /// </summary>
	    账号名长度超过限制_前缀10个英文字符 = 61454,

        /// <summary>
        /// 账号名包含非法字符(英文+数字)(illegal character in kf_account)
        /// </summary>
	    账号名包含非法字符_英文_数字 = 61455,

        /// <summary>
        /// 账号个数超过限制(10个客服账号)(kf_account count exceeded)
        /// </summary>
	    账号个数超过限制_10个客服账号 = 61456,

        /// <summary>
        /// 无效头像文件类型(invalid file type)
        /// </summary>
	    无效头像文件类型 = 61457,

        /// <summary>
        /// 请求正常 无语义结果
        /// </summary>
        请求正常_无语义结果 = 7000000,

        /// <summary>
        /// 缺失请求参数
        /// </summary>
        缺失请求参数 = 7000001,

        /// <summary>
        /// signature参数无效
        /// </summary>
        signature参数无效 = 7000002,

        /// <summary>
        /// 地理位置相关配置1无效
        /// </summary>
        地理位置相关配置1无效 = 7000003,

        /// <summary>
        /// 地理位置相关配置2无效
        /// </summary>
        地理位置相关配置2无效 = 7000004,

        /// <summary>
        /// 请求地理位置信息失败
        /// </summary>
        请求地理位置信息失败 = 7000005,

        /// <summary>
        /// 地理位置结果解析失败
        /// </summary>
        地理位置结果解析失败 = 7000006,

        /// <summary>
        /// 内部初始化失败
        /// </summary>
        内部初始化失败 = 7000007,

        /// <summary>
        /// 非法appid 获取密钥失败
        /// </summary>
        非法appid_获取密钥失败 = 7000008,

        /// <summary>
        /// 请求语义服务失败
        /// </summary>
        请求语义服务失败 = 7000009,

        /// <summary>
        /// 非法post请求
        /// </summary>
        非法post请求 = 7000010,

        /// <summary>
        /// post请求json字段无效
        /// </summary>
        post请求json字段无效 = 7000011,

        /// <summary>
        /// 查询query太短
        /// </summary>
        查询query太短 = 7000030,

        /// <summary>
        /// 查询query太长
        /// </summary>
        查询query太长 = 7000031,

        /// <summary>
        /// 城市经纬度信息缺失
        /// </summary>
        城市经纬度信息缺失 = 7000032,

        /// <summary>
        /// query请求语义处理失败
        /// </summary>
        query请求语义处理失败 = 7000033,

        /// <summary>
        /// 获取天气信息失败
        /// </summary>
        获取天气信息失败 = 7000034,

        /// <summary>
        /// 获取股票信息失败
        /// </summary>
        获取股票信息失败 = 7000035,

        /// <summary>
        /// utf8编码转换失败
        /// </summary>
        utf8编码转换失败 = 7000036
    }
}
