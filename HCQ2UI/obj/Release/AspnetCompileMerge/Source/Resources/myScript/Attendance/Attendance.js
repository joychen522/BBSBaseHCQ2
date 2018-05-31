$(function () {

    //日期控件--只显示年月
    $("#txtSearchDate").datepicker({
        language: "zh-CN",
        todayHighlight: true,
        format: 'yyyy-mm',
        autoclose: true,
        startView: 'months',
        maxViewMode:'years',
        minViewMode:'months'
    });

    //查询事件
    $("#btnSearch").click(function(){
        tableHelp.refresh(SetTableParam());
    });

    //参数列表
    function queryParams(params){
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            UnitID:$("#NodeUnitID").val(),
            txtSearchDate:$("#txtSearchDate").val(),
            txtSearchName:$("#txtSearchName").val()
        };
        return temp;
    }

    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);
});

//构建传入参数
function SetTableParam() {
    var table = {
        Columns: [
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1;
                }
            },
            {
                field: "A0101",
                title: "姓　名",
                align: "center",
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                }
            },
            {
                field: "A0177",
                title: "身份证号",
                align: "center"
            },
            {
                field: "GZ",
                title: "工种",
                align: "center"
            },
            {
                field: "XM",
                title: "所属项目",
                align: "center"
            },
            {
                field: "DW",
                title: "用工单位",
                align: "center"
            },
            {
                field: "YF",
                title: "考勤月份",
                align: "center"
            },
            {
                field: "TS",
                title: "考勤天数",
                align: "center"
            }

        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Attendance/GetAttendinfo",
        tool: $("#attendance_tool")
    };
    return table;
}