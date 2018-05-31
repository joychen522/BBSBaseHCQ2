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
var demo2 = null, pageType = "";//页面参数类别默认为:收文管理
var userID, isDocSys;
var docManager = {
    //1.初始化页面
    initPage: function (userID, isDocSys) {
        parent.showLoadBoxs();
        userID = userID; isDocSys = isDocSys;
        docManager.initPageParam();
        docManager.bindZtree();
        docManager.initDate();
        docManager.bindEvent();
        docManager.initTable();
        parent.delLoadBoxs();
    },
    //1.1初始化页面参数
    initPageParam:function(){
        pageType = $.getUrls("pageType");
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
                url: ctx + '/DocManager/GetDocTreeData?pageType=' + pageType,
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
                    var upLoad = $('#btnUploadFile');
                    if (treeNode.hasOwnProperty("doc_type") && (treeNode.doc_type === 0 || treeNode.doc_type === 3)) {
                        if(upLoad)
                            upLoad.attr("disabled", false);
                    } else {
                        if (upLoad)
                            upLoad.attr("disabled", true);
                    }
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
        if (row != null && row.length == 0)
            return false;
        //0：我的文档，1：我的分享，2：收到的分享，3：公有文档，4：回收站
        if(node.doc_type === "0" || (isDocSys.toLowerCase() === "true" && node.doc_type === "3"))
            return true;
        else
            return false;
    },
    //2.3初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/DocManager/InitTable",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                var node = docManager.getSelNode();
                if (!node) return null;
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    folder_id: node.id,//节点ID
                    doc_type:node.doc_type,//节点类型
                    keyword: $('#keyword').val(),//文档名
                    issue_start: $('#issue_start').val(),//发布开始时间
                    issue_end:$('#issue_end').val()//发布截止时间
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
            {
                field: '',//第一列序号
                title: '查看',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    var file_type = (row.file_type) ? row.file_type.toString().toLowerCase() : "";
                    if (file_type === "mp4" || file_type === "flv")
                        return "<button class=\"btn btn-success btn-circle btn-outline\" title=\"查看\" onclick=docManager.playVideo('" + row.file_name + "','" + row.url.toString().replace("~", window.location.origin + $.ctx()) + "') type=\"button\"><i class=\"fa fa-video-camera\"></i></button>";
                    if (file_type === "pdf" || file_type === "jpg" || file_type === "png" || file_type === "gif" || file_type === "doc" || file_type === "docx" || file_type === "xls" || file_type === "xlsx" || file_type === "ppt" || file_type === "pptx")
                        return "<button class=\"btn btn-success btn-circle btn-outline\" title=\"查看\" onclick=\"docManager.playWordFiles('" + file_type + "','" + row.file_name + "','" + row.url.toString() + "');\" type=\"button\"><i class=\"fa fa-video-camera\"></i></button>";
                    return "";
                }
            },
            {
                field: '',
                title: '分享',
                align: 'center',
                width:50,
                formatter: function (value, row, index) {
                    if (selNode.doc_type === 0)
                        return "<button class=\"btn btn-danger btn-circle btn-outline\" title=\"分享\" onclick=\"docManager.shareDocEvent(" + row.file_id + ")\" type=\"button\"><i class=\"fa fa-heart\"></i></button>";
                    else if (selNode.doc_type === 4)
                        return "<button class=\"btn btn-danger btn-circle btn-outline\" title=\"恢复\" onclick=\"docManager.recoverDocEvent(" + row.file_id + ")\" type=\"button\"><i class=\"fa fa-mail-reply\"></i></button>";
                    return "<button disabled=true class=\"btn btn-danger btn-circle btn-outline\" title=\"不可分享\" type=\"button\"><i class=\"fa fa-heart\"></i></button>";
                }
            },
            {
                field: 'url',
                title: '附件',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return "<a href=\"" + value.toString().replace("~", window.location.origin + $.ctx()) + "\" class=\"btn btn-primary btn-circle\" title=\"下载\"><i class=\"fa fa-download\"></i></a>";
                    return "";
                }
            },
	        {
	            field: 'file_name',
	            title: '文档名称',
	            align: 'center',
                width:300,
	            formatter: function (value, row, index) {
	                var file_type = (row.file_type) ? row.file_type.toString().toLowerCase() : "";
	                var alink = null, back = "";
	                if (file_type === "mp4" || file_type === "flv")
	                    alink = "<a href='#' onclick=docManager.playVideo('" + row.file_name + "','" + row.url.toString().replace("~", window.location.origin + $.ctx()) + "')>";
	                //if (value && value.toString().length > 20)
	                //    back = "<span title='" + value + "'>" + value.toString().substring(0, 20) + "...</span>";
                    else
	                    back = value;
	                if (alink)
	                    return alink + back + "</a>";
	                return back;
	            }
	        },
	        /*{
	            field: 'alias_name',
	            title: '文档别名',
	            align: 'center'
	        },
            {
	            field: 'file_size',
	            title: '文档大小',
	            align: 'center'
	        },{
	            field: 'issue_end',
	            title: '截止时间',
	            align: 'center'
	        }, {
	            field: 'issue_name',
	            title: '颁发机构',
	            align: 'center'
	        },*/
            {
                field: 'doc_type',
                title: '文档类型',
                align: 'center'
            }, {
                field: 'doc_number',
                title: '文号',
                align: 'center'
            }, {
	            field: 'create_name',
	            title: '上传者',
	            align: 'center'
	        }, {
	            field: 'issue_start',
	            title: '颁布时间',
	            align: 'center'
	        },
	        {
	            field: 'file_type',
	            title: '文档属性',
	            align: 'center'
	        }
            /*, {
	            field: 'create_time',
	            title: '上传时间',
	            align: 'center'
	        }*/],
            onClickRow: function (row, $element) {
                //var node = docManager.getSelNode();
                //if (row && (node.doc_type === 0 || node.doc_type === 4 || isDocSys.toLowerCase==="true" || row.create_id === userID)) {
                //    //启用
                //    $('#btnEditFile').attr("disabled", false);
                //    $('#btnDeleteFile').attr("disabled", false);
                //}
                //else {
                //    //禁用
                //    $('#btnEditFile').attr("disabled", true);
                //    $('#btnDeleteFile').attr("disabled", true);
                //}
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //在线预览文档
    playWordFiles: function (file_type,name, url) {
        if(!url)
            return false;
        var index = layer.msg("文档获取中......", { icon: 1, time: 5000 });
        //if (file_type === "pdf" || file_type === "jpg" || file_type === "gif" || file_type === "png") {
        //    window.parent.main_openWindowByLink({
        //        url: url.toString().replace("~", window.location.origin + $.ctx()),
        //        height: "95%",
        //        width: "1100px",
        //        title: name,
        //        scroll: true,
        //        btn: ''
        //    });
        //    return false;
        //}
        $.ajax({
            type: 'get',
            url: ctx + '/DocManager/CourseViewOnLine?fileName=' + encodeURI(url),
            //dataType: 'json',
            async: false,
            success: function (data) {
                layer.close(index);
                if (!data) {
                    layer.msg("温馨提示：内容为空或者不支持在线预览！");
                    return false;
                }
                window.parent.main_openWindowByLink({
                    url: data.toString().replace("\\", "/").replace("~", window.location.origin + $.ctx()),
                    height: "550px",
                    width: "1100px",
                    title: name,
                    scroll: true,
                    btn: ''
                });
            }
        });
    },
    //2.3.0 视频文件查看 在线播放
    playVideo: function (name,url) {
        $('#vide_src').attr("src", url);
        layer.open({
            title: name,
            type: 1,
            content: $('#video'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: '',
            area: ['800px', '500px']
        });
    },
    //2.3.1回收站恢复
    recoverDocEvent: function (file_id) {
        var index = layer.msg("文档恢复中请稍候...", { icon: 6, time: 6000 });
        $.ajax({
            type: 'post',
            url: ctx + '/DocManager/RecoverNodeById/'+file_id,
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
                    docManager.initListBox(file_id,$saveurl, index);
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
    //2.7初始化日期控件
    initDate:function(){
        var dateStart = {
            elem: '#issue_start',
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
            elem: '#issue_end',
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
        $('#issue_start').val(laydate.now().substring(0, 4) + '-01-01');
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
                content: ctx + '/DocManager/DocUpLoadFile?folder_id=' + node.id,
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
                title: [ '编辑上传文档', 'font-size:18px;'],
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
        //删除
        $('#btnDeleteFile').click(function () {
            var mark = true;
            var node = docManager.getSelNode();
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("没有数据或未选中记录行！", { icon: 5 });
                return false;
            }
            var reason = "您确定要删除当前选中文档吗？";
            var $url = "/DocManager/RemoveNodeById/" + row[0].file_id;
            //0：我的文档，
            //1：我的分享，撤销分享（可以撤销多个分享）
            //2：收到的分享，撤销分享（只能撤销一个）
            //3：公有文档，管理员可删除
            //4：回收站
            if (node.doc_type === 3 && isDocSys.toLowerCase() != "true" && row[0].create_id != userID)
                mark = false;
            if (!mark) {
                layer.msg("未选中记录行或者您没有权限操作！", { icon: 5 });
                return false;
            }
            if (node.doc_type === 1) {//我的分享，撤销分享（可以撤销多个分享）
                $url = "/DocManager/RemoveMyShareById/" + row[0].file_id;
                reason = "您确定要撤销对当前文档的分享吗？";
            } 
            if (node.doc_type === 2) {//收到的分享，撤销分享（只能撤销一个）
                $url = "/DocManager/RemoveFileToMeById/" + row[0].file_id;
                reason = "您确定要移除当前文档的分享吗？";
            } 
            if (node && node.doc_type === 4)//彻底删除DelNodeById
                $url = "/DocManager/DelNodeById/" + row[0].file_id;
            layer.confirm(reason, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
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
    //3.0关闭文档上传窗口
    closeUpLoadForm: function (data) {
        if (indexForm && data && data.Statu === 0) {
            layer.close(indexForm);
            $table.bootstrapTable('refresh');
        }
    },
    //3.1获取被选中节点
    getSelNode:function(){
        var zTree = $.fn.zTree.getZTreeObj("unitTreezTreeData"),
           nodes = zTree.getSelectedNodes(),
           treeNode = nodes[0];
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
                    docManager.editNode(e,name);
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

//菜单--权限关联管理
var docLimitData = null, $treetable;
var menuLimitManager = {
    //初始化资源表格
    initTreeTable: function () {
        var html = '<tr><td>文档菜单名称</td><td style="text-align:center;">操作权限</td></tr>';
        $('#treeTable').append(menuLimitManager.getNodesHtml(html, 0));
        $treetable = $('#treeTable').treeTable({
            theme: 'default',
            beforeExpand: function ($treeTable, id) {
                if ($('.' + id, $treeTable).length) {
                    return;
                }
                html = '';
                $treeTable.addChilds(menuLimitManager.getNodesHtml(html, id));
            }
        });
    },
    getNodesHtml: function (html, parentId, type) {
        $.ajax({
            type: "post",
            url: ctx + "/DocManager/GetDocMenuChildsByParentID",
            data: { "pid": parentId, type: type },
            dataType: "json",
            async: false,
            success: function (data) {
                for (var i = 0; i < data.nodes.length; i++) {
                    var $nodes = data.nodes[i];
                    var $action = menuLimitManager.action(data.nodes[i]);//封装操作按钮
                    html = menuLimitManager.nodeHtml(html, $nodes, $action);
                }
            }
        });
        return html;
    },
    nodeHtml: function (html, nodes, action) {
        var $pid = "", $hasChild = "";
        if (nodes.pId > 0)
            $pid = ' pId=' + nodes.pId;
        if (nodes.hasChild)
            $hasChild = ' hasChild="true" ';
        html += '<tr id=' + nodes.id + $pid + $hasChild + '>' + '<span ><td>' + nodes.name + '</td></span>' + '<td style="text-align:center;">' + action + '</td></tr>';
        return html;
    },
    //封装按钮事件
    action: function (node) {
        var checkStr = " everstate='uncheck' ";
        if (null != docLimitData && docLimitData.length > 0) {
            $.each(docLimitData, function (index, value) {
                if (value.folder_id.toString() === node.id.toString()) {
                    checkStr = " checked='checked' everstate='checked' ";
                    return false;
                }
            });
        }
        //选择框
        var check = "<input id='node" + node.id + "'  " + checkStr + " ownID='" + node.id + "' parentID='" + node.pId
            + "' onclick='menuLimitManager.checkClick(this);' class='menu-limit' value='" + node.id + "' type='checkbox' />";
        return check;
    },
    checkClick: function (cb) {
        var $this = $(cb), parentID = $this.attr("parentID");
        //判断是否选中父节点
        if ($this.is(':checked') && parentID != null && parentID != "0") {
            //被选中
            $('#node' + parentID).attr("checked", true);
        }
        //判断是否取消子节点
        if (!$this.is(':checked')) {
            //取消当前项，并且取消其下的子节点
            $(".menu-limit[parentID='" + $this.attr("ownID") + "']").attr("checked", false);
        }
    },
    //确认保存数据
    getMenuData: function () {
        var reak = "undeal";//默认不需要后端处理
        var checkedData = $('.menu-limit:checked');//选中的
        var uncheckData = $('.menu-limit').not("input:checked");//未选中的
        var addData = "", delData = "";
        if (checkedData.size() > 0) {
            $.each(checkedData, function (index, item) {
                if ($(item).attr("everstate") === "uncheck")
                    addData += $(item).attr("ownID") + ",";
            });
        } else {
            reak = "deal";//需要后端处理
            return ";|" + reak;
        }
        if (uncheckData.size() > 0) {
            $.each(uncheckData, function (index, item) {
                if ($(item).attr("everstate") === "checked")
                    delData += $(item).attr("ownID") + ",";
            });
        }
        if (addData != "" || delData != "")
            reak = "deal";//需要后端处理
        return addData + ";" + delData + "|" + reak;
    },
    //获取已经配置的权限数据
    getMenuLimitData: function () {
        var per_id = $.getUrls("per_id");//权限id
        if (per_id === null || per_id === undefined) {
            menuLimitManager.initTreeTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/DocManager/GetDocMenuLimitData/" + per_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    docLimitData = data.Data;
                    menuLimitManager.initTreeTable();
                } else {
                    layer.msg(data.Msg, { icon: 5 });
                }
            }
        });
    }
}