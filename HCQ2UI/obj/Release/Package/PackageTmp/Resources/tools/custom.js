/**
 * 帮助扩展
 */
var custom = function () { }

/**
*打开Dialog--Iframe方式
*参数：对象包含属性如下
*url:请求地址
*btn：数组定义按钮名称
*height：定义高度
*width：定义宽度
*success：加载成功事件
*yes：第一个按钮点击事件
*cancel：第二个按钮点击事件
*end：销毁弹窗后事件
*/
custom.DialogIframe = function (options) {
    var defaultOption = {
        id:'',
        anim: 0,//0-6种 弹出效果
        url: '',
        scroll: false,//是否显示滚动条、默认不显示
        btn: ['确定', '取消'],
        height: '700px',
        width: '450px',
        title: '信息',
        offset: 'auto',//默认垂直居中/设置top: offset:'0px'
        //shift: 4,//0-6种 弹出效果
        icon: 0,
        success: null,
        ismax: false,//打开是否最大化、默认不
        yes: null,
        btn2: null,
        cancel: null,
        end: null
    };
     
    if (options != null) {
        for (p in options) {
            defaultOption[p] = options[p];
        }
    }
    if (!defaultOption.scroll) {
        defaultOption.url = [defaultOption.url, 'no'];
    }

    layer.ready(function () {
        var la = layer.open({
            id:defaultOption.id,
            type: 2,
            anim: defaultOption.anim,
            title: defaultOption.title,
            skin: 'demo-class',
            scrollbar: defaultOption.scroll,
            offset: defaultOption.offset,
            area: [defaultOption.width, defaultOption.height],
            btn: defaultOption.btn,
            content: defaultOption.url,
            shift: defaultOption.shift,//0-6种 弹出效果
            icon: defaultOption.icon,
            shade: [0.8, '#393D49'],
            success: function (index, layero) {
                //说明：index是对象、layero是索引。。。
                var iframeId = $(index['selector']).find('iframe').eq(0).attr('id');
                //加载成功触发
                if (defaultOption.success != null) {
                    defaultOption.success(layero, index, iframeId);
                }
            },
            yes: function (index, layero) {
                var iframeId = $(layero['selector']).find('iframe').eq(0).attr('id');
                //第一个按钮触发
                if (defaultOption.yes != null) {
                    defaultOption.yes(index, layero, iframeId);
                }
            },
            btn2: function (index, obj) {
                if (defaultOption.btn2 != null) {
                    return defaultOption.btn2(index, obj);
                }
            },
            cancel: function (index, layero) {
                //第二个按钮触发
                if (defaultOption.cancel != null) {
                    defaultOption.cancel(index, layero);
                }
            },
            end: function () {
                //销毁触发
                if (defaultOption.end != null) {
                    defaultOption.end();
                }
            }
        });
        //判断是否全屏
        if (defaultOption.ismax)
            layer.full(la);
        return la;
    });
}

/**
*打开Dialog-方式
*参数：对象包含属性如下
*content：内容、类型：String/DOM/Array   如：$('#a')
*btn：数组定义按钮名称
*height：定义高度
*width：定义宽度
*success：加载成功事件
*yes：第一个按钮点击事件
*cancel：第二个按钮点击事件
*end：销毁弹窗后事件
*/
custom.Dialog = function (options) {
    var defaultOption = {
        title: '信息',
        content: null,
        scroll: false,//是否显示滚动条、默认不显示
        btn: ['确定', '取消'],
        height: '700px',
        width: '450px',
        offset: 'auto',//默认垂直居中/设置top: offset:'0px'
        ismax: false,//打开是否最大化、默认不
        success: null,
        yes: null,
        cancel: null,
        end: null
    };

    if (options != null) {
        for (p in options) {
            defaultOption[p] = options[p];
        }
    }

    layer.ready(function () {
        var la = layer.open({
            title: defaultOption.title,
            type: 1,//页面层
            skin: 'demo-class',
            scrollbar: defaultOption.scroll,
            area: [defaultOption.width, defaultOption.height],
            btn: defaultOption.btn,
            offset: defaultOption.offset,
            content: defaultOption.content,
            success: function (index, layero) {
                //加载成功触发
                if (defaultOption.success != null) {
                    defaultOption.success(index, layero);
                }
            },
            yes: function (index, layero) {
                //第一个按钮触发
                if (defaultOption.yes != null) {
                    defaultOption.yes(index, layero);
                }
            },
            btn2: function (index, obj) {
                if (defaultOption.btn2 != null) {
                    return defaultOption.btn2(index, obj);
                }
            },
            cancel: function (index, layero) {
                //第二个按钮触发
                if (defaultOption.cancel != null) {
                    defaultOption.cancel(index, layero);
                }
            },
            end: function () {
                //销毁触发
                if (defaultOption.end != null) {
                    defaultOption.end();
                }
            }
        });
        if (defaultOption.ismax) {
            layer.full(la);
        }
    });
}


/**
 * SimpleDateFormat工具 使用方法与Java中相同
 */
var SimpleDateFormat = function (pattern) {
    var reg = /[\-\/\.]/g;
    var format = new RegExp("^[ymd]+" + reg.source + "[ymd]+" + reg.source
			+ "[ymd]+$", "i");
    if (!format.test(pattern)) {
        throw new Error("the pattern paramters is not legal !");
    }
    this.pattern = pattern;
    this.reg = reg;
    this.spliter = pattern.replace(/[ymd]/gi, '').substr(1);
}

SimpleDateFormat.prototype.format = function (date) {
    if (!(date instanceof Date)) {
        throw new Error("the date paramter is not Date type.");
    }
    var spliter = this.spliter;
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    if (month > 9 && day > 9) {
        return year + spliter + month + spliter + day;
    } else if (month <= 9 && day <= 9) {
        return year + spliter + "0" + month + spliter + "0" + day;
    } else if (month <= 9 && day > 9) {
        return year + spliter + "0" + month + spliter + day;
    } else if (month > 9 && day <= 9) {
        return year + spliter + month + spliter + "0" + day;
    } else {
        return year + spliter + month + spliter + day;
    }
}

SimpleDateFormat.prototype.parse = function (str) {
    var pattern = this.pattern;
    var reg = new RegExp("^" + pattern.replace(/[ymd]/gi, '\\d') + "$");
    if (!reg.test(str)) {
        throw new Error("the str paramter could not be pattered.");
    }
    var tempDate = str.split(this.spliter);
    return new Date(tempDate[0], tempDate[1] - 1, tempDate[2]);
}