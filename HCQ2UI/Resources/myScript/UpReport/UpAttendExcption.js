
$(function () {

    ExpendTable.InitExpend();
    Table.Init();

    $("#btnSearch").click(function () {
        $("#TableFromData").bootstrapTable("refresh");
    });

    $("#btnRealAddress").click(function () {
        UpLoad.Test("real");
    });
});

var UpLoad = {
    //上报
    Test: function () {
        var row = $("#TableFromData").bootstrapTable("getSelections");
        if (row != null && row != "") {
            $("#divWage").addClass("overlay").show();
            $.ajax({
                type: "post",
                dataType: "text",
                data: { rows: JSON.stringify(row) },
                url: $.GetIISName() + "/UpReport/UpLoadExcptionAttend",
                success: function (data) {
                    $("#divWage").removeClass("overlay").hide();
                    if (data == "ok") {
                        layer.msg("上报成功！");
                        $("#TableFromData").bootstrapTable("refresh");
                    } else {
                        layer.msg("出现异常，未能完成上报！");
                    }
                }, error: function (XMLHttpRequest, error, text) {
                    $("#divWage").removeClass("overlay").hide();
                    layer.msg("上报失败！");
                }
            })
        } else {
            layer.msg("请选择人员！");
        }
    },

    //返回结果
    Result: function (data) {
        var type = UpLoad.upType($("#upType").val());
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

    //异常考勤上报
    Attend: function () {
        $("#divWage").addClass("overlay").show();
        $.ajax({
            type: "post",
            dataType: "text",
            data: { upType: "03" },
            url: $.GetIISName() + "/UpReport/UpLoadAttend",
            success: function (data) {
                $("#divWage").removeClass("overlay").hide();
                if (data != null) {
                    var re = data.split(',');
                    if (re[0] == "ok") {
                        if (re[1] > 0) {
                            layer.msg("上报成功，已完成" + re[1] + "条考勤异常信息上报！");
                        } else {
                            layer.msg("没有考勤异常需要上报");
                        }
                    } else {
                        layer.msg("上报失败");
                    }
                } else {
                    layer.msg("出现异常，未能完成上报！");
                }
            }, error: function (XMLHttpRequest, error, text) {
                $("#divWage").removeClass("overlay").hide();
                layer.msg("上报失败！");
            }
        })
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
    }
}

var SetCheckDate = {

    Init: function () {
        var da = new Date();

        var year = da.getFullYear();
        var month = da.getMonth() + 1;

        var yearMonth = "";
        //本年
        for (var i = month; i >= 1; i--) {
            if (yearMonth == "" || yearMonth == null || yearMonth == undefined) {
                yearMonth = year + "-" + (i <= 9 ? ("0" + i) : i);
            } else {
                yearMonth += "," + year + "-" + (i <= 9 ? ("0" + i) : i);
            }
        }

        //上一年
        for (var i = 12; i >= month; i--) {
            yearMonth += "," + (year - 1) + "-" + (i <= 9 ? ("0" + i) : i);
        }

        var spData = yearMonth.split(",");
        for (var i = 0; i < spData.length; i++) {
            $("#dateSelect").append("<option value='" + spData[i] + "'>" + spData[i] + "</option>");
        }
    },

    Load: function () {

        var da = new Date();

        var year = da.getFullYear();
        var month = da.getMonth() + 1;

        var yearMonth = "";
        //本年
        for (var i = month; i >= 1; i--) {
            if (yearMonth == "" || yearMonth == null || yearMonth == undefined) {
                yearMonth = year + "." + i;
            } else {
                yearMonth += "," + year + "." + i;
            }
        }

        //上一年
        for (var i = 12; i >= month; i--) {
            yearMonth += "," + (year - 1) + "." + i;
        }


        var html = "<table border=0 style='width:100px; height:100px;'>";
        var spData = yearMonth.split(',');
        for (var i = 0; i < spData.length; i++) {
            $("#dateSelect").append("<option value='" + spData[i] + "'>" + spData[i] + "</option>");

            html += "<tr>";
            html += "<td>" + spData[i] + "</td>";
            html += "</tr>";
        }
        html += "</table>";
        $("#divCheck").html(html);

        layer.open({
            type: 1,
            title: "选择日期",
            content: $("#SelectCheckDate"),
            area: ["700px", "400px"],
            btn: ["确定", "返回"],
            maxmin: true,
            yes: function () {
                alert("yesr");
            },
            cance: function () {
                alert("cance");
            }
        });
    }

}

var Table = {

    Init: function () {
        $("#TableFromData").bootstrapTable({
            queryParams: function (params) {
                var pageIndex = params.offset / params.limit + 1
                return {
                    page: 1,
                    rows: 5000,
                    UnitID: $("#JianDie").val(),
                    txtSreachName: $("#txtSearchName").val()
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
                        field: "A0101",
                        title: "姓　名",
                        align: "center",
                        formatter: function (value, row, index) {
                            return "<a href='#' onclick=Person.Detail('" + row.PersonID + "')>" + value + "</a>";
                        }
                    }, {
                        field: "A0177",
                        title: "身份证号",
                        align: "center"
                    }, {
                        field: "A0201",
                        title: "考勤时间",
                        align: "center",
                        formatter: function (value, row, index) {
                            return $.DateFormatTime(value, "yyyy-MM-dd hh:mm:ss");
                        }
                    }, {
                        field: "A0203",
                        title: "上班标识",
                        align: "center",
                        formatter: function (value, row, index) {
                            if (value == "1")
                                return "上班";
                            else
                                return "下班";
                        }
                    }, {
                        field: "B0001Name",
                        title: "所属项目",
                        align: "center"
                    }],
            url: $.GetIISName() + "/UpReport/GetUpAttendExcption",
            toolbar: "#table-tool",
            pagination: false,
            height: $.GetBodyHeight()
        });
    }

}