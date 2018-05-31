/*******************************************************
 *  角色管理 相关操作js
 * <p>Title: roleManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年1月10日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//角色管理
var $table, addOrEditUrl;
 var roleManager= {
     //初始化Table
     initTable: function () {
         var options = {
             url: ctx + "/SysRole/GetRoleData",
             cutHeight: 5,
             toolbar: "#exampleToolbar",
             queryParams: function (params) {
                 params = {
                     //页面大小  
                     rows: params.limit,
                     //第几页
                     page: params.offset / params.limit + 1,
                     sm_code: $('#smCode').val(),//所属模块
                     role_name: encodeURI($('#roleName').val())//角色名称
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
                      field: 'role_name',
                      title: '角色名',
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
                  }],
             onClickRow: function (row, $element) {
                 if (row.role_id != null && row.role_id > 0) {
                     //启用
                     $('#btnLimit').attr("disabled", false);
                     $('#btnGroup').attr("disabled", false);
                 } else {
                     //禁用
                     $('#btnLimit').attr("disabled", true);
                     $('#btnGroup').attr("disabled", true);
                 }
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
         $('#roleDetail').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             //清空表单
             $('#roleFormTable').resetHideValidForm();
             layer.open({
                 title: ['角色详细信息', 'font-size:18px;'],
                 type: 1,
                 content: $('#roleForm'),
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['480px', '98%'],
                 success: function (li, o) {
                     if (row != null) {
                         $('#roleFormTable')[0].reset();//重置表单
                         $('#roleFormTable').LoadForm(row[0]);//表单填充数据
                     }
                 }
             });
         });
         //添加
         $('#roleAdd').click(function () {
             roleManager.editForm();
         });
         //编辑
         $('#roleEdit').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             roleManager.editForm(row[0]);
         });
         //删除
         $('#roleDel').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中记录行~", { icon: 5 });
                 return false;
             }
             layer.confirm('您确定要删除当前选中角色？', {
                 btn: ['确定', '取消'] //按钮
             }, function () {
                 var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                 $.ajax({
                     type: 'post',
                     url: ctx + '/SysRole/DelRoleById/' + row[0].role_id,
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
         //权限分配
         $('#btnLimit').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             var $title = row[0].role_name + "：权限设置";
             addOrEditUrl = ctx + "/SysRole/RoleLimitList?role_id=" + row[0].role_id;//角色id
             //打开编辑
             var index = layer.open({
                 id: 'ifreamSetRoleLimit',
                 title: [$title, 'font-size:18px;'],
                 type: 2,
                 content: addOrEditUrl,
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['850px', '95%'],
                 success: function (li, o) {
                 },
                 yes: function (li, o) {
                     var roleData = document.getElementById("ifreamSetRoleLimit").firstChild.contentWindow.roleLimitManager.getRoleLimitData();
                     $.ajax({
                         type: 'post',
                         url: ctx + '/SysRole/SaveRoleLimitData/' + row[0].role_id,
                         data: { "roleData": roleData },
                         dataType: 'json',
                         async: false,
                         success: function (data) {
                             if (data.Statu == 0) {
                                 layer.msg(data.Msg, { icon: 1 });
                                 layer.close(index);
                             }
                             if (data.Statu == 1)
                                 layer.msg(data.Msg, { icon: 5 });
                         }
                     });

                 },
                 cancel: function (li, o) {

                 }
             });
         });
         //角色-组分配
         $('#btnGroup').click(function() {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             var $title = row[0].role_name + "：组设置";
             addOrEditUrl = ctx + "/SysRole/RoleGroupList?role_id=" + row[0].role_id;//角色id
             //打开编辑
             var index = layer.open({
                 id: 'ifreamSetRoleGroup',
                 title: [$title, 'font-size:18px;'],
                 type: 2,
                 content: addOrEditUrl,
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['850px', '95%'],
                 success: function (li, o) {
                 },
                 yes: function (li, o) {
                     var groupData = document.getElementById("ifreamSetRoleGroup").firstChild.contentWindow.roleGroupManager.getRoleGroupData();
                     $.ajax({
                         type: 'post',
                         url: ctx + '/SysRole/SaveRoleGroupData/' + row[0].role_id,
                         data: { "groupData": groupData },
                         dataType: 'json',
                         async: false,
                         success: function (data) {
                             if (data.Statu == 0) {
                                 layer.msg(data.Msg, { icon: 1 });
                                 layer.close(index);
                             }
                             if (data.Statu == 1)
                                 layer.msg(data.Msg, { icon: 5 });
                         }
                     });

                 },
                 cancel: function (li, o) {

                 }
             });
         });
     },
     //初始化下拉
     initSelect: function () {
         $('#sm_code').initModuleSelectpicker();
         $('#smCode').initModuleSelectpicker();
     },
     //编辑、添加
     editForm: function (row) {
         var $title = "编辑角色信息";
         if (row == null || row == "" || row == undefined) {
             $title = "添加角色信息";
             addOrEditUrl = ctx + "/SysRole/AddRoleByObj";//新增
         }
         else
             addOrEditUrl = ctx + "/SysRole/EditRoleByObj/" + row.role_id;//编辑
         //清空表单
         $('#roleFormTable').resetHideValidForm();
         //打开编辑
         layer.open({
             title: [$title, 'font-size:18px;'],
             type: 1,
             content: $('#roleForm'),
             scroll: true,//是否显示滚动条、默认不显示
             btn: ['确定', '取消'],
             area: ['450px', '80%'],
             success: function (li, o) {
                 if (row != null) {
                     $('#roleFormTable')[0].reset();//重置表单
                     $('#roleFormTable').LoadForm(row);//表单填充数据
                 }
             },
             yes: function (li, o) {
                 //登录失去焦点事件：新增，编辑时改变代码都需要验证
                 roleManager.submitForm(addOrEditUrl);
             },
             cancel: function (li, o) {

             }
         });

     },
     submitForm: function (addOrEditUrl) {
         var sm_code = $('#sm_code').val();
         if (!sm_code) {
             layer.msg("请选择所属模块！", { icon: 5 });
             return false;
         }
         if ($('#roleFormTable').valid()) {
             //验证通过
             $('#roleFormTable').ajaxSubmit({
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

//角色--权限设置
var $roleTable,roleData=null;
var roleLimitManager= {
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysLimit/GetAllLimitData",
            cutHeight: 5,
            toolbar: "",
            singleSelect:false,
            pageSize:100,
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
                     field:'state',
                     title: '权限分配',
                     checkbox: true,
                     formatter: function (value, row, index) {
                         if (null != roleData) {
                             $.each(roleData, function(index, item) {
                                 if (row.per_id.toString() === item.per_id.toString()) {
                                     value = {checked: true};
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
                     field: 'per_name',
                     title: '权限名称',
                     align: 'center'
                 }, {
                     field: 'per_code',
                     title: '权限代码',
                     align: 'center'
                 }, {
                     field: 'per_type',
                     title: '权限类别',
                     align: 'center'
                 },{
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
                     field: 'per_note',
                     title: '备注',
                     align: 'center'
            }]
        }
        $roleTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    getRoleLimitData: function () {
        var data = "";
        var row = $roleTable.bootstrapTable('getSelections');
        if (row != null && row.length == 0)
            return data;
        $.each(row, function(index, value) {
            data += value.per_id + ',';
        });
        return data;
    },
    initRoleData:function() {
        var role_id = $.getUrls("role_id");
        if (role_id === null || role_id === undefined) {
            roleLimitManager.initTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/SysRole/GetRoleLimitData/" + role_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    roleData = data.Data;
                    roleLimitManager.initTable();
                } else {
                    layer.msg(data.Msg, { icon: 5 });
                }
            }
        });
    }
}

//角色--组设置
var $groupTable, groupData = null;
var roleGroupManager = {
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysRole/GetAllGroupData",
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
                         if (null != groupData) {
                             $.each(groupData, function (index, item) {
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
        $groupTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    getRoleGroupData: function () {
        var data = "";
        var row = $groupTable.bootstrapTable('getSelections');
        if (row != null && row.length == 0)
            return data;
        $.each(row, function (index, value) {
            data += value.group_id + ',';
        });
        return data;
    },
    initRoleGroupData: function () {
        var role_id = $.getUrls("role_id");
        if (role_id === null || role_id === undefined) {
            roleGroupManager.initTable();
            return false;
        }
        $.ajax({
            type: "post",
            url: ctx + "/SysRole/GetRoleGroupData/" + role_id,
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    groupData = data.Data;
                    roleGroupManager.initTable();
                } else {
                    layer.msg(data.Msg, { icon: 5 });
                }
            }
        });
    }
}