
$(function () {
    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);
});

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        UnitID: $("#unit_id").val(),
        PersonName: $("#textName").val()
    };
    return temp;
}

function SetTableParam() {
    //构建传入参数
    var table = {
        Columns: [
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1
                }
            }, {
                field: "A0201",
                title: "签到时间",
                align: "center",
                formatter: function (value) {
                    if (value != null && value != "" && value != undefined)
                        return $.DateFormatTime(value,"yyyy-MM-dd hh:mm:ss");
                    else
                        return value;
                }
            }, {
                field: "A0101",
                title: "姓　名",
                align: "center",
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                }
            }, {
                field: "B0001Name",
                title: "所属项目",
                align: "left"
            }, {
                field: "E0386",
                title: "工种",
                align: "center"
            }, {
                field: "C0104",
                title: "移动电话",
                align: "center"
            }, {
                field: "A0177",
                title: "身份证号",
                align: "center"
            }, {
                field: "A0202",
                title: "标识",
                align: "center",
                formatter: function (value, row, index) {
                    if (value == "1")
                        return "进场";
                    else if (value == "0")
                        return "出场";
                    else (value == "")
                    return "";
                }
            }

        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Person/GetCheckWorkDaySoure",
        tool: "#WorkTimeTool"
    };
    return table;
}