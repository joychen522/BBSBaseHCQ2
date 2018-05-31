/*******************************************************
 *  禁毒日志 相关操作js
 * <p>Title: baneLogManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2018年4月17日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl;
var baneLogManager = {
     //初始化页面
    initPage: function () {
         baneLogManager.initSelect();
         baneLogManager.initTable();
         baneLogManager.bindEvent();
    },
     //初始化Table
     initTable: function () {
         var options = {
             url: ctx + "/BaneLog/GetBaneLogData",
             cutHeight: 5,
             toolbar: "#exampleToolbar",
             queryParams: function (params) {
                 params = {
                     //页面大小  
                     rows: params.limit,
                     //第几页
                     page: params.offset / params.limit + 1,
                     log_title: $('#log_title').val(),//标题
                     user_id: $('#logUser').val(),//操作者id
                     log_date_start:$('#log_date_start').val(),//操作开始日期
                     log_date_end: $('#log_date_end').val()//操作结束日期
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
                      field: 'user_name',
                      title: '操作者',
                      align: 'center'
                  }, {
                      field: 'log_type',
                      title: '操作类别',
                      align: 'center'
                  }, {
                      field: 'log_title',
                      title: '标题',
                      align: 'center'
                  }, {
                      field: 'log_date',
                      title: '操作日期',
                      align: 'center',
                      formatter: function (value, row, index) {
                          if (value != null && value != "" && value != undefined)
                              return $.formatDate(new Date(parseInt(value.slice(6))));
                          else
                              return "";
                      }
                  }, {
                      field: 'log_ip',
                      title: '操作者IP',
                      align: 'center'
                  }],
             onClickRow: function (row, $element) {
             }
         }
         $table = tableHelper.initTable("exampleTableToolbar", options);
     },
     //初始化下拉
     initSelect: function () {
         $.ajax({
             url: ctx + '/BaneLog/GetUserDict',
             type: "post",
             async: false,
             dataType: 'json',
             success: function (data) {
                 if (data.Statu === 1) {
                     layer.msg(data.Msg, { icon: 5 });
                     return false;
                 }
                 var itemStr;
                 $.each(data.Data, function (i, item) {
                     itemStr += "<option value='" + item.user_id + "'>" + item.user_name + "</option>";
                 });
                 $('#logUser').append(itemStr);
                 $('#logUser').selectpicker('refresh');
             },
             error: function () {
                 layer.msg('数据异常~', { icon: 5 });
             }
         });
     },
     //绑定事件
     bindEvent: function () {
         //查询
         $('#btnSearch').click(function () {
             $table.bootstrapTable('refresh');
         });
         //详细
         $('#baneDetail').click(function () {
             var row = $table.bootstrapTable('getSelections');
             if (row != null && row.length == 0) {
                 layer.msg("未选中行~", { icon: 5 });
                 return false;
             }
             row = row[0];
             //清空表单
             $('#baneFormTable').resetHideValidForm();
             layer.open({
                 title: ['日志详细信息', 'font-size:18px;'],
                 type: 1,
                 content: $('#baneForm'),
                 scroll: true,//是否显示滚动条、默认不显示
                 btn: ['确定', '取消'],
                 area: ['480px', '500px'],
                 success: function (li, o) {
                     if (row != null) {
                         if (row.log_date)//$.formatDate(new Date(parseInt(value.slice(6))));
                             row.log_date = $.formatDate(new Date(parseInt(row.log_date.slice(6))));
                         $('#baneFormTable')[0].reset();//重置表单
                         $('#baneFormTable').LoadForm(row);//表单填充数据
                     }
                 }
             });
         });
     }
 }