/*******************************************************
 *  征信（个人征信、企业征信） 相关操作js
 * <p>Title: userAskManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年3月2日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl;
var companyManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        companyManager.initTable();
        companyManager.initEvent();
        companyManager.initSelect();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable:function() {
        var options = {
            url: ctx + "/Company/InitOwnListTable",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    userName: encodeURI($('#userName').val()) //姓名查询
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
                field: 'WGJG_GRZX01',
                title: '编号',
                align: 'center'
            }, {
                field: 'WGJG_GRZX02',
                title: '姓名',
                align: 'center'
            }, {
                field: 'WGJG_GRZX04',
                title: '籍贯',
                align: 'center'
            }, {
                field: 'WGJG_GRZX03',
                title: '身份证',
                align: 'center'
            },{
                field: 'WGJG_GRZX06',
                title: '征信状态',
                align: 'center'
            }, {
                field: 'WGJG_GRZX05',
                title: '联系电话',
                align: 'center'
            }, {
                field: 'WGJG_GRZX07',
                title: '状态变更原因',
                align: 'center'
            }, {
                field: 'WGJG_GRZX10',
                title: '更新人',
                align: 'center'
            }, {
                field: 'WGJG_GRZX11',
                title: '更新时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined)
                        return $.formatDate(new Date(parseInt(value.slice(6))));
                    else
                        return "";
                }
            }]
        }
        $table = tableHelper.initTable("ownPanyTableToolbar", options);
    },
    //绑定事件
    initEvent: function () {
        //添加
        $('#ownAdd').click(function() {
            companyManager.editCompanyForm(null);
        });
        //编辑
        $('#ownEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row === null || row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            companyManager.editCompanyForm(row[0]);
        });
        //删除
        $('#ownDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row === null || row.length === 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前记录？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Company/DeleteOwnPany?rowID=' + row[0].RowID,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功~");
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
    },
    //初始化下拉
    initSelect:function() {
        $('#WGJG_GRZX04').initSelectpicker("MZ");
        $('#WGJG_GRZX06').initSelectpicker("ZXZT");
    },
    //添加、编辑
    editCompanyForm: function (row) {
        var $title = "添加个人征信";
        addOrEditUrl = ctx + "/Company/AddOwnPany"; //新增
        if (row != null && row != "" && row != undefined) {
            $title = "编辑个人征信";
            addOrEditUrl = ctx + "/Company/EditOwnPany?rowID=" + row.RowID;//编辑   
        }
        //清空表单
        $('#ownFormTable').resetHideValidForm();
        $('#WGJG_GRZX02').removeAttr("disabled");
        $('#WGJG_GRZX03').removeAttr("disabled");
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            content: $('#own_form'),
            type: 1,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['620px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    $('#ownFormTable')[0].reset();//重置表单
                    $('#ownFormTable').LoadForm(row,"text");//表单填充数据
                    $('#WGJG_GRZX02').attr("disabled", true);
                    $('#WGJG_GRZX03').attr("disabled", true);
                }
            },
            yes: function (li, o) {
                if ($('#ownFormTable').valid()) {
                    //验证通过
                    $('#ownFormTable').ajaxSubmit({
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
            cancel: function (li, o) {

            }
        });
    }
}

/**企业征信管理**/
var selUnit="";
var enterPriseManager= {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        //enterPriseManager.initUnitTree();
        enterPriseManager.initTable();
        enterPriseManager.initEvent();
        parent.delLoadBoxs();
    },
    initUnitTree:function(){
        $.ajax({
            type: "post",
            dataType: "text",
            async: false,
            url: ctx + "/CompProInfo/GetComTree",
            success: function (data) {
                if(data)
                    enterPriseManager.bindTree(eval("(" + data + ")"));
            }
        });
    },
    //初始化树
    bindTree: function (result) {
        var $treeUnit = $('#tree').initB01TreeView({
            checkFirst: true,
            data: result,
            onNodeSelected: function (e, o) {
                if (o.com_id === null || o.com_id === "null")
                    selUnit = "";
                else
                    selUnit = o.com_id;
                $table.bootstrapTable('refresh');
            }
        });
        //初始化时自动选择系统用户
        var temp = $treeUnit.treeview('getEnabled')[0];
        if (temp != null) {
            $treeUnit.treeview('selectNode', [temp.nodeId, { silent: true }]);
            selUnit = temp.com_id;
        }
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/CompProInfo/GetComInfosSoure",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    JianDieUnitID: selUnit,
                    txtSearchName: $('#unitName').val() //名称查询
                }
                return params;
            },
            columns: [
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1;
                }
            }, {
                field: "dwmc",
                title: "单位名称"
            }, {
                field: "tyshxydm",
                title: "统一社会信用代码",
                align: "center"
            }, {
                field: "Fddbrxm",
                title: "法定代表人",
                align: "center"
            }, {
                field: "solve_type",
                title: "处理状态",
                align: "center",
                formatter: function (value, row, index) {
                    if (row.WGJG_ZX06) {
                        if (value === 1)
                            return "已处理";
                        return "未处理";
                    }
                    return "-";
                }
            }, {
                field: "WGJG_ZX06",
                title: "信用状态",
                align: "center"
            }, {
                field: '',
                title: '明细',
                align: 'center',
                formatter: function (value, row, index) {
                    return "<button class=\"btn btn-primary \" type=\"button\" onclick=\"enterPriseManager.openDetailList(" + row.com_id + ",'"+row.dwmc+"')\"><span class=\"bold\">明细</span></button>";
                }
            }]
        }
        $table = tableHelper.initTable("enterPanyTableToolbar", options);
    },
    //打开企业详细一栏
    openDetailList: function (id,UnitName) {
        window.parent.main_openWindowByLink({
            url: ctx + '/Company/EnterDetailList?ent_id=' + id,
            height: "95%",
            width: "1100px",
            btn:'',
            title: UnitName + "：征信明细"
        });
    },
    initEvent:function() {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
    }
}

