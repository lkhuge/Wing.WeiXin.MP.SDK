﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wing.WeiXin.MP.SDK.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&se" +
            "cret={1}")]
        public string URLForGetAccessToken {
            get {
                return ((string)(this["URLForGetAccessToken"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}")]
        public string URLForCreateMenu {
            get {
                return ((string)(this["URLForCreateMenu"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}")]
        public string URLForGetMenu {
            get {
                return ((string)(this["URLForGetMenu"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}")]
        public string URLForDeleteMenu {
            get {
                return ((string)(this["URLForDeleteMenu"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}")]
        public string URLForGetWXUser {
            get {
                return ((string)(this["URLForGetWXUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}")]
        public string URLForGetWXUserList {
            get {
                return ((string)(this["URLForGetWXUserList"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}")]
        public string URLForGetWXUserListNext {
            get {
                return ((string)(this["URLForGetWXUserListNext"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}")]
        public string URLForCreateWXUserGroup {
            get {
                return ((string)(this["URLForCreateWXUserGroup"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}")]
        public string URLForGetWXUserGroupList {
            get {
                return ((string)(this["URLForGetWXUserGroupList"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}")]
        public string URLForGetWXUserGroupByWXUser {
            get {
                return ((string)(this["URLForGetWXUserGroupByWXUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}")]
        public string URLForModityWXUserGroup {
            get {
                return ((string)(this["URLForModityWXUserGroup"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}")]
        public string URLForMoveWXUserGroup {
            get {
                return ((string)(this["URLForMoveWXUserGroup"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}")]
        public string URLForSendCSMessage {
            get {
                return ((string)(this["URLForSendCSMessage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}")]
        public string URLForUploadMedia {
            get {
                return ((string)(this["URLForUploadMedia"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}")]
        public string URLForDownloadMedia {
            get {
                return ((string)(this["URLForDownloadMedia"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}")]
        public string URLForCreateQRCodeTicket {
            get {
                return ((string)(this["URLForCreateQRCodeTicket"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}")]
        public string URLForGetQRCode {
            get {
                return ((string)(this["URLForGetQRCode"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("zh_CN")]
        public string Language {
            get {
                return ((string)(this["Language"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&re" +
            "sponse_type=code&scope={2}&state={3}#wechat_redirect")]
        public string URLForOAuthGetCode {
            get {
                return ((string)(this["URLForOAuthGetCode"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&g" +
            "rant_type=authorization_code")]
        public string URLForOAuthGetAccessToken {
            get {
                return ((string)(this["URLForOAuthGetAccessToken"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_t" +
            "oken&refresh_token={1}")]
        public string URLForOAuthRefreshAccessToken {
            get {
                return ((string)(this["URLForOAuthRefreshAccessToken"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}")]
        public string URLForOAuthGetUserInfo {
            get {
                return ((string)(this["URLForOAuthGetUserInfo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}")]
        public string URLForSendAllUploadNews {
            get {
                return ((string)(this["URLForSendAllUploadNews"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}")]
        public string URLForSendAllByGroup {
            get {
                return ((string)(this["URLForSendAllByGroup"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}")]
        public string URLForSendAllByOpenIDList {
            get {
                return ((string)(this["URLForSendAllByOpenIDList"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}")]
        public string URLForSendAllDelete {
            get {
                return ((string)(this["URLForSendAllDelete"]));
            }
        }
    }
}
