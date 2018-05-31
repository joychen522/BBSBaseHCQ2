$(function () {
    SelectBox.TheTeam();
    Move.Load();
    Retire.Load();
    //首次加载table
    tableHelp.LoadData(table, queryParams);
})

//查询事件
$("#btnSearch").click(function () {
    tableHelp.refresh(table);
    SelectBox.TheTeam();
});

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1;
    var temp = {
        page: pageIndex,
        rows: params.limit,
        UnitID: $("#JianDie").val(),
        btnPersonType: $("#btnPersonType").val(),
        btnTeam: $("#btnTeam").val(),
        txtSearchName: $("#txtSearchName").val()
    };
    return temp;
}

//构建传入参数
var table = {
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
                    return value.replace(" 0:00:00", "");
                else
                    return value;
            }
        }, {
            field: "scan_contract",
            title: "劳动合同",
            align: "center",
            formatter: function (value, row, index) {
                if (value != null && value != "" && value != undefined) {
                    return "<a href='../Person/LookPri?url=" + encodeURI(value.replace("~", "..")) + "' target='_blank'>[查看]</a>";
                    ///return "<a href='" + value.replace("~", "..") + "' target='_blank'>[查看]</a>";
                }
                else
                    return "";
            }
        }, {
            field: "retire_time",
            title: "离职时间",
            align: "center",
            formatter: function (value, row, index) {
                if (value != null && value != "" && value != undefined) {
                    return value.replace(" 0:00:00", "");
                }
                else
                    return value;
            }
        }
    ],
    Contoller: "TableFromData",
    Height: window.innerHeight - 4,
    url: $.GetIISName() + "/Person/GetPersonNewinfor",
    tool: $("#table-tool")
};

$("#AddPerson").click(function () {
    var unit_id = $("#JianDie").val();
    if (unit_id != null && unit_id != "" && unit_id != undefined) {
        var index = layer.open({
            type: 2,
            title: "新增人员信息",
            content: $.GetIISName() + "/Person/NewPersonDetail?RowID=&UnitID=" + unit_id,
            area: ["100%", "100%"],
            maxmin: true,
            cancel: function () {
                tableHelp.refresh(table);
            }
        });
        layer.full(index);
    } else {
        layer.msg("请选择单位或者部门", { icon: 5 });
    }
});

$("#EditPerson").click(function () {
    var row = $("#TableFromData").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        var index = layer.open({
            type: 2,
            title: "编辑人员信息",
            content: $.GetIISName() + "/Person/NewPersonDetail?RowID=" + row[0].RowID + "&UnitID=" + row[0].UnitID,
            area: ["100%", "100%"],
            maxmin: true,
            cancel: function () {
                tableHelp.refresh(table);
            }
        });
        layer.full(index);
    } else {
        layer.msg("请选择需要编辑的人员", { icon: 5 });
    }
})

$("#DeletePerson").click(function () {
    layer.confirm("确定要删除该人员信息吗？", { btn: ["确定", "取消"] }, function () {
        var row = $("#TableFromData").bootstrapTable('getSelections');
        $.ajax({
            type: "post",
            dataType: "text",
            url: $.GetIISName() + "/Person/DeletePerson",
            data: { RowID: row[0].RowID },
            success: function (result) {
                if (result == "ok") {
                    layer.closeAll();
                    tableHelp.refresh(table);
                } else
                    layer.msg("删除失败", { icon: 5 });
            }
        });
    })
});

var operter = {
    AddPerson: function () {

    },
    EditPerson: function () {

    }
}

//新增成功，刷新一览
var SetFatherFuc = function () {
    tableHelp.refresh(table);
}

