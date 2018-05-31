/*******************************************************
 *  文档管理 相关操作js
 * <p>Title: docManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年5月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, unitzTree,//树控件
    selFolder_id = "", $addOrEditUrl,
    newCount = 1, selNode = null,//选中节点
    upfileData, indexForm;//上传文档数据
var demo2 = null, pageType = "", doc_type = "";//页面参数类别默认为:收文管理
var userID, isDocSys;
var docManager = {
    //1.初始化页面
    initPage: function (userID,isSys) {
        parent.showLoadBoxs();
        userID = userID;
        isDocSys = isSys;
        docManager.initPageParam();
        docManager.initTable();
        docManager.bindZtree();
        docManager.bindEvent();
        parent.delLoadBoxs();
    },
    //1.1初始化页面参数
    initPageParam:function(){
        pageType = $.getUrls("pageType");
        doc_type = $.getUrls("doc_type");
        if (!pageType)
            pageType = "";
    },
    //2.2采用Ztree初始化文档树
    bindZtree: function () {
        var setting = {
            view: {
                showIcon: true,
                selectedMulti: false,
                fontCss: getFontCss
            },
            edit:{
                enable: true,
                showRemoveBtn: false,
                showRenameBtn: false
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: ctx + "/DocManager/GetDocTreeData?doc_type=" + doc_type + "&pageType=" + pageType,
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
                        if (obj.children && obj.children.length>0)
                            unitzTree.expandNode(obj);
                    }
                },
                onClick: function (event, treeId, treeNode) {
                    $('#unitTree a').css("color", "#333");
                    $('#' + treeNode.tId + '_a').css("color", "#ffffff");
                    selNode = treeNode;//选中节点
                    if ($table && treeNode.id > 0)
                        $table.bootstrapTable('refresh');//刷新Table
                }
            }
        };
        unitzTree = $("#unitTree").initzTreeView(setting, 'ushow');
    },
    //2.2.3验证编辑按钮是否可以用
    checkBtn:function(){
        var node = docManager.getSelNode();
        var row = $table.bootstrapTable('getSelections');
        if (!row || row.length === 0)
            return false;
        //0：我的文档，1：我的分享，2：收到的分享，3：公有文档，4：回收站，5：等待审批
        if(node.doc_type === "0")
            return true;
        else
            return false;
    },
    //2.3初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/DocManager/InitTable",
            cutHeight: 5,
            showRefresh: true,
            showToggle: true,
            showColumns: true,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                var node = docManager.getSelNode();
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    folder_id: (node)?node.id:0,//节点ID
                    doc_type: (node) ? node.doc_type : "",//节点类型
                    file_status: ($('#FileType'))?$('#FileType').val():'',
                    keyword: $('#keyword').val()//文档名
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
            {
                field: '',
                title: '操作',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    switch (doc_type) {
                        case "0": {
                            //我的文档 允许设置分享
                            return "<button class=\"btn btn-danger btn-circle btn-outline\" title=\"分享\" onclick=\"docManager.shareDocEvent(" + row.file_id + ")\" type=\"button\"><i class=\"fa fa-heart\"></i></button>";
                        } break;
                        case "4": {
                            //从下架中恢复
                            return "<button class=\"btn btn-danger btn-circle btn-outline\" title=\"恢复\" onclick=\"docManager.recoverDocEvent(" + row.file_id + ")\" type=\"button\"><i class=\"fa fa-mail-reply\"></i></button>";
                        } break;
                        default: return index + 1; break;
                    }
                }
            },
            //{
            //    field: 'url',
            //    title: '附件',
            //    align: 'center',
            //    formatter: function (value, row, index) {
            //        if (value)
            //            return "<a href=\"" + value.toString().replace("~", window.location.origin + $.ctx()) + "\" class=\"btn btn-primary btn-circle\" title=\"下载\"><i class=\"fa fa-download\"></i></a>";
            //        return "";
            //    }
            //},
	        {
	            field: 'file_name',
	            title: '资源名称',
	            align: 'center',
                width:300,
	            formatter: function (value, row, index) {
	                if (value && value.toString().length > 20)
	                    back = "<span title='" + value + "'>" + value.toString().substring(0, 20) + "...</span>";
                    else
	                    back = value;
	                return back;
	            }
	        },
            {
                field: 'doc_type',
                title: '类型',
                align: 'center'
            }, {
                field: 'file_size',
                title: '大小',
                align: 'center'
            }, {
                field: 'file_money',
                title: '定价',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return value + " 元";
                    return 0;
                }
            }, {
                field: 'down_number',
                title: '下载次数',
                align: 'center'
            }, {
                field: 'file_classify',
                title: '分类',
                align: 'center'
            }, {
	            field: 'create_name',
	            title: '作者',
	            align: 'center'
	        }, {
	            field: 'create_time',
	            title: '发布时间',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //2.3.1回收站恢复
    recoverDocEvent: function (file_id) {
        layer.confirm("您确定要恢复当前选中文档至VR资源吗？", {
            btn: ['确定', '取消'] //按钮
        }, function () {
            var index = layer.msg("文档恢复中请稍候...", { icon: 6, time: 6000 });
            $.ajax({
                type: 'post',
                url: ctx + '/DocManager/RecoverNodeById/' + file_id,
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
        }, function () { });  
    },
    //2.4 分享文档事件
    shareDocEvent: function (file_id) {
        var index = layer.confirm('请选择您所需要分享的类别~', {
            btn: ['分享给用户', '分享给角色'] //按钮
        }, function () {
            //用户
            docManager.docSetObj(file_id, "/DocManager/GetShareDataByPerson", "/DocManager/SaveDataByPerson", index);
        }, function () {
            //角色
            docManager.docSetObj(file_id, "/DocManager/GetShareDataByRole", "/DocManager/SaveDataByRole", index);
        });
    },
    //2.5 分享文档设置
    docSetObj: function (file_id,$url, $saveurl, index) {
        $.ajax({
            url: ctx + $url,
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0) {
                    var obj = mess.Data;
                    $('.demo1').empty();//清空
                    $(obj).each(function () {
                        $("<option value='" + this['value'] + "' class='listBoxoption'>" + this['text'] + "</option>").appendTo($('.demo1'));
                    });
                    docManager.initListBox(file_id, $saveurl, index);
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
    //2.6 初始化ListBox
    initListBox: function (file_id,$saveUrl, index) {
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
        var UnitName = "文档分享设置"; //decodeURI($.getUrls("UnitName"));
        var createIndex = layer.open({
            title: [UnitName + '：支持多个关键字同时检索用：| 分割', 'font-size:18px;'],
            type: 1,
            content: $('#createDataBySel'),
            scroll: true, //是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['680px', '450px'],
            success: function (li, o) {
                if (index)
                    layer.close(index);
            },
            yes: function (li, o) {
                //保存数据
                var selData = $('[name="duallistbox_demo1"]').val();
                if (selData) {
                    $.ajax({
                        url: ctx + $saveUrl,
                        type: "post",
                        async: false,
                        data: { "personData": selData.join(','), "file_id": file_id },
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
    },
    //3.0.1关闭文档上传窗口
    closeUpLoadForm: function (data) {
        if (indexForm && data && data.Statu === 0) {
            layer.close(indexForm);
            $table.bootstrapTable('refresh');
        }
    },
    //3.事件绑定
    bindEvent: function () {
        //添加根节点
        $('#docAddPeer').click(function () {
            docManager.addEditNode({ isRoot: true, own: true });
        });
        //添加子级
        $('#docAddChild').click(function () {
            if (!selNode) {
                layer.msg("请选择指定节点添加！");
                return false;
            }
            //选中节点添加
            if (selNode && selNode.hasOwnProperty("if_create_child") && !selNode.if_create_child) {
                layer.msg("温馨提示：当前选中节点下不允许添加子节点！");
                return false;
            }
            docManager.addEditNode({ isRoot: false, own: false });
        });
        //编辑
        $('#docEdit').click(function () {
            var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
            if (!treeNode) {
                layer.msg("请选中需要编辑的节点！");
                return false;
            }
            //判断当前节点是否允许被编辑
            if (treeNode.hasOwnProperty("read_only") && treeNode.read_only) {
                layer.msg("温馨提示：当前节点系统设置为不允许被修改！");//只读
                return false;
            }
            docManager.addEditNode(treeNode);
        });
        //删除
        $('#docDel').click(function () {
            docManager.editRemove();
        });
        //查询
        $('#btnSearch').click(function () {  
            var zTree = docManager.getSelNode();
            if (!zTree) {
                layer.msg("温馨提示：查询前请先选着目录节点！", { icon: 1 });
                return null;
            }
            $table.bootstrapTable('refresh');
        });
        //系统操作
        //添加系统根节点
        $('#docAddSysPeer').click(function () {
            docManager.addOrEditSysNode({ isRoot: true });
        });
        //添加系统子节点
        $('#docAddSysChild').click(function () {
            docManager.addOrEditSysNode({ isRoot: false });
        });
        //编辑系统节点
        $('#docSysEdit').click(function () {
            var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
            if (!treeNode) {
                layer.msg("请选中需要编辑的节点！");
                return false;
            }
            docManager.addOrEditSysNode(treeNode);
        });
        //待审核资源
        $('#btnApproveFile').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (!row || row.length === 0) {
                layer.msg("没有数据或未选中记录行！", { icon: 5 });
                return false;
            }
            layer.confirm("您确定审核通过当前选中文档吗？", {
                btn: ['确定', '取消'] //按钮
            }, function () {
                //确定审核通过
                var index = layer.msg("数据提交中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/DocManager/ApproveFileByID/' + row[0].file_id,
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
            }, function () { });
        });
        //上传文档
        $('#btnUploadFile').click(function () {
            var node = docManager.getSelNode();
            if (!node) {
                layer.msg("温馨提示：上传文档请先选择需要上传的目录！");
                return false;
            }
            //打开编辑
            indexForm = layer.open({
                id: 'ifreamUpLoadData',
                title: [node.name + '：上传文档', 'font-size:18px;'],
                type: 2,
                content: ctx + '/DocManager/DocUpLoadFile?jsFileType=sourceManager&folder_id=' + node.id,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['800px', '98%'],
                yes: function (li, o) {
                    var menuData = document.getElementById("ifreamUpLoadData").firstChild.contentWindow.upLoadDoc();
                    if (menuData === null)
                        return false;
                },
                cancel: function (li, o) {

                }
            });
        });
        //编辑
        $('#btnEditFile').click(function () {
            if (!docManager.checkBtn()) {
                layer.msg("未选中记录行或者您没有权限操作！", { icon: 5 });
                return false;
            }
            var row = $table.bootstrapTable('getSelections');
            //打开编辑
            indexForm = layer.open({
                id: 'ifreamUpLoadData',
                title: ['编辑上传文档', 'font-size:18px;'],
                type: 2,
                content: ctx + '/DocManager/DocUpLoadFile?file_id=' + row[0].file_id,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['800px', '98%'],
                yes: function (li, o) {
                    var menuData = document.getElementById("ifreamUpLoadData").firstChild.contentWindow.upLoadDoc();
                    if (menuData == null)
                        return false;
                },
                cancel: function (li, o) {

                }
            });
        });
        //删除，下架，撤销分享等删除操作
        $('#btnDeleteFile').click(function () {
            var node = docManager.getSelNode();
            var row = $table.bootstrapTable('getSelections');
            if (!row || row.length === 0) {
                layer.msg("没有数据或未选中记录行！", { icon: 5 });
                return false;
            }
            var reason = "", $url = "";
            //0：我的文档（我的资源），
            //1：我的分享，撤销分享（可以撤销多个分享）
            //2：收到的分享，撤销分享（只能撤销一个）
            //3：公有文档（VR资源），管理员可删除
            //4：回收站（下架资源）
            //5：待审核资源
            //如果是VR资源(公用文档 需要是管理员才能进行下架操作)
            if (node.doc_type === "3" && isDocSys.toLowerCase() != "true") {
                layer.msg("温馨提示：您没有下架权限操作！", { icon: 5 });
                return false;
            }
            switch (node.doc_type) {
                case "1": {
                    //我的分享，撤销分享（可以撤销多个分享）
                    $url = "/DocManager/RemoveMyShareById/" + row[0].file_id;
                    reason = "您确定要撤销对当前文档的分享吗？";
                } break;
                case "2": {
                    //收到的分享，撤销分享（只能撤销一个）
                    $url = "/DocManager/RemoveFileToMeById/" + row[0].file_id;
                    reason = "您确定要移除当前文档的分享吗？";
                } break;
                case "3": {
                    //VR资源，下架操作（回收站）
                    $url = "/DocManager/RemoveNodeById/" + row[0].file_id;
                    reason = "您确定要对当前文档进行下架操作吗？";
                } break;
                case "4": {
                    //彻底删除DelNodeById
                    $url = "/DocManager/DelNodeById/" + row[0].file_id;
                    reason = "您确定要彻底删除当前选中文档吗？";
                } break;
                default: {
                    //我的资源：彻底删除
                    $url = "/DocManager/DelNodeById/" + row[0].file_id;
                    reason = "您确定要删除当前选中文档吗？";
                } break;
            }
            layer.confirm(reason, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("数据提交中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + $url,
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
            }, function () { });
        });
    },
    //3.1获取被选中节点
    getSelNode: function () {
        //获取页面doc_type参数
        //获取节点folder_id参数
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData");
        if (!zTree)
            return null;
        var nodes = zTree.getSelectedNodes(),
           treeNode = nodes[0];
        treeNode.doc_type = doc_type;
        return treeNode;
    },
    //4.添加，编辑
    addEditNode: function (e) {
        var title = "添加节点";
        if (!e.hasOwnProperty("isRoot"))
            title="编辑节点";
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
                    docManager.editNode(e, name);
                else
                    docManager.addNode(e, name);
            },
            cancel: function (li, o) {

            }
        });
    },
    //4.1添加节点：同级节点/子节点
    addNode: function (e,nodeName) {
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),//当前选择节点
            isParent = e.isParent,
			treeNode = nodes[0];
        var dataJson = {
            "pId": (e.isRoot) ? 0 : treeNode.id,
            "name": nodeName,
            "pageType": pageType
        };
        //后台先添加
        $.ajax({
            url: ctx + '/DocManager/AddNode',
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
    //4.2编辑节点
    editNode: function (treeNode, nodeName) {
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData");
        //后台编辑
        $.ajax({
            url: ctx + '/DocManager/EditNode',
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
    //5.删除节点
    editRemove: function (e) {
        layer.msg("提交数据~", { icon: 1, time: 5000 });
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
        if (!treeNode) {
            layer.msg("请选中需要删除的节点！");
            return false;
        }
        //判断当前节点是否允许被编辑
        if (treeNode.hasOwnProperty("read_only") && treeNode.read_only) {
            layer.msg("温馨提示：当前节点系统设置为不允许被删除！");//只读
            return false;
        }
        layer.confirm('您确定要删除当前选中节点吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            //后台删除
            $.ajax({
                url: ctx + '/DocManager/DeleteNode',
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
    //6.操作系统节点
    addOrEditSysNode: function (e) {
        var pid = 0;//默认添加根节点
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
			nodes = zTree.getSelectedNodes(),
			treeNode = nodes[0];
        //添加
        if (e.hasOwnProperty("isRoot")) {
            if(!e.isRoot)
                pid = treeNode.id;//添加子节点
        } else if(!treeNode){
            layer.msg("温馨提示：添加子节点请选中需要被添加的父节点！");
            return false;
        } else
            pid = treeNode.pId;//编辑
        $('#pId').val(pid);
        $('#pageType').val(pageType);
        var title = "添加节点";
        $addOrEditUrl = ctx + "/DocManager/AddSysNode";//新增
        if (!e.hasOwnProperty("isRoot")) {
            title = "编辑节点";
            $addOrEditUrl = ctx + "/DocManager/EditSysNode/" + treeNode.id;//编辑
        }
        //清空表单
        $('#nodeFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [title, 'font-size:18px;'],
            type: 1,
            content: $('#nodeForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '450px'],
            success: function (li, o) {
                if (e && !e.hasOwnProperty("isRoot")) {
                    $('#nodeFormTable')[0].reset();//重置表单
                    $('#name').val(e.name);
                    $('#pId').val(e.pId);
                    $('#was_share').selectpicker('val',  e.was_share.toString());
                    $('#if_create_child').selectpicker('val', e.if_create_child.toString());
                    $('#read_only').selectpicker('val', e.read_only.toString());
                    $('#doc_type').selectpicker('val', e.doc_type.toString());
                    $('#if_sys').selectpicker('val', e.if_sys.toString());
                    //$('#nodeFormTable').LoadForm(e);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#nodeFormTable').valid()) {
                    //验证通过
                    $('#nodeFormTable').ajaxSubmit({
                        url: $addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
                                var nodes = result.Data;
                                if (e.hasOwnProperty("isRoot")) {
                                    //添加
                                    if (e.isRoot)
                                        //添加根节点
                                        zTree.addNodes(null, { id: nodes.id, pId: 0, isParent: false, name: nodes.name, if_create_child: nodes.if_create_child, read_only: nodes.read_only, was_share: nodes.was_share, if_sys: nodes.if_sys, doc_type: nodes.doc_type });
                                    else
                                        zTree.addNodes(treeNode, { id: nodes.id, pId: nodes.pId, isParent: false, name: nodes.name, if_create_child: nodes.if_create_child, read_only: nodes.read_only, was_share: nodes.was_share, if_sys: nodes.if_sys, doc_type: nodes.doc_type });
                                } else {
                                    //编辑
                                    treeNode.name = nodes.name;
                                    treeNode.if_create_child = nodes.if_create_child;
                                    treeNode.read_only = nodes.read_only;
                                    treeNode.was_share = nodes.was_share;
                                    treeNode.if_sys = nodes.if_sys;
                                    treeNode.doc_type = nodes.doc_type;
                                    zTree.updateNode(treeNode);
                                }
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
