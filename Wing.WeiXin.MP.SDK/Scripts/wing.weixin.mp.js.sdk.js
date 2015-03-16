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
     * 载入配置
     *
     * param apiList API列表
     * param callback 获取成功后的回调方法
     */
    this.load = function (apiList, callback) {
        //判断接口列表是否存在
        if (!$.isArray(apiList) || apiList.length == 0) {
            alert('API列表不能为空');
        }

        //获取配置
        $.getJSON((typeof url == 'undefined' ? '/WeixinConfig' : url) +
            '?url=' + encodeURIComponent((typeof this.href != 'undefined') ? this.href : location.href) +
            '&apiList=' + apiList.join(), function (data) {
            //注入配置
            wx.config(data);

            //配置注入完成后执行对应操作
            wx.ready(function () {
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