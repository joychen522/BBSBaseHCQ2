
$(function () {
    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);

    $("#btnSearch").click(function () {
        tableHelp.refresh(SetTableParam());
    });
});

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        project_name: $("#textName").val()
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
                align: "center"
            }, {
                field: "B0112",
                title: "地址",
                align: "center"
            }, {
                field: "B0180",
                title: "联系人",
                align: "center"
            }, {
                field: "B0181",
                title: "联系电话",
                align: "center"
            }, {
                field: "project_status",
                title: "状态",
                align: "center"
            }
        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Enterprise/GetProjectSoure",
        tool: "#WorkTimeTool"
    };
    return table;
}