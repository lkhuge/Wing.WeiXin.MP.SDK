/*!
 * Weixin JS Menu Tool v0.0.8
 * 用于图形化操作菜单
 *
 * Dependency：
 * 1.JQuery
 * 2.Bootstrap(V3)
 *
 * Update(v0.0.8)：
 * [Fix]修正当子菜单数量满后删除子菜单后添加按钮没有出现的Bug
 * 
 * Usage：
 * $('#main').weixinmenu({
 *     getUrl: '/Menu?Operation=Get',                   //获取菜单APIUrl
 *     saveUrl: '/Menu?Operation=Save',                 //保存菜单APIUrl
 *     deleteUrl: '/Menu?Operation=Delete',             //删除菜单APIUrl
 *     getOAuthUrlUrl: '/Menu?Operation=GetOAuthUrl',   //获取OAuth地址APIUrl
 *     oauthCallback: ''                                //OAuth回调地址
 * });
 */

(function ($) {
    "use strict";

    //工具名称
    var toolName = 'weixinmenu';

    //主菜单数量
    var mainMenuCount = 3;

    //消息显示延迟
    var alertMessageTimeout = 2000;

    //工具ID前缀
    var toolIDPrefix = 'weixin-tool-menu-';

    //菜单编辑ID前缀
    var toolMenuEditIDPrefix = toolIDPrefix + 'menu-edit-';

    //设置
    var settings;

    $.fn[toolName] = function (options) {
        /// <summary>菜单工具</summary>
        /// <param name="options" type="Object">参数</param>

        settings = $.extend({
            getUrl: '/MenuTool?Operation=Get',
            saveUrl: '/MenuTool?Operation=Save',
            deleteUrl: '/MenuTool?Operation=Delete',
            getOAuthUrlUrl: '/MenuTool?Operation=GetOAuthUrl',
            oauthCallback: ''
        }, options);

        loadData($(this));
    };

    function loadList(dom) {
        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="dom" type="Object">工具Dom</param>

        var html =
            '<div id="' + toolIDPrefix + 'operate" class="btn-group" role="group">' +
                '<button type="button" id="' + toolIDPrefix + 'operate-refrush" class="btn btn-primary">刷新菜单</button>' +
                '<button type="button" id="' + toolIDPrefix + 'operate-save" class="btn btn-success">保存菜单</button>' +
                '<button type="button" id="' + toolIDPrefix + 'operate-delete" class="btn btn-warning">删除菜单</button>' +
            '</div>' +
            '<div id="' + toolIDPrefix + 'main" class="panel-group" role="tablist" aria-multiselectable="true">';
        for (var i = 0; i < mainMenuCount; i++) {
            html +=
                '<div class="panel panel-default">' +
                    '<div class="panel-heading" role="tab" id="' + toolIDPrefix + 'heading-' + i + '" data-toggle="collapse" data-parent="#' + toolIDPrefix + 'main" data-target="#' + toolIDPrefix + 'collapse-' + i + '" aria-expanded="false" aria-controls="' + toolIDPrefix + 'collapse-' + i + '">' +
                        '<h4>主菜单' + (i + 1) + '</h4>' +
                    '</div>' +
                    '<div id="' + toolIDPrefix + 'collapse-' + i + '" class="panel-collapse collapse" role="tabpanel" aria-labelledby="' + toolIDPrefix + 'heading-' + i + '">' +
                        '<div class="panel-body">' +
                            '<div class="panel panel-success">' +
                                '<div id="' + toolIDPrefix + 'head-' + i + '" class="panel-heading"></div>' +
                                '<div id="' + toolIDPrefix + 'body-' + i + '" class="panel-body"></div>' +
                                '<div id="' + toolIDPrefix + 'footer-' + i + '" class="panel-footer btn-group" role="group"></div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>';
        }
        dom.html(html + '</div>');
        bindOperateButton(dom);
    }

    function bindOperateButton(dom) {
        /// <summary>
        /// 绑定操作按钮事件
        /// </summary>
        /// <param name="dom" type="Object">工具Dom</param>

        $('#' + toolIDPrefix + 'operate-refrush').click(function () {
            loadData(dom);
        });
        $('#' + toolIDPrefix + 'operate-save').click(function () {
            confirm('确定保存？', function () {
                showLoading();
                $.post(settings.saveUrl, { Data: window.escape(JSON.stringify(getMenuObj())) }, function (data) {
                    hideLoading();
                    messageAlert(data.msg);
                }, 'json');
            });
        });
        $('#' + toolIDPrefix + 'operate-delete').click(function () {
            confirm('确定删除？', function () {
                showLoading();
                $.post(settings.deleteUrl, function (data) {
                    hideLoading();
                    messageAlert(data.msg);
                }, 'json');
            });
        });
    }

    function getMenuObj() {
        /// <summary>
        /// 获取菜单对象(Wing.WeiXin.MP.SDK.Entities.Menu.ForGet.MenuForGet)
        /// </summary>
        /// <return>菜单对象</return>

        var buttonList = [];
        for (var j = 0; j < mainMenuCount; j++) {
            var name = $('#' + toolIDPrefix + 'head-' + j).text();
            if (name !== '') {
                var type = $('#' + toolIDPrefix + 'head-' + j).data('menu-type');
                if (type === 'main') {
                    var subButtonList = [];
                    $('#' + toolIDPrefix + 'body-' + j).children().children().each(function () {
                        var subType = $(this).find('p[data-menu-type="type"]').eq(0).data('menu-value');
                        var subName = $(this).find('p[data-menu-type="name"]').eq(0).data('menu-value');
                        if (subType === 'view') {
                            subButtonList = subButtonList.concat([{
                                name: subName,
                                type: subType,
                                url: $(this).find('div[data-menu-type="url"]').eq(0).data('menu-value')
                            }]);
                        } else {
                            subButtonList = subButtonList.concat([{
                                name: subName,
                                type: subType,
                                key: $(this).find('p[data-menu-type="key"]').eq(0).data('menu-value')
                            }]);
                        }
                    });
                    buttonList = buttonList.concat([{
                        name: name,
                        sub_button: subButtonList
                    }]);
                } else {
                    if (type === 'view') {
                        buttonList = buttonList.concat([{
                            name: name,
                            type: type,
                            url: $('#' + toolIDPrefix + 'head-' + j).data('menu-url')
                        }]);
                    } else {
                        buttonList = buttonList.concat([{
                            name: name,
                            type: type,
                            key: $('#' + toolIDPrefix + 'head-' + j).data('menu-key')
                        }]);
                    }
                }
            }
        }

        return {
            menu: {
                button: buttonList
            }
        };
    }

    function loadData(dom) {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dom" type="Object">工具Dom</param>

        showLoading();
        loadList(dom);
        $.getJSON(settings.getUrl, function (data) {
            if (typeof data.msg != 'undefined') {
                messageAlert(data.msg);
                data = { button: [] };
            }
            for (var i = 0; i < mainMenuCount; i++) {
                if (i < data.button.length) {
                    $('#' + toolIDPrefix + 'head-' + i).text(data.button[i].name);
                    setMenu(i, data.button[i], true, true);
                } else {
                    setButton(i);
                }
            }
            hideLoading();
        });
    }

    function setMenu(id, obj, isMain, isNew, subDom) {
        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="id" type="Number">菜单ID</param>
        /// <param name="obj" type="Object">菜单节点对象</param>
        /// <param name="isMain" type="Boolean">是否为主菜单</param>
        /// <param name="isNew" type="Boolean">是否为新建</param>
        /// <param name="subDom" type="Object">子菜单Dom（如果不为空则为修改）</param>

        if (typeof subDom != 'undefined' && subDom != null) {
            subDom.html(getMenuItem(obj));
            return;
        }
        if (isMain) {
            $('#' + toolIDPrefix + 'head-' + id).text(obj.name);
            if (typeof obj.type == 'undefined') {
                //在主菜单节点上新建主菜单
                $('#' + toolIDPrefix + 'head-' + id).attr('data-menu-type', 'main');
                $('#' + toolIDPrefix + 'body-' + id).html('<ul class="list-group"></ul>');
                if (typeof obj.sub_button != 'undefined' && obj.sub_button.length > 0) {
                    obj.sub_button.forEach(function (d) {
                        $('#' + toolIDPrefix + 'body-' + id).children().append(
                            '<li class="list-group-item ' + toolIDPrefix + 'menu-item">' +
                            getMenuItem(d) + '</li>');
                    });
                }
                setButton(id, $('#' + toolIDPrefix + 'body-' + id).children().children().length);
            } else {
                //在主菜单节点上新建子菜单
                $('#' + toolIDPrefix + 'head-' + id).attr('data-menu-type', obj.type);
                if (obj.type === 'view') {
                    $('#' + toolIDPrefix + 'head-' + id).attr('data-menu-url', obj.url);
                } else {
                    $('#' + toolIDPrefix + 'head-' + id).attr('data-menu-key', obj.key);
                }
                $('#' + toolIDPrefix + 'body-' + id).html(getMenuItem(obj));
                setButton(id, -1);
            }
        } else {
            //在子菜单节点上新建子菜单
            $('#' + toolIDPrefix + 'body-' + id).children().append(
                '<li class="list-group-item ' + toolIDPrefix + 'menu-item">' + getMenuItem(obj) + '</li>');
            setButton(id, $('#' + toolIDPrefix + 'body-' + id).children().children().length);
        }

        $('.' + toolIDPrefix + 'menu-item p').unbind();
        $('.' + toolIDPrefix + 'menu-item p').click(function () {
            menuEdit(false, false, id, $(this));
        });
    }

    function getMenuItem(obj) {
        /// <summary>
        /// 根据子菜单节点对象获取菜单内容
        /// </summary>
        /// <param name="obj" type="Object">子菜单节点对象</param>

        var name = '<p data-menu-type="name" data-menu-value="' + obj.name + '"><strong>显示标题：' + obj.name + '</strong></p>';
        var type = '<p data-menu-type="type" data-menu-value="' + obj.type + '">类型：' + obj.type + '(' + getMenuTypeName(obj.type) + ')</p>';
        var key = (obj.type === 'view')
			? '<div data-menu-type="url" data-menu-value="' + obj.url + '">URL：<pre>' + obj.url + '</pre></div>'
			: '<p data-menu-type="key" data-menu-value="' + obj.key + '">Key：' + obj.key + '</p>';
        var opera = '<div class="panel-footer btn-group" role="group">' +
                        '<button type="button" class="btn btn-warning" data-menu-event="DeleteSub">删除</button>' +
                        '<button type="button" class="btn btn-warning" data-menu-event="MoveUpSub">上移</button>' +
                        '<button type="button" class="btn btn-warning" data-menu-event="MoveDownSub">下移</button>' +
                    '</div>';
        return name + type + key + opera;
    }

    function setButton(id, count) {
        /// <summary>
        /// 设置按钮信息
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="count" type="Number">子菜单数量（不填为没有主菜单，负数则为非主菜单）</param>

        addButtonDeleteSub(id);
        addButtonMoveUpSub(id);
        addButtonMoveDownSub(id);

        if (typeof count == 'undefined') {
            addButtonAddMain(id, true);
            return;
        }
        if (count < 0) {
            addButtonModityMain(id, true);
            addButtonDeleteMain(id);
            return;
        }
        if (count === 0) {
            addButtonAddSub(id, true);
            addButtonModityMain(id);
            addButtonDeleteMain(id);
            return;
        }
        if (count === 5) {
            addButtonModityMain(id, true);
            return;
        }
        addButtonAddSub(id, true);
        addButtonModityMain(id);
    }

    function addButtonAddSub(id, empty) {
        /// <summary>
        /// 添加添加子菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="empty" type="Boolean">添加之前是否清空</param>

        if (typeof empty != 'undefined' && empty) {
            $('#' + toolIDPrefix + 'footer-' + id).empty();
        }
        var html = '<button type="button" class="btn btn-info" data-menu-event="AddSub" data-menu-id="' + id + '">添加子菜单</button>';
        $('#' + toolIDPrefix + 'footer-' + id).append(html);
        $('button[data-menu-event="AddSub"][data-menu-id="' + id + '"]').unbind();
        $('button[data-menu-event="AddSub"][data-menu-id="' + id + '"]').click(function () {
            menuEdit(true, false, id);
        });
    }

    function addButtonDeleteSub(id) {
        /// <summary>
        /// 添加删除子菜单按钮
        /// </summary>

        $('button[data-menu-event="DeleteSub"]').unbind();
        $('button[data-menu-event="DeleteSub"]').click(function () {
            var btn = $(this);
            confirm('确定删除？', function () {
                //获取子菜单数量
                var main = btn.parents('ul').eq(0);
                var list = main.children('li');
                var num = list.length;

                btn.parents('li').eq(0).remove();
                setButton(id, num - 1);
            });
        });
    }

    function addButtonMoveUpSub(id) {
        /// <summary>
        /// 添加上移子菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>

        $('button[data-menu-event="MoveUpSub"]').unbind();
        $('button[data-menu-event="MoveUpSub"]').click(function () {
            var main = $(this).parents('ul').eq(0);
            var list = main.children('li');
            var thisHtml = $(this).parents('li').eq(0).html();
            var num = list.length;

            //加载数据
            var arr = new Array();
            var thisIndex = -1;
            list.each(function (i, d) {
                if (d.innerHTML === thisHtml) {
                    thisIndex = i;
                }
                arr[i] = d;
            });

            //第一个项 或者 找不到则退出
            if (thisIndex === -1 || thisIndex === 0) return;

            //交换
            var temp = arr[thisIndex];
            arr[thisIndex] = arr[thisIndex - 1];
            arr[thisIndex - 1] = temp;

            //清空
            main.empty();

            //写入
            $.each(arr, function(k, v) {
                main.append(v.outerHTML);
            });

            //重新绑定事件
            $('.' + toolIDPrefix + 'menu-item p').unbind();
            $('.' + toolIDPrefix + 'menu-item p').click(function () {
                menuEdit(false, false, id, $(this));
            });
            setButton(id, num);
        });
    }

    function addButtonMoveDownSub(id) {
        /// <summary>
        /// 添加下移子菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>

        $('button[data-menu-event="MoveDownSub"]').unbind();
        $('button[data-menu-event="MoveDownSub"]').click(function () {
            var main = $(this).parents('ul').eq(0);
            var list = main.children('li');
            var thisHtml = $(this).parents('li').eq(0).html();
            var num = list.length;

            //加载数据
            var arr = new Array();
            var thisIndex = -1;
            list.each(function (i, d) {
                if (d.innerHTML === thisHtml) {
                    thisIndex = i;
                }
                arr[i] = d;
            });

            //最后个项 或者 找不到则退出
            if (thisIndex === num - 1 || thisIndex === -1) return;

            //交换
            var temp = arr[thisIndex];
            arr[thisIndex] = arr[thisIndex + 1];
            arr[thisIndex + 1] = temp;

            //清空
            main.empty();

            //写入
            $.each(arr, function(k, v) {
                main.append(v.outerHTML);
            });

            //重新绑定事件
            $('.' + toolIDPrefix + 'menu-item p').unbind();
            $('.' + toolIDPrefix + 'menu-item p').click(function () {
                menuEdit(false, false, id, $(this));
            });
            setButton(id, main.length);
        });
    }

    function addButtonDeleteMain(id, empty) {
        /// <summary>
        /// 添加删除主菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="empty" type="Boolean">添加之前是否清空</param>

        if (typeof empty != 'undefined' && empty) {
            $('#' + toolIDPrefix + 'footer-' + (id + 1)).empty();
        }
        var html = '<button type="button" class="btn btn-danger" data-menu-event="DeleteMain" data-menu-id="' + id + '">删除主菜单</button>';
        $('#' + toolIDPrefix + 'footer-' + id).append(html);
        $('button[data-menu-event="DeleteMain"][data-menu-id="' + id + '"]').unbind();
        $('button[data-menu-event="DeleteMain"][data-menu-id="' + id + '"]').click(function () {
            confirm('确定删除？', function () {
                $('#' + toolIDPrefix + 'head-' + id).removeAttr('data-menu-type');
                $('#' + toolIDPrefix + 'head-' + id).empty();
                $('#' + toolIDPrefix + 'body-' + id).empty();
                $('#' + toolIDPrefix + 'footer-' + id).empty();
                setButton(id);
            });
        });
    }

    function addButtonModityMain(id, empty) {
        /// <summary>
        /// 添加修改主菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="empty" type="Boolean">添加之前是否清空</param>

        if (typeof empty != 'undefined' && empty) {
            $('#' + toolIDPrefix + 'footer-' + id).empty();
        }
        var html = '<button type="button" class="btn btn-primary" data-menu-event="ModityMain" data-menu-id="' + id + '">修改主菜单</button>';
        $('#' + toolIDPrefix + 'footer-' + id).append(html);
        $('button[data-menu-event="ModityMain"][data-menu-id="' + id + '"]').unbind();
        $('button[data-menu-event="ModityMain"][data-menu-id="' + id + '"]').click(function () {
            menuEdit(false, true, id);
        });
    }

    function addButtonAddMain(id, empty) {
        /// <summary>
        /// 添加添加主菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="empty" type="Boolean">添加之前是否清空</param>

        if (typeof empty != 'undefined' && empty) {
            $('#' + toolIDPrefix + 'footer-' + id).empty();
        }
        var html = '<button type="button" class="btn btn-success" data-menu-event="AddMain" data-menu-id="' + id + '">添加主菜单</button>';
        $('#' + toolIDPrefix + 'footer-' + id).append(html);
        $('button[data-menu-event="AddMain"][data-menu-id="' + id + '"]').unbind();
        $('button[data-menu-event="AddMain"][data-menu-id="' + id + '"]').click(function () {
            menuEdit(true, true, id);
        });
    }

    function getMenuTypeName(type) {
        /// <summary>
        /// 根据菜单类型获取菜单类型中文说明
        /// </summary>
        /// <param name="type" type="String">菜单类型</param>
        /// <return>菜单类型中文说明</return>

        if (type === 'click') return '点击推事件';
        if (type === 'view') return '跳转URL';
        if (type === 'scancode_push') return '扫码推事件';
        if (type === 'scancode_waitmsg') return '扫码推事件且弹出“消息接收中”提示框';
        if (type === 'pic_sysphoto') return '弹出系统拍照发图';
        if (type === 'pic_photo_or_album') return '弹出拍照或者相册发图';
        if (type === 'pic_weixin') return '弹出微信相册发图器';
        if (type === 'location_select') return '弹出地理位置选择器';

        return '未知类型';
    }

    function getMenuTypeSelect(isMain, selectItem) {
        /// <summary>
        /// 获取菜单类型下拉框内容
        /// </summary>
        /// <param name="isMain" type="Boolean">是否为主菜单</param>
        /// <param name="selectItem" type="String">已选择项目</param>
        /// <return>菜单类型下拉框内容</return>

        var isSelect = (typeof selectItem != 'undefined' && selectItem != null);
        var temp = '';
        if (isMain) {
            temp += '<option value="main"' + ((isSelect && selectItem === 'main') ? 'selected="selected"' : '') + '>主菜单</option>';
        }
        temp += '<option value="click"' + ((isSelect && selectItem === 'click') ? 'selected="selected"' : '') + '>点击推事件</option>' +
				'<option value="view"' + ((isSelect && selectItem === 'view') ? 'selected="selected"' : '') + '>跳转URL</option>' +
				'<option value="scancode_push"' + ((isSelect && selectItem === 'scancode_push') ? 'selected="selected"' : '') + '>扫码推事件</option>' +
				'<option value="scancode_waitmsg"' + ((isSelect && selectItem === 'scancode_waitmsg') ? 'selected="selected"' : '') + '>扫码推事件且弹出“消息接收中”提示框</option>' +
				'<option value="pic_sysphoto"' + ((isSelect && selectItem === 'pic_sysphoto') ? 'selected="selected"' : '') + '>弹出系统拍照发图</option>' +
				'<option value="pic_photo_or_album"' + ((isSelect && selectItem === 'pic_photo_or_album') ? 'selected="selected"' : '') + '>弹出拍照或者相册发图</option>' +
				'<option value="pic_weixin"' + ((isSelect && selectItem === 'pic_weixin') ? 'selected="selected"' : '') + '>弹出微信相册发图器</option>' +
				'<option value="location_select"' + ((isSelect && selectItem === 'location_select') ? 'selected="selected"' : '') + '>弹出地理位置选择器</option>';

        return temp;
    }

    function menuEdit(isNew, isMain, id, subDom) {
        /// <summary>
        /// 菜单编辑
        /// </summary>
        /// <param name="isNew" type="Boolean">是否为新建</param>
        /// <param name="isMain" type="Boolean">是否为主菜单</param>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="subDom" type="Object">子菜单Dom</param>

        $('body').append(
			'<div id="' + toolMenuEditIDPrefix + 'main" class="modal fade" role="dialog" aria-hidden="true">' +
				'<div class="modal-dialog">' +
					'<div class="modal-content">' +
						'<div class="modal-header">' +
							'<h4 class="modal-title">' + (isNew ? '新建' : '修改') + (isMain ? '主' : '子') + '菜单</h4>' +
						'</div>' +
						'<div id="' + toolMenuEditIDPrefix + 'body" class="modal-body">' +
							'<div class="form-group">' +
								'<label for="' + toolMenuEditIDPrefix + 'name" class="sr-only">菜单名称</label>' +
								'<div class="input-group">' +
									'<input type="text" id="' + toolMenuEditIDPrefix + 'name" name="name" class="form-control" placeholder="菜单名称">' +
									'<div class="input-group-addon">' +
										'<div class="glyphicon glyphicon-book"></div>' +
									'</div>' +
								'</div>' +
							'</div>' +
						'</div>' +
					'<div class="modal-footer">' +
						'<button type="button" id="' + toolMenuEditIDPrefix + 'save" class="btn btn-primary">保存</button>' +
						'<button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</div>');
        if (!isNew) {
            $('#' + toolMenuEditIDPrefix + 'name').val(isMain
                ? $('#' + toolIDPrefix + 'head-' + id).text()
                : subDom.find('p[data-menu-type="name"]').eq(0).data('menu-value'));
        }
        var canChangeType = (isNew || $('#' + toolIDPrefix + 'head-' + id).data('menu-type') !== 'main' || $('#' + toolIDPrefix + 'body-' + id).children().children().length === 0);
        if (canChangeType) {
            var selectType = (isNew ? null : (isMain
                ? $('#' + toolIDPrefix + 'head-' + id).data('menu-type')
                : subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value')));
            $('#' + toolMenuEditIDPrefix + 'body').append(
                '<div class="form-group">' +
                    '<label for="' + toolMenuEditIDPrefix + 'type" class="sr-only">菜单类型</label>' +
                    '<div class="input-group">' +
                        '<select id="' + toolMenuEditIDPrefix + 'type" name="type" class="form-control">' +
                            getMenuTypeSelect(isMain, selectType) +
                        '</select>' +
                        '<div class="input-group-addon">' +
                            '<div class="glyphicon glyphicon-th-list"></div>' +
                        '</div>' +
                    '</div>' +
                '</div>');
            var typeNow = $('#' + toolMenuEditIDPrefix + 'type').val();
            $('#' + toolMenuEditIDPrefix + 'body').append(getMenuTypeForm(id, typeNow, isMain, subDom));
            bindTypeChangeEvent(typeNow);
            $('#' + toolMenuEditIDPrefix + 'type').change(function () {
                var type = $(this).val();
                $('#' + toolMenuEditIDPrefix + 'body').append(getMenuTypeForm(id, type, isMain, subDom));
                bindTypeChangeEvent(type);
            });
        }
        $('#' + toolMenuEditIDPrefix + 'main').on('hidden.bs.modal', function () {
            $(this).remove();
        });

        $('#' + toolMenuEditIDPrefix + 'save').click(function () {
            menuEditSave(isNew, isMain, id, canChangeType, subDom);
        });

        $('#' + toolMenuEditIDPrefix + 'main').modal('show');
    }

    function menuEditSave(isNew, isMain, id, canChangeType, subDom) {
        /// <summary>
        /// 菜单编辑保存
        /// </summary>
        /// <param name="isNew" type="Boolean">是否为新建</param>
        /// <param name="isMain" type="Boolean">是否为主菜单</param>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="canChangeType" type="Boolean">是否能够修改类型</param>
        /// <param name="subDom" type="Object">子菜单Dom</param>

        var name = $('#' + toolMenuEditIDPrefix + 'name').val();
        if (name.length === 0) {
            messageAlert("主菜单名称不能为空");
            return;
        }
        if (name.length > 16) {
            messageAlert("主菜单名称不能超过16个字节");
            return;
        }
        if (canChangeType) {
            var type = $('#' + toolMenuEditIDPrefix + 'type').val();
            var obj = { name: name };
            if (type !== 'main') {
                obj = $.extend({ type: type }, obj);
                if (type === 'view') {
                    var url = $('#' + toolMenuEditIDPrefix + 'url').val();
                    obj = $.extend({ url: url }, obj);
                    if (url.length === 0) {
                        messageAlert("URL不能为空");
                        return;
                    }
                } else {
                    var key = $('#' + toolMenuEditIDPrefix + 'key').val();
                    obj = $.extend({ key: key }, obj);
                    if (key.length === 0) {
                        messageAlert("Key不能为空");
                        return;
                    }
                }
            }
            if (isMain) {
                if (isNew) {
                    if (type === 'main') {
                        setMenu(id, obj, true, true);
                    } else {
                        setMenu(id, obj, true, true);
                    }
                } else {
                    if (type === 'main') {
                        setMenu(id, obj, true, false);
                    } else {
                        setMenu(id, obj, true, false);
                    }
                }
            } else {
                if (isNew) {
                    setMenu(id, obj, false, true);
                } else {
                    
                    setMenu(id, obj, false, false, subDom);
                }
            }
        } else {
            if (isMain) {
                $('#' + toolIDPrefix + 'head-' + id).text(name);
            } else {
                var td = subDom.find('p[data-menu-type="name"]').eq(0);
                td.attr('data-menu-value', name);
                td.html('<strong>显示标题：' + name + '</strong>');
            }
        }

        $('#' + toolMenuEditIDPrefix + 'main').modal('hide');
    }

    function getMenuTypeForm(id, type, isMain, subDom) {
        /// <summary>
        /// 根据菜单类型获取对应的参数表单
        /// <pre>如果菜单类型和子菜单Dom都为空则返回空</pre>
        /// <pre>如果菜单类型为menu则返回空</pre>
        /// <pre>如果菜单类型为空但子菜单Dom不为空则通过子菜单获取菜单类型</pre>
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="type" type="String">菜单类型</param>
        /// <param name="isMain" type="Boolean">是否为主菜单</param>
        /// <param name="subDom" type="Object">子菜单Dom</param>
        /// <return>参数表单</return>

        $('.' + toolMenuEditIDPrefix + 'arg').remove();
        if (type == null) {
            if (typeof subDom == 'undefined' || subDom == null) return '';
            type = subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value');
        }
        if (type === 'main') return '';
        var val;
        if (type === 'view') {
            if (isMain) {
                if ($('#' + toolIDPrefix + 'head-' + id).data('menu-type') === type) {
                    val = $('#' + toolIDPrefix + 'head-' + id).data('menu-url');
                } else {
                    val = '';
                }
            } else {
                if (subDom != null && subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value') === type) {
                    val = subDom.find('p[data-menu-type="url"]').eq(0).data('menu-value');
                } else {
                    val = '';
                }
            }
            return '<div class="form-group ' + toolMenuEditIDPrefix + 'arg">' +
                        '<label for="' + toolMenuEditIDPrefix + 'url" class="sr-only">URL</label>' +
                        '<div class="input-group">' +
                            '<input type="text" id="' + toolMenuEditIDPrefix + 'url" name="url" value="' + val + '" class="form-control" placeholder="URL">' +
                            '<div class="input-group-addon">' +
                                '<div class="glyphicon glyphicon-pencil"></div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="form-group ' + toolMenuEditIDPrefix + 'arg">' +
                        '<div class="btn-group" data-toggle="buttons">' +
                            '<label class="btn btn-primary active">' +
                                '<input type="radio" name="' + toolMenuEditIDPrefix + 'url-type" id="' + toolMenuEditIDPrefix + 'url-type-common" value="common" autocomplete="off" checked>普通' +
                            '</label>' +
                            '<label class="btn btn-primary">' +
                                '<input type="radio" name="' + toolMenuEditIDPrefix + 'url-type" id="' + toolMenuEditIDPrefix + 'url-type-oauth" value="oauth" autocomplete="off">OAuth' +
                            '</label>' +
                        '</div>' +
                    '</div>' +
                    '<div class="form-group ' + toolMenuEditIDPrefix + 'arg hidden ' + toolMenuEditIDPrefix + 'arg-type">' +
                        '<label for="' + toolMenuEditIDPrefix + 'url-oauth-callback" class="sr-only">回调地址</label>' +
                        '<div class="input-group">' +
                            '<input type="text" id="' + toolMenuEditIDPrefix + 'url-oauth-callback" name="url-oauth-callback" value="' + settings.oauthCallback + '" class="form-control" placeholder="回调地址">' +
                            '<div class="input-group-addon">' +
                                '<div class="glyphicon glyphicon-cog"></div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="form-group ' + toolMenuEditIDPrefix + 'arg hidden ' + toolMenuEditIDPrefix + 'arg-type">' +
                        '<label for="' + toolMenuEditIDPrefix + 'url-oauth-type" class="sr-only">应用授权作用域</label>' +
                        '<select id="' + toolMenuEditIDPrefix + 'url-oauth-type" name="' + toolMenuEditIDPrefix + 'url-oauth-type" class="form-control">' +
                            '<option value="snsapi_base">基本授权</option>' +
                            '<option value="snsapi_userinfo">用户信息授权</option>' +
                        '</select> ' +
                    '</div>' +
                    '<div class="form-group ' + toolMenuEditIDPrefix + 'arg hidden ' + toolMenuEditIDPrefix + 'arg-type">' +
                        '<label for="' + toolMenuEditIDPrefix + 'url-oauth-state" class="sr-only">参数</label>' +
                        '<div class="input-group">' +
                            '<input type="text" id="' + toolMenuEditIDPrefix + 'url-oauth-state" name="url-oauth-state" class="form-control" placeholder="参数">' +
                            '<div class="input-group-addon">' +
                                '<div class="glyphicon glyphicon-tag"></div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="form-group ' + toolMenuEditIDPrefix + 'arg hidden ' + toolMenuEditIDPrefix + 'arg-type">' +
                        '<button class="btn btn-primary" id="' + toolMenuEditIDPrefix + 'url-oauth-geturl">获取OAuth地址</button>' +
                    '</div>';
        }
        if (isMain) {
            if ($('#' + toolIDPrefix + 'head-' + id).data('menu-type') === type) {
                val = $('#' + toolIDPrefix + 'head-' + id).data('menu-key');
            } else {
                val = '';
            }
        } else {
            if (subDom != null && subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value') === type) {
                val = subDom.find('p[data-menu-type="key"]').eq(0).data('menu-value');
            } else {
                val = '';
            }
        }
        return '<div class="form-group ' + toolMenuEditIDPrefix + 'arg">' +
                    '<label for="' + toolMenuEditIDPrefix + 'key" class="sr-only">Key</label>' +
                    '<div class="input-group">' +
                        '<input type="text" id="' + toolMenuEditIDPrefix + 'key" name="key" value="' + val + '" class="form-control" placeholder="Key">' +
                        '<div class="input-group-addon">' +
                            '<div class="glyphicon glyphicon-pencil"></div>' +
                        '</div>' +
                    '</div>' +
                '</div>';
    }

    function bindTypeChangeEvent(type) {
        /// <summary>
        /// 绑定菜单类型改变事件
        /// </summary>
        /// <param name="type" type="String">菜单类型</param>

        if (type === 'view') {
            $('input[name="' + toolMenuEditIDPrefix + 'url-type"]').change(function () {
                var urlType = $(this).val();
                if (urlType === 'common') {
                    $('.' + toolMenuEditIDPrefix + 'arg-type').addClass('hidden');
                    $('#' + toolMenuEditIDPrefix + 'url').removeAttr('readonly');
                }
                if (urlType === 'oauth') {
                    $('.' + toolMenuEditIDPrefix + 'arg-type').removeClass('hidden');
                    $('#' + toolMenuEditIDPrefix + 'url').attr('readonly', 'readonly');
                }
            });
            $('#' + toolMenuEditIDPrefix + 'url-oauth-geturl').click(function () {
                var oauthCallback = $('#' + toolMenuEditIDPrefix + 'url-oauth-callback').val();
                if (oauthCallback.length === 0) {
                    messageAlert("OAuth回调地址不能为空");
                    return;
                }
                var oauthType = $('#' + toolMenuEditIDPrefix + 'url-oauth-type').val();
                var oauthState = $('#' + toolMenuEditIDPrefix + 'url-oauth-state').val();
                if (oauthState.length === 0) {
                    messageAlert("OAuth参数不能为空");
                    return;
                }
                if (oauthState.length > 128) {
                    messageAlert("OAuth参数不能大于128字节");
                    return;
                }
                if (!new RegExp('^[A-Za-z0-9]+$').test(oauthState)) {
                    messageAlert("OAuth参数必须满足a-zA-Z0-9");
                    return;
                }
                showLoading();
                $.post(settings.getOAuthUrlUrl, {
                    callback: oauthCallback,
                    type: oauthType,
                    state: oauthState
                }, function (data) {
                    hideLoading();
                    if (typeof data.msg != 'undefined') {
                        messageAlert(data.msg);
                        return;
                    }
                    $('#' + toolMenuEditIDPrefix + 'url').val(data.url);
                }, 'json');
            });
            return;
        }
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