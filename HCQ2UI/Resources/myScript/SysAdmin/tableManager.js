/*******************************************************
 *  表结构管理 相关操作js
 * <p>Title: tableManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年1月28日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var table_name='';
var tableManager = {
    //初始化页面
    initPage:function(){
        //初始化菜单
        parent.showLoadBoxs();
        tableManager.initTableStructTree();
        tableManager.initTable();
        parent.delLoadBoxs();
    },
    //初始化表结构树
    initTableStructTree: function () {
        /// <summary>Table初始化菜单列表</summary>  
        $.ajax({
            url: ctx + '/SysTableStruct/GetStrcutData',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu == 0)
                    tableManager.bindTree(mess.Data);
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
        $("#tree").empty();//清空
        $("#tree").initB01TreeView({
            data: result,
            onNodeSelected: function (e, o) {
                if (o.table_name != null && o.table_name != "null" && o.table_name != "") {
                    table_name = o.table_name;
                    $table.bootstrapTable('refresh');
                }
            }
        });
    },
    initTable: function () {
        var options = {
            url: ctx + "/SysTableStruct/InitTableData",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    fieldName: encodeURI($('#fieldName').val()),
                    table_name: table_name//菜单ID
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
                     field: 'table_name',
                     title: '表名称',
                     align: 'center'
                 }, {
                     field: 'field_name',
                     title: '字段名称',
                     align: 'center'
                 }, {
                     field: 'field_cname',
                     title: '字段中文名',
                     align: 'center'
                 }, {
                     field: 'data_type',
                     title: '数据类型',
                     align: 'center'
                 }, {
                     field: 'not_null',
                     title: '是否必填',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value)
                             return "是";
                        return "否";
                     }
                 }, {
                     field: 'field_len',
                     title: '字段长度',
                     align: 'center'
                 }, {
                     field: 'create_time',
                     title: '创建日期',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined)
                             return $.formatDate(new Date(parseInt(value.slice(6))));
                         else
                             return "";
                     }
                 }, {
                     field: 'field_note',
                     title: '备注',
                     align: 'center'
                 }]
        }
        $table = tableHelper.initTable("TableToolbar", options);
    }
}