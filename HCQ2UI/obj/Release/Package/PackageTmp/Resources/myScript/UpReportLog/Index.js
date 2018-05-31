
$(function () {
    ExpendTable.InitExpend();
    Table.ProjectTable();

    $("#btnSearch").click(function () {
        $("#TableFromData").bootstrapTable("refresh");
    });

})

var Table = {
    ProjectTable: function () {
        $("#TableFromData").bootstrapTable({
            queryParams: function (params) {
                var pageIndex = params.offset / params.limit + 1;
                return {
                    page: pageIndex,
                    rows: params.limit,
                    UnitID: $("#JianDie").val(),
                    txtSearchName: $("#txtSearchName").val()
                };
            },
            columns: [
                    {
                        field: "Number",
                        title: "序号",
                        align: "center",
                        formatter: function (value, row, index) {
                            return index + 1;
                        }
                    }, {
                        field: "up_unit_id",
                        title: "项目名称",
                        align: "center",
                        formatter: function (value, row, index) {
                            if (value != null && value != "") {
                                return GetUnitName(value);
                            } else {
                                return "";
                            }
                        }
                    }, {
                        field: "up_name",
                        title: "上报类别",
                        align: "center"
                    }, {
                        field: "up_context",
                        title: "上报内容",
                        align: "center"
                    }, {
                        field: "up_date",
                        title: "上报日期",
                        align: "center",
                        formatter: function (value, row, index) {
                            return $.DateFormat(value);
                        }
                    }, {
                        field: "up_user_name",
                        title: "上报人",
                        align: "center",
                        formatter: function (value, row, index) {
                            return value;
                        }
                    }],
            url: $.GetIISName() + "/UpReportLog/GetLogSoure",
            toolbar: "#table-tool",
            pagination: true,
            height: $.GetBodyHeight()
        });
    }

}