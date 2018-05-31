$(function () {
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
            rows: 20,
            unitId: $("#JianDie").val(),
            unitName: $("#JianDie1").val(),
            personName: $("#txtSearchName").val()
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
                    field: "xm",
                    title: "姓名",
                    align: "center"
                }, {
                    field: "sfzh",
                    title: "身份证号码",
                    align: "center"
                }, {
                    field: "kqsj",
                    title: "签到时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "") {
                            return FormDate(value);
                        } else {
                            return "";
                        }
                    }
                }, {
                    field: "sbbs",
                    title: "标识",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == "1") {
                            return "上班";
                        } else {
                            return "下班";
                        }
                    }
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
            url: $.GetIISName() + "/UpReport/GetUpCheckSoure",
            tool: $("#table-tool")
        };
    }
};

var Query = {

}

function FormDate(value) {
    //2017 09 07 11 12 24
    var text = value.substr(0, 4) + "-" + value.substr(4, 2) + "-" + value.substr(6, 2);
    text += " " + value.substr(8, 2) + ":" + value.substr(10, 2) + ":" + value.substr(12, 2)
    return text;
}