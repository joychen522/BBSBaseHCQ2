
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
                field: "B0001Name",
                title: "项目名称",
                align: "center"
            }, {
                field: "B0116",
                title: "保障金",
                align: "center"
            }, {
                field: "QXTJ01",
                title: "欠薪金额(万元)",
                align: "center"
            }, {
                field: "People2",
                title: "总人数",
                align: "center"
            }, {
                field: "People",
                title: "欠薪人数",
                align: "center"
            }
        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Enterprise/GetQXTJSoure",
        tool: "#WorkTimeTool"
    };
    return table;
}