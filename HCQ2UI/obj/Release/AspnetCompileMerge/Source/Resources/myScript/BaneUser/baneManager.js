/*******************************************************
 *  戒毒人员 相关操作js
 * <p>Title: baneManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//戒毒人员管理
var unitzTree, selNode, $table, addOrEditUrl, orgId = null, isParent = false, folder_path,issetOut=false;
var addIndex;
var $userIdentCode = "hiden_identify";
var baneManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        baneManager.initData();
        baneManager.bindZtree();
        baneManager.initTable();
        baneManager.bindEvent();
        baneManager.initSelect();
        parent.delLoadBoxs();
    },
    //初始化页面参数
    initData:function(){
        if (isBaneShow) 
            $userIdentCode = "user_identify";
    },
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
            url: ctx + "/BaneUser/GetBaneUserData",
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
                    orgId: orgId,//选中单位(组织机构)
                    folder_path: folder_path,
                    isParent:isParent?1:0,
                    baneName: encodeURI($('#baneName').val()),//人员姓名
                    baneType: encodeURI($('#baneType').val()),//戒毒类别
                    baneEnd: encodeURI($('#baneEnd').val())//结束原因
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
	        }, {
	            field: 'user_type',
	            title: '人员类别',
	            align: 'center'
	        }, {
	            field: 'user_phone',
	            title: '联系电话',
	            align: 'center'
	        }, {
	            field: 'start_date',
	            title: '报到时间',
	            align: 'center'
	        }, {
	            field: 'end_date',
	            title: '结束时间',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {

            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        $('#baneEnd').initSelectpicker("EndReason",null,true);//结束原因
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //添加
        $('#baneAdd').click(function () {
            if (isParent) {
                layer.msg("添加用户需要选中最底层结构！");
                return false;
            }
            baneManager.editForm();
        });
        //编辑
        $('#baneEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row!=null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            baneManager.editForm(row[0]);
        });
        //删除
        $('#baneDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择用户？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6,time:6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneUser/DelBaneUserById',
                    data: { user_identify: row[0].user_identify },
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
            }, function () {});
        });
        //导出
        $('#baneExportExcel').click(function () {
            jsToExcel('exampleTableToolbar','', '禁毒人员记录.xls');
        });
    },
    //编辑、添加
    editForm: function (row,isDetail) {
        var $url = ctx + "/BaneUser/AddBaneUser?orgId=" + orgId + "&user_identify=";
        if (row)
            $url += row.user_identify;//新增
        addIndex = window.parent.main_openWindowByLink({
            title:'人员基本信息',
            url: $url,
            btn: '',
            height:'98%',
            width:'90%',
            ismax: false,
            cancel: function (index, layero) {
                $table.bootstrapTable('refresh');
            }
        });
    },
    //关闭新增窗口
    closeAddForm: function () {
        if (addIndex)
            layer.close(addIndex);
    }
}


