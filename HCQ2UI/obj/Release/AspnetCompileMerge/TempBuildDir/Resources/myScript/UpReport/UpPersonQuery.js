$(function () {
    tableHelp.LoadData(TableFormAction.SetTable(), TableFormAction.queryParams);
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
                    field: "sfzhm",
                    title: "身份证号码",
                    align: "center"
                }, {
                    field: "xb",
                    title: "性别",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == 1)
                            return "男";
                        else if (value == 2)
                            return "女";
                        else
                            return "";
                    }
                }, {
                    field: "gzgzhdfs",
                    title: "工种工资核定方式",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == 1)
                            return "按天";
                        else if (value == 2)
                            return "按小时";
                        else if (value == 3)
                            return "按月";
                        else if (value == 4)
                            return "按件";
                        else
                            return "";
                    }
                }
                , {
                    field: "gzgzhdbz",
                    title: "工种工资核定标准",
                    align: "center"
                }, {
                    field: "ssdwzw",
                    title: "所属单位职务",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == 1)
                            return "管理人员";
                        else if (value == 2)
                            return "务工人员";
                        else if (value == 3)
                            return "劳资专管员";
                    }
                }, {
                    field: "ryid",
                    title: "附件",
                    align: "center",
                    formatter: function (value, row, index) {
                        var text = "<a href='#' onclick='Query.Photo(\"" + value + "\")'>[照片]</a>";
                        text += "　<a href='#' onclick='Query.Hetong(\"" + value + "\")'>[劳务合同]</a>";
                        return text;
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
            url: $.GetIISName() + "/UpReport/GetUpPersonQuerySoure",
            tool: $("#table-tool")
        };
    }
};

var Query = {

    Photo: function (RowID) {
        $.ajax({
            type: "post",
            dataType: "text",
            url: $.GetIISName() + "/UpReport/GetPersonPhoto",
            data: { RowID: RowID },
            success: function (result) {
                if (result != "" && result != null) {
                    $("#imPhoto").attr("src", "data:image/jpeg;base64," + result);//显示图片
                    layer.open({
                        type: 1,
                        title: "查看照片",
                        content: $("#divPhoto"),
                        area: ["700px", "400px"],
                        maxmin: true,
                        cancel: function () {

                        }
                    });
                }
            }
        });
    },

    Hetong: function (RowID) {
        $.ajax({
            type: "post",
            dataType: "text",
            url: $.GetIISName() + "/UpReport/GetPersonHetong",
            data: { RowID: RowID },
            success: function (result) {
                if (result != "" && result != null) {
                    $("#imHetong").attr("src", "data:image/jpeg;base64," + result);//显示图片
                    layer.open({
                        type: 1,
                        title: "查看合同",
                        content: $("#divHetong"),
                        area: ["700px", "400px"],
                        maxmin: true,
                        cancel: function () {

                        }
                    });
                }
            }
        });
    }

}

function FormDate(value) {
    //2017 09 07 11 12 24
    var text = value.substr(0, 4) + "-" + value.substr(4, 2) + "-" + value.substr(6, 2);
    text += " " + value.substr(8, 2) + ":" + value.substr(10, 2) + ":" + value.substr(12, 2)
    return text;
}