(function ($) {
    "use strict";

    //工具名称
    var toolName = 'weixindebug';

    //消息显示延迟
    var alertMessageTimeout = 2000;

    //工具ID前缀
    var toolIDPrefix = 'weixin-tool-debug-';

    //设置
    var settings;

    $.fn[toolName] = function (options) {
        /// <summary>调试工具</summary>
        /// <param name="options" type="Object">参数</param>

        settings = $.extend({
            submitMessageUrl: '/DebugTool?Mode=SubmitMessage',
            refreshServerUrl: '/DebugTool?Mode=RefreshServer',
            account: '',
            openid: ''
        }, options);

        loadTab($(this));
    };

    function loadTab(dom) {
        /// <summary>
        /// 加载Tab
        /// </summary>
        /// <param name="dom" type="Object">工具Dom</param>

        dom.html(
            '<div role="tabpanel">' +
                '<ul class="nav nav-tabs" role="tablist">' +
                    '<li role="presentation" class="active">' +
                        '<a href="#' + toolIDPrefix + 'tab-test-text" aria-controls="' + toolIDPrefix + 'tab-test-text" role="tab" data-toggle="tab">调试文本消息</a>' +
                    '</li>' +
                    '<li role="presentation">' +
                        '<a href="#' + toolIDPrefix + 'tab-test-event-click" aria-controls="' + toolIDPrefix + 'tab-test-event-click" role="tab" data-toggle="tab">调试菜单Click消息</a>' +
                    '</li>' +
                    '<li role="presentation">' +
                        '<a href="#' + toolIDPrefix + 'tab-test-custom" aria-controls="' + toolIDPrefix + 'tab-test-custom" role="tab" data-toggle="tab">调试自定义消息</a>' +
                    '</li>' +
                    '<li role="presentation">' +
                        '<a href="#' + toolIDPrefix + 'tab-server" aria-controls="' + toolIDPrefix + 'tab-server" role="tab" data-toggle="tab">服务器操作</a>' +
                    '</li>' +
                '</ul>' +
                '<div class="tab-content">' +
                    '<div role="tabpanel" class="tab-pane active" id="' + toolIDPrefix + 'tab-test-text">' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-text-account" class="sr-only">微信账号</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-text-account" name="account" class="form-control" placeholder="微信账号" value="' + settings.account + '">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-flag"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-text-open-id" class="sr-only">微信用户OpenID</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-text-open-id" name="open-id" class="form-control" placeholder="微信用户OpenID" value="' + settings.openid + '">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-user"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-text-content" class="sr-only">文本消息内容</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-text-content" name="content" class="form-control" placeholder="文本消息内容">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-file"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<button type="button" class="btn btn-primary" id="' + toolIDPrefix + 'tab-test-text-submit">提交</button>' +
                        '</div>' +
                    '</div>' +
                    '<div role="tabpanel" class="tab-pane" id="' + toolIDPrefix + 'tab-test-event-click">' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-event-click-account" class="sr-only">微信账号</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-event-click-account" name="account" class="form-control" placeholder="微信账号" value="' + settings.account + '">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-flag"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-event-click-open-id" class="sr-only">微信用户OpenID</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-event-click-open-id" name="open-id" class="form-control" placeholder="微信用户OpenID" value="' + settings.openid + '">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-user"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-event-click-event-key" class="sr-only">事件Key</label>' +
                            '<div class="input-group">' +
                                '<input type="text" id="' + toolIDPrefix + 'tab-test-event-click-event-key" name="event-key" class="form-control" placeholder="事件Key">' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-file"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<button type="button" class="btn btn-primary" id="' + toolIDPrefix + 'tab-test-event-click-submit">提交</button>' +
                        '</div>' +
                    '</div>' +
                    '<div role="tabpanel" class="tab-pane" id="' + toolIDPrefix + 'tab-test-custom">' +
                        '<div class="form-group">' +
                            '<label for="' + toolIDPrefix + 'tab-test-custom-content" class="sr-only">正文</label>' +
                            '<div class="input-group">' +
                                '<textarea id="' + toolIDPrefix + 'tab-test-custom-content" name="content" class="form-control" rows="10" placeholder="正文"></textarea>' +
                                '<div class="input-group-addon">' +
                                    '<button class="glyphicon glyphicon-file"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<button type="button" class="btn btn-primary" id="' + toolIDPrefix + 'tab-test-custom-submit">提交</button>' +
                        '</div>' +
                    '</div>' +
                    '<div role="tabpanel" class="tab-pane" id="' + toolIDPrefix + 'tab-server">' +
                        '<div class="btn-group" role="group">' +
                            '<button type="button" id="' + toolIDPrefix + 'tab-server-refrush-server" class="btn btn-primary">重启服务器</button>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>');

        bindButton();
    }

    function bindButton() {
        /// <summary>
        /// 绑定按钮事件
        /// </summary>

        $('#' + toolIDPrefix + 'tab-test-text-submit').click(function() {
            testTextSubmit();
        });
        $('#' + toolIDPrefix + 'tab-test-event-click-submit').click(function() {
            testEventClickSubmit();
        });
        $('#' + toolIDPrefix + 'tab-test-custom-submit').click(function() {
            testCustomSubmit();
        });
        $('#' + toolIDPrefix + 'tab-server-refrush-server').click(function() {
            refrushServer();
        });
    }

    function testTextSubmit() {
        /// <summary>
        /// 提交测试文本消息
        /// </summary>
        var account = $('#' + toolIDPrefix + 'tab-test-text-account').val();
        if (account.length < 1) {
            messageAlert('微信账号不能为空');
            return;
        }
        var openid = $('#' + toolIDPrefix + 'tab-test-text-open-id').val();
        if (openid.length < 1) {
            messageAlert('OpenID不能为空');
            return;
        }
        var content = $('#' + toolIDPrefix + 'tab-test-text-content').val();
        submitMessage(
            '<xml>' +
                '<ToUserName><![CDATA[' + account + ']]></ToUserName>' +
                '<FromUserName><![CDATA[' + openid + ']]></FromUserName> ' +
                '<CreateTime>1348831860</CreateTime>' +
                '<MsgType><![CDATA[text]]></MsgType>' +
                '<Content><![CDATA[' + content + ']]></Content>' +
                '<MsgId>1234567890123456</MsgId>' +
            '</xml>');
    }

    function testEventClickSubmit() {
        /// <summary>
        /// 提交测试菜单Click消息
        /// </summary>
        var account = $('#' + toolIDPrefix + 'tab-test-event-click-account').val();
        if (account.length < 1) {
            messageAlert('微信账号不能为空');
            return;
        }
        var openid = $('#' + toolIDPrefix + 'tab-test-event-click-open-id').val();
        if (openid.length < 1) {
            messageAlert('OpenID不能为空');
            return;
        }
        var eventkey = $('#' + toolIDPrefix + 'tab-test-event-click-event-key').val();
        if (eventkey.length < 1) {
            messageAlert('事件Key不能为空');
            return;
        }
        submitMessage(
            '<xml>' +
                '<ToUserName><![CDATA[' + account + ']]></ToUserName>' +
                '<FromUserName><![CDATA[' + openid + ']]></FromUserName>' +
                '<CreateTime>123456789</CreateTime>' +
                '<MsgType><![CDATA[event]]></MsgType>' +
                '<Event><![CDATA[CLICK]]></Event>' +
                '<EventKey><![CDATA[' + eventkey + ']]></EventKey>' +
            '</xml>');
    }

    function testCustomSubmit() {
        /// <summary>
        /// 提交测试自定义消息
        /// </summary>
        var content = $('#' + toolIDPrefix + 'tab-test-custom-content').val();
        if (content.length < 1) {
            messageAlert('正文不能为空');
            return;
        }
        submitMessage(content);
    }

    function refrushServer() {
        /// <summary>
        /// 刷新服务器
        /// </summary>

        confirm('确定刷新服务器?', function () {
            showLoading();
            $.getJSON(settings.refreshServerUrl, function (data) {
                hideLoading();
                messageAlert(data.msg);
            });
        });
    }

    function submitMessage(msg) {
        /// <summary>
        /// 提交消息
        /// </summary>
        /// <param name="result" type="String">测试消息结果</param>

        showLoading();
        $.post(settings.submitMessageUrl, { Data: window.escape(msg) }, function(data) {
            if (typeof data.msg != 'undefined') {
                hideLoading();
                messageAlert(data.msg);
                return;
            }
            hideLoading();
            showResult(data.data);
        });
    }

    function showResult(result) {
        /// <summary>
        /// 显示测试消息结果
        /// </summary>
        /// <param name="result" type="String">测试消息结果</param>

        $('body').append(
            '<div id="' + toolIDPrefix + 'show-result" class="modal fade" role="dialog" aria-hidden="true">' +
                '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                        '<div class="modal-header">' +
                            '<h4 class="modal-title">测试消息结果</h4>' +
                        '</div>' +
                        '<div class="modal-body">' +
                            '<p><strong>' + result + '</strong></p>' +
                        '</div>' +
                        '<div class="modal-footer">' +
                            '<button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>');
        $('#' + toolIDPrefix + 'show-result').on('hidden.bs.modal', function () {
            $(this).remove();
        });
        $('#' + toolIDPrefix + 'show-result').modal('show');
    }

    function confirm(msg, okCallback) {
        /// <summary>
        /// 确定提示
        /// </summary>
        /// <param name="msg" type="String">确定提示内容</param>
        /// <param name="okCallback" type="Function">确定回调事件</param>


        $('body').append('<div id="' + toolIDPrefix + 'confirm" class="modal fade" role="dialog" aria-hidden="true">' +
			'<div class="modal-dialog">' +
				'<div class="modal-content">' +
					'<div class="modal-header">' +
						'<h4 class="modal-title">提示</h4>' +
					'</div>' +
					'<div class="modal-body">' +
						'<p><strong>' + msg + '</strong></p>' +
					'</div>' +
					'<div class="modal-footer">' +
						'<button type="button" id="' + toolIDPrefix + 'confirm-ok" class="btn btn-primary">确定</button>' +
						'<button type="button" class="btn btn-default" data-dismiss="modal">取消</button>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</div>');
        $('#' + toolIDPrefix + 'confirm').on('hidden.bs.modal', function () {
            $(this).remove();
        });
        $('#' + toolIDPrefix + 'confirm-ok').click(function () {
            okCallback();
            $('#' + toolIDPrefix + 'confirm').modal('hide');
        });
        $('#' + toolIDPrefix + 'confirm').modal('show');
    }

    function showLoading() {
        /// <summary>显示载入框</summary>

        $('body').append(
			'<div id="' + toolIDPrefix + 'loading" class="modal fade" data-backdrop="static">' +
				'<div class="modal-dialog">' +
					'<div class="modal-content">' +
						'<div class="modal-body">' +
							'<p>Loading。。。</p>' +
						'</div>' +
					'</div>' +
				'</div>' +
			'</div>');
        $('#' + toolIDPrefix + 'loading').on('hidden.bs.modal', function () {
            $(this).remove();
        });
        $('#' + toolIDPrefix + 'loading').modal('show');
    }

    function hideLoading() {
        /// <summary>隐藏载入框</summary>

        $('#' + toolIDPrefix + 'loading').modal('hide');
    }

    function messageAlert(msg) {
        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="msg" type="String">消息</param>

        $('body').append(
            '<div id="' + toolIDPrefix + 'message-alert" class="alert alert-info alert-dismissible fade in" role="alert" style="display:none;z-index:9999;position:fixed;left:0;top:0;right:0">' +
                '<strong>' + msg + '</strong>' +
            '</div>');
        $('#' + toolIDPrefix + 'message-alert').slideDown('fast');
        setTimeout(function () {
            $('#' + toolIDPrefix + 'message-alert').alert('close');
        }, alertMessageTimeout);
    }
}(jQuery));