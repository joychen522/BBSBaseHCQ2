/*******************************************************
 *  添加戒毒人员 相关操作js
 * <p>Title: addBaneManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var addOrEditUrl,//添加、编辑保存地址
    $table,//table对象
    user_identify;//戒毒人员身份证
//违法犯罪记录--档案袋
var criminalMannager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        criminalMannager.initData();
        criminalMannager.bindEvent();
        criminalMannager.initTable();
        parent.delLoadBoxs();
    },
    //初始化参数
    initData: function () {
        user_identify = $.getUrls("user_identify");
        if (!user_identify)
            layer.alert("身份参数异常！");
        $('#user_identify').val(user_identify);
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/BaneUser/GetCriminalBaneData",
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
                    user_identify: user_identify//身份证
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
                field: 'user_identify',
                title: '身份证',
                align: 'center'
            },
	        {
	            field: 'start_drug_date',
	            title: '开始吸毒日期',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if (value)
	                    return $.formatDate(new Date(parseInt(value.slice(6))));
	                else
	                    return "";
	            }
	        },
	        {
	            field: 'drug_year',
	            title: '吸毒史(年)',
	            align: 'center'
	        },
	        {
	            field: 'force_time',
	            title: '强制戒毒次数',
	            align: 'center'
	        }, {
	            field: 'force_insulate',
	            title: '强制隔离次数',
	            align: 'center'
	        }, {
	            field: 'other_record',
	            title: '其他违法犯罪记录',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //添加
        $('#criminalAdd').on('click', function () {
            criminalMannager.editForm();
        });
        //编辑
        $('#criminalEdit').on('click', function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            criminalMannager.editForm(row[0]);
        });
        //删除
        $('#criminalDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中记录行吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneUser/DelCriminalBaneById/' + row[0].cr_id,
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
        var $title = "新增";
        addOrEditUrl = ctx + "/BaneUser/AddCriminalRecord";//新增
        if (row) {
            $title = "编辑";
            addOrEditUrl = ctx + "/BaneUser/EditCriminalRecord";//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#orgform'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['550px', '460px'],
            success: function (li, o) {
                if (row != null) {
                    if (row.start_drug_date)
                        row.start_drug_date = $.formatDate(new Date(parseInt(row.start_drug_date.slice(6))));
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

//家庭成员&社会关系--档案袋
var fr_type = 0;
var familyMannager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        familyMannager.initData();
        familyMannager.initSelect();
        familyMannager.bindEvent();
        familyMannager.initTable();
        parent.delLoadBoxs();
    },
    //初始化参数
    initData: function () {
        user_identify = $.getUrls("user_identify");
        fr_type = $.getUrls("fr_type");
        if (!user_identify)
            layer.alert("身份参数异常！");
        $('#user_identify').val(user_identify);
        $('#fr_type').val(fr_type);
    },
    //初始化下拉
    initSelect:function(){
        $('#fr_edu').initSelectpicker("UserEdu", null, true);
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/BaneUser/GetFamilyBaneData",
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
                    user_identify: user_identify,//身份证
                    fr_type: fr_type //类别
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
                field: 'user_identify',
                title: '身份证',
                align: 'center'
            },
            {
                field: 'fr_name',
                title: '姓名',
                align: 'center'
            },
            {
                field: 'fr_sex',
                title: '性别',
                align: 'center'
            },
	        {
	            field: 'fr_birth',
	            title: '出生日期',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if (value)
	                    return $.formatDate(new Date(parseInt(value.slice(6))));
	                else
	                    return "";
	            }
	        },
	        {
	            field: 'fr_edu',
	            title: '学历',
	            align: 'center'
	        },
	        {
	            field: 'fr_family_url',
	            title: '家庭住址',
	            align: 'center'
	        }, {
	            field: 'fr_job',
	            title: '职业',
	            align: 'center'
	        }, {
	            field: 'fr_unit',
	            title: '工作单位',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //添加
        $('#familyAdd').on('click', function () {
            familyMannager.editForm();
        });
        //编辑
        $('#familyEdit').on('click', function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            familyMannager.editForm(row[0]);
        });
        //删除
        $('#familyDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中记录行吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneUser/DelFamilyBaneById/' + row[0].fr_id,
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
        var $title = "新增";
        addOrEditUrl = ctx + "/BaneUser/AddFamilyRecord";//新增
        if (row) {
            $title = "编辑";
            addOrEditUrl = ctx + "/BaneUser/EditFamilyRecord";//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#orgform'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['650px', '540px'],
            success: function (li, o) {
                if (row != null) {
                    if (row.fr_birth)
                        row.fr_birth = $.formatDate(new Date(parseInt(row.fr_birth.slice(6))));
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


//定期检测记录--档案袋
var hidden_identify;
var urinalysisMannager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        urinalysisMannager.initData();
        urinalysisMannager.bindEvent();
        urinalysisMannager.initTable();
        parent.delLoadBoxs();
    },
    //初始化参数
    initData: function () {
        user_identify = $.getUrls("user_identify");
        hidden_identify = $.getUrls("hidden_identify");
        if (!user_identify)
            layer.alert("身份参数异常！");
        $('#user_identify').val(user_identify);
        //判断是否需要显示操作按钮 
        var type = $.getUrls("type");
        if (type && type === "record")
            $('#exampleToolbar').css("display", "none");
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/BaneUser/GetUrinalysisBaneData",
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
                    user_identify: user_identify//身份证
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
                field: 'user_identify',
                title: '身份证',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return hidden_identify;
                    else
                        return "";
                }
            },
	        {
	            field: 'ur_should_date',
	            title: '本次应到定期检测时间',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if (value)
	                    return $.formatDate(new Date(parseInt(value.slice(6))));
	                else
	                    return "";
	            }
	        },
            {
                field: 'ur_reality_date',
                title: '实际定期检测时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return $.formatDataLong(new Date(parseInt(value.slice(6))));
                    else
                        return "";
                }
            },
	        {
	            field: 'ur_manager',
	            title: '检测人',
	            align: 'center'
	        },
	        {
	            field: 'ur_result',
	            title: '定期检测结果',
	            align: 'center'
	        }, {
	            field: 'ur_attach',//ur_attach,ur_file_name
	            title: '附件',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if (value)
	                    return "<a href=\"" + value.toString().replace("~", window.location.origin + $.ctx()) + "\" class=\"btn btn-primary btn-circle\" title=\"" + row.ur_file_name + "\"><i class=\"fa fa-download\"></i></a>";
	                else
	                    return "";
	            }
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //添加
        $('#urinalysisAdd').on('click', function () {
            urinalysisMannager.editForm();
        });
        //编辑
        $('#urinalysisEdit').on('click', function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            urinalysisMannager.editForm(row[0]);
        });
        //删除
        $('#urinalysisDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中记录行吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneUser/DelUrinalysisBaneById/' + row[0].ur_id,
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
        var $title = "新增";
        addOrEditUrl = ctx + "/BaneUser/AddUrinalysisRecord";//新增
        if (row) {
            $title = "编辑";
            addOrEditUrl = ctx + "/BaneUser/EditUrinalysisRecord";//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#orgform'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['610px', '540px'],
            success: function (li, o) {
                var reg = /^\d{4}\-\d{2}-\d{2}$/;
                if (row != null) {
                    if (row.ur_should_date && !row.ur_should_date.match(reg))
                        row.ur_should_date = $.formatDate(new Date(parseInt(row.ur_should_date.slice(6))));
                    if (row.ur_reality_date && !row.ur_reality_date.match(reg))
                        row.ur_reality_date = $.formatDataLong(new Date(parseInt(row.ur_reality_date.slice(6))));
                    if (row.ur_input_date && !row.ur_input_date.match(reg))
                        row.ur_input_date = $.formatDate(new Date(parseInt(row.ur_input_date.slice(6))));
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