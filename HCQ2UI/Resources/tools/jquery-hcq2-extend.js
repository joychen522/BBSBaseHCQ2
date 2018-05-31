/*给jquery扩展方法*/
/*不使用布局页时通过这种方式获取上下文地址**/
(function ($) {
    $.extend($, {
        //获取上下文地址
        ctx: function () {
            var curWwwPath = window.document.location.href;
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
            var localhostPaht = curWwwPath.substring(0, pos);
            var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
            return projectName;
        },
        //获取URL地址参数
        getUrls:function(name) {
            var re = new RegExp("[&,?]" + name + "=([^\\&]*)", "i");
            var a = re.exec(document.location.search);
            if (a != null && a != undefined)
                return a[1];
            else 
                return null;
        },
        //yyyy-mm-dd
        formatDate: function (date) {
            /// <summary>日期格式化</summary>     
            /// <param name="date" type="String/Date">字符串/日期格式</param>        
            var d;
            if (date instanceof Date)
                d = date;
            else
                d = new Date(date);
            var month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;
            return [year, month, day].join('-');
        },
        formatDataLong:function(date) {
            /// <summary>日期格式化yyyy-mm-dd ss:mm</summary>     
            /// <param name="date" type="String/Date">字符串/日期格式</param>        
            var d;
            if (date instanceof Date)
                d = date;
            else
                d = new Date(date);
            var month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear(),
            hours=d.getHours(),
            minut=d.getMinutes(),
            second=d.getSeconds();
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;
            return [year, month, day].join('-') + " " + [hours, minut, second].join(':');
        },
        //字符串（yyyy-mm-dd）转日期
        strToDate: function (str) {
            if (!str)
                return null;
            str = str.replace(/-/g, '/');
            // 创建日期对象
            return new Date(str);
        },
        //获取字典数据
        getFiledCode: function (code) {
            var dataCode = null;
            if (!code)
                return null;
            $.ajax({
                url: ctx + '/SysCommon/GetDictionaryByCode',
                type: "post",
                async: false,
                data: { fieldCode: code },
                dataType: 'json',
                success: function (data) {
                    if (data.Statu === 1) 
                        layer.msg(data.Msg, { icon: 5 });
                    else
                        dataCode = data.Data;
                    return dataCode;
                },
                error: function () {
                    layer.msg('数据异常~', { icon: 5 });
                    return dataCode;
                }
            });
        }
    });
}(jQuery));
var moduleData = null;
; (function ($) {
    //表单填充数据
    $.fn.LoadForm = function (data, type) {
        var $type = "val";
        if (type)
            $type = type;
        var This = this;
        for (key in data) {
            $(This).find("[name='" + key + "']").each(function () {
                if ($(this).hasClass("selectpicker")) {
                    if($type==="text")
                        $(this).selectpicker({ noneSelectedText: data[key] });
                    else
                        $(this).selectpicker('val', data[key]);
                } else {
                    $(this).val(data[key]);
                }
            });
        }
        return this;
    }
    //auto select form 表单数据填充
    $.fn.autoLoadForm = function (data, type) {
        var $type = "val",This=this;
        if (type)
            $type = type;
        var setSelectVal = function (key, val) {
            var Inter = setInterval(function () {
                val = (val || val === false) ? val.toString() : val;
                if ($type === "text")
                    $('#' + key).selectpicker({ noneSelectedText:val});
                else
                    $('#' + key).selectpicker('val', val);
                window.clearInterval(Inter);
            }, 500);
        }
        for (key in data) {
            $(This).find("[name='" + key + "']").each(function () {
                if ($(this).hasClass("selectpicker") && key) {
                    setSelectVal(key, data[key]);
                } else {
                    $(this).val(data[key]);
                }
            });
        }
        return this;
    }
    /*重置表单验证状态并取消表单的验证提示*/
    $.fn.resetHideValidForm = function () {
        var validator = $(this).validate();
        validator.resetForm();
        //清理
        $(this).find('.has-error').each(function () {
            $(this).removeClass("has-error");
        });
        $(this).find('input').each(function () {
            $(this).tooltip('hide');
        });
        $(this).find('select').each(function () {
            $(this).tooltip('hide');
        });
        $(this).find('textarea').each(function () {
            $(this).tooltip('hide');
        });
        return this;
    }
    /*单元格合并插件：jquery.table.rowspan.js*/
    $.fn.rowspan = function (colIdx) {
        /// <summary>单元格合并插件</summary>     
        /// <param name="colIdx" type="int">colIdx要合并的列序号，从0开始</param>     
        return this.each(function() {
            var that;
            $('tr', this).each(function(row) {
                $('td:eq(' + colIdx + ')', this).filter(':visible').each(function(col) {  
                    if (that != null && $(this).html() === $(that).html()) {
                        rowspan = $(that).attr("rowSpan");
                        if (rowspan == undefined) {
                            $(that).attr("rowSpan", 1);
                            rowspan = $(that).attr("rowSpan");
                        }
                        rowspan = Number(rowspan) + 1;
                        $(that).attr("rowSpan", rowspan);
                        $(this).hide();
                    } else {
                        that = this;
                    }
                });
            });
        });
    }
    /*****selectpicker 初始化**********/
    $.fn.initSelectpicker = function (fieldCode,action,notSel) {
        var This = this;
        if (!fieldCode)
            return false;
        if (!action)
            action = "GetDictionaryByCode";
        var promise = $.ajax({
            url: ctx + '/SysCommon/' + action,
            type: "post",
            cache: false,
            async: false,
            data: { fieldCode: fieldCode },
            dataType: 'json',
            success: function (data) {
                if (data.Statu === 1) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var itemStr = "";
                $.each(data.Data, function (i, item) {
                    if (i === 0 && !notSel)
                        itemStr += "<option value='" + item.code_value + "' selected=''>" + item.code_name + "</option>";
                    else
                        itemStr += "<option value='" + item.code_value + "'>" + item.code_name + "</option>";
                });
                $(This).append(itemStr);
                $(This).selectpicker('refresh');
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
        return promise;
    }
    /*****初始化模块下拉**********/
    $.fn.initModuleSelectpicker = function () {
        var This = this;
        if (moduleData) {
            var itemStr = "";
            $.each(moduleData, function (i, item) {
                if (i === 0) {
                    //设置选中的模块 保存到cookie
                    //$.cookie("sm_code", item.value);
                    itemStr += "<option value='" + item.value + "' selected=''>" + item.text + "</option>";
                }
                else
                    itemStr += "<option value='" + item.value + "'>" + item.text + "</option>";
            });
            $(This).append(itemStr);
            $(This).selectpicker('refresh');
            return false;
        }
        $.ajax({
            url: ctx + '/SysCommon/GetModuleDictionaryByCode',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (data) {
                moduleData = data.Data;
                if (data.Statu === 1) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var itemStr = "";
                $.each(data.Data, function (i, item) {
                    if (i === 0){
                        //设置选中的模块 保存到cookie
                        //$.cookie("sm_code", item.value);
                        itemStr += "<option value='" + item.value + "' selected=''>" + item.text + "</option>";
                    }
                    else
                        itemStr += "<option value='" + item.value + "'>" + item.text + "</option>";
                });
                $(This).append(itemStr);
                $(This).selectpicker('refresh');
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    }
    /*****selectpicker 初始化**********/
    $.fn.initSelectcontrol = function (fieldCode) {
        var This = this;
        if (!fieldCode)
            return false;
        $.ajax({
            url: ctx + '/SysCommon/GetDictionaryByCode',
            type: "post",
            async: false,
            data: { fieldCode: fieldCode },
            dataType: 'json',
            success: function (data) {
                if (data.Statu === 1) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var itemStr = "";
                $.each(data.Data, function (i, item) {
                    if (i === 0)
                        itemStr += "<option value='" + item.code_value + "' selected=''>" + item.code_name + "</option>";
                    else
                        itemStr += "<option value='" + item.code_value + "'>" + item.code_name + "</option>";
                });
                $(This).append(itemStr);
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    }
})($);