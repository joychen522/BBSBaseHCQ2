//#region 帮助类
/********************************************帮助类****************************************************************/
function help() { }
/********************************************帮助类help对象公用的/***********************************************/

//只能输入纯数字
help.prototype.int = function (htmlId) {

}
/*******************************************↑帮助类help对象公用的**************************************************/


/********************************************帮助类help静态调用的函数*************************************************/
//只能输入纯数字
help.int = function (htmlId) {
    var val = $("#" + htmlId + "").val();
    $("#" + htmlId + "").val(val.replace(/\D/g, ''));
}
//时间处理
help.date = function (txt) {
    if (txt.value.length == 1) {
        txt.value = txt.value.replace(/[^\d\.\-]/g, '');
    }
    var curData = new Date();
    if (txt.value.length == 2) {
        if (txt.value != parseInt(curData.getFullYear().toString().substr(0, 2)) - 1 && txt.value != curData.getFullYear().toString().substr(0, 2)) {
            if (txt.value <= parseInt(curData.getFullYear().toString().substr(2, 4)) + 4) {
                txt.value = curData.getFullYear().toString().substr(0, 2) + txt.value + ".";
            }
            else {
                txt.value = parseInt(curData.getFullYear().toString().substr(0, 2)) - 1 + txt.value + ".";
            }
        }
    }

    if (txt.value.length > 6) {
        var str = txt.value.substr(parseInt(txt.value.indexOf(".")) + 1, txt.value.length),
            str1 = txt.value.substr(parseInt(txt.value.indexOf("-")) + 1, txt.value.length);
        if (parseInt(txt.value.indexOf(".")) != -1) {
            if (parseInt(str) > 12) {
                str = "12";
                txt.value = str1;
                txt.value = txt.value.substr(0, parseInt(txt.value.indexOf(".")) + 1) + str;
            }
        }
        if (parseInt(txt.value.indexOf("-")) != -1) {
            if (parseInt(str1) > 12) {
                str1 = "12";
                txt.value = txt.value.substr(0, parseInt(txt.value.indexOf("-")) + 1) + str1;
            }
        }
    }
}
//身份证获得焦点
help.FocusIdentify = function (id) {
    AddStaffNameClass.IdCode = $("#" + id + "").val();
}
//身份证验证
help.Identity = function (id) {
    if (AddStaffNameClass.state == 0) {
        var code = $("#" + id + "").val();
        code = code.replace(/[ ]/g, "");
        $("#" + id + "").val(code)
        if (AddStaffNameClass.IdCode == code) {//没有操作
            return;
        }
        var reg = help.IdentityId(code);
        if (reg != true) {
            help.alerterror(reg);
            AddStaffNameClass.IdentityState = 1;
            AddStaffNameClass.IdentifyAdd = 0;
        } else {
            $.post("../Ajax/GWhelp.ashx?action=StaffIdentity", { id: encodeURIComponent(code) }, function (GetData) {
                if (GetData["state"] == 1) {
                    AddStaffNameClass.IdentityState = 1;
                    if (GetData["mess"] == "存在此身份证的人员") {
                        AddStaffNameClass.IdentifyAdd = 1;
                    } else {
                        AddStaffNameClass.IdentifyAdd = 0;
                    }
                    help.alerterror(GetData["mess"]);
                } else {
                    //根据身份证得到的内容填充
                    GetData = eval("(" + GetData["data"] + ")");
                    $("#TXT_hometown_name").val(GetData["JG"]);
                    $("#SELECT_staff_sex").combobox("select", GetData["XB"]);
                    $('#DATETIME_staff_birth').val(GetData["SR"]);
                    InputOnchange(document.getElementById("TXT_hometown_name"));
                    InputOnchange(document.getElementById("DATETIME_staff_birth"));
                    AddStaffNameClass.IdentityState = 1;
                    AddStaffNameClass.IdentifyAdd = 0;
                }
            });
        }
    }
}
//身份证验证
help.IdentityId = function (sId) {
    var aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }
    var iSum = 0;
    var info = "";
    if (!/^\d{17}(\d|x)$/i.test(sId)) return "你输入的身份证长度或格式错误";
    sId = sId.replace(/x$/i, "a");
    if (aCity[parseInt(sId.substr(0, 2))] == null) return "你的身份证地区非法";
    sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
    var d = new Date(sBirthday.replace(/-/g, "/"));
    if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) return "身份证上的出生日期非法";
    for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
    if (iSum % 11 != 1) return "你输入的身份证号非法";
    return true; //aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女")  
}
//时间处理的补全时间
help.BlulDate = function (id) {
    var value = $("#" + id + "").val(),
        val = "";
    if (value.length > 5) {
        val = value.substring(0, 5);
        value = value.substring(5, 6);
        if (value < 10 && value > 0) {
            value = "0" + "" + value + "";
            val = "" + val + "" + value;
            $("#" + id + "").val(val);
        }
    } else if (value.length == 5) {
        $("#" + id + "").val("" + value + "" + "01");
    }
}
///比较时间大小
help.isdate = function (startTime, endTime) {
    var arr1 = startTime.split("-");
    var arr2 = endTime.split("-");
    var date1 = new Date(parseInt(arr1[0]), parseInt(arr1[1]) - 1, parseInt(arr1[2]), 0, 0, 0);
    var date2 = new Date(parseInt(arr2[0]), parseInt(arr2[1]) - 1, parseInt(arr2[2]), 0, 0, 0);
    if (date1.getTime() > date2.getTime()) {
        //        alert('结束日期不能小于开始日期', this);
        return false;
    } else {
        return true;
    }
    return false;
}
//判断对象是否为空。var a= new Obj();  var a={};空返回false/不为空返回true
help.isObjNull = function (obj) {
    for (var name in obj) {
        if (obj.hasOwnProperty(name)) {
            return false;
        }
    }
    return true;
}
//判断是否是谷歌浏览器
help.isChrome = function () {
    return navigator.userAgent.toLowerCase().match(/chrome/) != null;
}
//获取高度和宽度
help.heightwidth = function () {
    var winWidth = 0,
        winHeight = 0;
    //获取窗口宽度 
    if (window.innerWidth)
        winWidth = window.innerWidth;
    else if ((document.body) && (document.body.clientWidth))
        winWidth = document.body.clientWidth;
    //获取窗口高度 
    if (window.innerHeight)
        winHeight = window.innerHeight;
    else if ((document.body) && (document.body.clientHeight))
        winHeight = document.body.clientHeight;
    //通过深入Document内部对body进行检测，获取窗口大小 
    if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
        winHeight = document.documentElement.clientHeight;
        winWidth = document.documentElement.clientWidth;
    }
    return { width: winWidth, height: winHeight };
}
//弹出无图标的
help.alert = function (Str) {
    //$.messager.alert('系统消息', Str);
    art.dialog({
        title: '系统消息',
        zIndex: 10000,
        lock: true,
        content: Str
    });
}
//弹出错误信息
help.alerterror = function (Str) {
    //$.messager.alert('异常消息', Str, 'error');
    art.dialog({
        title: '系统消息',
        icon: 'error',
        lock: true,
        zIndex: 10000,
        content: Str
    });
}
//弹出警告消息
help.alterj = function (Str) {
    //$.messager.alert('系统消息', Str, 'warning');
    art.dialog({
        title: '系统消息',
        icon: 'warning',
        lock: true,
        zIndex: 10000,
        content: Str
    });
}
//弹出问题消息
help.alertw = function (Str) {
    //$.messager.alert('系统消息', Str, 'question');
    art.dialog({
        title: '系统消息',
        icon: 'question',
        lock: true,
        zIndex: 10000,
        content: Str
    });
}
//弹出成功消息
help.alertc = function (Str) {
    //$.messager.alert('系统消息', Str, 'info');
    art.dialog({
        title: '系统消息',
        time: 1,
        zIndex: 10000,
        icon: 'succeed',
        content: Str
    });
}
//交互的弹出框
help.jh = function (content, yes, no) {
    return artDialog({
        id: 'Confirm',
        icon: 'question',
        fixed: true,
        lock: true,
        zIndex: 10000,
        opacity: .1,
        content: content,
        ok: function (here) {
            return yes.call(this, here);
        },
        cancel: function (here) {
            return no && no.call(this, here);
        }
    });
};
//跟随元素的弹出框
help.follw = function (id, value) {
    art.dialog({
        follow: document.getElementById(id),
        content: value
    });
}
//正在加载
help.title = function (str) {
    return artDialog({
        id: 'Tipsopen',
        title: false,
        content: str,
        cancel: false,
        fixed: true,
        lock: true
    })
}
//关闭正在加载
help.titleclose = function () {
    art.dialog.list["Tipsopen"].close();
}
//获取iframe
help.IframeStr = function (name, url) {
    return "<iframe name='" + name + "' frameborder='0' src='" + url + "' style='width:100%;height:100%;'></iframe>";
}
//错误处理
help.JsError = function (err) {
    var txt = "此页面存在一个错误。\n\n"
    txt += "错误描述: " + err.description + "\n\n"
    txt += "点击OK继续。\n\n"
    alert(txt);
}
//禁用复制粘贴
help.CV = function () {
    if (event.ctrlKey && (event.keyCode == 86 || event.keyCode == 67)) //禁用Ctrl+C 和Ctrl+V
    {
        return false;
    }
}

