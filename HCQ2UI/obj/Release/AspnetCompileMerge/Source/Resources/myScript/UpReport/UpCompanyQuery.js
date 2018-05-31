/**
 * BootstrapTable的默认设置参数
 */
//默认get
//$.fn.bootstrapTable.defaults.method='post'; 
////设置为 true 禁用 AJAX 数据缓存 默认：true
//$.fn.bootstrapTable.defaults.cache=false;
////设置为 true 会有隔行变色效果默认：false
//$.fn.bootstrapTable.defaults.striped=true;
////设置为 true 会在表格底部显示分页条/默认：false
//$.fn.bootstrapTable.defaults.pagination=true;
////设置为false 将禁止所有列的排序、默认：true
//$.fn.bootstrapTable.defaults.sortable=true;
////设置在哪里进行分页，可选值为 'client' 或者 'server'。设置 'server'时，必须设置 服务器数据地址（url）或者重写ajax方法、默认值：client
//$.fn.bootstrapTable.defaults.sidePagination='server';
//$.fn.bootstrapTable.defaults.paginationFirstText='首页';
//$.fn.bootstrapTable.defaults.paginationPreText='上一页';
//$.fn.bootstrapTable.defaults.paginationNextText='下一页';
//$.fn.bootstrapTable.defaults.paginationLastText='尾页';
////设置true 将在点击行时，自动选择rediobox 和 checkbox、默认值：false
//$.fn.bootstrapTable.defaults.clickToSelect=true;
////发送到服务器的数据编码类型/默认：application/json、使用默认的后台：request.getParameter获取不到
//$.fn.bootstrapTable.defaults.contentType = 'application/x-www-form-urlencoded';
////请求服务器数据时的参数
//$.fn.bootstrapTable.defaults.queryParams = function(params){
//    var pageIndex = params.offset / params.limit + 1
//    var temp = {
//        page : pageIndex, // 页码
//        rows : params.limit // 页面大小
//    };
//    return temp;
//}


$(function () {
    //$('#TableFromData').bootstrapTable({
    //    queryParams: function (params) {
    //        var pageIndex = params.offset / params.limit + 1
    //        return {
    //            page: 1,
    //            rows: 500,
    //            dwmc: $("#txtSearchName").val()
    //        };
    //    },
    //    columns: [
    //            {
    //                field: "Number",
    //                title: "序号",
    //                align: "center",
    //                formatter: function (value, row, index) {
    //                    return index + 1;
    //                }
    //            }, {
    //                field: "dwmc",
    //                title: "单位名称",
    //                align: "center"
    //            }, {
    //                field: "xmmc",
    //                title: "项目名称",
    //                align: "center"
    //            }, {
    //                field: "tyshxydm",
    //                title: "统一社会信用代码",
    //                align: "center"
    //            }, {
    //                field: "Fddbrxm",
    //                title: "法定代表人姓名",
    //                align: "center"
    //            }, {
    //                field: "LXR",
    //                title: "联系人",
    //                align: "center"
    //            }, {
    //                field: "LXDH",
    //                title: "联系人电话",
    //                align: "center"
    //            }
    //    ],
    //    url: $.GetIISName() + "/UpReport/GetUpCompanySoure",
    //    responseHandler: function (rec) {
    //        rec.rows = eval("("+rec.rows+")");

    //        return rec;
    //    }
    //});

    //首次加载table
    tableHelp.LoadData(TableFormAction.SetTable(), TableFormAction.queryParams, TableFormAction.responseHandler);
    
    $("#btnSearch").click(function () {
        tableHelp.refresh(TableFormAction.SetTable());
    });
})

var TableFormAction = {

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        return {
            page: 1,
            rows: 500,
            dwmc: $("#txtSearchName").val()
        };
    },

    SetTable: function () {
        return {
            Columns: [
                {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: "dwmc",
                    title: "单位名称",
                    align: "left"
                }, {
                    field: "xmmc",
                    title: "项目名称",
                    align: "left"
                }, {
                    field: "tyshxydm",
                    title: "统一社会信用代码",
                    align: "center"
                }, {
                    field: "fddbrxm",
                    title: "法定代表人姓名",
                    align: "center"
                }, {
                    field: "lxr",
                    title: "联系人",
                    align: "center"
                }, {
                    field: "lxdh",
                    title: "联系人电话",
                    align: "center"
                }, {
                    field: "sbsj",
                    title: "上报时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "") {
                            return FormDate(value);
                        } else {
                            return "";
                        }
                    }
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight(),
            url: $.GetIISName() + "/UpReport/GetUpCompanySoure",
            tool: $("#table-tool")
        };
    }
};

function FormDate(value) {
    //2017 09 07 11 12 24
    var text = value.substr(0, 4) + "-" + value.substr(4, 2) + "-" + value.substr(6, 2);
    text += " " + value.substr(8, 2) + ":" + value.substr(10, 2) + ":" + value.substr(12, 2)
    return text;
}