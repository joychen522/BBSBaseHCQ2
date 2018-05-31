$(function () {
    var date = new Date();
    //日期控件
    $("#txtSearchDate").datepicker({
        show: true,
        format: "yyyy-mm-dd",
        language: "zh-CN",
        weekStart: 1,
        autoclose: true,
        orientation: "right",
        todayBtn: "linked"
    }).val(date.getFullYear() + "-" + (parseInt(date.getMonth()) + 1) + "-" + (parseInt(date.getDate())));

    $("#treeview").height($.GetBodyHeight());

    //查询事件
    $("#btnSearch").click(function () {
        var isNull = $("#isNull").val();
        var txtSearchDate = $("#txtSearchDate").val();
        if (isNull != "" && isNull != null && isNull != undefined) {
            if (txtSearchDate == "" || txtSearchDate == null || txtSearchDate == undefined) {
                layer.msg("请选择签到日期");
                return;
            }
        }
        tableHelp.refresh(SetTableParam());
    });

    //参数列表
    function queryParams(params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            unitID: $("#NodeUnitID").val(),
            search: $("#textName").val(),
            txtSearchDate: $("#txtSearchDate").val(),
            isType: $("#isNull").val()
        };
        return temp;
    }

    //首次加载table
    tableHelp.LoadData(SetTableParam(), queryParams);

    Check.Load();
});

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
                field: "A0101",
                title: "姓　名",
                align: "center",
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                }
            }, {
                field: "B0001Name",
                title: "所属项目",
                align: "center"
            }, {
                field: "E0386",
                title: "工种",
                align: "center"
            }, {
                field: "C0104",
                title: "移动电话",
                align: "center"
            }, {
                field: "A0177",
                title: "身份证号",
                align: "center"
            }, {
                field: "A0201",
                title: "上班时间",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined) {
                        var data = value.replace("T", " ");
                        if (row.isFill == 1){
                            data += "<a onclick='buFun(\"" + row.buUser + "\",\"" + row.buReason + "\")'>(补)</a>";
                        }
                        return data;
                    }
                    else
                        return value;
                }
            }, {
                field: "lowWorkDate",
                title: "下班时间",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined) {
                        var data = value.replace("T", " ");
                        if (row.lowIsFill == 1) {
                            data += "<a onclick='buFun(\"" + row.lowUser + "\",\"" + row.lowReason + "\")'>(补)</a>";
                        }
                        return data;
                    }
                    else
                        return value;
                }
            }, {
                field: "check_count",
                title: "次数",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined) {
                        var check = value.split(',');
                        return "<a href='#' onclick='ClickCheckCount(\"" + value + "\")'>" + check.length + "</a>";
                    } else {
                        return "";
                    }
                }
            }, {
                field: "A0202",
                title: "标识",
                align: "center",
                formatter: function (value, row, index) {
                    if (value == "1")
                        return "进场";
                    else if (value == "0")
                        return "出场";
                    else (value == "")
                    return "";
                }
            }

        ],
        Contoller: "TableFromData",
        Height: $.GetBodyHeight(),
        url: $.GetIISName() + "/Person/GetWorkTime",
        tool: "#WorkTimeTool"
    };
    return table;
}

//目录点击事件
function treeClick(event, treeId, o) {
    $("#NodeUnitID").val(o.unit_id);
    var isNull = $("#isNull").val();
    var txtSearchDate = $("#txtSearchDate").val();
    if (isNull != "" && isNull != null && isNull != undefined) {
        if (txtSearchDate == "" || txtSearchDate == null || txtSearchDate == undefined) {
            layer.msg("请选择签到日期");
            return;
        }
    }
    tableHelp.refresh(SetTableParam());
}

function ClickCheckCount(value) {
    var check = value.split(',');
    var content = "<table width='100%'>";
    for (var i = 0; i < check.length; i++) {
        content += "<tr>"
        content += "<td style='height:30px; border:1px solid gray;' >" + check[i] + "</td>";
        content += "</tr>";
    }
    content += "</table>";
    layer.open({
        type: 1,
        title: false,
        content: content,
        area: ["400px", "200px"],
        maxmin: true
    });
}

var Check = {
    Load: function () {
        tableHelpMove.LoadData(Check.Table(), Check.QueryParams);
        $("#btnCheck").click(function () {
            tableHelpMove.refresh(Check.Table());
            layer.open({
                type: 1,
                title: "补签",
                content: $("#divCheck"),
                area: ["80%", "80%"],
                btn: ["确认", "取消"],
                maxmin: true,
                yes: function () {
                    var retire_time = $("#check_date").val();
                    if (retire_time != null && retire_time != "") {
                        Check.Save();
                    } else {
                        layer.msg("请录入补签日期！");
                    }
                },
                cance: function () {

                }
            });
        });
    },

    Save: function () {
        var check_date = $("#check_date").val();
        var check_reason = $("#check_reason").val();
        if (check_date != null && check_date != "") {
            if (check_reason.length <= 40) {
                var row = $("#check_table").bootstrapTable('getSelections');
                if (row != null && row != "") {
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        data: { check_date: check_date, check_reason: check_reason, rows: JSON.stringify(row) },
                        url: $.GetIISName() + "/Person/BuCheckPerson",
                        success: function (data) {
                            if (data.status == "0") {
                                layer.msg(data.msg, { icon: 5 });
                                layer.closeAll();
                                $("#check_date").val("");
                                $("#check_reason").val("");
                                tableHelpMove.refresh(Check.Table());
                                tableHelp.refresh(SetTableParam());
                            } else {
                                layer.msg("补签失败！");
                            }
                        }
                    })
                } else
                    layer.msg("请选择需要补签的人员！");
            } else {
            layer.msg("补签原因字符太长！");
            }
        } else {
            layer.msg("补签时间不能为空！");
        }
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
                        return index + 1
                    }
                }, {
                    field: "A0101",
                    title: "姓　名",
                    align: "center",
                    formatter: function (value, row, index) {
                        return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                    }
                }, {
                    field: "C0104",
                    title: "移动电话",
                    align: "center"
                }, {
                    field: "A0177",
                    title: "身份证号",
                    align: "center"
                }
            ],
            Contoller: "check_table",
            Height: $.GetBodyHeight() - 220,
            url: $.GetIISName() + "/Person/GetBuCheckSoure",
            tool: "#check_tool"
        };
        return table;
    },

    QueryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: 1,
            rows: 1000,
            UnitID: $("#NodeUnitID").val(),
            txtSearchDate: $("#txtSearchDate").val()
        };
        return temp;
    }
}

function buFun(user,reason){
    var content = "补签用户：" + user;
    content += "<br />补签原因：" + reason;
    layer.open({
        type: 1,
        title: false,
        content: content,
        area: ["400px", "200px"],
        maxmin: true
    });
}