/*******************************************************
 *  权限管理 相关操作js
 * <p>Title: userManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2016年12月28日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table,addOrEditUrl,saveUrl;
var limitManager = {
    //初始化Table
    initTable: function () {
        var options= {
            url: ctx + "/SysLimit/GetLinitData",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams:function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    sm_code: $('#smCode').val(),//所属模块
                    perType:$('#perType').val(),//权限类别
                    per_name: encodeURI($('#perName').val())//权限名称
                }
                return params;
            },
            columns: [
                 {
                     radio: true
                 },{
	                field: '',//第一列序号
	                title: '序号',
	                align: 'center',
	                width:50,
	                formatter: function (value, row, index) {
	                    return index + 1;
	                }
	            },{
	                field: 'per_name',
	                title: '权限名',
	                align: 'center'
	            },{
	                field: 'per_type',
	                title: '权限类别',
	                align: 'center'
                }, {
                    field: 'per_code',
                    title: '权限代码',
                    align: 'center'
                }, {
                    field: 'creator_date',
                    title: '创建日期',
                    align: 'center',
                    formatter: function (value, row, index) {
                        if (value != null && value != "" && value != undefined)
                            return $.formatDate(new Date(parseInt(value.slice(6))));
                        else
                            return "";
                    }
                }],
            onClickRow: function(row, $element) {
                if (row.per_id != null && row.per_id > 0)
                    //启用
                    $('#btnLimit').attr("disabled", false);
                else
                    //禁用
                    $('#btnLimit').attr("disabled", true);
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //详细
        $('#limitDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#limitFormTable').resetHideValidForm();
            layer.open({
                title: ['权限详细信息', 'font-size:18px;'],
                type: 1,
                content: $('#limitForm'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['480px', '98%'],
                success: function (li, o) {
                    if (row != null) {
                        $('#limitFormTable')[0].reset();//重置表单
                        $('#limitFormTable').LoadForm(row[0]);//表单填充数据
                    }
                }
            });
        });
        //添加
        $('#limitAdd').click(function () {
            limitManager.editForm();
        });
        //编辑
        $('#limitEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            limitManager.editForm(row[0]);
        });
        //删除
        $('#limitDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中权限？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/SysLimit/DelLimitById/' + row[0].per_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu == 0) {
                            layer.msg("删除成功...");
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
        //权限设置
        $('#btnLimit').click(function() {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            if (!row[0].pc_openUrl) {
                layer.msg("当前权限无需配置！");
                return false;
            }
            var $title = row[0].per_name + "：权限设置";
            addOrEditUrl = ctx + row[0].pc_openUrl + row[0].per_id+"&sm_code="+row[0].sm_code;
            saveUrl = ctx + row[0].pc_saveUrl + row[0].per_id;
            //打开编辑
            var index= layer.open({
                id: 'ifreamSetMenuLimit',
                title: [$title, 'font-size:18px;'],
                type: 2,
                content: addOrEditUrl,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: [row[0].pc_width, row[0].pc_height],
                success: function (li, o) {
                    
                },
                yes: function (li, o) {
                    //所选菜单项
                    var menuData = document.getElementById("ifreamSetMenuLimit").firstChild.contentWindow.menuLimitManager.getMenuData();
                    if (menuData == null)
                        return false;
                    $.ajax({
                        type: 'post',
                        url: saveUrl,
                        data: { "menuData": menuData.split('|')[0], "reak": menuData.split('|')[1] },
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.Statu == 0) {
                                layer.msg(data.Msg, { icon: 1 });
                                layer.close(index);
                            }
                            if (data.Statu == 1) 
                                layer.msg(data.Msg, { icon: 5 });
                        }
                    });
                },
                cancel: function (li, o) {

                }
            });
        });
    },
    //初始化下拉
    initSelect: function () {
        $('#sm_code').initModuleSelectpicker(); 
        $('#smCode').initModuleSelectpicker();
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "编辑权限信息";
        if (row == null || row == "" || row == undefined) {
            $title = "添加权限信息";
            addOrEditUrl = ctx + "/SysLimit/AddLimit";//新增
        }
        else 
            addOrEditUrl = ctx + "/SysLimit/EditLimit?per_id=" + row.per_id;//编辑
        //清空表单
        $('#limitFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#limitForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['450px', '95%'],
            success: function (li, o) {
                if (row != null) {
                    $('#limitFormTable')[0].reset();//重置表单
                    $('#limitFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                //验证是否存在
                if ($('#per_code').val() == null)
                    return false;
                //登录失去焦点事件：新增，编辑时改变代码都需要验证
                if (row == null || row == "" || row == undefined
                    || row.per_code != $('#per_code').val()) {
                    $.ajax({
                        type: 'post',
                        url: ctx + '/SysLimit/CheckLimit',
                        data: { "per_code": encodeURI($('#per_code').val()) },
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.Statu == 0)
                                limitManager.submitForm(addOrEditUrl);
                            if (data.Statu == 1) {
                                $('#per_code').focus();
                                layer.msg(data.Msg, { icon: 5 });
                                return false;
                            }
                        }
                    });
                }else
                    limitManager.submitForm(addOrEditUrl);
            },
            cancel: function (li, o) {

            }
        });

    },
    submitForm: function (addOrEditUrl) {
        var sm_code = $('#sm_code').val();
        if (!sm_code) {
            layer.msg("请选择所属模块！", { icon: 5 });
            return false;
        }
        if ($('#limitFormTable').valid()) {
            //验证通过
            $('#limitFormTable').ajaxSubmit({
                url: addOrEditUrl,
                type: "post",
                dataType: "json",
                beforeSubmit: function (arr, $form, options) {
                    layer.msg("提交数据~", { icon: 1, time: 5000 });
                    return true;
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
    }
}
