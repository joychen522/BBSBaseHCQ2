$(function () {

    //日期控件
    $("#txtSearchDate").datepicker({
        show: true,
        format: "yyyy-mm",
        language: "zh-CN",
        weekStart: 1,
        autoclose: true,
        orientation: "right",
        todayBtn: "linked"
    });

    //查询事件
    $("#btnSearch").click(function () {
        tableHelp.refresh(SetTableParam());
    });

    //参数列表
    function queryParams(params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            UnitID: $("#NodeUnitID").val(),
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    }
    
    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);
});

function SetTableParam() {
    //构建传入参数
    var table = {
        Columns: [
            {
                field: "Number",
                title: "序号",
                align: "center",
                width: 30,
                formatter: function (value, row, index) {
                    return index + 1
                }
            },
            {
                field: "WGJG0201",
                title: "发放时间",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != "" && value != null) {
                        return $.DateFormat(value);
                    } else {
                        return "";
                    }
                }
            },
            {
                field: "WGJG0202",
                title: "约定发放时间",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != "" && value != null) {
                        return $.DateFormat(value);
                    } else {
                        return "";
                    }
                }
            },
            {
                field: "A0101",
                title: "姓　名",
                align: "center",
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                },
                width: 60
            },
            {
                field: "A0177",
                title: "身份证号",
                align: "center"
            },
            {
                field: "E0386",
                title: "工种",
                align: "center"
            },
            {
                field: "WGJG0203",
                title: "工资发放方式",
                align: "center"
            },
            {
                field: "WGJG0204",
                title: "工资",
                align: "center"
            }
            ,
            {
                field: "WGJG0205",
                title: "打卡天数",
                align: "center"
            }
            ,
            {
                field: "WGJG0206",
                title: "实际工作天数",
                align: "center"
            }
            ,
            {
                field: "WGJG0207",
                title: "应发工资",
                align: "center"
            }
            ,
            {
                field: "WGJG0208",
                title: "实际发放",
                align: "center"
            }
            ,
            {
                field: "WGJG0211",
                title: "是否发放",
                align: "center"
            }

        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Attendance/GetWageDetail",
        tool: $("#wagedetail_tool")
    };
    return table;
}
