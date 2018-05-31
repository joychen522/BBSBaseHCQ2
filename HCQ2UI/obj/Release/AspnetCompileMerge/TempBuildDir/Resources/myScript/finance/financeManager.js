/*******************************************************
 *  工资发放 相关操作js
 * <p>Title: financeManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月6日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table,
    selUnit = "", $addOrEditUrl;
var financeManager = {
    //初始化页面
    initPage:function() {
        parent.showLoadBoxs();
        financeManager.initUnitDataByAjax();
        financeManager.initDate();
        financeManager.initSelect();
        financeManager.initTable();
        financeManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化单位树
    initUnitDataByAjax:function() {
        var $treeUnit = $('#tree').initB01TreeView({
            data: null,
            checkFirst: true,
            onNodeSelected: function (e, o) {
                if (o.unitID != null && o.unitID != "null" && o.unitID != "") {
                    selUnit = o.unitID;
                    $table.bootstrapTable('refresh');
                }
            }
        });
        //初始化时选择一个节点
        var temp = $treeUnit.treeview('getEnabled')[0];
        if (temp != null) {
            $treeUnit.treeview('selectNode', [temp.nodeId, { silent: true }]);
            selUnit = temp.unitID;
        }
    },
    //初始化日期控件
    initDate:function() {
        var dateStart = {
            elem: '#dateStart',
            format: 'YYYY-MM-DD',
            max: '2099-06-16 23:59:59', //最大日期
            istime: true,
            istoday: false,
            choose: function (datas) {
                dateEnd.min = datas; //开始日选好后，重置结束日的最小日期
                dateEnd.start = datas; //将结束日的初始值设定为开始日
            }
        };
        var dateEnd = {
            elem: '#dateEnd',
            format: 'YYYY-MM-DD',
            min: laydate.now(),
            max: '2099-06-16 23:59:59',
            istime: true,
            istoday: false,
            choose: function (datas) {
                dateStart.max = datas; //结束日选好后，重置开始日的最大日期
            }
        };
        laydate(dateStart);
        laydate(dateEnd);
        //$('#dateStart').val(laydate.now().substring(0, 4) + '-01-01');
        //$('#dateEnd').val(laydate.now().substring(0, 4) + '-12-31');
    },
    initSelect: function () {
        $('#WGJG0101').initSelectpicker("GDBS");
        $('#CodeItem').initSelectpicker("GDBS");
    },
    //初始化列表
    initTable:function() {
        var options = {
            url: ctx + "/Finance/InitTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    WGJG0103: encodeURI($('#userName').val()),//发放原因
                    CodeItemValue: $('#CodeItem').selectpicker('val'),//状态
                    startDate:$('#dateStart').val(),
                    endDate: $('#dateEnd').val(),
                    unitID: selUnit//单位代码
                }
                return params;
            },
            columns: [
            {
                radio:true
            },{
                     field: '',//第一列序号
                     title: '序号',
                     align: 'center',
                     width: 50,
                     formatter: function (value, row, index) {
                        return index + 1;
                     }
                 }, {
                     field: 'UnitName',
                     title: '用工单位',
                     align: 'center'
                }, {
                     field: 'CodeItemName',
                     title: '归档标示',
                     align: 'center'
                 }, {
                     field: 'WGJG0102',
                     title: '发放时间',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined) {
                             var data = $.formatDate(new Date(parseInt(value.slice(6))));"1-01-01";
                             return data === "1-01-01" ? "-" : data;
                         }
                         else
                            return "-";
                     }
                 }, {
                     field: 'WGJG0103',
                     title: '发放原因',
                     align: 'left'
                }, {
                     field: 'WGJG0107',
                     title: '约定发薪日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined) {
                             var data =$.formatDate(new Date(parseInt(value.slice(6))));
                             return data === "1-01-01" ? "-" : data;
                         }
                         else
                            return "-";
                     }
                 }, {
                     field: 'WGJG0105',
                     title: '发放主题',
                     align: 'center',
                     formatter: function(value, row, index) {
                         return "<a href='#' onclick=financeManager.openWageDetail('" + row.RowID + "')>查看</a>";
                     }
                 }, {
                     field: 'allPerson',
                     title: '总人数',
                     align: 'center'
                 }, {
                     field: 'surePerson',
                     title: '已确认',
                     align: 'center'
                 }, {
                     field: 'WGJG0106',
                     title: '制单人',
                     align: 'center'
                 }, {
                     field: 'WGJG0104',
                     title: '备注',
                     align: 'center',
                     formatter: function (value, row, index) {
                         return "<a href='#' onclick=financeManager.openWGJG0104('" + row.RowID + "')>查看</a>";
                     }
                 }],
            onClickRow: function (row, $element) {
                if (row.RowID != null) {
                    //启用
                    $('#financeGrant').attr("disabled", false);
                    $('#financeTake').attr("disabled", false);
                } else {
                    //禁用
                    $('#financeGrant').attr("disabled", true);
                    $('#financeTake').attr("disabled", true);
                }
            },
            onLoadSuccess:function(data) {
                $("th[data-field='WGJG0103']").css("text-align", "center");
            }
        }
        $table = tableHelper.initTable("financeTableToolbar", options);
    },
    //绑定事件
    bindEvent: function () {
        //添加
        $('#financeAdd').click(function() {
            layer.msg("研发中，请等待........");
        });
        //编辑
        $('#financeEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            financeManager.editForm(row[0]);
        });
        //删除
        $('#financeDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择记录？', {
                btn: ['确定', '取消'] //按钮
            }, function() {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Finance/DelFinace?rowid=' + row[0].RowID,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg(data.Msg);
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            },function(){});
        });
        //发放
        $('#financeGrant').click(function () {
            layer.msg("研发中，请等待........");
        });
        //归档
        $('#financeTake').click(function () {
            layer.msg("研发中，请等待........");
        });
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
    },
    editForm:function(row) {
        //清空表单
        $('#groupFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑', 'font-size:18px;'],
            type: 1,
            content: $('#group_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    if (row.WGJG0107 != null)
                        row.WGJG0107 = $.formatDate(new Date(parseInt(row.WGJG0107.slice(6))));
                    $('#groupFormTable')[0].reset(); //重置表单
                    $('#groupFormTable').LoadForm(row); //表单填充数据
                    $('#unitinfo').val(row.UnitName);
                    $('#WGJG0101').selectpicker('val', row.CodeItemValue);
                }
            },
            yes: function (li, o) {
                if ($('#groupFormTable').valid()) {
                    //验证通过
                    $('#groupFormTable').ajaxSubmit({
                        url: ctx + "/Finance/EditFinance?rowid=" + row.RowID,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                $table.bootstrapTable('refresh');
                                layer.msg('保存成功', { icon: 1 });
                            }
                            else
                                layer.alert(result.Msg, { icon: 5 });
                        },
                        error: function (xhr, status, error, $form) {
                            layer.msg("保存失败~", { icon: 5 });
                        }
                    });
                }
            },
            cancel: function (li, o) {

            }
        });
    },
    openWageDetail:function(rowid) {
        //打开编辑
        layer.open({
            title: ['发放人员', 'font-size:18px;'],
            type: 2,
            content: ctx + '/Finance/GrantPersonsList?rowid=' + rowid,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定'],
            area: ['1000px', '98%'],
            cancel: function (li, o) {

            }
        });
    },
    //查看备注
    openWGJG0104:function(rowid) {
        $.ajax({
            url: ctx + '/Finance/GetNoteByRowId',
            type: "post",
            async: false,
            data:{rowid:rowid},
            dataType: 'json',
            success: function (mess) {
                layer.open({
                    icon:6,
                    shade: false,
                    title: false,
                    content: mess.Msg === null ? '无备注信息' : mess.Msg,
                    btn: ''
                });
            },
            error: function () {
                layer.msg('登录异常~', { icon: 5 });
            }
        });
    }
}

//工资维护
var addOrEditUrl,
    selUnit = "",
    addUnit = null,
    selName = "";
var initSelectableTree;
var wageManager = {
    //初始化单位树
    initUnitDataByAjax: function () {
        initSelectableTree = $('#tree').initB01TreeView({
            data: null,
            checkFirst: true,
            onNodeSelected: function (e, o) {
                addUnit = null;
                if (o.unitID != null && o.unitID != "null" && o.unitID != "") {
                    selUnit = o.unitID;
                    selName = o.text;
                    addUnit = o.unitID;
                    $table.bootstrapTable('refresh');
                }
            },
            onNodeUnchecked: function (event, node) {
                addUnit = null;
                selUnit = null;
                selName = null;
                $table.bootstrapTable('refresh');
            }
        });
        //初始化时选择一个节点
        var temp = initSelectableTree.treeview('getEnabled')[0];
        if (temp != null) {
            initSelectableTree.treeview('selectNode', [temp.nodeId, { silent: true }]);
            addUnit = temp.unitID;
            selUnit = temp.unitID;
            selName = temp.text;
        }
    },
    searchUnit: function () {
        //清空之前的搜索状态
        $checkableTree.treeview('clearSearch');
        var selUnit = $('#selRow').val();
        var result = $checkableTree.treeview('search', [selUnit, {
            ignoreCase: true,     // case insensitive
            exactMatch: true,    // like or equals
            revealResults: true  // reveal matching nodes
        }]);
        if (result.length > 0) 
            $checkableTree.treeview('checkNode', [result, { silent: true }]);
    },
    //初始化Table
    initTable:function() {
        var options = {
            url: ctx + "/Finance/InitGroupTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    unitID: selUnit//单位代码
                }
                return params;
            },
            columns: [
            {
                radio: true
            }, {
                field: '',//第一列序号
                title: '序号',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    return index + 1;
                }
            }, {
                field: 'WGJG0103',
                title: '发放主题',
                align: 'left'
            }, {
                field: 'WGJGDAY',
                title: '约定发薪日',
                align: 'center'
            }, {
                field: 'WGJG0203',
                title: '工资发放方式',
                align: 'center'
            }, {
                field: 'allPerson',
                title: '人数',
                align: 'center'
            }, {
                field: 'WGJG0101',
                title: '状态',
                align: 'center',
                cellStyle: function (value, row, index, field) {
                    //active,success,info,warning,danger
                    if(value==="冻结")
                        return { classes: 'danger' };
                    return { classes: 'success' };
                    //return { css: {
                    //    "background":"red"
                    //} };
                    //
                }
            }, {
                field: 'WGJG0105',
                title: '发放详情',
                align: 'center',
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=wageManager.openWageTempDetail('" + row.RowID + "','" + row.UnitName + "','" + row.UnitID + "')>管理</a>";
                }
            }],
            onClickRow: function (row, $element) {
                if (row.RowID != null) {
                    //启用
                    $('#groupGrant').attr("disabled", false);
                } else {
                    //禁用
                    $('#groupGrant').attr("disabled", true);
                }
            },
            onLoadSuccess: function (data) {
                $("th[data-field='WGJG0103']").css("text-align", "center");
            }
        }
        $table = tableHelper.initTable("financeTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        $('#WGJG0101').initSelectpicker("ZJFFBJ");
        $('#WGJG0203').initSelectpicker("GZFFFS");
    },
    //绑定事件
    bindEvent: function () {
        //添加
        $('#groupAdd').click(function () {
            if (addUnit === null) {
                layer.msg("请选择底层单位~", { icon: 5 });
                return false;
            }
            wageManager.editForm();
        });
        //编辑
        $('#groupEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            wageManager.editForm(row[0]);
        });
        //删除
        $('#groupDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择记录？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Finance/DelTemplateById/' + row[0].DispOrder,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu == 0) {
                            layer.msg(data.Msg);
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
        //发放
        $('#groupGrant').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            if (row[0].CodeItemValue != "04") {
                layer.msg("温馨提示 只有标记为：发起状态才能发放~", { icon: 5 });
                return false;
            }
            layer.open({
                title: '填写约定发放日期', //不显示标题
                type: 1,
                area: ['480px', '180px'],
                btn:['确定','取消'],
                content: $('#ydDate'),
                yes:function() {
                    var wgDate = $('#WGJG0107').val();
                    if (wgDate === null || wgDate === "") {
                        layer.msg("请正确选择时间~", { icon: 5 });
                        return false;
                    }
                    //wageManager.grantWage(wgDate, row[0].RowID);
                    //发放前验证是否发放过
                    $.ajax({
                        url: ctx + '/Finance/CheckGrantWage',
                        type: "post",
                        async: false,
                        data: { "wgDate": wgDate, "rowid": row[0].RowID },
                        dataType: 'json',
                        success: function (result) {
                            if (result.Statu === 1) {
                                layer.confirm('温馨提示：'+result.Msg+'是否需要重新发放？', {
                                    btn: ['确定', '取消'] //按钮
                                }, function () {
                                    //确定
                                    wageManager.grantWage(wgDate, row[0].RowID);
                                }, function () { });
                            } else {
                                wageManager.grantWage(wgDate, row[0].RowID);
                            }
                        },
                        error: function () {
                            layer.msg('数据异常~', { icon: 5 });
                        }
                    });
                },
                cancel:function() {
                    
                }
            });
        });
        //测试登录
        //$('#groupAPIGrant').click(function () {
        //    var index = layer.msg("登录中...", { icon: 6, time: 6000 });
        //    $.ajax({
        //        type: 'post',
        //        url: 'http://58.16.28.4/HCQ2API/api/SysLoginApi/AppUserLogin',
        //        dataType: 'json',
        //        data: {
        //            "match_signature": "af895dc00e502574d82c9cd9a9f7cfcb",
        //            "match_timestamp": "20170316144611",
        //            "match_nonce":"123",
        //            "user_name": "lqq",
        //            "user_password":"123"
        //        },
        //        async: false,
        //        success: function (data) {
        //            layer.close(index);
        //            if (data.errcode === 0) 
        //                layer.alert(JSON.stringify(data));
        //            else
        //                layer.msg(data.errmsg, { icon: 5 });
        //        }
        //    });
        //});
        ////测试工资下发
        //$('#groupWageSent').click(function () {
        //    var index = layer.msg("工资下发中...", { icon: 6, time: 6000 });
        //    $.ajax({
        //        type: 'post',
        //        url: 'http://localhost/HCQ2WebAPI/api/WageManager/WageSentDown',
        //        contentType: "application/json",
        //        dataType: "json",
        //        data: {
        //            match_signature: "af895dc00e502574d82c9cd9a9f7cfcb",
        //            match_timestamp: "20170316144611",
        //            match_nonce: "123",
        //            userid: "85bd789b540c4da39fb8904b8a2ed7c6",
        //            orgid: "011002"
        //        },
        //        async: false,
        //        success: function (data) {
        //            layer.close(index);
        //            if (data.errcode === 0)
        //                layer.alert(JSON.stringify(data));
        //            else
        //                layer.msg(data.errmsg, { icon: 5 });
        //        },
        //        error: function (XMLHttpRequest, textStatus, errorThrown) {
        //            layer.msg("异常");
        //            // 通常 textStatus 和 errorThrown 之中
        //            // 只有一个会包含信息
        //            //this; // 调用本次AJAX请求时传递的options参数
        //        }
        //    });
        //});
        ////测试工资登记
        //$('#groupWageCheck').click(function () {
        //    var index = layer.msg("工资登记中...", { icon: 6, time: 6000 });
        //    $.ajax({
        //        type: 'post',
        //        url: 'http://58.16.28.4/HCQ2API/api/WageManager/WageRegister',
        //        dataType: 'json',
        //        data: {
        //            "match_signature": "af895dc00e502574d82c9cd9a9f7cfcb",
        //            "match_timestamp": "20170316144611",
        //            "match_nonce": "123",
        //            "userid": "c0705a7f9b694d5a800b32c9cbf6915b",
        //            "personsalaryid": "02358611F80246FEA810CDDCBE0B3F2F",
        //            "personid": "EEE302E1529B4FD08ADD02E7AB70F83E",//陈松
        //            "signtime": "2017-03-27 15:15:20"
        //        },
        //        async: false,
        //        success: function (data) {
        //            layer.close(index);
        //            if (data.errcode === 0)
        //                layer.alert(JSON.stringify(data));
        //            else
        //                layer.msg(data.errmsg, { icon: 5 });
        //        }
        //    });
        //});
        ////测试组织机构下发
        //$('#groupOrg').click(function () {
        //    var index = layer.msg("组织机构下发中...", { icon: 6, time: 6000 });
        //    $.ajax({
        //        type: 'post',
        //        url: 'http://58.16.28.4/HCQ2API/api/OrgManager/OrgSentDown',
        //        dataType: 'json',
        //        data: {
        //            "match_signature": "af895dc00e502574d82c9cd9a9f7cfcb",
        //            "match_timestamp": "20170316144611",
        //            "match_nonce": "123",
        //            "userid": "c0705a7f9b694d5a800b32c9cbf6915b"
        //        },
        //        async: false,
        //        success: function (data) {
        //            layer.close(index);
        //            if (data.errcode === 0)
        //                layer.alert(JSON.stringify(data));
        //            else
        //                layer.msg(data.errmsg, { icon: 5 });
        //        }
        //    });
        //});
    },
    grantWage: function (wgDate, rowid) {
        layer.msg("发放中......", { icon: 6, time: 6000 });
        //开始发放
        $.ajax({
            url: ctx + '/Finance/StartGrantWage',
            type: "post",
            async: false,
            data: { "wgDate": wgDate, "rowid": rowid },
            dataType: 'json',
            success: function (mess) {
                layer.closeAll();
                layer.msg(mess.Msg,{icon:6});
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "新增";
        addOrEditUrl = ctx + "/Finance/AddTemplate"; //新增
        if (row != null && row != undefined) {
            $title = "编辑";
            addOrEditUrl = ctx + "/Finance/EditTemplate?id=" + row.DispOrder;//编辑
        }
        //清空表单
        $('#groupFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type:1,
            content: $('#group_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    if (row.WGJG0107 != null) 
                        row.WGJG0107 = $.formatDate(new Date(parseInt(row.WGJG0107.slice(6))));
                    $('#groupFormTable')[0].reset(); //重置表单
                    $('#groupFormTable').LoadForm(row); //表单填充数据
                    $('#unitinfo').val(row.UnitName);
                    $('#WGJG0101').selectpicker('val', row.CodeItemValue);
                    $('#WGJG0203').selectpicker('val', row.WGJG0203_Name);
                } else {
                    $('#unitinfo').val(selName);
                    $('#UnitID').val(addUnit);//单位代码
                }
            },
            yes: function (li, o) {
                if ($('#groupFormTable').valid()) {
                    //验证通过
                    $('#groupFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                $table.bootstrapTable('refresh');
                                layer.msg('保存成功', { icon: 1 });
                            }   
                            else
                                layer.alert(result.Msg, { icon: 5 });
                        },
                        error: function (xhr, status, error, $form) {
                            layer.msg("保存失败~", { icon: 5 });
                        }
                    });
                }
            },
            cancel: function(li,o) {

            }
        });
    },
    openWageTempDetail: function (rowid, UnitName,UnitID) {
        //打开编辑
        layer.open({
            title: ['发放人员', 'font-size:18px;'],
            type: 2,
            content: ctx + '/Finance/WageTempList?rowid=' + rowid + '&UnitName=' + encodeURI(UnitName) + '&UnitID=' + UnitID,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定'],
            area: ['1000px', '98%'],
            yes: function (li, o) {
                layer.closeAll();
                $table.bootstrapTable('refresh');
            },
            cancel: function (li, o) {
                $table.bootstrapTable('refresh');
            }
        });
    }
}

//工资发放详细
var $tableWage, demo2 = null;
var tips = null;
var wageDetailManager = {
    //初始化列表
    initTable: function () {
        var options = {
            url: ctx + "/Finance/InitWageTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            singleSelect:false,//非单选
            queryParams: function(params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    userName: encodeURI($('#userName').val()),
                    unitID: $.getUrls("unitID"),
                    dateStart: $.getUrls("dateStart"),
                    dateEnd: $.getUrls("dateEnd"),
                    rowid: $.getUrls("rowid"),
                    sendType: $.getUrls("sendType")
                }
                return params;
            },
            columns: [
                {
                    checkbox: true
                }, {
                    field: '',//第一列序号
                    title: '序号',
                    align: 'center',
                    width: 50,
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: 'WGJG0201',
                    title: '发放时间',
                    align: 'center',
                    formatter: function (value, row, index) {
                        if (value != null && value != "" && value != undefined) {
                            var temp=$.formatDate(new Date(parseInt(value.slice(6))));
                            return temp === "1-01-01" ? "" : temp;
                        }
                        else
                            return "";
                    }
                }, {
                    field: 'WGJG0202',
                    title: '约定发薪日期',
                    align: 'center',
                    formatter: function (value, row, index) {
                        if (value != null && value != "" && value != undefined) {
                            var temp = $.formatDate(new Date(parseInt(value.slice(6))));
                            return temp === "1-01-01" ? "" : temp;
                        }
                        else
                            return "";
                    }
                }, {
                    field: 'A0101',
                    title: '&nbsp;&nbsp;姓名&nbsp;&nbsp;',
                    width:100,
                    align: 'center'
                }, {
                    field: 'E0386',
                    title: '&nbsp;&nbsp;&nbsp;工种&nbsp;&nbsp;&nbsp;',
                    align: 'center'
                }, {
                    field: 'WGJG0203',
                    title: '发放方式',
                    align: 'center'
                }, {
                    field: 'WGJG0204',
                    title: '工资',
                    align: 'center'
                }, {
                    field: 'WGJG0205',
                    title: '打卡天数',
                    align: 'center'
                }, {
                    field: 'WGJG0206',
                    title: '实际工作天数',
                    align: 'center'
                }, {
                    field: 'WGJG0207',
                    title: '应发工资',
                    align: 'center'
                }, {
                    field: 'WGJG0208',
                    title: '实际发放',
                    align: 'center'
                }, {
                    field: 'WGJG0209',
                    title: '备注',
                    align: 'center'
                }, {
                    field: 'WGJG0212',
                    title: '签到时间',
                    align: 'center',
                    formatter: function (value, row, index) {
                        if (value != null && value != "" && value != undefined) {
                            var dt=$.formatDate(new Date(parseInt(value.slice(6))));
                            if (dt === "1-01-01")
                                return "";
                            return dt;
                        }
                        else
                            return "";
                    }
                }, {
                    field: 'PClassID',
                    title: '人员库',
                    align: 'center'
                }]
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    initGrantEvent:function() {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
    },
    //*************************************
    initSelect:function(){
        $.ajax({
            url: ctx + '/Finance/GetInTeamData',
            type: "post",
            async: false,
            data: { UnitID: $.getUrls("UnitID") },
            dataType: 'json',
            success: function (data) {
                if (data.Statu === 1) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var itemStr = "";
                $.each(data.Data, function (i, item) {
                    if (i === 0)
                        itemStr += "<option value='" + item.com_value + "' selected=''>" + item.com_text + "</option>";
                    else
                        itemStr += "<option value='" + item.com_value + "'>" + item.com_text + "</option>";
                });
                $('#in_team').append(itemStr);
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
        $.ajax({
            url: ctx + '/SysCommon/GetDictionaryByOldSysCode',
            type: "post",
            async: false,
            data: { fieldCode: $.getUrls("UnitID") },
            dataType: 'json',
            success: function (data) {
                if (data.Statu === 1) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var itemStr = "";
                $.each(data.Data, function (i, item) {
                    if (i === 0)
                        itemStr += "<option value='" + item.CodeValue + "' hassubinfo=\"true\" selected=''>" + item.CodeText + "</option>";
                    else
                        itemStr += "<option value='" + item.CodeValue + "' hassubinfo=\"true\">" + item.CodeText + "</option>";
                });
                $('#post0386').append(itemStr);
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //绑定事件
    initEvent: function () {
        //单位选择
        $('#selUnit').click(function () {
            var row = $table.bootstrapTable('getData');
            if (row != null && row.length > 0) {
                layer.confirm('当前模板下已有数据，重新选择将会覆盖之前生成的数据，您确定要重新生成吗？', { btn: ['确定', '取消'] }, function() {
                    wageDetailManager.createData();
                }, function() { });
            } else
                wageDetailManager.createData();
        });
        //手动选择
        $('#selPerson').click(function() {
            var row = $table.bootstrapTable('getData');
            var index = null;
            if (row != null && row.length > 0) {
                index = layer.confirm('当前模板下已有数据，重新选择将会覆盖之前生成的数据，您确定要重新生成吗？', { btn: ['确定', '取消'] }, function() {
                    wageDetailManager.createDataBySelect(index);
                }, function() {});
            } else
                wageDetailManager.createDataBySelect(index);
        });
        //编辑
        $('#TempEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            wageDetailManager.editWageTempForm(row[0]);
        });
        //删除
        $('#TempDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择用户？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                layer.msg("删除中......", {
                    icon: 6,
                    time: 6000
                });
                var row = $table.bootstrapTable('getSelections');
                if (row != null && row.length === 0) {
                    layer.msg("未选中行~", { icon: 5 });
                    return false;
                }
                $.ajax({
                    url: ctx + '/Finance/DelWageTemp?a0177=' + row[0].A0177,
                    type: "post",
                    async: false,
                    dataType: 'json',
                    success: function (mess) {
                        if (mess.Statu == 0) {
                            layer.closeAll();
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.open({
                                shade: false,
                                title: false,
                                content: mess.Msg,
                                btn: ''
                            });
                    },
                    error: function () {
                        layer.msg('数据异常~', { icon: 5 });
                    }
                });
            },function(){});
        });
        //查询
        $('#btnSearch').click(function() {
            $table.bootstrapTable('refresh');
        });
        //打开导入
        $('#btnImport').mousemove(function () {
            if (!tips) {
                tips = layer.tips('温馨提示：导入之前请下载模板编辑后再上传导入！', '#btnImport', {
                    tips: [1, '#3595CC'],
                    time: 4000,
                    end: function () {
                        tips = null;
                    }
                });
            }
        }).blur(function () {
            tips = null;
        }).click(function () {
            layer.open({
                title: ['导入Excel：', 'font-size:18px;'],
                type: 1,
                content: $('#wageImport'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['取消'],
                area: ['480px', '260px'],
                cancel: function (li, o) {

                }
            });
        });
        //打开下载
        $('#btnDownloadTemplate').click(function () {   
            layer.open({
                title: ['模板参数配置：', 'font-size:18px;'],
                type: 1,
                content: $('#wageDownload'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['下载','取消'],
                area: ['480px', '80%'],
                yes: function (li, o) {
                    wageDetailManager.downloadTemp();
                    return false;
                },
                cancel: function (li, o) {

                }
            });
        });
        //导入事件
        $('#btnImportPerson').click(function () {
            var UnitID = $.getUrls("UnitID"), rowid = $.getUrls("rowid");
            var file = $('#filePerson');
            if (!file.val()) {
                layer.msg("上传文件为空！");
                return false;
            }
            if (file.val().split('.')[1] != "xls") {
                layer.msg("上传文件格式不正确！请正确上传.xls文件！");
                return false;
            }
            if ($('#wageImportFormTable').valid()) {
                //验证通过
                $('#wageImportFormTable').ajaxSubmit({
                    url: ctx + '/Finance/ImportPersonData?UnitID=' + UnitID + '&RowID=' + rowid,
                    type: "post",
                    dataType: "json",
                    beforeSubmit: function (arr, $form, options) {
                        layer.msg("提交数据~", { icon: 1, time: 5000 });
                    },
                    success: function (result, status, xhr, $form) {
                        layer.closeAll();
                        if (result.Statu === 0) {
                            $table.bootstrapTable('refresh');
                            layer.msg('保存成功', { icon: 1 });
                        }
                        else
                            layer.alert(result.Msg, { icon: 5 });
                    },
                    error: function (xhr, status, error, $form) {
                        layer.msg("保存失败~", { icon: 5 });
                    }
                });
            }
        });
    },
    //下载模板事件
    downloadTemp:function(){
        var UnitID = $.getUrls("UnitID");
        layer.msg("导出中......", { icon: 1, time: 6000 });
        var params = "UnitID=" + UnitID + "&post0386=" + $('#post0386').val() + "&in_team=" + $('#in_team').val();
        var personExport = $('#personExport');
        personExport.attr("href", ctx + "/Finance/DownLoadTemplate?" + params);
        document.getElementById("personExport").click();
        return false;
    },
    //单位选择：生成数据
    createData:function() {
        layer.msg("提交数据中......", {
            icon: 6,
            time: 6000
        });
        var rowid = $.getUrls("rowid");
        $.ajax({
            url: ctx + '/Finance/CreateDataByRowId?rowid=' + rowid,
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu == 0) {
                    layer.closeAll();
                    $table.bootstrapTable('refresh');
                }
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //手动选择：生成数据
    createDataBySelect: function (index) {
        var UnitID = $.getUrls("UnitID");
        $.ajax({
            url: ctx + '/Finance/GetPersonByUnitID',
            type: "post",
            async: false,
            data: { "UnitID": UnitID },
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0) {
                    var obj = mess.Data;
                    $('.demo1').empty();//清空
                    $(obj).each(function () {
                        $("<option value='" + this['value'] + "' class='listBoxoption'>" + this['text'] + "</option>").appendTo($('.demo1'));
                    });
                    wageDetailManager.initListBox(index);
                }
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //初始化ListBox
    initListBox: function (index) {
        if (demo2)
            demo2.bootstrapDualListbox('refresh');
        demo2 = $('.demo1').bootstrapDualListbox({
            nonSelectedListLabel: '未选择数据',
            selectedListLabel: '已选择数据',
            preserveSelectionOnMove: '撤销',
            filterTextClear: '显示全部',
            filterPlaceHolder: '输入关键字筛选',
            moveSelectedLabel: '单个选择',
            moveAllLabel: '全部选择',
            removeSelectedLabel: '单个撤销',
            removeAllLabel: '全部撤销',
            infoText: '显示全部 {0}',
            infoTextEmpty: '未选择记录',
            infoTextFiltered: '<span class="label label-warning">检索到</span> {0} 总共 {1}',
            moveOnSelect: false
        });
        var UnitName = decodeURI($.getUrls("UnitName"));
        var createIndex = layer.open({
            title: [UnitName + '：选择人员生成数据：支持多个关键字同时检索用：| 分割', 'font-size:18px;'],
            type: 1,
            content: $('#createDataBySel'),
            scroll: true, //是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['680px', '98%'],
            success: function(li, o) {
                if (index)
                    layer.close(index);
            },
            yes: function(li, o) {
                //确认生成数据
                var selData = $('[name="duallistbox_demo1"]').val();
                if (selData) {
                    $.ajax({
                        url: ctx + '/Finance/CreateDataBySelect',
                        type: "post",
                        async: false,
                        data: { "personData": selData.join(','), "rowid": $.getUrls("rowid") },
                        dataType: 'json',
                        success: function (mess) {
                            if (mess.Statu === 0)
                                layer.msg(mess.Msg, { icon: 6 });
                            else
                                layer.msg(mess.Msg, { icon: 5 });
                            layer.close(createIndex);
                            $table.bootstrapTable('refresh');
                        },
                        error: function() {
                            layer.msg('数据异常~', { icon: 5 });
                        }
                    });
                }
            },
            cancel: function(li, o) {

            }
        });
    },
    editWageTempForm: function (row) {
        if (row === null || row === undefined) {
            layer.meg("数据异常~",{icon:5});
            return false;
        }
        //清空表单
        $('#wageTempFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑：'+row.A0101, 'font-size:18px;'],
            type: 1,
            content: $('#wageTemp_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    if (row.WGJG0202 != null)
                        row.WGJG0202 = $.formatDate(new Date(parseInt(row.WGJG0202.slice(6))));
                    $('#wageTempFormTable')[0].reset(); //重置表单
                    $('#wageTempFormTable').LoadForm(row); //表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#wageTempFormTable').valid()) {
                    //验证通过
                    $('#wageTempFormTable').ajaxSubmit({
                        url: ctx + '/Finance/EditWageTemp?a0177='+row.A0177,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                $table.bootstrapTable('refresh');
                                layer.msg('保存成功', { icon: 1 });
                            }
                            else
                                layer.alert(result.Msg, { icon: 5 });
                        },
                        error: function (xhr, status, error, $form) {
                            layer.msg("保存失败~", { icon: 5 });
                        }
                    });
                }
            },
            cancel: function (li, o) {

            }
        });
    },
    initWageTempTable:function() {
        var options = {
            url: ctx + "/Finance/InitWageTempTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    userName: encodeURI($('#userName').val()),
                    rowid: $.getUrls("rowid")
                }
                return params;
            },
            columns: [
                {
                    radio:true
                }, {
                    field: '',//第一列序号
                    title: '序号',
                    align: 'center',
                    width: 50,
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: 'A0101',
                    title: '&nbsp;&nbsp;姓名&nbsp;&nbsp;',
                    width: 100,
                    align: 'center'
                }, {
                    field: 'E0386',
                    title: '&nbsp;&nbsp;&nbsp;工种&nbsp;&nbsp;&nbsp;',
                    align: 'center'
                }, {
                    field: 'WGJG0203',
                    title: '发放方式',
                    align: 'center'
                }, {
                    field: 'WGJG0204',
                    title: '工资',
                    align: 'center',
                    formatter: function (value, row, index) {
                        return "<a href=\"#\" name=\"WGJG0204_" + index + "\" class=\"editTable\"  tableType=\"editWage\" column=\"WGJG0204\" data-type=\"text\" data-pk=\"" + row.A0177 + "\" data-title=\"工资\">" + value + "</a>";
                    }
                }, {
                    field: 'WGJG0205',
                    title: '打卡天数',
                    align: 'center',
                    formatter: function (value, row, index) {
                        return "<a href=\"#\" name=\"WGJG0205_" + index + "\" class=\"editTable\"  tableType=\"editDay\" column=\"WGJG0205\" data-type=\"text\" data-pk=\"" + row.A0177 + "\" data-title=\"打卡天数\">" + value + "</a>";
                    }
                }, {
                    field: 'WGJG0206',
                    title: '实际工作天数',
                    align: 'center',
                    formatter: function (value, row, index) {
                        return "<a href=\"#\" name=\"WGJG0206_" + index + "\" class=\"editTable\"  tableType=\"editDay\" column=\"WGJG0206\" data-type=\"text\" data-pk=\"" + row.A0177 + "\" data-title=\"实际工作天数\">" + value + "</a>";
                    }
                }, {
                    field: 'WGJG0207',
                    title: '应发工资',
                    align: 'center',
                    formatter: function (value, row, index) {
                        return "<a href=\"#\" name=\"WGJG0207_" + index + "\" class=\"editTable\"  tableType=\"editWage\" column=\"WGJG0207\" data-type=\"text\" data-pk=\"" + row.A0177 + "\" data-title=\"应发工资\">" + value + "</a>";
                    }
                }, {
                    field: 'WGJG0208',
                    title: '实际发放',
                    align: 'center',
                    formatter: function (value, row, index) {
                        return "<a href=\"#\" name=\"WGJG0208_" + index + "\" class=\"editTable\" tableType=\"editWage\" column=\"WGJG0208\" data-type=\"text\" data-pk=\"" + row.A0177 + "\" data-title=\"实际发放\">" + value + "</a>";
                    }
                }, {
                    field: 'WGJG0209',
                    title: '备注',
                    align: 'center'
                }],
            onClickRow:function(row, $element) {
                curRow = row;
            },
            onLoadSuccess: function (aa, bb, cc) {
                $(".editTable").editable({
                    url: function (params) {
                        var sName = $(this).attr("column");
                        curRow[sName] = params.value;
                        $.ajax({
                            type: 'POST',
                            url: ctx + '/Finance/EditWageTempBySingle?columnName=' + sName,
                            data: curRow,
                            dataType: 'JSON',
                            success: function (result, textStatus, jqXHR) {
                                if (result.Statu === 0)
                                    layer.msg(result.Msg, { icon: 6 });
                                else 
                                    layer.msg(result.Msg, { icon: 5 });
                            },
                            error: function () { layer.msg("保存失败~", { icon: 5 }); }
                        });
                    },
                    type: 'text',
                    validate: function (value) { //字段验证
                        if (!$.trim(value)) 
                            return '不能为空';
                        var tableType = $(this).attr("tableType");
                        var result = null, meg = "";
                        if (tableType === "editWage") {
                            //工资
                            result = value.match(/^[0-9]*$/);
                            if (result === null) meg = "工资项目必须录入数字型！";
                        }
                        else if (tableType === "editDay") {
                            //天数
                            result = value.match(/^(0|[1-2]?[1-9]|[1-3][0]|[31])$/);
                            if (result === null) meg = "打卡天数项目必须录入小于31的整型数据！";
                        }
                        if (meg != "")
                            return meg;
                    }
                });
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    }
}