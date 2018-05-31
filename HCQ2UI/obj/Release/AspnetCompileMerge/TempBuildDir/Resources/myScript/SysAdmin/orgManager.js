/*******************************************************
 *  权限管理 相关操作js
 * <p>Title: userManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2016年12月28日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var unitzTree, selNode, $table,demo2,isParent;
var orgManager={
    //1.初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        orgManager.bindZtree();
        orgManager.bindEvent();
        orgManager.initTable();
        parent.delLoadBoxs();
    },
    //2.1采用Ztree初始化文档树
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
                //drag: {
                //    isCopy: false,
                //    isMove: true
                //}
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: ctx + '/SysOrg/GetOrgTreeData',
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
                        unitzTree.selectNode(obj);
                        unitzTree.setting.callback.onClick(null, unitzTree.setting.treeId, obj);//模拟点击事件 
                        if (obj.children && obj.children.length > 0)
                            unitzTree.expandNode(obj);
                    }
                },
                onClick: function (event, treeId, treeNode) {
                    $('#unitTree a').css("color", "#333");
                    $('#' + treeNode.tId + '_a').css("color", "#ffffff");
                    selNode = treeNode;//选中节点
                    isParent = treeNode.isParent;
                    if ($table && treeNode.id > 0) {
                        $('#btnSetUser').attr("disabled", false);
                        $table.bootstrapTable('refresh');//刷新Table
                    } else
                        $('#btnSetUser').attr("disabled", true);
                },
                //拖拽结束回调
                onDrop: function (event, treeId, treeNodes, targetNode, moveType, isCopy) {
                    //treeId：目标节点 targetNode 所在 zTree 的 treeId，便于用户操控
                    //treeNodes：被拖拽的节点 JSON 数据集合
                    //targetNode：成为 treeNodes 拖拽结束的目标节点 JSON 数据对象
                    //moveType：指定移动到目标节点的相对位置
                    //isCopy：拖拽是否为复制
                    //拖拽后 后台保存
                    //$.ajax({
                    //    url: ctx + '/SysOrg/SetFolderPath',
                    //    type: "post",
                    //    async: false,
                    //    data: { folder_id: treeNodes[0].id, folder_pid: targetNode.id },
                    //    dataType: 'json',
                    //    success: function (mess) {
                    //        if(mess.Statu===0)
                    //            layer.msg("移动成功！", {icon:6});
                    //        else
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
                }
            }
        };
        unitzTree = $("#unitTree").initzTreeView(setting, 'ushow');
    },
    //2.2绑定事件
    bindEvent: function () {
        //添加根节点
        $('#orgAddPeer').click(function () {
            orgManager.addEditNode({ isRoot: true, own: true });
        });
        //添加子级
        $('#orgAddChild').click(function () {
            if (!selNode) {
                layer.msg("请选择指定节点添加！");
                return false;
            }
            orgManager.addEditNode({ isRoot: false, own: false });
        });
        //编辑
        $('#orgEdit').click(function () {
            var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
            if (!treeNode) {
                layer.msg("请选中需要编辑的节点！");
                return false;
            }
            orgManager.addEditNode(treeNode);
        });
        //删除
        $('#orgDel').click(function () {
            if (isParent) {
                layer.msg("当前节点下还有子节点，请先删除子节点！");
                return false;
            }
            orgManager.editRemove();
        });
        //查询
        $('#btnSearch').click(function () {
            var zTree = docManager.getSelNode();
            if (!zTree) {
                layer.msg("温馨提示：查询前请先选中目录节点！", { icon: 1 });
                return null;
            }
            $table.bootstrapTable('refresh');
        });
        //分配用户
        $('#btnSetUser').click(function () {
            var node = orgManager.getSelNode();
            if (!node) {
                layer.msg("请选择需要配置的组织机构！");
                return false;
            }
            $.ajax({
                url: ctx + '/SysOrg/GetOrgDataByPerson/'+node.id,
                type: "post",
                async: false,
                dataType: 'json',
                success: function (mess) {
                    if (mess.Statu === 0) {
                        var obj = mess.Data;
                        var waitObj = (obj) ? obj.waitObj : null;//待分配
                        var fineObj = (obj) ? obj.fineObj : null;//已分配
                        $('.demo1').empty();//清空
                        $(waitObj).each(function () {
                            $("<option value='" + this['value'] + "' class='listBoxoption'>" + this['text'] + "</option>").appendTo($('.demo1'));
                        });
                        $(fineObj).each(function () {
                            $("<option value='" + this['value'] + "' selected='selected' class='listBoxoption'>" + this['text'] + "</option>").appendTo($('.demo1'));
                        });
                        orgManager.initListBox(node.id);
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
        });
        //移除
        //$('#btnDelUser').click(function () {
        //    var row = $table.bootstrapTable('getSelections');
        //    if (row != null && row.length == 0) {
        //        layer.msg("未选中记录行~", { icon: 5 });
        //        return false;
        //    }
        //    var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
		//	nodes = zTree.getSelectedNodes(),
		//	treeNode = nodes[0];
        //    if (!treeNode) {
        //        layer.msg("请选中需节点！");
        //        return false;
        //    }
        //    layer.confirm('您确定要从当前组织机构中移除当前用户吗？', {
        //        btn: ['确定', '取消'] //按钮
        //    }, function () {
        //        var index = layer.msg("移除中...", { icon: 6, time: 6000 });
        //        $.ajax({
        //            type: 'post',
        //            url: ctx + '/SysOrg/RemoveUser/',
        //            dataType: 'json',
        //            async: false,
        //            data: { "folder_id": treeNode.id,"user_id": row[0].user_id},
        //            success: function (data) {
        //                layer.close(index);
        //                if (data.Statu === 0) {
        //                    layer.msg("删除成功...");
        //                    $table.bootstrapTable('refresh');
        //                }
        //                else
        //                    layer.msg(data.Msg, { icon: 5 });
        //            }
        //        });
        //    }, function () { });
        //});
    },
    //2.3.添加，编辑
    addEditNode: function (e) {
        var title = "添加节点";
        if (!e.hasOwnProperty("isRoot"))
            title = "编辑节点";
        //打开编辑
        layer.open({
            title: [title, 'font-size:18px;'],
            type: 1,
            content: $('#EditNode'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '200px'],
            success: function (li, o) {
                if (e != null)
                    $('#addNode').val(e.name);
            },
            yes: function (li, o) {
                var name = $('#addNode').val();
                if (!name || name.length === 0) {
                    layer.msg("温馨提示：节点名称不允许为空！");
                    return false;
                }
                if (!e.hasOwnProperty("isRoot"))
                    orgManager.editNode(e, name);
                else
                    orgManager.addNode(e, name);
            },
            cancel: function (li, o) {

            }
        });
    },
    //2.3.1添加节点：同级节点/子节点
    addNode: function (e, nodeName) {
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),//当前选择节点
            isParent = e.isParent,
			treeNode = nodes[0];
        var dataJson = {
            "pId": (e.isRoot) ? 0 : treeNode.id,
            "name": nodeName
        };
        //后台先添加
        $.ajax({
            url: ctx + '/SysOrg/AddNode',
            type: "post",
            async: false,
            dataType: 'json',
            data: dataJson,
            success: function (result) {
                parent.delLoadBoxs();
                if (result.Statu === 0) {
                    if (e.isRoot)
                        //添加根节点
                        zTree.addNodes(null, { id: result.Data, pId: 0, isParent: false, name: nodeName });
                    else
                        zTree.addNodes(treeNode, { id: result.Data, pId: treeNode.id, isParent: false, name: nodeName });
                    selNode = treeNode;
                    layer.closeAll();
                }
                layer.msg(result.Msg);
            },
            error: function () {
                parent.delLoadBoxs();
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //2.3.2编辑节点
    editNode: function (treeNode, nodeName) {
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData");
        //后台编辑
        $.ajax({
            url: ctx + '/SysOrg/EditNode',
            type: "post",
            async: false,
            dataType: 'json',
            data: { "id": treeNode.id, "name": nodeName },
            success: function (result) {
                if (result.Statu === 0) {
                    treeNode.name = nodeName;
                    zTree.updateNode(treeNode);
                    layer.closeAll();
                }
                layer.msg(result.Msg);
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //2.3.3删除节点
    editRemove: function (e) {
        layer.msg("提交数据~", { icon: 1, time: 5000 });
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
        if (!treeNode) {
            layer.msg("请选中需要删除的节点！");
            return false;
        }
        layer.confirm('您确定要删除当前选中节点吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            //后台删除
            $.ajax({
                url: ctx + '/SysOrg/DeleteNode',
                type: "post",
                async: false,
                dataType: 'json',
                data: { "id": treeNode.id },
                success: function (result) {
                    if (result.Statu === 0) {
                        zTree.removeNode(treeNode);
                        selNode = null;
                    }
                    layer.msg(result.Msg);
                },
                error: function () {
                    layer.msg('数据异常~', { icon: 5 });
                }
            });
        }, function () { });
    },
    //2.4获取被选中节点
    getSelNode: function () {
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
           nodes = zTree.getSelectedNodes(),
           treeNode = nodes[0];
        return treeNode;
    },
    //2.5初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysOrg/InitTable",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                var node = orgManager.getSelNode();
                if (!node) return null;
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    folder_id: node.id,//节点ID
                    keyword: $('#keyword').val()//文档名
                }
                return params;
            },
            columns: [
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
               field: 'id',
               title: '状态',
               align: 'center',
               formatter: function (value, row, index) {
                   if (value != null && value > 0)
                       return "<font color='red'>禁用</font>";
                   return "<font color='green'>正常</font>";
               }
           },
           {
               field: 'user_name',
               title: '用户名',
               align: 'center'
           },
           {
               field: 'login_name',
               title: '登录名',
               align: 'center'
           },
           {
               field: 'user_sex',
               title: '性别',
               align: 'center'
           }, {
               field: 'user_qq',
               title: 'QQ',
               align: 'center'
           }, {
               field: 'user_phone',
               title: '电话号码',
               align: 'center'
           }, {
               field: 'user_email',
               title: 'Email',
               align: 'center'
           }, {
               field: 'user_birth',
               title: '出生日期',
               align: 'center',
               formatter: function (value, row, index) {
                   if (value != null && value != "")
                       return value.replace(/\//g, '-');
                   return "-";
               }
           }],
            onClickRow: function (row, $element) {
                var node = orgManager.getSelNode();
                if (row) {
                    //启用
                    $('#btnDelUser').attr("disabled", false);
                }
                else {
                    //禁用
                    $('#btnDelUser').attr("disabled", true);
                }
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //2.6初始化ListBox
    initListBox: function (folder_id) {
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
        var createIndex = layer.open({
            title: [ '组织机构设置：支持多个关键字同时检索用：| 分割', 'font-size:18px;'],
            type: 1,
            content: $('#createDataBySel'),
            scroll: true, //是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['680px', '450px'],
            success: function (li, o) {
            },
            yes: function (li, o) {
                //保存数据
                var selData = $('[name="duallistbox_demo1"]').val();
                if (selData) {
                    $.ajax({
                        url: ctx + '/SysOrg/SaveOrgData',
                        type: "post",
                        async: false,
                        data: { "personData": selData.join(','), "folder_id": folder_id },
                        dataType: 'json',
                        success: function (mess) {
                            if (mess.Statu === 0)
                                layer.msg(mess.Msg, { icon: 6 });
                            else
                                layer.msg(mess.Msg, { icon: 5 });
                            layer.close(createIndex);
                            $table.bootstrapTable('refresh');
                        },
                        error: function () {
                            layer.msg('数据异常~', { icon: 5 });
                        }
                    });
                }
            },
            cancel: function (li, o) {

            }
        });
    }
}