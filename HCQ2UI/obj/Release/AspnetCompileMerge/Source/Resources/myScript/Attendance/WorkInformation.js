$(function () {
    //日期控件
    $("#txtSearchDate").datepicker({
        show: true,
        format: "yyyy-mm-dd",
        language: "zh-CN",
        weekStart: 1,
        autoclose: true,
        orientation:"right",
        todayBtn:"linked"
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
            txtSearchName:$("#txtSearchName").val(),
            txtSearchDate:$("#txtSearchDate").val()
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
                formatter: function (value, row, index) {
                    return index + 1
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
                field: "B0001",
                title: "所属项目",
                align: "center"
            },
            {
                field: "B0002",
                title: "用工单位",
                align: "center"
            },
            {
                field: "E0386",
                title: "工　种",
                align: "center"
            },
            {
                field: "WGJGFather",
                title: "上期发薪时间",
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
                title: "预计发薪时间",
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
                field: "WGJG0206",
                title: "工作天数",
                align: "center"
            }
        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Attendance/GetWorkInfor",
        tool: $("#workinformation_tool")
    };
    return table;
}
