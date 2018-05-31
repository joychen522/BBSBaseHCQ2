/*******************************************************
 *  用户组管理 相关操作js
 * <p>Title: userGroupManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2016年12月28日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl;
 var userGroupManager= {
     //初始化Table
     initTable: function () {
         var options = {
             url: ctx + "/SysUserGroup/GetUserGroupData",
             cutHeight: 5,
             toolbar: "#exampleToolbar",
             queryParams: function (params) {
                 params = {
                     //页面大小  
                     rows: params.limit,
                     //第几页
                     page: params.offset / params.limit + 1,
                     sm_code: $('#smCode').val(),//所属模块
                     group_name: encodeURI($('#groupName').val())//用户组名称
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
                  }],
             onClickRow: function (row, $element) {
                 if (row.group_id != null && row.group_id > 0) {
                     //启用
                     $('#btnUser').attr("disabled", false);
                     $('#btnRole').attr("disabled", false);
                 } else {
                     //禁用
                     $('#btnUser').attr("disabled", true);
                     $('#btnRole').attr("disabled", true);
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
         $('#userGroupDetail').click(function () {
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
                 area: ['480px', '400px'],
                 success: function (li, o) {
                     if (row != null) {
                         $('#groupFormTable')[0].reset();//重置表单
                         $('#groupFormTable').LoadForm(row[0]);//表单填充数据
                     }
                 }
             });
         });
         //添加
         $('#userGroupAdd').click(function () {
             userGroupManager.editForm();
         });
         //编辑
         $('#userGroupEdit').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             userGroupManager.editForm(row[0]);
         });
         //删除
         $('#userGroupDel').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中记录行~", { icon: 5 });
                 return false;
             }
             layer.confirm('您确定要删除当前选中组吗？', {
                 btn: ['确定', '取消'] //按钮
             }, function () {
                 var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                 $.ajax({
                     type: 'post',
                     url: ctx + '/SysUserGroup/DelGroupById/' + row[0].group_id,
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
         //授权用户
         $('#btnUser').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             var $title = row[0].group_name + "：配置用户一览";
             addOrEditUrl = ctx + "/SysUserGroup/UserAndGroupList?group_id=" + row[0].group_id;
             //打开编辑
             var index = layer.open({
                 id: 'ifreamUserGroup',
                 title: [$title, 'font-size:18px;'],
                 type: 2,
                 content: addOrEditUrl,
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['900px', '95%'],
                 success: function (li, o) {
                 },
                 yes: function (li, o) {
                     layer.close(index);
                 },
                 cancel: function (li, o) {
                 }
             });
         });
         //授权角色
         $('#btnRole').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             var $title = row[0].group_name + "：配置角色一览";
             addOrEditUrl = ctx + "/SysUserGroup/RoleAndGroupList?group_id=" + row[0].group_id;
             //打开编辑
             var index = layer.open({
                 id: 'ifreamRoleGroup',
                 title: [$title, 'font-size:18px;'],
                 type: 2,
                 content: addOrEditUrl,
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['900px', '95%'],
                 success: function (li, o) {
                 },
                 yes: function (li, o) {
                     layer.close(index);
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
         var $title = "添加组信息";
         addOrEditUrl = ctx + "/SysUserGroup/AddGroup";//新增
         if (row != null && row != "" && row != undefined) {
             $title = "编辑组信息";
             addOrEditUrl = ctx + "/SysUserGroup/EditGroup?group_id=" + row.group_id;//编辑
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
             area: ['450px', '400px'],
             success: function (li, o) {
                 if (row != null) {
                     $('#groupFormTable')[0].reset();//重置表单
                     $('#groupFormTable').LoadForm(row);//表单填充数据
                 }
             },
             yes: function (li, o) {
                 userGroupManager.submitForm(addOrEditUrl);
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

//用户组-用户管理
var $userTable;
var userAndGroupManager = {
    //初始化Table
    initTable:function() {
        var options = {
            url: ctx + "/SysUserGroup/GetUserAndGroupData",
            cutHeight: 5,
            pageSize:100,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    userName: encodeURI($('#userName').val())//用户组名称
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
                 }, {
                     field: 'user_name',
                     title: '用户名',
                     align: 'center'
                 }, {
                     field: 'user_sex',
                     title: '性别',
                     align: 'center'
                 }, {
                     field: 'user_qq',
                     title: 'QQ',
                     align: 'center'
                 }, {
                     field: 'user_email',
                     title: '电子邮件',
                     align: 'center'
                 }, {
                     field: 'user_birth',
                     title: '生日',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }]
        }
        $userTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定事件
    initEvent:function() {
        $('#btnSearch').click(function() {
            $userTable.bootstrapTable("refresh");
        });
    }
}

//用户组-角色管理
var $roleTable;
var roleAndGroupManager= {
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysUserGroup/GetRoleAndGroupData",
            cutHeight: 5,
            pageSize: 100,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    roleName: encodeURI($('#roleName').val())//用户组名称
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
                     title: '创建时间',
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
        $roleTable = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定事件
    initEvent: function () {
        $('#btnSearch').click(function () {
            $roleTable.bootstrapTable("refresh");
        });
    }
}