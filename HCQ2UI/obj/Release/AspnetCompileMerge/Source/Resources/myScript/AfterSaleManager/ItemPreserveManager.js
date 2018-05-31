/*******************************************************
 * 项目台账 相关操作js
 * <p>Title: ItemPreserveManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年10月16日 下午5:41:57
 * @version 1.0
 * *****************************************************/ 
//项目台账管理
var $table, addOrEditUrl, selUnit = null,
    unitName, unitCode;//项目名称，项目代码
var ItemPreserveManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        ItemPreserveManager.initUnitData();
        ItemPreserveManager.initTable();
        ItemPreserveManager.bindEvent();
        ItemPreserveManager.initSelect();
        parent.delLoadBoxs();
    },
    //获取单位数据
    initUnitData: function () {
        Lutai.Load("seSheng", "seShi", "searchArea", "testTree", 1, function (event, treeId, treeNode) {
            if (!treeNode || treeNode.type != 2) {
                selUnit = treeNode.area_code; unitName = ""; unitCode = "";
            } else {
                selUnit = treeNode.area_code;//区域代码
                unitName = treeNode.text;
                unitCode = treeNode.unit_id;//项目ID
            }
            $('#area_code').val(selUnit);
            //设置项目值
            $('#unit_name').val(unitName);
            $('#unit_code').val(unitCode);
            $table.bootstrapTable('refresh');
        });
        //$.ajax({
        //    url: ctx + '/ItemPreserve/InitAreaTreeData',
        //    type: "post",
        //    async: false,
        //    dataType: 'json',
        //    success: function (mess) {
        //        if (mess.Statu === 0) {
        //            ItemPreserveManager.initUnitTree(mess.Data);
        //        } else
        //            layer.open({
        //                shade: false,
        //                title: false,
        //                content: mess.Msg,
        //                btn: ''
        //            });
        //    },
        //    error: function () {
        //        layer.msg('数据异常~', { icon: 5 });
        //    }
        //});
    },
    //初始化单位树
    initUnitTree: function (result) {
        //if (!result) {
        //    result = {}; result.data = "";
        //}
        //var $treeUnit = $('#tree').initB01TreeView({
        //    checkFirst: true,
        //    data: result,
        //    onNodeSelected: function (e, o) {
        //        if (o.unitID === null || o.unitID === "null"){
        //            selUnit = ""; unitName = ""; unitCode = "";
        //        }
        //        else {
        //            selUnit = "110101";//o.unitID;
        //            unitName = "经典天成三期";
        //            unitCode = "011";
        //        }
        //        $('#area_code').val(selUnit);
        //        //设置项目值
        //        $('#unit_name').val(unitName);
        //        $('#unit_code').val(unitCode);
        //        $table.bootstrapTable('refresh');
        //    }
        //});
        //初始化时自动选择系统用户
        //var temp = $treeUnit.treeview('getEnabled')[0];
        //if (temp != null) {
        //    $treeUnit.treeview('selectNode', [temp.nodeId, { silent: true }]);
        //    selUnit = temp.unitID;
        //}
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/ItemPreserve/InitTable",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    area_code: (selUnit === null) ? "" : selUnit,//区域代码
                    unit_code: unitCode,//所选项目代码
                    status: encodeURI($('#status').val())//项目状态
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
	        {
	            field: '',//第一列序号
	            title: '序号',
	            align: 'center',
	            width: 50,
	            formatter: function (value, row, index) {
	                return index + 1;
	            }
	        },
            {
                field: 'unit_name',
                title: '项目名称',
                align: 'center'
            },
            {
                field: 'ip_status_text',
                title: '项目状态',
                align: 'center'
            },
            {
                field: 'tail_after',
                title: '跟踪过',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return value + "天";
                    return "";
                }
            },
            {
                field: 'pact_money',
                title: '合同金额',
                align: 'center'
            },
            {
                field: 'pay_money',
                title: '缴纳金额',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value > 0)
                        return "<a onclick=\"ItemPreserveManager.openPayMoney(" + row.ip_id + ");\">" + value + "</a>";
                    return "";
                }
            },
	        {
	            field: 'pay_date',
	            title: '缴费到期',
	            align: 'center'
	        },
	        {
	            field: 'pay_cash_money',
	            title: '应缴押金',
	            align: 'center'
	        },
	        {
	            field: 'pra_cash_money',
	            title: '实缴押金',
	            align: 'center'
	        }, {
	            field: 'duty_person',
	            title: '负责人',
	            align: 'center'
	        }, {
	            field: 'touch_phone',
	            title: '联系电话',
	            align: 'center'
	        }, {
	            field: 'bag_file',
	            title: '合同',
	            align: 'center',
	            formatter: function (value, row, index) {
	                return "<a onclick=\"ItemPreserveManager.openUpFile(" + row.ip_id + ",'" + row.unit_name + "');\">附件</a>";
	            }
	        }, {
	            field: 'pact_start',
	            title: '合同开始',
	            align: 'center'
	        }, {
	            field: 'record_name',
	            title: '记录人',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //打开编辑 缴纳金额
    openPayMoney:function(id){
        layer.open({
            title: ['缴纳金额', 'font-size:18px;'],
            type: 2,
            content: ctx + '/ItemPreserve/CashList?ip_id=' + id,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定'],
            area: ['800px', '98%'],
            yes: function (li, o) {
                layer.closeAll();
                cashMoneyManager.countCashMoney(id);
                $table.bootstrapTable('refresh');
            },
            cancel: function (li, o) {
                $table.bootstrapTable('refresh');
            }
        });
    },
    //打开 附件管理
    openUpFile:function(id,unit_name){
        layer.open({
            title: ['合同附件', 'font-size:18px;'],
            type: 2,
            content: ctx + '/ItemPreserve/PactFlieList?ip_id=' + id + '&unit_name=' + encodeURI(unit_name),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定'],
            area: ['800px', '98%'],
            yes: function (li, o) {
                layer.closeAll();
                $table.bootstrapTable('refresh');
            },
            cancel: function (li, o) {
                $table.bootstrapTable('refresh');
            }
        });
    },
    //初始化下拉
    initSelect: function () {
        $('#status').initSelectpicker("ItemStatus");
        $('#ip_status').initSelectpicker("ItemStatus");
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //详细
        $('#itemDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#orgFormTable').resetHideValidForm();
            layer.open({
                title: ['用户详细信息', 'font-size:18px;'],
                type: 1,
                content: $('#org_form'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['480px', '98%'],
                success: function (li, o) {
                    if (row != null) {
                        $('#orgFormTable')[0].reset();//重置表单
                        $('#orgFormTable').LoadForm(row[0]);//表单填充数据
                    }
                }
            });
        });
        //添加
        $('#itemAdd').click(function () {
            var area_code = $('#area_code').val(),
                unit_name = $('#unit_name').val(),
                unit_code = $('#unit_code').val();
            if (!area_code || !unit_name || !unit_code) {
                layer.msg("温馨提示：请先正确选择区域下的项目！");
                return false;
            }
            ItemPreserveManager.editForm();
        });
        //编辑
        $('#itemEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            ItemPreserveManager.editForm(row[0]);
        });
        //删除
        $('#itemDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中项目记录吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/ItemPreserve/DelItemByID/' + row[0].ip_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功...");
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加项目台账";
        if (row == null || row == "" || row == undefined)
            addOrEditUrl = ctx + "/ItemPreserve/AddItem";//新增
        else {
            $title = "编辑项目台账信息";
            addOrEditUrl = ctx + "/ItemPreserve/EditItem?ip_id=" + row.ip_id;//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#org_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    $('#orgFormTable')[0].reset();//重置表单
                    $('#orgFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#orgFormTable').valid()) {
                    //验证通过
                    $('#orgFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
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

//缴纳金额管理
var ip_id,$cashTable;
var cashMoneyManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        ip_id = $.getUrls("ip_id");
        cashMoneyManager.initTable();
        cashMoneyManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/ItemPreserve/InitCashTable",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    ip_id: ip_id//项目状态
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
	        {
	            field: '',//第一列序号
	            title: '序号',
	            align: 'center',
	            width: 50,
	            formatter: function (value, row, index) {
	                return index + 1;
	            }
	        },
            {
                field: 'cash_start_date',
                title: '开始时间',
                align: 'center'
            },
            {
                field: 'cash_end_date',
                title: '结束时间',
                align: 'center'
            },
            {
                field: 'cash_money',
                title: '缴纳金额',
                align: 'center'
            }],
            onClickRow: function (row, $element) {
            }
        }
        $cashTable = tableHelper.initTable("cashTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //添加
        $('#cashAdd').click(function () {
            if (!ip_id) {
                layer.msg("温馨提示：项目台账主键为空！");
                return false;
            }
            $('#ip_id').val(ip_id);
            cashMoneyManager.editForm();
            //重新统计缴纳金额
            cashMoneyManager.countCashMoney(ip_id);
        });
        //编辑
        $('#cashEdit').click(function () {
            var row = $cashTable.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            cashMoneyManager.editForm(row[0]);
            //重新统计缴纳金额
            cashMoneyManager.countCashMoney(ip_id);
        });
        //删除
        $('#cashDel').click(function () {
            var row = $cashTable.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中项目记录吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/ItemPreserve/DelItemCashByID/' + row[0].cd_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功...");
                            $cashTable.bootstrapTable('refresh');
                            //重新统计缴纳金额
                            cashMoneyManager.countCashMoney(ip_id);
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
    },
    //编辑、添加
    editForm: function (row) {
        if (row == null || row == "" || row == undefined)
            addOrEditUrl = ctx + "/ItemPreserve/AddCashItem";//新增
        else
            addOrEditUrl = ctx + "/ItemPreserve/EditCashItem?cd_id=" + row.cd_id;//编辑
        //清空表单
        $('#cashFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑/添加信息', 'font-size:18px;'],
            type: 1,
            content: $('#cashForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['460px', '300px'],
            success: function (li, o) {
                if (row != null) {
                    $('#cashFormTable')[0].reset();//重置表单
                    $('#cashFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#cashFormTable').valid()) {
                    //验证通过
                    $('#cashFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
                                $cashTable.bootstrapTable('refresh');
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
    //重新统计缴纳金额
    countCashMoney: function (id) {
        if (!id) {
            layer.msg("温馨提示：需要重新汇总统计的数据不存在！");
            return false;
        }
        $.ajax({
            type: 'post',
            url: ctx + '/ItemPreserve/CalculateCashMoney/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                layer.close(index);
                if (data.Statu === 0)
                    $table.bootstrapTable('refresh');
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    }
}

//台账附件管理
var $pactTable, indexForm, unit_name;
var pactFlieManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        ip_id = $.getUrls("ip_id");
        unit_name = decodeURI($.getUrls("unit_name"));
        pactFlieManager.initTable();
        pactFlieManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/ItemPreserve/InitPactTable",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    ip_id: ip_id//项目状态
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
	        {
	            field: '',//第一列序号
	            title: '序号',
	            align: 'center',
	            width: 50,
	            formatter: function (value, row, index) {
	                return index + 1;
	            }
	        },
            {
                field: 'pd_file',
                title: '合同附件',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return "<a href=\"" + value.toString().replace("~", window.location.origin + $.ctx()) + "\" class=\"btn btn-primary btn-circle\" title=\"下载\"><i class=\"fa fa-download\"></i></a>";
                    return "";
                }
            },
            {
                field: 'pd_name',
                title: '文件名称',
                align: 'center'
            },
            {
                field: 'pd_date',
                title: '文件上传时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return $.formatDate(new Date(parseInt(value.slice(6))));
                    else
                        return "";
                }
            }],
            onClickRow: function (row, $element) {
            }
        }
        $pactTable = tableHelper.initTable("pactTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //添加
        $('#pactAdd').click(function () {
            if (!ip_id) {
                layer.msg("温馨提示：项目台账主键为空！");
                return false;
            }
            indexForm = layer.open({
                id: 'ifreamUpLoadData',
                title: [unit_name + '：上传文档', 'font-size:18px;'],
                type: 2,
                content: ctx + '/ItemPreserve/PactUpLoadFile?ip_id=' + ip_id,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['600px', '95%'],
                yes: function (li, o) {
                    var menuData = document.getElementById("ifreamUpLoadData").firstChild.contentWindow.upLoadDoc();
                    if (menuData == null)
                        return false;
                },
                cancel: function (li, o) {

                }
            });
        });
        //删除
        $('#pactDel').click(function () {
            var row = $pactTable.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中项目记录吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/ItemPreserve/DelItemPactByID/' + row[0].pd_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功...");
                            $pactTable.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
    },
    //添加
    editForm: function (row) {
        addOrEditUrl = ctx + "/ItemPreserve/AddCashItem";//新增
        //清空表单
        $('#cashFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑信息', 'font-size:18px;'],
            type: 1,
            content: $('#cashForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['460px', '300px'],
            success: function (li, o) {
                if (row != null) {
                    $('#cashFormTable')[0].reset();//重置表单
                    $('#cashFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#cashFormTable').valid()) {
                    //验证通过
                    $('#cashFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
                                $cashTable.bootstrapTable('refresh');
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
    //3.0关闭文档上传窗口
    closeUpLoadForm: function (data) {
        if (indexForm && data && data.Statu === 0) {
            layer.close(indexForm);
            $pactTable.bootstrapTable('refresh');
        }
    }
}

