$(function () {
    PersonIndex.Load();
})

var PersonIndex = {

    Load: function () {
        //首次加载table
        tableHelp.LoadData(PersonIndex.Table(), PersonIndex.QueryParam);
    },

    Table: function () {
        return table = {
            Columns: [
                {
                    radio: true
                }, {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: "A0101",
                    title: "姓　名",
                    align: "center",
                    formatter: function (value, row, index) {
                        return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                    }
                }, {
                    field: "A0107",
                    title: "性别",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == "1")
                            return "男";
                        else if (value == "2")
                            return "女";
                    }
                }, {
                    field: "C0101",
                    title: "年龄",
                    align: "center"
                }, {
                    field: "A0177",
                    title: "身份证号",
                    align: "center"
                }, {
                    field: "A0114",
                    title: "籍贯",
                    align: "center"
                }, {
                    field: "E03861",
                    title: "工种",
                    align: "center"
                }, {
                    field: "A0178",
                    title: "工资",
                    align: "center"
                }, {
                    field: "C0104",
                    title: "移动电话",
                    align: "center"
                }, {
                    field: "E0359",
                    title: "入职时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "" && value != undefined)
                            return value.replace("0:00:00", "");
                        else
                            return value;
                    }
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight(),
            url: $.GetIISName() + "/Person/GetTablePersonInfo",
            tool: "#Index-Tool"
        };
    },

    QueryParam: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            UnitID: $("#NodeUnitID").val(),
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    }
}

