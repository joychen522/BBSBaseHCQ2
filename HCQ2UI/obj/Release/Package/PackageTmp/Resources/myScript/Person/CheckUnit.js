
$(function () {
    var date = new Date();
    //日期控件
    $("#txtSearchDate").datepicker({
        show: true,
        format: "yyyy-mm-dd",
        language: "zh-CN",
        weekStart: 1,
        autoclose: true,
        orientation: "right",
        todayBtn: "linked"
    }).val(date.getFullYear() + "-" + (parseInt(date.getMonth()) + 1) + "-" + (parseInt(date.getDate())));

    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);

    $("#btnSearch").click(function () {
        var checkTime = $("#txtSearchDate").val();
        if (checkTime != "" && checkTime != null)
            tableHelp.refresh(SetTableParam());
        else
            layer.msg("签到日期不能为空！");
    });

});

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        project_name: $("#textName").val(),
        select_date: $("#txtSearchDate").val()
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
                field: "UnitName",
                title: "项目名称",
                align: "left"
            }, {
                field: "CheckWorkers",
                title: "出工人数",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "")
                        return "<a href='#' onclick='ClickCount(\"" + row.UnitID + "\")'>" + value + "</a>";
                    else
                        return 0;
                }
            }, {
                field: "totalWorkers",
                title: "总人数",
                align: "center"
            }, {
                field: "CheckPepe",
                title: "出工率",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "")
                        return value + "%";
                    else
                        return 0;
                }
            }
        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Person/GetMainCheckUnitSoure",
        tool: "#WorkTimeTool"
    };
    return table;
}

var ClickCount = function (UnitID) {
    var checkDate = $("#txtSearchDate").val();
    if (checkDate != "" && checkDate != null) {
        layer.open({
            type: 2,
            title: false,
            content: $.GetIISName() + "/Person/MainCheckUnitPerson?UnitID=" + UnitID + "&checkdate=" + checkDate,
            area: ["80%", "80%"],
            maxmin: false
        });
    }
    else
        layer.msg("签到日期不能为空！");

}