/******企业征信详细一栏******/
var ent_id, $entTable, tips;
var enterDeatilManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        enterDeatilManager.initSelect();
        enterDeatilManager.initTable();
        enterDeatilManager.initEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        ent_id = $.getUrls("ent_id");
        var options = {
            url: ctx + "/Company/InitEnterDeatilData/" + ent_id,
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1
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
                field: 'WGJG_ZX02',
                title: '企业名称',
                align: 'center'
            }, {
                field: 'ed_title',
                title: '标题',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value && value.toString().length > 20)
                        return "<label id=\"ed_title_" + index + "\" onmousemove=\"enterDeatilManager.showTip(this)\" onmouseout=\"enterDeatilManager.clsoeTip(this)\" dataTitle=\"" + value + "\"  class=\"tipClass\">" + value.toString().substring(0, 20) + "...</label>";
                    return value;
                }
            }, {
                field: 'ent_text',
                title: '征信状态',
                align: 'center'
            }, {
                field: 'solve_type',
                title: '处理状态',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value && value === 1)
                        return "已处理";
                    return "未处理";
                }
            }, {
                field: 'ed_note',
                title: '处理情况',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value && value.toString().length > 20)
                        return "<label id=\"ed_note_" + index + "\" onmousemove=\"enterDeatilManager.showTip(this)\" onmouseout=\"enterDeatilManager.clsoeTip(this)\" dataTitle=\"" + value + "\"  class=\"tipClass\">" + value.toString().substring(0, 20) + "...</label>";
                    return value;
                }
            }, {
                field: 'ed_reason',
                title: '失信原因',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value && value.toString().length > 20)
                        return "<label id=\"ed_reason_" + index + "\" onmousemove=\"enterDeatilManager.showTip(this)\" onmouseout=\"enterDeatilManager.clsoeTip(this)\" dataTitle=\"" + value + "\"  class=\"tipClass\">" + value.toString().substring(0, 20) + "...</label>";
                    return value;
                }
            }, {
                field: 'ed_time',
                title: '失信时间',
                align: 'center'
            }, {
                field: 'ed_create',
                title: '记录人',
                align: 'center'
            }, {
                field: 'is_success',
                title: '是否影响企业征信',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value.toString() === "0")
                        return "影响";
                    return "不影响";
                }
            }, {
                field: 'ed_time',
                title: '记录时间',
                align: 'center'
            }]
        }
        $entTable = tableHelper.initTable("enterDetailTableToolbar", options);
    },
    //显示title
    showTip: function (cb) {
        if (!tips) {
            tips = layer.tips($(cb).attr("dataTitle"), '#' + cb.id, {
                tips: [1, '#3595CC'],
                time: 90000,
                area: '500px',
                end: function () {
                    tips = null;
                },
                cancel: function (index, layero) {
                    
                }
            });
        }
    },
    //隐藏title
    clsoeTip: function (cb) {
        if (tips)
            layer.close(tips);
    },
    //初始化下拉
    initSelect:function(){
        $('#ent_type').initSelectpicker("ZXZT");
    },
    //绑定事件
    initEvent: function () {
        //添加
        $('#entAdd').click(function () {
            enterDeatilManager.addOrEditForm(null);
        });
        //编辑
        $('#entEdit').click(function () {
            var row = $entTable.bootstrapTable('getSelections');
            if (row === null || row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            enterDeatilManager.addOrEditForm(row[0]);
        });
        //删除
        $('#entDel').click(function () {
            var row = $entTable.bootstrapTable('getSelections');
            if (row === null || row.length === 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前记录？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Company/DelEnterDetail/' + row[0].ed_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功~");
                            $entTable.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
        //查询
        $('#btnSearch').click(function () {
            $entTable.bootstrapTable('refresh');
        });
        //绑定选择案件类型
        $('#case_type').change(function () {
            var str = $(this).val();//0：欠薪，1：其他
            var pay_money = $('#pay_money'), pay_person = $('#pay_person');
            if (str === "1") {
                pay_money.val("0");
                pay_person.val("0");
            } 
        });
    },
    addOrEditForm: function (row) {
        var $title = "添加征信";
        addOrEditUrl = ctx + "/Company/AddEnterDetail"; //新增
        $('#ent_id').val(ent_id);
        if (row != null && row != "" && row != undefined) {
            $title = "编辑征信";
            addOrEditUrl = ctx + "/Company/EditEnterDetail?ed_id=" + row.ed_id;//编辑   
        }
        //清空表单
        $('#enterFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            content: $('#enter_form'),
            type: 1,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['620px', '95%'],
            success: function (li, o) {
                if (row != null) {
                    $('#enterFormTable')[0].reset();//重置表单
                    $('#enterFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#enterFormTable').valid()) {
                    var pay_person = $('#pay_person'), pay_money = $('#pay_money');
                    if (!pay_person.val())
                        pay_person.val("0");
                    if (!pay_money.val())
                        pay_money.val("0");
                    //验证通过
                    $('#enterFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                $entTable.bootstrapTable('refresh');
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
    }
}