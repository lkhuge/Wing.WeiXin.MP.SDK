/**
 * Weixin JS SDK
 *
 * param url 获取参数接口的URL(不填默认是"/WeixinConfig")
 */
function weixin(url) {
    if (typeof jQuery == 'undefined') {
        alert('未发现JQuery');
    }

    /**
     * 设置是否为调试模式
     * 用于PC端调试（不加载微信接口）
     */
    this.isDebug = true;

    /**
     * 是否隐藏右上角菜单
     */
    this.isHideOptionMenu = false;

    /**
     * 显示右上角菜单
     */
    this.showOptionMenu = function () {
        if (this.isDebug) return;
        wx.showOptionMenu();
    }

    /**
     * 显示右上角菜单
     */
    this.closeWindow = function () {
        if (this.isDebug) return;
        wx.closeWindow();
    }

    /**
     * 载入配置
     *
     * param apiList API列表
     * param callback 获取成功后的回调方法
     */
    this.load = function (apiList, callback) {
        //判断是否为调试模式
        if (this.isDebug) {
            //执行回调方法
            callback();
        }

        //判断接口列表是否存在
        if (!$.isArray(apiList) || apiList.length == 0) {
            alert('API列表不能为空');
        }

        //判断是否需要隐藏右上角菜单
        if (isHideOptionMenu) {
            apiList.concat(['hideOptionMenu']);
        }

        //获取配置
        $.getJSON((typeof url == 'undefined' ? '/WeixinConfig' : url) +
            '?url=' + encodeURIComponent((typeof this.href != 'undefined') ? this.href : location.href) +
            '&apiList=' + apiList.join(), function (data) {
            //注入配置
            wx.config(data);

            //配置注入完成后执行对应操作
            wx.ready(function () {
                //判断是否需要隐藏右上角菜单
                if (isHideOptionMenu) {
                    wx.hideOptionMenu();;
                }

                //执行回调方法
                callback();
            });

            //配置注入失败
            wx.error(function (res) {
                failCallback(res);
            });
        });
    }

    /**
     * 获取失败后的回调方法
     *
     * param r 失败原因描述
     */
    this.failCallback = function (r) {
        alert('配置注入失败，原因：' + r);
    }
}