//人员调动和复制
var Move = {

    Load: function () {
        tableHelpMove.LoadData(Move.Table(), Move.QueryParams);
        $("#btnMove").click(function () {
            tableHelpMove.refresh(Move.Table());
            layer.open({
                type: 1,
                title: "人员移动、复制",
                content: $("#divMove"),
                area: ["700px", "400px"],
                btn: ["移动", "复制", "取消"],
                maxmin: true,
                btn1: function () {
                    var unit_id = $("#moveJianDie").val();
                    if (unit_id != null || unit_id != "") {
                        Move.ModeProject();
                    } else {
                        layer.msg("请选择目标项目！");
                    }
                },
                btn2: function () {
                    var unit_id = $("#moveJianDie").val();
                    if (unit_id != null || unit_id != "") {
                        Move.CpoyProject();
                    } else {
                        layer.msg("请选择目标项目！");
                    }
                }
            });
        });

    },

    ModeProject: function () {
        var new_id = $("#moveJianDie").val();
        var old_id = $("#JianDie").val();
        var row = $("#move_table").bootstrapTable('getSelections');
        if (row != null && row != "") {
            $.ajax({
                type: "post",
                dataType: "text",
                data: { id: new_id, rows: JSON.stringify(row) },
                url: $.GetIISName() + "/Person/MovePerson",
                success: function (result) {
                    tableHelpMove.refresh(Move.Table());
                    alert(result);
                    layer.closeAll();
                    tableHelp.refresh(table);
                }
            })
        } else
            layer.msg("请选择需要移动的人员！");
    },

    CpoyProject: function () {
        var new_id = $("#moveJianDie").val();
        var old_id = $("#JianDie").val();
        var row = $("#move_table").bootstrapTable('getSelections');
        if (row != null && row != "") {
            $.ajax({
                type: "post",
                dataType: "text",
                data: { id: new_id, rows: JSON.stringify(row) },
                url: $.GetIISName() + "/Person/CopyPerson",
                success: function (result) {
                    tableHelpMove.refresh(Move.Table())
                    alert(result);
                    layer.closeAll();
                    tableHelp.refresh(table);
                }
            })
        } else
            layer.msg("请选择需要复制的人员！");
    },

    Table: function () {
        var table = {
            Columns: [
                {
                    checkbox: true
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
                    field: "A0177",
                    title: "性别",
                    align: "center"
                }
            ],
            Contoller: "move_table",
            Height: 300,
            url: $.GetIISName() + "/Person/GetPersonNewinfor"
        };
        return table;
    },

    QueryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1;
        var temp = {
            page: 1,
            rows: 500,
            UnitID: $("#JianDie").val(),
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    }
}


var Retire = {
    Load: function () {
        tableHelpMove.LoadData(Retire.Table(), Retire.QueryParams);
        $("#btnRetire").click(function () {
            tableHelpMove.refresh(Retire.Table());
            layer.open({
                type: 1,
                title: "办理离职",
                content: $("#divRetire"),
                area: ["700px", "400px"],
                btn: ["确认办理", "取消"],
                maxmin: true,
                yes: function () {
                    var retire_time = $("#retire_time").val();
                    if (retire_time != null && retire_time != "") {
                        Retire.Save();
                    } else {
                        layer.msg("请录入离职日期！");
                    }
                },
                cance: function () {

                }
            });
        });
    },

    Save: function () {
        var row = $("#retire_table").bootstrapTable('getSelections');
        if (row != null && row != "") {
            $.ajax({
                type: "post",
                dataType: "text",
                data: { retire_time: $("#retire_time").val(), rows: JSON.stringify(row) },
                url: $.GetIISName() + "/Person/RetirePerson",
                success: function (result) {
                    alert(result);
                    layer.closeAll();
                    tableHelpMove.refresh(Retire.Table());
                    tableHelp.refresh(table);
                }
            })
        } else
            layer.msg("请选择需要办理离职的人员！");
    },

    Table: function () {
        var table = {
            Columns: [
                {
                    checkbox: true
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
                    field: "A0177",
                    title: "性别",
                    align: "center"
                }
            ],
            Contoller: "retire_table",
            Height: 300,
            url: $.GetIISName() + "/Person/HandleRetireSoure",
            tool: $("#retire_tool")
        };
        return table;
    },

    QueryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1;
        var temp = {
            page: 1,
            rows: 500,
            UnitID: $("#JianDie").val(),
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    }
}

var SelectBox = {
    TheTeam: function () {
        var checked = $("#btnTeam").val();
        $("#btnTeam").html("");
        $("#btnTeam").append("<option>所属队伍</option>");
        $.ajax({
            type: "post",
            dataType: "json",
            asunc: false,
            url: $.GetIISName() + "/Person/GetSelectTeamBox",
            data: { UnitID: $("#JianDie").val() },
            success: function (data) {
                if (data != null) {
                    for (var i = 0; i < data.length; i++) {
                        if (checked == data[i].com_id)
                            $("#btnTeam").append("<option selected='selected' value='" + data[i].com_id + "'>" + data[i].dwmc + "</option>");
                        else
                            $("#btnTeam").append("<option value='" + data[i].com_id + "'>" + data[i].dwmc + "</option>");
                    }
                }
            }
        })
    }

}