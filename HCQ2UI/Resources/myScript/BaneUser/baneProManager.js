/*******************************************************
 *  定期检测 相关操作js
 * <p>Title: baneProManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var unitzTree, selNode, $table, addOrEditUrl, orgId = null, isParent = false, folder_path,issetOut=false;
var addIndex, queryType;
var $userIdentCode = "hiden_identify";
var queryDay,baneTask;
var baneProManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        baneProManager.initData();
        baneProManager.bindZtree();
        baneProManager.initTable();
        baneProManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化参数
    initData: function () {
        //页面查询类别
        queryType = $.getUrls("queryType");
        queryDay = $.getUrls("queryDay");
        baneTask = $.getUrls("baneTask");
        if (isBaneShow) 
            $userIdentCode = "user_identify";
        var setVal = function (key,val) {
            var Inter = setInterval(function () {
                    $('#' + key).selectpicker('val', val);
                window.clearInterval(Inter);
            }, 500);
        }
        if (queryDay || queryDay === "0")
            setVal('banedays', queryDay);
        if(baneTask && baneTask==="all")
            setVal('baneTask', '0');
    },
    //初始化社区
    bindZtree: function () {
        var setting = {
            view: {
                showIcon: true,
                selectedMulti: false,
                fontCss: getFontCss
            },
            edit: {
                enable: true,
                showRemoveBtn: false,
                showRenameBtn: false
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: ctx + '/SysOrg/GetOrgTreeDataByRelation',
                autoParam: ["id"]
            },
            data: {
                key: { title: "name", name: "name" },
                simpleData: { enable: true, idKey: "id", pIdKey: "pId", rootPId: 0 }
            },
            check: {
                enable: false,
                autoCheckTrigger: true,
                chkStyle: "checkbox",
                chkboxType: { "Y": "p", "N": "ps" }
            },
            callback: {
                onAsyncError: function () {
                    layer.msg("初始化单位结构树失败~", { icon: 5 });
                },
                onAsyncSuccess: function (event, treeId, treeNode, msg) {
                    //树加载完成后执行
                    var obj = unitzTree.getNodes()[0];
                    if (obj) {
                        issetOut = true;
                        unitzTree.selectNode(obj);
                        unitzTree.setting.callback.onClick(null, unitzTree.setting.treeId, obj);//模拟点击事件 
                        if (obj.children && obj.children.length > 0)
                            unitzTree.expandNode(obj);
                    }
                },
                onClick: function (event, treeId, treeNode) {
                    $('#unitTree a').css("color", "#333");
                    $('#' + treeNode.tId + '_a').css("color", "#ffffff");
                    isParent = treeNode.isParent;
                    orgId = treeNode.id;
                    selNode = treeNode;//选中节点
                    folder_path = treeNode.folder_path;
                    if (issetOut)
                    { setTimeout(function () { $table.bootstrapTable('refresh'); issetOut = false; }, 1000); }
                    else
                        $table.bootstrapTable('refresh');
                }
            }
        };
        unitzTree = $("#unitTree").initzTreeView(setting, 'ushow');
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/BaneProUser/GetBaneUserData",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            //showExport: true,//显示导出
            exportTypes: ['excel'],
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    baneTask:$('#baneTask').val(),
                    orgId: orgId,//选中单位(组织机构)
                    folder_path: folder_path,
                    isParent: isParent ? 1 : 0,
                    banedays: $("#banedays").val(),
                    queryType: queryType,//页面查询类别 all：总人数，should：本月应检人员，finish：本月已检人员，dated：过检人员
                    baneName: encodeURI($('#baneName').val()),//人员姓名
                    baneType: encodeURI($('#baneType').val())//戒毒类别
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
                field: 'ur_id',//第一列序号
                title: '操作',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    if (row.approve_status===0)
                        return "<button onclick=baneProManager.editBaneProDataById('" + value + "','" + row.user_name + "','" + row[$userIdentCode] + "') class='btn btn-info btn-xs' type='button'><i class='fa fa-paste'></i>办 理</button>";
                    return "";
                }
            },
	        {
	            field: 'user_name',
	            title: '用户名',
	            align: 'center'
	        },
	        {
	            field: 'user_sex',
	            title: '性别',
	            align: 'center'
	        },
	        {
	            field: 'user_age',
	            title: '年龄',
	            align: 'center'
	        }, {
	            field: $userIdentCode,
	            title: '身份证',
	            align: 'center'
	        },{
	            field: 'user_type',
	            title: '人员类别',
	            align: 'center'
	        }, {
	            field: 'this_date',
	            title: '本次定期检测时间',
	            align: 'center'
	        }, {
	            field: 'next_date',
	            title: '下次定期检测时间',
	            align: 'center',
	            formatter: function (value, row, index) {
	                if (!value)
	                    return "";
	                var sdate = $.strToDate(value), now = new Date();//下次尿检日期日期格式
	                var days = now.getTime() - sdate.getTime();
	                var day = parseInt(days / (1000 * 60 * 60 * 24));
	                if (day > 0)
	                return "<font color='red' title='已过检测日期'>" + value + "</font>";//大于0表示已过尿检日期
	                else if (day > -7)
	                    return "<font color='blue' title='还有" + day + "天应该检测'>" + value + "</font>";//大于-7表示一周内应该尿检
	                return value;//未到尿检时间
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
        //查看记录
        $('#baneConduct').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row!=null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            baneProManager.recordForm(row[0].user_identify, row[0][$userIdentCode]);
        });
        //删除
        $('#baneDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中记录？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneProUser/DelBaneProUserById/'+row[0].ur_id,
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
        //导出
        $('#baneExportExcel').click(function () {
            jsToExcel('exampleTableToolbar', '', '监管禁毒人员记录.xls');
        });
    },
    //办理尿检登记
    editBaneProDataById:function(ur_id,user_name,user_identify){
        //先获取待编辑数据
        $.ajax({
            type: 'post',
            url: ctx + '/BaneProUser/GetProEditDataById/' + ur_id,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0){
                    if(data.Url==="0")
                        data.Data.user_identify = user_identify;
                    baneProManager.openBanePro(data.Data, user_name);
                }   
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    },
    openBanePro: function (row,user_name) {
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        layer.open({
            title: ['现场检测报告书', 'font-size:18px;'],
            type: 1,
            content: $('#orgform'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['650px', '90%'],
            success: function (li, o) {
                if (row) {
                    //现场检测地点
                    if (!row.ur_site)
                        row.ur_site = "社工站";
                    if (row.ur_should_date)
                        row.ur_should_date = $.formatDate(new Date(parseInt(row.ur_should_date.slice(6))));
                    if (row.ur_input_date)
                        row.ur_input_date = $.formatDate(new Date(parseInt(row.ur_input_date.slice(6))));
                    if (row.ur_reality_date)
                        row.ur_reality_date = $.formatDataLong(new Date(parseInt(row.ur_reality_date.slice(6))));
                    $('#orgFormTable')[0].reset();//重置表单
                    $('#orgFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#orgFormTable').valid()) {
                    //验证通过
                    $('#orgFormTable').ajaxSubmit({
                        url: ctx + '/BaneProUser/EditUrinalysisRecord',
                        type: "post",
                        data: { approveStatus:1},
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
                                layer.msg('保存成功', { icon: 1 });
                                $table.bootstrapTable('refresh');
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
    //查看历史记录
    recordForm: function (user_identify, hidden_identify) {
        layer.open({
            title: ['定期检测记录一栏', 'font-size:18px;'],
            type: 2,
            content: ctx + '/BaneUser/UrinalysisList?user_identify=' + user_identify + '&type=record&hidden_identify=' + hidden_identify,
            scroll: true,//是否显示滚动条、默认不显示
            btn: '',
            area: ['1000px', '95%'],
            cancel: function (li, o) {

            }
        });
    }
}


