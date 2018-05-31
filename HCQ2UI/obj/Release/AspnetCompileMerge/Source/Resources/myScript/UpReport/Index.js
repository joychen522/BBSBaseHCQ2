$(function () {

    ExpendTable.InitExpend();

    //首次加载table
    //tableHelpMove.LoadData(Table.SetTable(), Table.queryParams);
    Table.ProjectTable();

    $("#btnSearch").click(function () {
        $("#TableFromData").bootstrapTable("refresh");
        //tableHelpMove.refresh(Table.SetTable());
    });

    UpData.Load();
})

//信息上报
var UpData = {

    Load: function () {

        $("#UpData").click(function () {
            //$("#UpData").html("正在上报...").attr("disabled", "disabled");
            var upType = $("#upType").val();
            var row = $("#TableFromData").bootstrapTable("getSelections");
            if (row != null && row != "") {
                $("#divWage").addClass("overlay").show();
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: { upType: upType, rows: JSON.stringify(row) },
                    url: $.GetIISName() + "/UpReport/UpLoad",
                    success: function (data) {
                        $("#divWage").removeClass("overlay").hide();
                        if (data != null) {
                            UpData.Result(data);
                        } else {
                            layer.msg("出现异常，未能完成上报！");
                        }
                    }, error: function (XMLHttpRequest, error, text) {
                        $("#divWage").removeClass("overlay").hide();
                        layer.msg("上报失败！");
                    }
                })
            } else
                layer.msg("请选择需要上报数据的所属企业信息！");
        });

        $("#UpDataForm").click(function () {
            //$("#UpData").html("正在上报...").attr("disabled", "disabled");
            var upType = $("#upType").val();
            var row = $("#TableFromData").bootstrapTable("getSelections");
            if (row != null && row != "") {
                $("#divWage").addClass("overlay").show();
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: { upType: upType, rows: JSON.stringify(row) },
                    url: $.GetIISName() + "/UpReport/UpLoadForm",
                    success: function (data) {
                        $("#divWage").removeClass("overlay").hide();
                        if (data != null) {
                            UpData.Result(data);
                        } else {
                            layer.msg("出现异常，未能完成上报！");
                        }
                    }, error: function (XMLHttpRequest, error, text) {
                        $("#divWage").removeClass("overlay").hide();
                        layer.msg("上报失败！");
                    }
                })
            } else
                layer.msg("请选择需要上报数据的所属企业信息！");
        });

    },

    //返回结果
    Result: function (data) {

        var type = UpData.upType($("#upType").val());
        var resultMess = "";

        for (var i = 0; i < data.length; i++) {
            if (resultMess == "")
                resultMess = data[i].comName + ">>>>" + data[i].projectName + " " + type + " <strong style='color:red;'>" + data[i].mess + "</strong>";
            else
                resultMess += "<br />" + data[i].comName + ">>>>" + data[i].projectName + " " + type + " <strong style='color:red;'>" + data[i].mess + "</strong>";
            if (data[i].errorMsg != null && data[i].errorMsg != "" && data[i].errorMsg != undefined)
                resultMess += data[i].errorMsg;
        }

        $("#lblMess").html(resultMess);
        layer.open({
            type: 1,
            title: type + "：结果",
            content: $("#divResult"),
            area: ["700px", "400px"],
            btn: ["返回"],
            maxmin: true,
            yes: function () {
                $("#lblMess").html("");
                layer.closeAll();
                $("#TableFromData").bootstrapTable("refresh");
            },
            cance: function () {
                $("#lblMess").html("");
                layer.closeAll();
                $("#TableFromData").bootstrapTable("refresh");
            }
        });
    },

    upType: function (menuType) {
        var text = "";
        switch (menuType) {
            case "01":
                text = "企业项目上报";
                break;
            case "02":
                text = "人员信息上报";
                break;
            case "03":
                text = "人员照片上报";
                break;
            case "04":
                text = "考勤信息上报";
                break;
            case "05":
                text = "工资发放信息上报";
                break;
            case "06":
                text = "欠薪信息上报";
                break;
            case "07":
                text = "上报欠薪个人明细";
                break;
            case "08":
                text = "人员附件上报（含简易合同等）";
                break;
            case "09":
                text = "工资专户信息上报";
                break;
            default:
                break;
        }
        return text;
    },

    UpLoadCheckDate:function(){
        


    }
}

var Table = {

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: 1,
            rows: 500,
            UnitID: $("#JianDie").val(),
            JianDieUnitID: $("#JianDieUnitID").val(),
            txtSearchName: $("#txtSearchName").val(),
            comType: "1"
        };
        return temp;
    },

    SetTable: function () {
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
                    field: "dwmc",
                    title: "单位名称",
                    align: "center"
                }, {
                    field: "QXLB",
                    title: "类别",
                    align: "center",
                    formatter: function (value, row, index) {
                        return "工程类总包";
                    }
                }, {
                    field: "tyshxydm",
                    title: "统一社会信用代码",
                    align: "center"
                }, {
                    field: "Fddbrxm",
                    title: "法定代表人姓名",
                    align: "center"
                }, {
                    field: "LXR",
                    title: "联系人",
                    align: "center"
                }, {
                    field: "LXDH",
                    title: "联系人电话",
                    align: "center"
                }
            ],
            Contoller: "TableFromData",
            Height: window.innerHeight - 4,
            url: $.GetIISName() + "/CompProInfo/GetComBindSoure",
            tool: $("#table-tool")
        };
        return table;
    },

    ProjectTable: function () {
        $("#TableFromData").bootstrapTable({
            queryParams: function (params) {
                var pageIndex = params.offset / params.limit + 1
                return {
                    page: 1,
                    rows: 500,
                    UnitID: "",
                    txtSearchName: $("#txtSearchName").val()
                };
            },
            columns: [
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
                    }, {
                        field: "in_compay",
                        title: "施工单位",
                        align: "center",
                        formatter: function (value, row, index) {
                            return GetComName(value);
                        }
                    }, {
                        field: "if_upload",
                        title: "项目是否上报",
                        align: "center",
                        formatter: function (value, row, index) {
                            if (value == "1")
                                return "<span style='color:blue;'>是</span>";
                            else
                                return "<span style='color:red;'>否</span>";
                        }
                    }],
            url: $.GetIISName() + "/Enterprise/GetManage",
            toolbar: "#table-tool",
            pagination: false,
            height: $.GetBodyHeight()
        });

    }
};