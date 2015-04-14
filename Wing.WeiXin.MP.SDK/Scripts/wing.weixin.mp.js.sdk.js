/*!
 * Weixin JS SDK v0.0.3
 * 用于简化微信JS初始化操纵（搭配Wing.Weixin.MP.SDK）
 *
 * Update(v0.0.3)：
 * 1.全部封装WXJS接口
 */

/*!
 * 版本1.0.0接口
 * 
 * onMenuShareTimeline
 * onMenuShareAppMessage
 * onMenuShareQQ
 * onMenuShareWeibo
 * startRecord
 * stopRecord
 * onVoiceRecordEnd
 * playVoice
 * pauseVoice
 * stopVoice
 * onVoicePlayEnd
 * uploadVoice
 * downloadVoice
 * chooseImage
 * previewImage
 * uploadImage
 * downloadImage
 * translateVoice
 * getNetworkType
 * openLocation
 * getLocation
 * hideOptionMenu
 * showOptionMenu
 * hideMenuItems
 * showMenuItems
 * hideAllNonBaseMenuItem
 * showAllNonBaseMenuItem
 * closeWindow
 * scanQRCode
 * chooseWXPay
 * openProductSpecificView
 * addCard
 * chooseCard
 * openCard
 * 
 * 所有菜单项列表
 * 
 * 基本类
 * 
 * 举报: "menuItem:exposeArticle"
 * 调整字体: "menuItem:setFont"
 * 日间模式: "menuItem:dayMode"
 * 夜间模式: "menuItem:nightMode"
 * 刷新: "menuItem:refresh"
 * 查看公众号（已添加）: "menuItem:profile"
 * 查看公众号（未添加）: "menuItem:addContact"
 * 传播类
 * 
 * 发送给朋友: "menuItem:share:appMessage"
 * 分享到朋友圈: "menuItem:share:timeline"
 * 分享到QQ: "menuItem:share:qq"
 * 分享到Weibo: "menuItem:share:weiboApp"
 * 收藏: "menuItem:favorite"
 * 分享到FB: "menuItem:share:facebook"
 * 分享到 QQ 空间/menuItem:share:QZone
 * 保护类
 * 
 * 调试: "menuItem:jsDebug"
 * 编辑标签: "menuItem:editTag"
 * 删除: "menuItem:delete"
 * 复制链接: "menuItem:copyUrl"
 * 原网页: "menuItem:originPage"
 * 阅读模式: "menuItem:readMode"
 * 在QQ浏览器中打开: "menuItem:openWithQQBrowser"
 * 在Safari中打开: "menuItem:openWithSafari"
 * 邮件: "menuItem:share:email"
 * 一些特殊公众号: "menuItem:share:brand"
 */

