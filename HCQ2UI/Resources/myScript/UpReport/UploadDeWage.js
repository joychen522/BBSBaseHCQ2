$(function () {
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
            xmmc: $("#txtSearchName").val()
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
                    field: "qxzje",
                    title: "欠薪总金额(万元)",
                    align: "center"
                }, {
                    field: "qxzrs",
                    title: "欠薪总人数",
                    align: "center"
                }, {
                    field: "qyfzrxm",
                    title: "企业负责人姓名",
                    align: "center"
                }, {
                    field: "lxdh",
                    title: "联系电话",
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
            url: $.GetIISName() + "/UpReport/GetUploadDeWageSoure",
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