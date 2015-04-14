(function ($) {
    "use strict";

    //工具名称
    var toolName = 'weixinmenu';

    //主菜单数量
    var mainMenuCount = 3;

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
            deleteUrl: '/MenuTool?Operation=Delete'
        }, options);

        showLoading();
        loadList($(this));
        $.getJSON(settings.getUrl, function (data) {
            if (typeof data.msg != 'undefined') {
                alert(data.msg);
            } else {
                loadData(data);
            }
            hideLoading();
        });
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
            showLoading();
            loadList(dom);
            $.getJSON(settings.getUrl, function (data) {
                loadData(data);
                hideLoading();
            });
        });
        $('#' + toolIDPrefix + 'operate-save').click(function () {
            confirm('确定保存？', function () {
                showLoading();
                $.post(settings.saveUrl, { Data: window.escape(JSON.stringify(getMenuObj())) }, function (data) {
                    hideLoading();
                    alert(data.msg);
                });
            });
        });
        $('#' + toolIDPrefix + 'operate-delete').click(function () {
            confirm('确定删除？', function () {
                showLoading();
                $.post(settings.deleteUrl, function (data) {
                    hideLoading();
                    alert(data.msg);
                });
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
            if (name != '') {
                var type = $('#' + toolIDPrefix + 'head-' + j).data('menu-type');
                if (type == 'main') {
                    var subButtonList = [];
                    $('#' + toolIDPrefix + 'body-' + j).children().children().each(function () {
                        var subType = $(this).find('p[data-menu-type="type"]').eq(0).data('menu-value');
                        var subName = $(this).find('p[data-menu-type="name"]').eq(0).data('menu-value');
                        if (subType == 'view') {
                            subButtonList = subButtonList.concat([{
                                name: subName,
                                type: subType,
                                url: $(this).find('p[data-menu-type="url"]').eq(0).data('menu-value')
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
                    if (type == 'view') {
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

    function loadData(data) {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="data" type="Object">菜单对象</param>

        for (var i = 0; i < mainMenuCount; i++) {
            if (i < data.button.length) {
                $('#' + toolIDPrefix + 'head-' + i).text(data.button[i].name);
                setMenu(i, data.button[i], true, true);
            } else {
                setButton(i);
            }
        }
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
                if (obj.type == 'view') {
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

        $('.' + toolIDPrefix + 'menu-item').unbind();
        $('.' + toolIDPrefix + 'menu-item').click(function () {
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
        var key = (obj.type == 'view')
			? '<p data-menu-type="url" data-menu-value="' + obj.url + '">URL：' + obj.url + '</p>'
			: '<p data-menu-type="key" data-menu-value="' + obj.key + '">Key：' + obj.key + '</p>';

        return name + type + key;
    }

    function setButton(id, count) {
        /// <summary>
        /// 设置按钮信息
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="count" type="Number">子菜单数量（不填为没有主菜单，负数则为非主菜单）</param>

        if (typeof count == 'undefined') {
            addButtonAddMain(id, true);
            return;
        }
        if (count < 0) {
            addButtonModityMain(id, true);
            addButtonDeleteMain(id);
            return;
        }
        if (count == 0) {
            addButtonAddSub(id, true);
            addButtonModityMain(id);
            addButtonDeleteMain(id);
            return;
        }
        if (count == 5) {
            addButtonModityMain(id, true);
            addButtonDeleteSub(id);
            return;
        }
        addButtonAddSub(id, true);
        addButtonModityMain(id);
        addButtonDeleteSub(id);
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
        $('button[data-menu-event="AddSub"][data-menu-id="' + id + '"]').click(function () {
            menuEdit(true, false, id);
        });
    }

    function addButtonDeleteSub(id, empty) {
        /// <summary>
        /// 添加删除子菜单按钮
        /// </summary>
        /// <param name="id" type="Number">主菜单ID</param>
        /// <param name="empty" type="Boolean">添加之前是否清空</param>

        if (typeof empty != 'undefined' && empty) {
            $('#' + toolIDPrefix + 'footer-' + id).empty();
        }
        var html = '<button type="button" class="btn btn-warning" data-menu-event="DeleteSub" data-menu-id="' + id + '">删除子菜单</button>';
        $('#' + toolIDPrefix + 'footer-' + id).append(html);
        $('button[data-menu-event="DeleteSub"][data-menu-id="' + id + '"]').click(function () {
            confirm('确定删除？', function () {
                $('#' + toolIDPrefix + 'body-' + id).children().children().remove('li:last');
                var childDom = $('#' + toolIDPrefix + 'body-' + id).children().children();
                setButton(id, childDom.length);
            });
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

        if (type == 'click') return '点击推事件';
        if (type == 'view') return '跳转URL';
        if (type == 'scancode_push') return '扫码推事件';
        if (type == 'scancode_waitmsg') return '扫码推事件且弹出“消息接收中”提示框';
        if (type == 'pic_sysphoto') return '弹出系统拍照发图';
        if (type == 'pic_photo_or_album') return '弹出拍照或者相册发图';
        if (type == 'pic_weixin') return '弹出微信相册发图器';
        if (type == 'location_select') return '弹出地理位置选择器';

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
            temp += '<option value="main"' + ((isSelect && selectItem == 'main') ? 'selected="selected"' : '') + '>主菜单</option>';
        }
        temp += '<option value="click"' + ((isSelect && selectItem == 'click') ? 'selected="selected"' : '') + '>点击推事件</option>' +
				'<option value="view"' + ((isSelect && selectItem == 'view') ? 'selected="selected"' : '') + '>跳转URL</option>' +
				'<option value="scancode_push"' + ((isSelect && selectItem == 'scancode_push') ? 'selected="selected"' : '') + '>扫码推事件</option>' +
				'<option value="scancode_waitmsg"' + ((isSelect && selectItem == 'scancode_waitmsg') ? 'selected="selected"' : '') + '>扫码推事件且弹出“消息接收中”提示框</option>' +
				'<option value="pic_sysphoto"' + ((isSelect && selectItem == 'pic_sysphoto') ? 'selected="selected"' : '') + '>弹出系统拍照发图</option>' +
				'<option value="pic_photo_or_album"' + ((isSelect && selectItem == 'pic_photo_or_album') ? 'selected="selected"' : '') + '>弹出拍照或者相册发图</option>' +
				'<option value="pic_weixin"' + ((isSelect && selectItem == 'pic_weixin') ? 'selected="selected"' : '') + '>弹出微信相册发图器</option>' +
				'<option value="location_select"' + ((isSelect && selectItem == 'location_select') ? 'selected="selected"' : '') + '>弹出地理位置选择器</option>';

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
        var canChangeType = (isNew || $('#' + toolIDPrefix + 'head-' + id).data('menu-type') != 'main' || $('#' + toolIDPrefix + 'body-' + id).children().children().length == 0);
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
            $('#' + toolMenuEditIDPrefix + 'body').append(getMenuTypeForm(id, $('#' + toolMenuEditIDPrefix + 'type').val(), isMain, subDom));
            $('#' + toolMenuEditIDPrefix + 'type').change(function () {
                var type = $(this).val();
                $('#' + toolMenuEditIDPrefix + 'body').append(getMenuTypeForm(id, type, isMain, subDom));
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
        if (name.length == 0) {
            alert("主菜单名称不能为空");
            return;
        }
        if (name.length > 16) {
            alert("主菜单名称不能超过16个字节");
            return;
        }
        if (canChangeType) {
            var type = $('#' + toolMenuEditIDPrefix + 'type').val();
            var obj = { name: name };
            if (type != 'main') {
                obj = $.extend({ type: type }, obj);
                if (type == 'view') {
                    var url = $('#' + toolMenuEditIDPrefix + 'url').val();
                    obj = $.extend({ url: url }, obj);
                    if (url.length == 0) {
                        alert("URL不能为空");
                        return;
                    }
                } else {
                    var key = $('#' + toolMenuEditIDPrefix + 'key').val();
                    obj = $.extend({ key: key }, obj);
                    if (key.length == 0) {
                        alert("Key不能为空");
                        return;
                    }
                }
            }

            if (isMain) {
                if (isNew) {
                    if (type == 'main') {
                        setMenu(id, obj, true, true);
                    } else {
                        setMenu(id, obj, true, true);
                    }
                } else {
                    if (type == 'main') {
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
            $('#' + toolIDPrefix + 'head-' + id).text(name);
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
        if (type == 'main') return '';
        var val;
        if (type == 'view') {
            if (isMain) {
                if ($('#' + toolIDPrefix + 'head-' + id).data('menu-type') == type) {
                    val = $('#' + toolIDPrefix + 'head-' + id).data('menu-url');
                } else {
                    val = '';
                }
            } else {
                if (subDom != null && subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value') == type) {
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
                    '</div>';
        }
        if (isMain) {
            if ($('#' + toolIDPrefix + 'head-' + id).data('menu-type') == type) {
                val = $('#' + toolIDPrefix + 'head-' + id).data('menu-key');
            } else {
                val = '';
            }
        } else {
            if (subDom != null && subDom.find('p[data-menu-type="type"]').eq(0).data('menu-value') == type) {
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
}(jQuery));