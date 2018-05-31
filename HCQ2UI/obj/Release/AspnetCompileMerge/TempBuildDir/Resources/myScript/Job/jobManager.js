/*******************************************************
 *  就业模块 相关操作js
 * <p>Title: jobManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年8月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//发布用工需求
var $table, addOrEditUrl, selUnit = null;
var jobManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        jobManager.initUnitData();
        jobManager.initTable();
        jobManager.bindEvent();
        jobManager.initSelect();
        parent.delLoadBoxs();
    },
    //获取单位数据
    initUnitData: function () {
        $.ajax({
            url: ctx + '/Job/GetUnitData',
            type: "post",
            async: false,
            dataType: 'json',
            data:{"use_status":1},
            success: function (mess) {
                if (mess.Statu === 0) {
                    jobManager.initUnitTree(mess.Data);
                } else
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
    //初始化单位树
    initUnitTree: function (result) {
        var $treeUnit = $('#tree').initB01TreeView({
            checkFirst: true,
            data: result,
            onNodeSelected: function (e, o) {
                if (!o.com_id)
                    selUnit = 0;
                else
                    selUnit = o.com_id;
                $table.bootstrapTable('refresh');
            }
        });
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/Job/InitJobTable",
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
                    com_id:selUnit,
                    jobName: encodeURI($('#jobName').val()),//岗位
                    jobStartMoney: $('#jobStartMoney').val(),//起薪
                    jobEndMoney: $('#jobEndMoney').val()//截止
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
                field: 'work_type_value',
                title: '岗位',
                align: 'center'
            },
	        {
	            field: 'trade_type',
	            title: '行业',
	            align: 'center'
	        },
	        {
	            field: 'work_city',
	            title: '工种城市',
	            align: 'center'
	        },
            {
                field: 'use_wage_start',
                title: '薪资',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value > 0 && row.use_wage_end === 0)
                        return value + "以上";
                    if (value===0 && row.use_wage_end === 0)
                        return "面议";
                    if (row.use_wage_end > 0)
                        return value + "-" + row.use_wage_end;
                    return "";
                }
            },
	        {
	            field: 'use_sex',
	            title: '性别',
	            align: 'center'
	        }, {
	            field: 'use_age',
	            title: '年龄',
	            align: 'center'
	        }, {
	            field: 'use_edu',
	            title: '学历',
	            align: 'center'
	        }, {
	            field: 'ageLimit',
	            title: '工作年限',
	            align: 'center'
	        }, {
	            field: 'use_major',
	            title: '专业要求',
	            align: 'center'
	        }, {
	            field: 'work_start',
	            title: '就职日期',
	            align: 'center'
	        }, {
	            field: 'issue_start',
	            title: '发布日期',
	            align: 'center'
	        }, {
	            field: 'use_status',
	            title: '状态',
	            align: 'center',
	            formatter: function (value, row, index) {
	                switch (value) {
	                    case 1: return "待发布"; break;
	                    case 2: return "已发布"; break;
	                    case 3: return "已结束"; break;
	                    case 4: return "已作废"; break;
	                }
	                return "-";
	            }
	        }],
            onClickRow: function (row, $element) {
                
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        $('#work_type').initSelectpicker("JobPost");
        $('#use_edu').initSelectpicker("JobEdu");
        $('#use_ageLimit').initSelectpicker("WorkExperience");
        //工作城市
        $('#work_city').initSelectpicker("AB", "GetWorkCityDictionary");
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //详细
        $('#jobDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#orgFormTable').resetHideValidForm();
            layer.open({
                title: ['招聘详细信息', 'font-size:18px;'],
                type: 1,
                content: $('#org_form'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['620px', '98%'],
                success: function (li, o) {
                    if (row != null) {
                        $('#orgFormTable')[0].reset();//重置表单
                        $('#orgFormTable').LoadForm(row[0]);//表单填充数据
                    }
                }
            });
        });
        //添加
        $('#jobAdd').click(function () {
            if (!selUnit || selUnit === 0) {
                layer.msg("请先选择招聘单位，再添加招聘信息", { icon: 5 });
                return false;
            }
            jobManager.editForm();
        });
        //编辑
        $('#jobEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            jobManager.editForm(row[0]);
        });
        //删除
        $('#jobDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中招聘信息吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Job/delJobData/' + row[0].use_id,
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
        //图片上传
        $('#header_img').on('change', function (e) {
            //文件全名
            var name = e.currentTarget.files[0].name;
            if (!name)
                return false;
            var type = name.substring(name.lastIndexOf(".") + 1).toLowerCase();
            if (type != "png" && type != "jpg" && type != "jpeg") {
                $('#header_img').val('');
                layer.msg("请上传规格为：100*100，格式为： png/jpg/jpeg的图片！", { icon: 5 });
                return false;
            }
        });
    },
    //编辑、添加
    editForm: function (row) {
        var title = "添加招聘信息";
        if (row == null || row == "" || row == undefined)
            addOrEditUrl = ctx + "/Job/AddJobData?com_id=" + selUnit;//新增
        else {
            title = "编辑招聘信息";
            addOrEditUrl = ctx + "/Job/EditJobData?use_id=" + row.use_id;//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [title, 'font-size:18px;'],
            type: 1,
            content: $('#org_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['620px', '98%'],
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

//查看用工需求
var lookJobManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        lookJobManager.initUnitData();
        lookJobManager.initTable();
        lookJobManager.bindEvent();
        lookJobManager.initSelect();
        parent.delLoadBoxs();
    },
    //获取单位数据
    initUnitData: function () {
        $.ajax({
            url: ctx + '/Job/GetUnitData',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0) {
                    lookJobManager.initUnitTree(mess.Data);
                } else
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
    //初始化单位树
    initUnitTree: function (result) {
        var $treeUnit = $('#tree').initB01TreeView({
            checkFirst: true,
            data: result,
            onNodeSelected: function (e, o) {
                if (!o.com_id)
                    selUnit = 0;
                else
                    selUnit = o.com_id;
                $table.bootstrapTable('refresh');
            }
        });
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/Job/InitIssueTable",
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
                    com_id: selUnit,
                    use_status: $('#use_status').val(),//状态
                    jobName: encodeURI($('#jobName').val()),//岗位
                    jobStartMoney: $('#jobStartMoney').val(),//起薪
                    jobEndMoney: $('#jobEndMoney').val()//截止
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
                field: 'work_type_value',
                title: '岗位',
                align: 'center'
            },
	        {
	            field: 'trade_type',
	            title: '行业',
	            align: 'center'
	        },
	        {
	            field: 'work_city',
	            title: '工种城市',
	            align: 'center'
	        },
            {
                field: 'use_wage_start',
                title: '薪资',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value > 0 && row.use_wage_end === 0)
                        return value + "以上";
                    if (value === 0 && row.use_wage_end === 0)
                        return "面议";
                    if (row.use_wage_end > 0)
                        return value + "-" + row.use_wage_end;
                    return "";
                }
            },
	        {
	            field: 'use_sex',
	            title: '性别',
	            align: 'center'
	        }, {
	            field: 'use_age',
	            title: '年龄',
	            align: 'center'
	        }, {
	            field: 'use_edu',
	            title: '学历',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if(value && value==="0")
	                    return "不限";
	                return value;
	            }
	        }, {
	            field: 'ageLimit',
	            title: '工作年限',
	            align: 'center'
	        }, {
	            field: 'use_major',
	            title: '专业要求',
	            align: 'center'
	        }, {
	            field: 'work_start',
	            title: '就职日期',
	            align: 'center'
	        }, {
	            field: 'issue_start',
	            title: '发布日期',
	            align: 'center'
	        }, {
	            field: 'subLen',
	            title: '简历数',
	            align: 'center',
	            formatter: function (value, row, index) {
                    if(value)
                        return "<a href='#' onclick=\"lookJobManager.openIssueList('" + row.work_type_value + "'," + row.use_id + ")\">" + value + "</a>";
                    return "-";
	            }
	        }],
            onClickRow: function (row, $element) {

            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        $('#work_type').initSelectpicker("JobPost");
        $('#use_edu').initSelectpicker("JobEdu");
        $('#use_ageLimit').initSelectpicker("WorkExperience");
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //详细
        $('#jobDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#orgFormTable').resetHideValidForm();
            layer.open({
                title: ['招聘详细信息', 'font-size:18px;'],
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
        //编辑
        $('#jobEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            jobManager.editForm(row[0]);
        });
        //撤销发布
        $('#jobDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要撤销当前选中招聘信息吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/Job/RepealJob/' + row[0].use_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("撤销成功...", { icon: 6 });
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
    },
    //打开简历详细
    openIssueList: function (wageName,use_id) {
        if (!use_id)
            return false;
        var index = layer.open({
            title: [wageName, 'font-size:18px;'],
            type: 2,
            content: ctx + "/Job/IssueList?use_id=" + use_id,
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['900px', '95%'],
            success: function (li, o) {
            },
            yes: function (li, o) {
                var job_status = $('#job_status').val();//状态
                $.ajax({
                    type: 'post',
                    url: ctx + '/SysUser/SaveUserGroupData/' + row[0].user_id,
                    data: { "userData": userData },
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        if (data.Statu === 0) {
                            layer.msg(data.Msg, { icon: 1 });
                            layer.close(index);
                        }
                        if (data.Statu === 1)
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            },
            cancel: function (li, o) {

            }
        });
    }
}

//简历管理
var use_id = 0;
var issueManager = {
    //初始化页面
    initPage: function () {
        use_id = $.getUrls("use_id");
        parent.showLoadBoxs();
        issueManager.initTable();
        issueManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/Job/InitResumeTable/" + use_id,
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
                    A0101: encodeURI($('#A0101').val()),//姓名
                    C0104: $('#C0104').val(),//电话
                    A0410: encodeURI($('#A0410').val())//专业
                }
                return params;
            },
            columns: [
            {
                checkbox:true
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
                field: 'A0101',
                title: '投递人',
                align: 'center'
            },
	        {
	            field: 'C0104',
	            title: '电话',
	            align: 'center'
	        },
	        {
	            field: 'A0114',
	            title: '籍贯',
	            align: 'center'
	        },
            {
                field: 'A0111',
                title: '出生日期',
                align: 'center'
            },
	        {
	            field: 'A0107',
	            title: '性别',
	            align: 'center'
	        }, {
	            field: 'A0108',
	            title: '学历',
	            align: 'center'
	        }, {
	            field: 'A0410',
	            title: '专业',
	            align: 'center'
	        }, {
	            field: 'send_date',
	            title: '申请日期',
	            align: 'center'
	        }, {
	            field: 'job_status',
	            title: '状态',
	            align: 'center',
	            formatter: function (value, row, index) {
	                switch (value) {
	                    case "01": return "等待邀请"; break;
	                    case "02": return "邀请面试"; break;
	                    case "03": return "邀请工作"; break;
	                    default: return "放弃"; break;
	                }
	            }
	        }],
            onClickRow: function (row, $element) {

            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //邀请操作
        $('#btnOperator').click(function () {
            //判断是否选中简历
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            var person = "";
            for (var i = 0; i < row.length; i++) 
                person += row[i].job_id + ",";
            layer.open({
                title: ['编辑状态', 'font-size:18px;'],
                type: 1,
                content: $('#org_form'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['480px', '90%'],
                yes: function (li, o) {
                    if ($('#orgFormTable').valid()) {
                        //验证通过
                        $('#orgFormTable').ajaxSubmit({
                            url: ctx + '/Job/ModifyIssue',
                            type: "post",
                            dataType: "json",
                            data: { "job_id": person },
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
        });
    }
}