function weixin(url) {
    /// <summary>Weixin JS SDK</summary>
    /// <param name="url" type="String">获取参数接口的URL(不填默认是"/WeixinConfig")</param>

    /// <field name='isDebug' type='Boolean'>
    /// 设置是否为调试模式
    /// 用于PC端调试（不加载微信接口）
    /// </field>
    /// <field name='isHideOptionMenu' type='Boolean'>
    /// 是否隐藏右上角菜单
    /// </field>
    this.isDebug = true;
    this.isHideOptionMenu = false;

    if (typeof jQuery == 'undefined') {
        alert('未发现JQuery');
    }

    this.load = function (apiList, callback) {
        /// <summary>载入配置</summary>
        /// <param name="apiList" type="String">API列表</param>
        /// <param name="callback" type="Function">获取成功后的回调方法</param>

        //判断是否为调试模式
        if (this.isDebug) {
            //执行回调方法
            callback();

            return;
        }

        //判断接口列表是否存在
        if (!$.isArray(apiList) || apiList.length == 0) {
            throw 'API列表不能为空';
        }

        //判断是否需要隐藏右上角菜单
        if (this.isHideOptionMenu) {
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
                        hideOptionMenu();
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

    this.failCallback = function (r) {
        /// <summary>获取失败后的回调方法</summary>
        /// <param name="r" type="String">API失败原因描述</param>

        throw ('配置注入失败，原因：' + r);
    }

    this.onMenuShareTimeline = function (title, link, imgUrl, success, cancel) {
        /// <summary>
        /// “分享到朋友圈”按钮点击状态及自定义分享内容
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="title" type="String">分享标题</param>
        /// <param name="link" type="String">分享链接</param>
        /// <param name="imgUrl" type="String">分享图标</param>
        /// <param name="success" type="Function">用户确认分享后执行的回调函数</param>
        /// <param name="cancel" type="Function">用户取消分享后执行的回调函数</param>

        if (this.isDebug) {
            success(title, link, imgUrl);
            return;
        }
        wx.onMenuShareTimeline({
            title: title,
            link: link,
            imgUrl: imgUrl,
            success: success,
            cancel: cancel
        });
    }

    this.onMenuShareAppMessage = function (title, desc, link, imgUrl, type, dataUrl, success, cancel) {
        /// <summary>
        /// “分享给朋友”按钮点击状态及自定义分享内容
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="title" type="String">分享标题</param>
        /// <param name="desc" type="String">分享描述</param>
        /// <param name="link" type="String">分享链接</param>
        /// <param name="imgUrl" type="String">分享图标</param>
        /// <param name="type" type="String">分享类型,music、video或link，不填默认为link</param>
        /// <param name="dataUrl" type="String">如果type是music或video，则要提供数据链接，默认为空</param>
        /// <param name="success" type="Function">用户确认分享后执行的回调函数</param>
        /// <param name="cancel" type="Function">用户取消分享后执行的回调函数</param>

        if (this.isDebug) {
            success(title, desc, link, imgUrl, type, dataUrl);
            return;
        }
        wx.onMenuShareAppMessage({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgUrl,
            type: type,
            dataUrl: dataUrl,
            success: success,
            cancel: cancel
        });
    }

    this.onMenuShareQQ = function (title, desc, link, imgUrl, success, cancel) {
        /// <summary>
        /// “分享到QQ”按钮点击状态及自定义分享内容
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="title" type="String">分享标题</param>
        /// <param name="desc" type="String">分享描述</param>
        /// <param name="link" type="String">分享链接</param>
        /// <param name="imgUrl" type="String">分享图标</param>
        /// <param name="success" type="Function">用户确认分享后执行的回调函数</param>
        /// <param name="cancel" type="Function">用户取消分享后执行的回调函数</param>

        if (this.isDebug) {
            success(title, desc, link, imgUrl);
            return;
        }
        wx.onMenuShareQQ({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgUrl,
            success: success,
            cancel: cancel
        });
    }

    this.onMenuShareWeibo = function (title, desc, link, imgUrl, success, cancel) {
        /// <summary>
        /// “分享到腾讯微博”按钮点击状态及自定义分享内容
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="title" type="String">分享标题</param>
        /// <param name="desc" type="String">分享描述</param>
        /// <param name="link" type="String">分享链接</param>
        /// <param name="imgUrl" type="String">分享图标</param>
        /// <param name="success" type="Function">用户确认分享后执行的回调函数</param>
        /// <param name="cancel" type="Function">用户取消分享后执行的回调函数</param>

        if (this.isDebug) {
            success(title, desc, link, imgUrl);
            return;
        }
        wx.onMenuShareWeibo({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgUrl,
            success: success,
            cancel: cancel
        });
    }

    this.startRecord = function (debugEvent) {
        /// <summary>
        /// 开始录音
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">调试模式的回调函数</param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.startRecord();
    }

    this.stopRecord = function (success, debugRes) {
        /// <summary>
        /// 停止录音
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="success(debugRes)" type="Function">
        /// 用户确认分享后执行的回调函数
        /// 返回录音的本地ID(res.localId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.stopRecord({
            success: success
        });
    }

    this.onVoiceRecordEnd = function (complete, debugRes) {
        /// <summary>
        /// 监听录音自动停止
        /// 如果处于调试模式则直接调用complete
        /// </summary>
        /// <param name="complete(debugRes)" type="Function">
        /// 录音时间超过一分钟没有停止的时候会执行 complete 回调
        /// 返回录音的本地ID(res.localId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            complete(debugRes);
            return;
        }
        wx.onVoiceRecordEnd({
            complete: complete
        });
    }

    this.playVoice = function (localId, debugEvent) {
        /// <summary>
        /// 播放语音
        /// 如果处于调试模式则直接调用debugEvent
        /// </summary>
        /// <param name="localId" type="String">需要播放的音频的本地ID，由stopRecord接口获得</param>
        /// <param name="debugEvent(localId)" type="Function">
        /// 调试模式的回调函数
        /// 需要停止的音频的本地ID，由stopRecord接口获得
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(localId);
            return;
        }
        wx.playVoice({
            localId: localId
        });
    }

    this.pauseVoice = function (localId, debugEvent) {
        /// <summary>
        /// 暂停播放
        /// 如果处于调试模式则直接调用debugEvent
        /// </summary>
        /// <param name="localId" type="String">需要暂停的音频的本地ID，由stopRecord接口获得</param>
        /// <param name="debugEvent(localId)" type="Function">
        /// 调试模式的回调函数
        /// 需要停止的音频的本地ID，由stopRecord接口获得
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(localId);
            return;
        }
        wx.pauseVoice({
            localId: localId
        });
    }

    this.stopVoice = function (localId, debugEvent) {
        /// <summary>
        /// 停止播放
        /// 如果处于调试模式则直接调用debugEvent
        /// </summary>
        /// <param name="localId" type="String">需要停止的音频的本地ID，由stopRecord接口获得</param>
        /// <param name="debugEvent(localId)" type="Function">
        /// 调试模式的回调函数
        /// 需要停止的音频的本地ID，由stopRecord接口获得
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(localId);
            return;
        }
        wx.stopVoice({
            localId: localId
        });
    }

    this.onVoicePlayEnd = function (success, debugRes) {
        /// <summary>
        /// 监听语音播放完毕
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="success(res)" type="Function">
        /// 语音播放完毕会执行 success 回调
        /// 返回录音的本地ID(res.localId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.onVoicePlayEnd({
            success: success
        });
    }

    this.uploadVoice = function (localId, isShowProgressTips, success, debugRes) {
        /// <summary>
        /// 上传语音
        /// 如果处于调试模式则直接调用success
        ///
        /// 上传语音有效期3天，可用微信多媒体接口下载语音到自己的服务器，
        /// 此处获得的 serverId 即 media_id
        /// </summary>
        /// <param name="localId" type="String">需要上传的音频的本地ID，由stopRecord接口获得</param>
        /// <param name="isShowProgressTips" type="Number">默认为1，显示进度提示</param>
        /// <param name="success(res)" type="Function">
        /// 上传语音完毕会执行 success 回调
        /// 返回音频的服务器端ID(res.serverId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.uploadVoice({
            localId: localId,
            isShowProgressTips: isShowProgressTips,
            success: success
        });
    }

    this.downloadVoice = function (serverId, isShowProgressTips, success, debugRes) {
        /// <summary>
        /// 下载语音
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="serverId" type="String">需要下载的音频的服务器端ID，由uploadVoice接口获得</param>
        /// <param name="isShowProgressTips" type="Number">默认为1，显示进度提示</param>
        /// <param name="success(res)" type="Function">
        /// 下载语音完毕会执行 success 回调
        /// 返回音频的本地ID(res.localId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.downloadVoice({
            serverId: serverId,
            isShowProgressTips: isShowProgressTips,
            success: success
        });
    }

    this.chooseImage = function (success, debugRes) {
        /// <summary>
        /// 拍照或从手机相册中选图
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="success(res)" type="Function">
        /// 用户确认分享后执行的回调函数
        /// 返回选定照片的本地ID列表(res.localIds) localId可以作为img标签的src属性显示图片
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.chooseImage({
            success: success
        });
    }

    this.previewImage = function (current, urls, debugEvent) {
        /// <summary>
        /// 预览图片
        /// 如果处于调试模式则直接调用debugEvent
        /// </summary>
        /// <param name="current" type="String">当前显示的图片链接</param>
        /// <param name="urls" type="Array">需要预览的图片链接列表</param>
        /// <param name="debugEvent(current, urls)" type="Function">
        /// 调试模式的回调函数
        /// 当前显示的图片链接
        /// 需要预览的图片链接列表
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(current, urls);
            return;
        }
        wx.previewImage({
            current: current,
            urls: urls
        });
    }

    this.uploadImage = function (localId, isShowProgressTips, success, debugRes) {
        /// <summary>
        /// 上传图片
        /// 如果处于调试模式则直接调用success
        ///
        /// 上传图片有效期3天，可用微信多媒体接口下载图片到自己的服务器
        /// 此处获得的 serverId 即 media_id
        /// </summary>
        /// <param name="localId" type="String">需要上传的图片的本地ID，由chooseImage接口获得</param>
        /// <param name="isShowProgressTips" type="Number">默认为1，显示进度提示</param>
        /// <param name="success(res)" type="Function">
        /// 上传图片完毕会执行 success 回调
        /// 返回图片的服务器端ID(res.serverId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.uploadImage({
            localId: localId,
            isShowProgressTips: isShowProgressTips,
            success: success
        });
    }

    this.downloadImage = function (serverId, isShowProgressTips, success, debugRes) {
        /// <summary>
        /// 下载图片
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="serverId" type="String">需要下载的图片的服务器端ID，由uploadImage接口获得</param>
        /// <param name="isShowProgressTips" type="Number">默认为1，显示进度提示</param>
        /// <param name="success(res)" type="Function">
        /// 下载图片完毕会执行 success 回调
        /// 返回图片下载后的本地ID(res.localId)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.downloadImage({
            serverId: serverId,
            isShowProgressTips: isShowProgressTips,
            success: success
        });
    }

    this.translateVoice = function (localId, isShowProgressTips, success, debugRes) {
        /// <summary>
        /// 识别音频并返回识别结果
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="localId" type="String">需要识别的音频的本地Id，由录音相关接口获得</param>
        /// <param name="isShowProgressTips" type="Number">默认为1，显示进度提示</param>
        /// <param name="success(res)" type="Function">
        /// 识别音频完毕会执行 success 回调
        /// 语音识别的结果(res.translateResult)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.translateVoice({
            localId: localId,
            isShowProgressTips: isShowProgressTips,
            success: success
        });
    }

    this.getNetworkType = function (success, debugRes) {
        /// <summary>
        /// 获取网络状态
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="success(res)" type="Function">
        /// 获取网络状态完毕会执行 success 回调
        /// 返回网络类型2g，3g，4g，wifi(res.networkType)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.getNetworkType({
            success: success
        });
    }

    this.openLocation = function (latitude, longitude, name, address, scale, infoUrl, debugEvent) {
        /// <summary>
        /// 使用微信内置地图查看位置
        /// 如果处于调试模式则直接调用debugEvent
        /// </summary>
        /// <param name="latitude" type="Float">纬度，浮点数，范围为90 ~ -90</param>
        /// <param name="longitude" type="Float">经度，浮点数，范围为180 ~ -180</param>
        /// <param name="name" type="String">位置名</param>
        /// <param name="address" type="String">地址详情说明</param>
        /// <param name="scale" type="Number">地图缩放级别,整形值,范围从1~28。默认为最大</param>
        /// <param name="infoUrl" type="String">在查看位置界面底部显示的超链接,可点击跳转</param>
        /// <param name="debugEvent(latitude, longitude, name, address, scale, infoUrl)" type="Function">
        /// 调试模式的回调函数
        /// 纬度，浮点数，范围为90 ~ -90
        /// 经度，浮点数，范围为180 ~ -180
        /// 位置名
        /// 地址详情说明
        /// 地图缩放级别,整形值,范围从1~28。默认为最大
        /// 在查看位置界面底部显示的超链接,可点击跳转
        /// </param>

        if (this.isDebug) {
            debugEvent(latitude, longitude, name, address, scale, infoUrl);
            return;
        }
        wx.openLocation({
            latitude: latitude,
            longitude: longitude,
            name: name,
            address: address,
            scale: scale,
            infoUrl: infoUrl
        });
    }

    this.getLocation = function (success, debugRes) {
        /// <summary>
        /// 获取地理位置
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="success(res)" type="Function">
        /// 获取地理位置完毕会执行 success 回调
        /// 纬度，浮点数，范围为90 ~ -90
        /// 经度，浮点数，范围为180 ~ -180
        /// 速度，以米/每秒计
        /// 位置精度
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.getLocation({
            success: success
        });
    }

    this.hideOptionMenu = function (debugEvent) {
        /// <summary>
        /// 隐藏右上角菜单
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">
        /// 调试模式的回调函数
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.hideOptionMenu();
    }

    this.showOptionMenu = function (debugEvent) {
        /// <summary>
        /// 显示右上角菜单
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">
        /// 调试模式的回调函数
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.showOptionMenu();
    }

    this.hideMenuItems = function (menuList, debugEvent) {
        /// <summary>
        /// 批量隐藏功能按钮
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="menuList" type="Array">要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮</param>
        /// <param name="debugEvent(menuList)" type="Function">
        /// 调试模式的回调函数
        /// 要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(menuList);
            return;
        }
        wx.hideMenuItems({
            menuList: menuList
        });
    }

    this.showMenuItems = function (menuList, debugEvent) {
        /// <summary>
        /// 批量显示功能按钮
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="menuList" type="Array">要显示的菜单项</param>
        /// <param name="debugEvent(menuList)" type="Function">
        /// 调试模式的回调函数
        /// 要显示的菜单项
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(menuList);
            return;
        }
        wx.showMenuItems({
            menuList: menuList
        });
    }

    this.hideAllNonBaseMenuItem = function (debugEvent) {
        /// <summary>
        /// 隐藏所有非基础按钮
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">
        /// 调试模式的回调函数
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.hideAllNonBaseMenuItem();
    }

    this.showAllNonBaseMenuItem = function (debugEvent) {
        /// <summary>
        /// 显示所有功能按钮
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">
        /// 调试模式的回调函数
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.showAllNonBaseMenuItem();
    }

    this.closeWindow = function (debugEvent) {
        /// <summary>
        /// 关闭当前网页窗口
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="debugEvent()" type="Function">
        /// 调试模式的回调函数
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent();
            return;
        }
        wx.closeWindow();
    }

    this.scanQRCode = function (needResult, scanType, success, debugRes) {
        /// <summary>
        /// 调起微信扫一扫
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="needResult" type="Number">默认为0，扫描结果由微信处理，1则直接返回扫描结果，</param>
        /// <param name="scanType" type="Array">可以指定扫二维码还是一维码("qrCode","barCode")，默认二者都有</param>
        /// <param name="success(res)" type="Function">
        /// 微信扫一扫完毕会执行 success 回调
        /// 当needResult 为 1 时，扫码返回的结果(res.resultStr)
        /// 一维码： "EAN_13,123123123"
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.scanQRCode({
            needResult: needResult,
            scanType: scanType,
            success: success
        });
    }

    this.chooseWXPay = function (timestamp, nonceStr, prepay_id, signType, paySign, success, debugRes) {
        /// <summary>
        /// 发起一个微信支付请求
        /// 如果处于调试模式则直接调用success
        /// 
        /// prepay_id 通过微信支付统一下单接口拿到，paySign 采用统一的微信支付 Sign 签名生成方法，
        /// 注意这里 appId 也要参与签名，appId 与 config 中传入的 appId 一致，
        /// 即最后参与签名的参数有appId, timeStamp, nonceStr, package, signType。
        /// 
        /// 微信支付统一下单接口文档：http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=9_1
        /// 微信支付签名算法：http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=4_3
        /// 微信支付开发教程：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course3_tmpl&lang=zh_CN
        /// </summary>
        /// <param name="timestamp" type="Number">
        /// 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。
        /// 但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
        /// </param>
        /// <param name="nonceStr" type="String">支付签名随机串，不长于 32 位</param>
        /// <param name="prepay_id" type="String">统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）</param>
        /// <param name="signType" type="String">签名方式，默认为'SHA1'，使用新版支付需传入'MD5'</param>
        /// <param name="paySign" type="String">支付签名</param>
        /// <param name="success(res)" type="Function">发起一个微信支付请求完毕会执行 success 回调</param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.chooseWXPay({
            timestamp: timestamp,
            nonceStr: nonceStr,
            prepay_id: prepay_id,
            signType: signType,
            paySign: paySign,
            success: success
        });
    }

    this.openProductSpecificView = function (productId, viewType, debugEvent) {
        /// <summary>
        /// 跳转微信商品页
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="productId" type="String">商品id</param>
        /// <param name="viewType" type="Number">0.默认值，普通商品详情页1.扫一扫商品详情页2.小店商品详情页</param>
        /// <param name="debugEvent(productId, viewType)" type="Function">
        /// 调试模式的回调函数
        /// 商品id
        /// 0.默认值，普通商品详情页1.扫一扫商品详情页2.小店商品详情页
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(productId, viewType);
            return;
        }
        wx.openProductSpecificView({
            productId: productId,
            viewType: viewType
        });
    }

    this.addCard = function (cardList, success, debugRes) {
        /// <summary>
        /// 批量添加卡券
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="cardList" type="Array">
        /// 需要添加的卡券列表
        /// 结构： {cardId: '', cardExt: '' }
        /// </param>
        /// <param name="success(res)" type="Function">
        /// 批量添加卡券完毕会执行 success 回调
        /// 添加的卡券列表信息(res.cardList)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.chooseCard({
            cardList: cardList,
            success: success
        });
    }

    this.chooseCard = function (shopId, cardType, cardId, timestamp, nonceStr, signType, cardSign, success, debugRes) {
        /// <summary>
        /// 调起适用于门店的卡券列表并获取用户选择列表
        /// 如果处于调试模式则直接调用success
        /// </summary>
        /// <param name="shopId" type="String">门店Id</param>
        /// <param name="cardType" type="String">卡券类型</param>
        /// <param name="cardId" type="String">卡券Id</param>
        /// <param name="timestamp" type="Number">卡券签名时间戳</param>
        /// <param name="nonceStr" type="String">卡券签名随机串</param>
        /// <param name="signType" type="String">签名方式，默认'SHA1'</param>
        /// <param name="cardSign" type="String">卡券签名</param>
        /// <param name="success(res)" type="Function">
        /// 用户选中的卡券完毕会执行 success 回调
        /// 用户选中的卡券列表信息(res.cardList)
        /// </param>
        /// <param name="debugRes" type="Object">调试模式时调用的测试参数</param>

        if (this.isDebug) {
            success(debugRes);
            return;
        }
        wx.chooseCard({
            shopId: shopId,
            cardType: cardType,
            cardId: cardId,
            timestamp: timestamp,
            nonceStr: nonceStr,
            signType: signType,
            cardSign: cardSign,
            success: success
        });
    }

    this.openCard = function (cardList, debugEvent) {
        /// <summary>
        /// 查看微信卡包中的卡券
        /// 如果处于调试模式则执行回调函数
        /// </summary>
        /// <param name="cardList" type="Array">
        /// 需要添加的卡券列表
        /// 结构： {cardId: '', cardExt: '' }
        /// </param>
        /// <param name="debugEvent(cardList)" type="Function">
        /// 调试模式的回调函数
        /// 需要添加的卡券列表
        /// </param>

        if (this.isDebug) {
            if (typeof debugEvent != 'undefined') debugEvent(cardList);
            return;
        }
        wx.openCard({
            cardList: cardList
        });
    }
}