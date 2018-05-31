var addOrEditUrl, $treetable;
//菜单管理
var menuManager = {
    //初始化资源表格
    initTreeTable: function () {
        var html = '<tr><td>菜单名称</td><td style="text-align:center;">访问路径</td><td style="text-align:center;">操作</td></tr>';
        $('#treeTable').append(menuManager.getNodesHtml(html, 0));
        $treetable = $('#treeTable').treeTable({
            theme: 'default',
            beforeExpand: function ($treeTable, id) {
                if ($('.' + id, $treeTable).length) {
                    return;
                }
                html = '';
                $treeTable.addChilds(menuManager.getNodesHtml(html, id));
            }
        });
    },
    //初始化下拉
    initSelect: function () {
        $('#sm_code').initModuleSelectpicker();
        $('#smCode').initModuleSelectpicker();
    },
    //绑定事件
    bindEvent: function () {
        $('#btnSearch').click(function () {
            $('#treeContent').empty();
            $('#treeContent').append($('<table id="treeTable" class="table table-striped table-bordered table-hover table-condensed" style="font-size: 13px"></table>'));
            menuManager.initTreeTable();
        });
    },
    getNodesHtml: function (html, parentId,type) {
        $.ajax({
            type: "post",
            url: ctx + "/SysMenu/GetSysMenuChildsByParentID",
            data: { "pid": parentId, type: type, sm_code: $('#smCode').val() },
            dataType: "json",
            async: false,
            success: function (data) {
                for (var i = 0; i < data.nodes.length; i++) {
                    var $nodes = data.nodes[i];
                    var $action = menuManager.action(data.nodes[i]);//封装操作按钮
                    html = menuManager.nodeHtml(html, $nodes, $action);
                }
            }
        });
        return html;
    },
    nodeHtml: function (html, nodes, action) {
        var $pid = "", $hasChild="";
        if (nodes.pId > 0)
            $pid = ' pId=' + nodes.pId;
        if (nodes.hasChild)
            $hasChild = ' hasChild="true" ';
        html += '<tr id=' + nodes.id + $pid + $hasChild+'>' + '<span ><td>' + nodes.name + '</td></span>' + '<td>' + (nodes.url!=null?nodes.url:" ") + '</td>' + '<td style="text-align:center;">' + action + '</td></tr>'; 
        return html;
    },
    //封装按钮事件
    action: function (node) {
        //编辑
        var action = "<button onclick=menuManager.editMenuDataById('" + node.id + "') class='btn btn-info btn-xs'><i class='fa fa-paste'></i>编辑</button>  ";
        //删除
        action += "<button onclick=menuManager.del('" + node.id + "','"+node.name+"') class='btn btn-danger btn-xs'><i class='fa fa-times'></i>删除</button>   ";
        //操作
        action += "<span class='dropdown'>" +
            "<button class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' >节点操作<b class='caret'></b></button>";
        //添加同级节点
        action += "<ul class='dropdown-menu'><li><a href='#' onclick=menuManager.add('"+node.id+"','" + node.pId + "','same')>添加同级节点</a></li>";
        //添加子节点
        action += "<li><a href='#' onclick=menuManager.add('"+node.id+"','" + node.id + "','child')>添加子节点</a></li>";
        //上移
        action += "<li><a href='#' onclick=menuManager.upOrDownMove('"+node.id+"','up')>上移</a></li>";
        //下移
        action += "<li><a href='#' onclick=menuManager.upOrDownMove('"+node.id+"','down')>下移</a></li></ul></span>";
        return action;
    },
    //编辑
    edit: function (name,url,id,icon,sm_code,data) {
        addOrEditUrl = ctx + "/SysMenu/EditMenu/" + id;//编辑
        //清空表单
        $('#menuFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑菜单', 'font-size:18px;'],
            type: 1,
            content: $('#menuForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '450px'],
            success: function (li, o) {
                $('#folder_name').val(name);
                $('#sm_code').selectpicker('val', sm_code);//data
                $('#folder_type').selectpicker('val', data.Data.folder_type);
                if (url!=null && url!="null" && url!="")
                    $('#folder_url').val(url);
                if (icon != null && icon != "null" && icon != "") {
                    $('#folder_image').val(icon);
                    $('#iconSelect').removeAttr("class");
                    $('#iconSelect').attr("class", "fa " + icon);
                    $('#iconSelect').text("");
                    $('#iconSelect').text(" "+icon.substr(3));
                }
                if (data.Data.is_Bus)
                    $('#is_Bus_img').attr('checked', true);
                else
                    $('#is_Bus_img').removeAttr('checked');
                if (data.Data.Bus_Code)
                    $('#Bus_Code').val(data.Data.Bus_Code);
            },
            yes: function (li, o) {
                var sm_code = $('#sm_code').val();
                if (!sm_code) {
                    layer.msg("请选择所属模块！", { icon: 5 });
                    return false;
                }
                //如果开启流程则 必须录入业务代码
                var is_Bus = $('#is_Bus_img').is(':checked'), bus_code = $('#Bus_Code').val();
                $('#is_Bus').val(is_Bus);
                if (is_Bus && !bus_code) {
                    layer.msg("开启业务流程必须录入业务代码！", { icon: 5 });
                    return false;
                }
                if ($('#menuFormTable').valid()) {
                    //验证通过
                    $('#menuFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                var tds = $treetable.find('tr[id=' + result.Data.folder_id + ']').children("td");
                                var $namehtml = "";
                                for (var i = 0; i < tds.eq(0).find("span").length; i++) {
                                    $namehtml += tds.eq(0).find("span")[i].outerHTML;
                                };
                                //更新数据
                                tds.eq(0).html($namehtml + result.Data.folder_name);
                                tds.eq(1).text(result.Data.folder_url);
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
    },
    //删除
    del:function(id,name) {
        layer.confirm('您确定要删除当前选中菜单，删除菜单同时会删除菜单对应的按钮？', {
            btn: ['确定', '取消'] //按钮
        }, function() {
            //确认删除
            var index = layer.msg("删除中...", { icon: 6, time: 6000 });
            $.ajax({
                type: 'post',
                url: ctx + '/SysMenu/DeleteMenuById/' + id,
                data: { name:encodeURI(name)},
                dataType: 'json',
                async: false,
                success: function (data) {
                    layer.close(index);
                    if (data.Statu == 0) {
                        layer.msg("删除成功...");
                        //手动移除行
                        var tds = $treetable.find('tr[id=' + id + ']');
                        if (tds != null)
                            $(tds).remove();
                    }
                    else
                        layer.msg(data.Msg, { icon: 5 });
                }
            });
        }, function() {});
    },
    //添加同级、子节点
    add:function(id,pid,type) {
        addOrEditUrl = ctx + "/SysMenu/AddMenu/";//添加
        //清空表单
        $('#menuFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['添加菜单', 'font-size:18px;'],
            type: 1,
            content: $('#menuForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '450px'],
            success: function (li, o) {
                $('#is_Bus_img').attr('checked', false);
            },
            yes: function (li, o) {
                var sm_code = $('#sm_code').val();
                if (!sm_code) {
                    layer.msg("请选择所属模块！", { icon: 5 });
                    return false;
                }
                //如果开启流程则 必须录入业务代码
                var is_Bus = $('#is_Bus_img').is(':checked'), bus_code = $('#Bus_Code').val();
                $('#is_Bus').val(is_Bus);
                if (is_Bus && !bus_code) {
                    layer.msg("开启业务流程必须录入业务代码！", { icon: 5 });
                    return false;
                }
                if ($('#menuFormTable').valid()) {
                    //验证通过
                    $('#menuFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        data:{pid:pid},
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            layer.closeAll();
                            if (result.Statu === 0) {
                                var html = menuManager.getNodesHtml("", result.Data,"own");
                                 if(type=="child")
                                     //添加子节点
                                     $treetable.addChild(pid,html);
                                 else if (type == "same") 
                                    //添加兄弟节点
                                    $treetable.addSibling(id, html);
                                layer.msg('添加成功', { icon: 1 });
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
    //上移/下移
    upOrDownMove:function(id,type) {
        var index = layer.msg("排序中...", { icon: 6, time: 6000 });
        var tds = $treetable.find('tr[id=' + id + ']');
        var tempMove = null;
        switch(type) {
            case "up":{
                    //上移
                    tempMove = $(tds).attr("isfirstone");
                    if (tempMove === "true") {
                        layer.msg("当前对象已经排在本层次的第一位置了~", { icon: 5 });
                        return false;
                    }
                }
                break;
            case "down":{
                    //下移
                    tempMove = $(tds).attr("islastone");
                    if (tempMove === "true") {
                        layer.msg("当前对象已经排在本层次的最后一位置了~", { icon: 5 });
                        return false;
                    }
                }
                break;
        }
        $.ajax({
            type: 'post',
            url: ctx + '/SysMenu/OrderMenuById/' + id,
            dataType: 'json',
            data:{type:type},
            async: false,
            success: function (data) {
                layer.close(index);
                if (data.Statu === 0) {
                    if (type === "up") {
                        //上移
                        var $uptr = $treetable.find('tr[id=' + data.Data + ']');//上一个元素id
                        var $str = $treetable.find('tr[id=' + id + ']');//当前元素id
                        $uptr.before($str);
                    } else if (type === "down") {
                        //下移
                        var $downtr = $treetable.find('tr[id=' + data.Data + ']');//下一个元素id
                        var $str = $treetable.find('tr[id=' + id + ']');//当前元素id
                        $downtr.after($str);
                    }
                }
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    },
    editMenuDataById:function(id) {
        $.ajax({
            type: 'post',
            url: ctx + '/SysMenu/GetMenuDataById/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0)
                    menuManager.edit(data.Data.folder_name, data.Data.folder_url, id, data.Data.folder_image,data.Data.sm_code,data);
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    }
}
//设置图标
menuManager.bandEvent=function() {
    $('#btnSetIcon').click(function () {
        var index = layer.open({
            id: 'ifreamSelectMenu',
            title: ['选择菜单图标'],
            type: 2,
            content: ctx + '/SysMenu/SelectMenuIcon',
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['800px', '450px'],
            yes: function (li, o) {
                //保存
                var icon = document.getElementById("ifreamSelectMenu").firstChild.contentWindow.getIcon();
                $('#iconSelect').removeAttr("class");
                $('#iconSelect').attr("class", "fa " + icon);
                $('#iconSelect').text("");
                $('#iconSelect').text(" "+icon.substr(3));
                $('#folder_image').val(icon);
                layer.close(index);
            },
            cancel: function (li, o) {

            }
        }); 
    });
}

var menuLimitData = null;
//菜单--权限关联管理
var menuLimitManager= {
    //初始化资源表格
    initTreeTable: function () {
        var html = '<tr><td>菜单名称</td><td style="text-align:center;">访问路径</td><td style="text-align:center;">操作权限</td></tr>';
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
            url: ctx + "/SysMenu/GetSysMenuChildsByParentID",
            data: { "pid": parentId, type: type, sm_code: $.getUrls("sm_code") },
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
        html += '<tr id=' + nodes.id + $pid + $hasChild + '>' + '<span ><td>' + nodes.name + '</td></span>' + '<td>' + (nodes.url != null ? nodes.url : " ") + '</td>' + '<td style="text-align:center;">' + action + '</td></tr>';
        return html;
    },
    //封装按钮事件
    action: function (node) {
        var checkStr = " everstate='uncheck' ";
        if (null != menuLimitData && menuLimitData.length>0) {
            $.each(menuLimitData, function(index,value) {
                if (value.folder_id.toString() === node.id.toString()) {
                    checkStr = " checked='checked' everstate='checked' ";
                    return false;
                }
            });
        } 
        //选择框
        var check = "<input id='node" + node.id + "'  "+checkStr+" ownID='"+node.id+"' parentID='" + node.pId
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
            $.each(checkedData, function(index, item) {
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
    getMenuLimitData:function() {
        var per_id = $.getUrls("per_id");
        if (per_id === null || per_id === undefined) {
            menuLimitManager.initTreeTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/SysMenu/GetMenuLimitData/" + per_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    menuLimitData = data.Data;
                    menuLimitManager.initTreeTable();
                } else {
                    layer.msg(data.Msg, { icon: 5 });
                }
            }
        });
    }
}