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
        /// access_token超时
        /// </summary>
        access_token超时 = 42001,

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
    }
}
