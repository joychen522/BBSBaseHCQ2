
$(function () {
    //首次加载table
    tableHelp.LoadData(Table.SetTable(), Table.queryParams);

    $("#btnSearch").click(function () {
        tableHelp.refresh(Table.SetTable());
    });
})

var Table = {

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            txtSearchName: $("#txtSearchName").val(),
            com_id: $("#JianDie").val()
        };
        return temp;
    },

    SetTable: function () {
        var table = {
            Columns: [
        {
            field: "Number",
            title: "序号",
            align: "center",
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
            field: "UnitID",
            title: "编码",
            align: "center"
        }, {
            field: "UnitName",
            title: "名称",
            align: "left"
        }, {
            field: "GCLB",
            title: "工程类别",
            align: "center"
        }, {
            field: "B0114",
            title: "项目规模(万元)",
            align: "center"
        }, {
            field: "B0108",
            title: "业主单位",
            align: "center"
        }],
            Contoller: "TableFromData",
            Height: window.innerHeight - 2,
            url: $.GetIISName() + "/CompProInfo/GetCompanyPojectSoure",
            tool: $("#table-tool")
        };
        return table;
    }
};