/*******************************************************
 *  请假管理 相关操作js
 * <p>Title: userAskManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, selUnit=null, addOrEditUrl;
var userAskManager = {
    //初始化页面
    initPage: function() {
        parent.showLoadBoxs();
        userAskManager.initUnitTree();
        userAskManager.initDate();
        userAskManager.initTable();
        userAskManager.initEvent();
        userAskManager.initSelect();
        parent.delLoadBoxs();
    },
    //初始化单位树
    initUnitTree: function () {
        var $treeUnit = $('#tree').initB01TreeView({
            checkFirst: true,
            data: null,
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
    //初始化日期
    initDate: function () {
        //查询区域时间
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
        $('#dateStart').val(laydate.now().substring(0, 4) + '-01-01');
        $('#dateEnd').val(laydate.now().substring(0, 4) + '-12-31');
        //添加区域时间
        var dateStart = {
            elem: '#ask_startDate',
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
            elem: '#ask_endDate',
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
    },
    //初始化Table
    initTable:function() {
        var options = {
            url: ctx + "/MonthChart/InitAskTable",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    dateStart: $('#dateStart').val(),
                    dateEnd: $('#dateEnd').val(),
                    unitID: selUnit, //单位代码
                    userName: encodeURI($('#userName').val()) //姓名查询
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
                field: 'user_name',
                title: '请假人',
                align: 'center'
            }, {
                field: 'ask_type_text',
                title: '请假类别',
                align: 'center'
            }, {
                field: 'ask_startDate',
                title: '开始时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined) 
                        return value.replace(/\//g, "-");
                    else
                        return "-";
                }
            }, {
                field: 'ask_endDate',
                title: '结束时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined) 
                        return value.replace(/\//g, "-");
                    else
                        return "-";
                }
            }, {
                field: 'ask_day',
                title: '请假天数',
                align: 'center'
            }, {
                field: 'ask_operUser',
                title: '操作者',
                align: 'center'
            }, {
                field: 'ask_operDate',
                title: '操作时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined)
                        return value.replace(/\//g, "-");
                    else
                        return "-";
                }
            }, {
                field: 'ask_status_text',
                title: '状态',
                align: 'center'
            }]
        }
        $table = tableHelper.initTable("userAskTableToolbar", options);
    },
    //绑定事件
    initEvent: function () {
        //查询
        $('#btnSearch').click(function() {
            $table.bootstrapTable('refresh');
        });
        //添加
        $('#askAdd').click(function() {
            if (selUnit === null) {
                layer.msg("请选选择单位~", { icon: 5 });
                return false;
            }
            userAskManager.editAskForm(null);
        });
        //编辑
        $('#askEdit').click(function() {
            var row = $table.bootstrapTable('getSelections');
            if (row === null || row.length === 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            userAskManager.editAskForm(row[0]);
        });
        //删除
        $('#askDel').click(function() {
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
                    url: ctx + '/MonthChart/DeleteAsk/' + row[0].ask_id,
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
    },
    //初始化下拉框
    initSelect: function () {
        $('#ask_type').initSelectpicker("QJTYPE");
        $('#ask_status').initSelectpicker("QJSTATUS");
    },
    //添加、编辑
    editAskForm: function (row) {
        var $title = "添加请假条";
        addOrEditUrl = ctx + "/MonthChart/AddAsk"; //新增
        if (row != null && row != "" && row != undefined) {
            $title = "编辑请假条";
            addOrEditUrl = ctx + "/MonthChart/EditAsk?ask_id=" + row.ask_id;//编辑   
        }            
        //清空表单
        $('#askFormTable').resetHideValidForm();
        //添加单位数据
        $('#ask_unit').val(selUnit);
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            content: $('#ask_form'),
            type:1,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area:['620px','98%'],
            success: function (li, o) {
                if (row != null) {
                    $('#askFormTable')[0].reset();//重置表单
                    $('#askFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#askFormTable').valid()) {
                    //验证通过
                    $('#askFormTable').ajaxSubmit({
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