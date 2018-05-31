//URL地址栏获取参数  $.GetUrlParam("参数名")
(function ($) {
    $.GetUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }
    //格式化日期 年月日
    $.DateFormat = function (jsondate) {
        jsondate = jsondate.replace("/Date(", "").replace(")/", "");
        if (jsondate.indexOf("+") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("+"));
        }
        else if (jsondate.indexOf("-") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("-"));
        }

        var date = new Date(parseInt(jsondate, 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }

    //格式化日期 年月日 时分秒
    $.DateFormatTime = function (value,date_type) {
        if (value == null || value == "") {
            return "";
        }
        var dt = parseDate(value);
        return dt.format(date_type);
    },

    //获取IIS名称
    $.GetIISName = function () {
        var curWwwPath = window.document.location.href;
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        var localhostPaht = curWwwPath.substring(0, pos);
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
        return (projectName);
    },

    //获取Table高度
    $.GetBodyHeight = function () {
        return (window.innerHeight - 6);
    },

    $.myover = function () {
        event.srcElement.style.cursor = "default";
        event.srcElement.style.color = "white";
        event.srcElement.style.background = "#1E90FF"; //blue
    },

    $.myout = function () {
        event.srcElement.style.color = "";
        event.srcElement.style.background = "";
    }
})(jQuery);

var parseDate = function (timeSpan) {
    var timeSpan = timeSpan.replace('Date', '').replace('(', '').replace(')', '').replace(/\//g, '');
    var d = new Date(parseInt(timeSpan));
    return d;
};

//为Date类型拓展一个format方法，用于格式化日期 
Date.prototype.format = function (format) // 
{
    var o = {
        "M+": this.getMonth() + 1, //month  
        "d+": this.getDate(),    //day  
        "h+": this.getHours(),   //hour  
        "m+": this.getMinutes(), //minute  
        "s+": this.getSeconds(), //second  
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter  
        "S": this.getMilliseconds() //millisecond  
    };
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1,
            (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1,
                RegExp.$1.length == 1 ? o[k] :
                    ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};