
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
                field: 'ed_title',
                title: '标题',
                align: 'center'
            }, {
                field: 'ed_reason',
                title: '失信原因',
                align: 'center'
            }, {
                field: 'ed_create',
                title: '记录人',
                align: 'center'
            }, {
                field: 'ed_time',
                title: '记录时间',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value != null && value != "")
                        return $.DateFormat(value);
                    else
                        return "";
                }
            }, {
                field: 'ed_note',
                title: '备注',
                align: 'center'
            }
        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Company/GetMainCompaySoure",
        tool: "#WorkTimeTool"
    };
    return table;
}