/*******************************************************
 *  模块权限 相关操作js
 * <p>Title: moduleManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年9月9日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var menuLimitManager = {
    //初始化zTree
    initzTree: function () {
        per_id = $.getUrls("per_id");
        var setting = {
            view: {
                showIcon: false,
                selectedMulti: false,
                fontCss: getFontCss
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: ctx + "/SysModule/GetUnitTreeData/" + per_id,
                autoParam: ["id"]
            },
            data: {
                key: { title: "name", name: "name" },
                simpleData: { enable: true, idKey: "id", pIdKey: "pId", rootPId: 0 }
            },
            check: {
                enable: true,
                autoCheckTrigger: true,
                chkStyle: "checkbox",
                chkboxType: { "Y": "p", "N": "ps" }
            },
            callback: {
                onAsyncError: function () {
                    layer.msg("初始化单位结构树失败~", { icon: 5 });
                },
                onClick: function (event, treeId, treeNode) {
                    layer.msg(treeNode.name);
                }
            }
        };
        lintzTree = $("#moduleTree").initzTreeView(setting);
    },
    //获取所有被选中的数据
    getMenuData: function () {
        var addData = "", delData = "", reak = "undeal";//默认不需要后端处理
        var treeObj = lintzTree;
        var nodes = treeObj.getCheckedNodes(true);//选中的
        var unodes = treeObj.getCheckedNodes(false);//未选中的
        if (nodes.length > 0) {
            $.each(nodes, function (index, item) {
                if ($(item).attr("everstate") === "uncheck")
                    addData += item.id + ",";
            });
        } else {
            //全部删除，没有选中一个
            reak = "deal";//需要后端处理
            return ";|" + reak;
        }
        if (unodes.length > 0) {
            $.each(unodes, function (index, item) {
                if ($(item).attr("everstate") === "checked")
                    delData += item.id + ",";
            });
        }
        if (addData != "" || delData != "")
            reak = "deal";//需要后端处理
        return addData + ";" + delData + "|" + reak;
    }
}

//模块管理
var $table, addOrEditUrl;
var moduleManager = {
    //初始化页面
    initPage: function () {
        moduleManager.initTable();
        moduleManager.bindEvent();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysModule/InitModuleTable",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    moduleName: encodeURI($('#moduleName').val())//模块名称
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
                     field: 'sm_name',
                     title: '模块名称',
                     align: 'center'
                 }, {
                     field: 'sm_code',
                     title: '模块代码',
                     align: 'center'
                 }, {
                     field: 'sm_image1',
                     title: '菜单图标',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value) 
                             return "<a href=\"#\"><i class=\"fa " + value + "\"></i></a>";
                         return "";
                     }
                 }, {
                     field: 'sm_image2',
                     title: '模块图标',
                     align: 'center'
                 }, {
                     field: 'if_start',
                     title: '是否启用',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value)
                             return "已启用";
                         return "已停用";
                     }
                 }, {
                     field: 'create_name',
                     title: '创建人',
                     align: 'center'
                 }, {
                     field: 'create_time',
                     title: '创建时间',
                     align: 'center',
                      formatter: function (value, row, index) {
                         if (value)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         return "";
                     }
                 }],
            onClickRow: function (row, $element) {

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
        $('#moduleDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#groupFormTable').resetHideValidForm();
            layer.open({
                title: ['权限详细信息', 'font-size:18px;'],
                type: 1,
                content: $('#groupForm'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['480px', '95%'],
                success: function (li, o) {
                    if (row != null) {
                        $('#groupFormTable')[0].reset();//重置表单
                        $('#groupFormTable').LoadForm(row[0]);//表单填充数据
                    }
                }
            });
        });
        //添加
        $('#moduleAdd').click(function () {
            moduleManager.editForm();
        });
        //编辑
        $('#moduleEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            moduleManager.editForm(row[0]);
        });
        //删除
        $('#moduleDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('温馨提示：删除模块将造成系统不可使用的风险，您确定要删除当前选中模块吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/SysModule/DelModule/' + row[0].sm_id,
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
        //设置菜单图标
        $('#btnSetIcon').click(function () {
            var index = layer.open({
                id: 'ifreamSelectMenu',
                title: ['选择菜单图标'],
                type: 2,
                content: ctx + '/SysMenu/SelectMenuIcon',
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['800px', '95%'],
                yes: function (li, o) {
                    //保存
                    var icon = document.getElementById("ifreamSelectMenu").firstChild.contentWindow.getIcon();
                    $('#iconSelect').removeAttr("class");
                    $('#iconSelect').attr("class", "fa " + icon);
                    $('#iconSelect').text("");
                    $('#iconSelect').text(" " + icon.substr(3));
                    $('#sm_image1').val(icon);
                    layer.close(index);
                },
                cancel: function (li, o) {

                }
            });
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加组信息";
        addOrEditUrl = ctx + "/SysModule/AddModule";//新增
        if (row != null && row != "" && row != undefined) {
            $title = "编辑组信息";
            addOrEditUrl = ctx + "/SysModule/EditModule?sm_id=" + row.sm_id;//编辑
        }
        //清空表单
        $('#groupFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#groupForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['450px', '95%'],
            success: function (li, o) {
                if (row != null) {
                    $('#groupFormTable')[0].reset();//重置表单
                    $('#groupFormTable').LoadForm(row);//表单填充数据
                    $('#if_start').selectpicker('val', row.if_start.toString());
                    $('#iconSelect').removeAttr("class");
                    $('#iconSelect').attr("class", "fa " + row.sm_image1);
                }
            },
            yes: function (li, o) {
                moduleManager.submitForm(addOrEditUrl);
            },
            cancel: function (li, o) {

            }
        });

    },
    submitForm: function (addOrEditUrl) {
        var if_start = $('#if_start').val();
        if (!if_start) {
            layer.msg("请选择是否启用！", { icon: 5 });
            return false;
        }
        if ($('#groupFormTable').valid()) {
            //验证通过
            $('#groupFormTable').ajaxSubmit({
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
