/*******************************************************
 *  权限管理 相关操作js
 * <p>Title: elementManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年1月9日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//元素管理
var $table,
    menuId=0,
    addOrEditUrl;
var elementManager = {
    //初始化菜单列表
    initMenu:function() {
        /// <summary>Table初始化菜单列表</summary>  
        $.ajax({
            url: ctx + '/SysMenu/GetSysMenuAllData',
            type: "post",
            async: false,
            dataType: 'json',
            data: { sm_code: $('#smCode').val() },
            success: function (mess) {
                if (mess.Statu == 0)
                    elementManager.bindTree(mess.Data);
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('登录异常~', { icon: 5 });
            }
        });
    },
    bindTree: function (result) {
        $("#MenuTreeview").empty();//清空
        $("#MenuTreeview").treeview({
            levels: 1,
            color: "#428bca",
            data: result,
            highlightSelected: true, //是否高亮选中
            nodeIcon: 'glyphicon glyphicon-globe',
            emptyIcon: '', //没有子节点的节点图标
            onNodeSelected: function (e, o) {
                if (o && o.folder_id>0) {
                    menuId = o.folder_id;
                    $table.bootstrapTable('refresh');
                }
            }
        });
    },
    //初始化下拉
    initSelect: function () {
        $('#smCode').initModuleSelectpicker();
    },
    //绑定事件
    bindEvent: function () {
        //添加
        $('#elementAdd').click(function() {
            if (menuId === 0)
                layer.msg("未选中菜单具体菜单树~", { icon: 5 });
            else
                elementManager.addOredit();
        });
        //编辑
        $('#elementEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            elementManager.addOredit(row[0]);
        });
        //删除
        $('#elementDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中元素？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/SysElement/DelElementById/' + row[0].pe_id,
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
    //初始化表格
    initTable: function () {
        var options = {
            url: ctx + "/SysElement/InitTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    menuId: menuId//菜单ID
                }
                return params;
            },
            columns: [
                 {
                     radio: true
                 }, {
                     field: '',//第一列序号
                     title: '序号',
                     align: 'center',
                     width: 50,
                     formatter: function (value, row, index) {
                         return index + 1;
                     }
                 }, {
                     field: 'pe_name',
                     title: '元素名称',
                     align: 'center'
                 }, {
                     field: 'pe_code',
                     title: '元素代码',
                     align: 'center'
                 }, {
                     field: 'pe_event',
                     title: '元素事件',
                     align: 'center'
                 }, {
                     field: 'pe_func',
                     title: '元素方法',
                     align: 'center'
                }, {
                    field: 'pe_create_time',
                     title: '创建日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }, {
                     field: 'pe_note',
                     title: '备注',
                     align: 'center'
            }]
        }
        $table = tableHelper.initTable("elementTableToolbar", options);
    },
    //添加/编辑
    addOredit: function (row) {
        var $title = "编辑元素信息";
        if (row == null || row == "" || row == undefined) {
            $title = "添加元素信息";
            addOrEditUrl = ctx + "/SysElement/AddElement?folder_id=" + menuId;//新增
        }
        else
            addOrEditUrl = ctx + "/SysElement/EditElement?pe_id=" + row.pe_id;//编辑
        //清空表单
        $('#elementFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#elementForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['450px', '430px'],
            success: function (li, o) {
                if (row != null) {
                    $('#elementFormTable')[0].reset();//重置表单
                    $('#elementFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                elementManager.submitForm(addOrEditUrl);
            },
            cancel: function (li, o) {

            }
        });
    },
    submitForm: function (addOrEditUrl) {
        if ($('#elementFormTable').valid()) {
            //验证通过
            $('#elementFormTable').ajaxSubmit({
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


//元素--权限关联管理
var elementLimitData = null,
    elementData = null,$elTable,sm_code;
var menuLimitManager = {
    //初始化资源表格
    initTreeTable: function () {
        var html = '<tr><td>菜单名称</td><td>访问路径</td><td>操作权限</td></tr>';
        $('#treeTable').append(menuLimitManager.getNodesHtml(html, 0));
        $elTable = $('#treeTable').treeTable({
            theme: 'default',
            expandLevel: 1,
            beforeExpand: function ($treeTable, id) {
                if ($('.' + id, $treeTable).length)
                    return;
                html = '';
                $treeTable.addChilds(menuLimitManager.getNodesHtml(html, id));
            }
        });
    },
    getNodesHtml: function (html, parentId, type) {
        //获取parentId，folder_id对于的元素
        $.ajax({
            type: "post",
            url: ctx + "/SysElement/GetElementDataByFolderPId/" + parentId,
            dataType: "json",
            data: { sm_code: sm_code },
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    elementData = data.Data;
                } else {
                    elementData = null;
                    layer.msg(data.Msg, { icon: 5 });
                }
            }
        });
        //根据parentId获取菜单数据
        $.ajax({
            type: "post",
            url: ctx + "/SysMenu/GetSysMenuChildsByParentID",
            data: { "pid": parentId, type: type, sm_code: sm_code },
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
        html += '<tr id=' + nodes.id + $pid + $hasChild + '>' + '<span ><td>' + nodes.name + '</td></span>' + '<td>' + (nodes.url != null ? nodes.url : " ") + '</td>' + '<td>' + action + '</td></tr>';
        return html;
    },
    //封装按钮事件
    action: function (node) {
        var checkStr = "", check = "";
        if (null != elementData) {
            $.each(elementData, function (index, value) {
                //判断是否为当前页面下的元素，是则绘制
                if (value.folder_id.toString() === node.id.toString()) {
                    checkStr = " everstate='uncheck' ";
                    if (null != elementLimitData) {
                        //判断元素是否配置过权限
                        $.each(elementLimitData, function (index, item) {
                            if (item.pe_id.toString() === value.pe_id.toString()) {
                                checkStr = " checked='checked' everstate='checked' ";
                                return false;
                            }
                        });
                    }
                    check += "<label style='padding-left:8px;'><input id='node" + value.pe_id + "'  " + checkStr + " peid='" + value.pe_id + "' class='element-limit' value='" + value.pe_id + "' type='checkbox'/>&nbsp;" + value.pe_name + "</label>";
                }        
            });
        }
        return check;
    },
    //确认保存数据
    getMenuData: function () {
        var reak = "undeal";//默认不需要后端处理
        var checkedData = $('.element-limit:checked');//选中的
        var uncheckData = $('.element-limit').not("input:checked");//未选中的
        var addData = "", delData = "";
        if ($('.element-limit').size() <= 0) 
            return ";|undeal";
        if (checkedData.size() > 0) {
            $.each(checkedData, function(index, item) {
                if ($(item).attr("everstate") === "uncheck")
                    addData += $(item).attr("peid") + ",";
            });
        } else {
            reak = "deal";//需要后端处理
            return ";|" + reak;
        }
        if (uncheckData.size() > 0) {
            $.each(uncheckData, function (index, item) {
                if ($(item).attr("everstate") === "checked")
                    delData += $(item).attr("peid") + ",";
            });
        }
        if (addData != "" || delData != "")
            reak = "deal";//需要后端处理
        return addData + ";" + delData + "|" + reak;
    },
    getElementLimitData: function () {
        sm_code = $.getUrls("sm_code");
        var per_id = $.getUrls("per_id");
        if (per_id === null || per_id === undefined) {
            menuLimitManager.initTreeTable();
            return false;
        }
        //根据权限id获取元素
        $.ajax({
            type: "post",
            url: ctx + "/SysElement/GetElementLimitDataById/" + per_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) 
                    elementLimitData = data.Data;
                menuLimitManager.initTreeTable();
            }
        });
    }
}