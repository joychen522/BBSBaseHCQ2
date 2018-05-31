/*******************************************************
 *  系统设置>用户管理 相关操作js
 * <p>Title: userManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2016年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//用户管理
var $table, addOrEditUrl, selUnit = null;
var userManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        userManager.initUnitPage();
        userManager.initTable();
        userManager.bindEvent();
        userManager.initSelect();
        parent.delLoadBoxs();
    },
    initUnitPage: function () {
        //社工人员，默认为系统用户
        if (UserType === "socialUser") {
            selUnit = areaCode;
            $('#orgUnit').val(selUnit);//设置组织机构代码选项
            $('#tree').css("display", "none");
            $('#userContext').removeClass("col-sm-10");
            $('#userContext').addClass("col-sm-12");
        }else
            userManager.initUnitData();
    },
    //获取单位数据
    initUnitData: function () {
        $.ajax({
            url: ctx + '/SysUser/GetUserUnitTreeData',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0) {
                    userManager.initUnitTree(mess.Data);
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
                if (!o.folder_id) 
                    selUnit = "";
                else
                    selUnit = o.folder_id;
                $('#orgUnit').val(selUnit);//设置组织机构代码选项
                $table.bootstrapTable('refresh');
            }
        });
        //初始化时自动选择系统用户
        var temp = $treeUnit.treeview('getEnabled')[0];
        if (temp != null) {
            $treeUnit.treeview('selectNode', [temp.nodeId, { silent: true }]);
            selUnit = temp.folder_id;
            $('#orgUnit').val(selUnit);//设置组织机构代码选项
        }
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysUser/GetUserData",
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
                    folder_id: (selUnit === null) ? "" : selUnit,//单位代码
                    userName: encodeURI($('#userName').val())//用户名，登录名
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
                if (row.user_id != null && row.user_id > 0) {
                    //启用
                    $('#btnActId').attr("disabled", false);
                    $('#btnUserGroup').attr("disabled", false);
                    $('#btnRole').attr("disabled", false); 
                    $('#btnProxy').attr("disabled", false);
                    $('#btnResetPassWord').attr("disabled", false);
                } else {
                    //禁用
                    $('#btnActId').attr("disabled", true);
                    $('#btnUserGroup').attr("disabled", true);
                    $('#btnRole').attr("disabled", true);
                    $('#btnProxy').attr("disabled", true);
                    $('#btnResetPassWord').attr("disabled", true);
                }
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        $('#user_type').initSelectpicker("UserType");
        //$.getJSON(ctx + '/SysUser/GetRoleData', function (data) {
        //    if (data.Statu === 1) {
        //        layer.msg(data.Msg, { icon: 5 });
        //        return false;
        //    }
        //    var itemStr;
        //    $.each(data.Data, function (i, item) {
        //        itemStr += "<option value='" + item.role_id + "'>" + item.role_name + "</option>";
        //    });
        //    $('#userRoles').append(itemStr);
        //    $('#userRoles').selectpicker('refresh');
        //});
    },
    //绑定默认事件
    bindEvent: function () {
        $('#login_name').blur(function () {
            if ($(this).val() == null)
                return false;
            //登录失去焦点事件
            $.ajax({
                type: 'post',
                url: ctx + 'SysUser/CheckUser',
                data: { "login_name": encodeURI($('#login_name').val()) },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data.Statu == 1) {
                        $('#login_name').focus();
                        layer.msg(data.msg, { icon: 5 });
                    }   
                }
            });
        });
        //激活
        $('#btnActId').click(function() {
            //激活
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            var index = layer.msg("激活中...", { icon: 6, time: 9000 });
            $.ajax({
                type: 'post',
                url: ctx + '/SysUser/ActUserById/' + row[0].id,
                dataType: 'json',
                async: false,
                success: function (data) {
                    layer.close(index);
                    if (data.Statu == 0) {
                        layer.msg(data.Msg);
                        $table.bootstrapTable('refresh');
                    }
                    else
                        layer.msg(data.Msg, { icon: 5 });
                }
            });
        });
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //详细
        $('#userDetail').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            //清空表单
            $('#orgFormTable').resetHideValidForm();
            layer.open({
                title: ['用户详细信息', 'font-size:18px;'],
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
        //添加
        $('#userAdd').click(function () {
            $('#markPass').val("");
            $('#markPass').css("display", "block");
            userManager.editForm();
        });
        //编辑
        $('#userEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row!=null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            $('#markPass').val("123");
            $('#markPass').css("display","none");
            userManager.editForm(row[0]);
        });
        //删除
        $('#userDel').click(function () {
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
                    url: ctx + '/SysUser/DelUserById/' + row[0].user_id,
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
        //分配用户组
        $('#btnUserGroup').click(function() {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            var $title = row[0].user_name + "：用户组设置";
            addOrEditUrl = ctx + "/SysUser/UserGroupList?user_id=" + row[0].user_id;//用户id
            //打开编辑
            var index = layer.open({
                id: 'ifreamSetUserGroup',
                title: [$title, 'font-size:18px;'],
                type: 2,
                content: addOrEditUrl,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['850px', '95%'],
                success: function (li, o) {
                },
                yes: function (li, o) {
                    var userData = document.getElementById("ifreamSetUserGroup").firstChild.contentWindow.userGroupManager.getUserGroupData();
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
        });
        //分配角色
        $('#btnRole').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            var $title = row[0].user_name + "：角色设置";
            addOrEditUrl = ctx + "/SysUser/UserRoleList?user_id=" + row[0].user_id;//用户id
            //打开编辑
            var index = layer.open({
                id: 'ifreamSetUserRole',
                title: [$title, 'font-size:18px;'],
                type: 2,
                content: addOrEditUrl,
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['850px', '95%'],
                success: function (li, o) {
                },
                yes: function (li, o) {
                    var roleData = document.getElementById("ifreamSetUserRole").firstChild.contentWindow.userRoleManager.getUserRoleData();
                    if (roleData == null)
                        return false;
                    $.ajax({
                        type: 'post',
                        url: ctx + '/SysUser/SaveUserRoleData/' + row[0].user_id,
                        data: { "roleData": roleData },
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
        });
        //代管配置
        $('#btnProxy').click(function() {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            var $title = row[0].user_name + "：代管配置";
            addOrEditUrl = ctx + "/SysUser/UserProxy?user_id=" + row[0].user_id;//用户id
            //打开编辑
            var index = layer.open({
                id: 'ifreamSetUserUnitData',
                title: [$title, 'font-size:18px;'],
                type: 2,
                content: addOrEditUrl,
                scroll: true,//是否显示滚动条、默认不显示 
                btn: ['确定', '取消'],
                area: ['451px', '500px'],
                success: function (li, o) {
                },
                yes: function (li, o) {
                    var unitData = document.getElementById("ifreamSetUserUnitData").firstChild.contentWindow.userzTree.getCheckedUnit();
                    if (unitData == null)
                        return false;
                    $.ajax({
                        type: 'post',
                        url: ctx + '/SysUser/SaveUserUnitData/' + row[0].user_id,
                        data: { "unitData": JSON.stringify(unitData) },
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
        });
        //重置密码
        $('#btnResetPassWord').click(function () {
            var rows = $table.bootstrapTable('getSelections');
            if (rows != null && rows.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            var row = rows[0];
            //清空表单
            $('#orgFormPassWord').resetHideValidForm();
            //打开编辑
            layer.open({
                title: [row.user_name+'：重置密码', 'font-size:18px;'],
                type: 1,
                content: $('#orgPassWord'),
                scroll: true,//是否显示滚动条、默认不显示
                btn: ['确定', '取消'],
                area: ['480px', '250px'],
                success: function (li, o) {
                    if (row != null) {
                        row.user_pwd = "";
                        $('#orgFormPassWord')[0].reset();//重置表单
                        $('#orgFormPassWord').LoadForm(row);//表单填充数据
                    }
                },
                yes: function (li, o) {
                    if ($('#orgFormPassWord').valid()) {
                        //验证通过
                        $('#orgFormPassWord').ajaxSubmit({
                            url: ctx + '/SysUser/ResetPassWord',
                            type: "post",
                            dataType: "json",
                            beforeSubmit: function (arr, $form, options) {
                                layer.msg("提交数据~", { icon: 1, time: 5000 });
                            },
                            success: function (result, status, xhr, $form) {
                                if (result.Statu === 0) {
                                    layer.closeAll();
                                    layer.msg('密码重置成功', { icon: 1 });
                                }
                                else
                                    layer.alert(result.Msg, { icon: 5 });
                            },
                            error: function (xhr, status, error, $form) {
                                layer.msg("密码重置失败~", { icon: 5 });
                            }
                        });
                    }
                },
                cancel: function (li, o) {

                }
            });
            //userManager.editForm(row[0]);
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加用户信息";
        if (row == null || row == "" || row == undefined)
            addOrEditUrl = ctx + "/SysUser/AddUser";//新增
        else {
            if (!selUnit)
                $('#userTypeSel').show();
            else
                $('#userTypeSel').hide();
            addOrEditUrl = ctx + "/SysUser/EditUser?user_id=" + row.user_id;//编辑
            $title = "编辑用户信息";
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type:1,
            content: $('#org_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row != null) {
                    row.user_pwd = "";
                    if (row.user_birth != null && row.user_birth.length >10) 
                        row.user_birth = $.formatDate(new Date(parseInt(row.user_birth.slice(6))));
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
                        data: { selUnit: selUnit },
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
            cancel: function(li,o) {
                
            }
        });
    }
}

//用户分配角色
var $userRoleTable, roleData = null;
var userRoleManager= {
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysUser/GetAllRoleData",
            cutHeight: 5,
            toolbar: "",
            singleSelect: false,
            pageSize: 100,
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1
                }
                return params;
            },
            columns: [
                 {
                     field: 'state',
                     title: '角色分配',
                     checkbox: true,
                     formatter: function (value, row, index) {
                         if (null != roleData) {
                             $.each(roleData, function (index, item) {
                                 if (row.role_id.toString() === item.role_id.toString()) {
                                     value = { checked: true };
                                     return false;
                                 }
                             });
                         }
                         return value;
                     }
                 }, {
                     field: '',//第一列序号
                     title: '序号',
                     align: 'center',
                     width: 50,
                     formatter: function (value, row, index) {
                         return index + 1;
                     }
                 }, {
                     field: 'role_name',
                     title: '角色名称',
                     align: 'center'
                 }, {
                     field: 'role_code',
                     title: '角色代码',
                     align: 'center'
                 }, {
                     field: 'creator_date',
                     title: '创建日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }, {
                     field: 'role_note',
                     title: '备注',
                     align: 'center'
                 }]
        }
        $userRoleTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    //保存时收集选中的角色集合
    getUserRoleData: function () {
        var data = "";
        var row = $userRoleTable.bootstrapTable('getSelections');
        if (row != null && row.length == 0)
            return data;
        $.each(row, function (index, value) {
            data += value.role_id + ',';
        });
        return data;
    },
    initRoleData: function () {
        var user_id = $.getUrls("user_id");
        if (user_id === null || user_id === undefined) {
            userRoleManager.initTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/SysUser/GetUserRoleData/" + user_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) 
                    roleData = data.Data;
                userRoleManager.initTable();
            }
        });
    }
}

//用户分配组
var $userGroupTable, userData = null;
var userGroupManager = {
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysUser/GetAllGroupData",
            cutHeight: 5,
            toolbar: "",
            singleSelect: false,
            pageSize: 100,
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1
                }
                return params;
            },
            columns: [
                 {
                     field: 'state',
                     title: '分配组',
                     checkbox: true,
                     formatter: function (value, row, index) {
                         if (null != userData) {
                             $.each(userData, function (index, item) {
                                 if (row.group_id.toString() === item.group_id.toString()) {
                                     value = { checked: true };
                                     return false;
                                 }
                             });
                         }
                         return value;
                     }
                 }, {
                     field: '',//第一列序号
                     title: '序号',
                     align: 'center',
                     width: 50,
                     formatter: function (value, row, index) {
                         return index + 1;
                     }
                 }, {
                     field: 'group_name',
                     title: '组名称',
                     align: 'center'
                 }, {
                     field: 'group_cname',
                     title: '组别名',
                     align: 'center'
                 }, {
                     field: 'creator_date',
                     title: '创建日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }, {
                     field: 'update_date',
                     title: '更新日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }, {
                     field: 'group_note',
                     title: '备注',
                     align: 'center'
                 }]
        }
        $userGroupTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    //保存时收集选中的角色集合
    getUserGroupData: function () {
        var data = "";
        var row = $userGroupTable.bootstrapTable('getSelections');
        if (row != null && row.length == 0)
            return data;
        $.each(row, function (index, value) {
            data += value.group_id + ',';
        });
        return data;
    },
    initUserGroupData: function () {
        var user_id = $.getUrls("user_id");
        if (user_id === null || user_id === undefined) {
            userGroupManager.initTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/SysUser/GetUserGroupData/" + user_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0)
                    userData = data.Data;
                userGroupManager.initTable();
            }
        });
    }
}

//用户--代管配置管理
var user_id,unitzTree;
var userzTree = {
    //初始化zTree
    initzTree: function () {
        user_id = $.getUrls("user_id");
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
                url: ctx + "/SysUser/GetProUnitTreeData/" + user_id,
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
                onAsyncError :function() {
                    layer.msg("初始化单位结构树失败~",{icon:5});
                },
                onClick: function (event, treeId, treeNode) {
                    layer.msg(treeNode.name);
                }
            }
        };
        unitzTree = $("#unitTree").initzTreeView(setting);
    },
    //获取所有被选中的数据
    getCheckedUnit: function () {
        var userData = "";
        var treeObj = unitzTree; 
        var nodes = treeObj.getCheckedNodes(true);
        if (nodes === null || nodes.length === 0)
            return userData;
        userData = [];
        $.each(nodes, function(index, item) {
            userData.push({ "user_id": user_id, "unit_id": item.id, "unit_pid": item.pId, "unit_name": item.name, "tree_type": item.tree_type });
        });
        return userData;
    }
}
