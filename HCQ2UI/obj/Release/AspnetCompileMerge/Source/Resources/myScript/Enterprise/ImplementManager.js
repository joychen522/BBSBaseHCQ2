$(function () {
    var boxStr = [{ "boxname": "install_date" }, { "boxname": "collect_date" }, { "boxname": "cost_date" }];
    for (var i = 0; i < boxStr.length; i++) {
        $("#" + boxStr[i]["boxname"] + "").datepicker({
            show: true,
            format: "yyyy-mm-dd",
            language: "zh-CN",
            weekStart: 1,
            autoclose: true,
            orientation: "right",
            todayBtn: "linked"
        });
    }
    // 手机号码验证
    jQuery.validator.addMethod("isMobile", function (value, element) {
        var length = value.length;
        var mobile = /^(13[0-9]{9})|(18[0-9]{9})|(14[0-9]{9})|(17[0-9]{9})|(15[0-9]{9})$/;
        return this.optional(element) || (length == 11 && mobile.test(value));
    });
    //表单验证
    $("#implementForm").validate({
        rules: {
            owner_name: "required",
            B0001: "required",
            B0002: "required",
            B000201: "required",
            respon_name: "required",
            install_date: "required",
            collect_date: "required",
            cost_money: {
                required: true,
                range: [1, 100000000]
            },
            use_status: "required",
            cost_date: "required",
            respon_phone: {
                required: false,
                minlength: 11,
                isMobile: true
            }
        },
        messages: {
            owner_name: "业务单位不能空",
            B0001: "总包单位不能为空",
            B0002: "用工单位不能为空",
            B000201: "工地名称不能为空",
            respon_name: "责任人不能为空",
            install_date: "安装日期不能为空",
            collect_date: "采集日期不能为空",
            cost_money: {
                required: "费用不能为空",
                range: "请输入正确的费用"
            },
            use_status: "状态不能为空",
            cost_date: "付费日期不能为空",
            respon_phone: {
                required: "联系电话不能为空",
                minlength: "请输入正确的手机号码",
                isMobile: "请输入正确的手机号码"
            }
        }
    });

});
var Index = {

    Init: function () {
        Index.Load();
        $("#btnSearch").click(function () {
            Index.Refresh();
        });
        $("#AddImplement").click(function () {
            var index = layer.open({
                type: 1,
                area: ["700px", "500px"],
                title: "添加实施记录",
                fixed: false, //不固定
                maxmin: true,
                content: $("#implementOption"),
                btn: ["保存", "返回"],
                yes: function (index, layero) {
                    if ($("#implementForm").valid()) {
                        Implement.Save();
                    }
                },
                btn2: function (index, layero) {
                    Implement.TextNull();
                },
                cancel: function () {
                    Implement.TextNull();
                }
            });
        });

        $("#EditImplement").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                Implement.SetTextBox(row);
                $("#implementOption").css("display", "block");
                var index = layer.open({
                    type: 1,
                    area: ["700px", "500px"],
                    title: "编辑实施记录",
                    fixed: false, //不固定
                    maxmin: true,
                    content: $("#implementOption"),
                    btn: ["保存", "返回"],
                    yes: function (index, layero) {
                        if ($("#implementForm").valid()) {
                            Implement.Save();
                        }
                    },
                    btn2: function (index, layero) {
                        Implement.TextNull();
                    },
                    cancel: function () {
                        Implement.TextNull();
                    }
                });
            } else {
                layer.msg("请选择需要编辑的行！", { icon: 5 });
            }
        });

        $("#DeleteImplement").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                layer.confirm("确认要删除改实施记录吗？", { btn: ["确定", "取消"] }, function () {
                    Implement.DeleteImplement(row);
                })
            } else {
                layer.msg("请选择需要删除的行！", { icon: 5 });
            }
        });

    },

    Load: function () {
        tableHelp.LoadData(Index.tableParam(), Index.queryParams);
    },

    Refresh: function () {
        tableHelp.refresh(Index.tableParam());
    },

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    },

    tableParam: function () {
        var table = {
            Columns: [
                {
                    radio: true
                }, {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1
                    }
                }, {
                    field: "owner_name",
                    title: "业主单位",
                    align: "center"
                }, {
                    field: "B0001",
                    title: "总包单位",
                    align: "center"
                }, {
                    field: "B0002",
                    title: "用工单位",
                    align: "center"
                }, {
                    field: "B000201",
                    title: "工地名称",
                    align: "center"
                }, {
                    field: "respon_name",
                    title: "责任人",
                    align: "center"
                }, {
                    field: "respon_phone",
                    title: "联系电话",
                    align: "center"
                }, {
                    field: "install_date",
                    title: "安装日期",
                    align: "center",
                    formatter: function (value, row, idex) {
                        if (value != "" && value != null) {
                            return $.DateFormat(value);
                        } else {
                            return "";
                        }
                    }
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight(),
            url: $.GetIISName() + "/Implement/GetSoure",
            tool: "#table-tool"
        };
        return table;
    }

}

var Implement = {

    Save: function () {
        $.ajax({
            type: "post",
            dataType: "json",
            data: $("#implementForm").serialize(),
            url: $.GetIISName() + "/Implement/SaveImplement",
            success: function (data) {
                if (data.Statu == 1) {
                    Index.Refresh();
                    layer.closeAll();
                    Implement.TextNull();
                }
                layer.msg(data.Msg);
            }
        });
    },

    DeleteImplement: function (row) {
        $.ajax({
            type: "post",
            dataType: "text",
            data: { implement_id: row[0].imp_id },
            url: $.GetIISName() + "/Implement/DeleteImplement",
            success: function (data) {
                if (data == "ok") {
                    Index.Refresh();
                    layer.msg("删除成功");
                } else {
                    layer.msg("删除失败");
                }

            }
        });
    },

    SetTextBox: function (row) {
        Implement.TextNull();
        $("#JianDieImplement").val(row[0].imp_id);
        $("#owner_name").val(row[0].owner_name);
        $("#B0001").val(row[0].B0001);
        $("#B0002").val(row[0].B0002);
        $("#B000201").val(row[0].B000201);
        $("#respon_name").val(row[0].respon_name);
        $("#respon_phone").val(row[0].respon_phone);
        $("#install_date").val((row[0].install_date != "" && row[0].install_date != null) ? $.DateFormat(row[0].install_date) : "");
        $("#collect_date").val((row[0].collect_date != "" && row[0].collect_date != null) ? $.DateFormat(row[0].collect_date) : "");
        $("#cost_money").val(row[0].cost_money);
        $("#use_status").val(row[0].use_status);
        $("#cost_date").val((row[0].cost_date != "" && row[0].cost_date != null) ? $.DateFormat(row[0].cost_date) : "");
        $("#impl_note").val(row[0].impl_note);
    },

    TextNull: function () {
        $("#implementOption").css("display", "none");
        $("#JianDieImplement").val("");
        $("#owner_name").val("");
        $("#B0001").val("");
        $("#B0002").val("");
        $("#B000201").val("");
        $("#respon_name").val("");
        $("#respon_phone").val("");
        $("#install_date").val("");
        $("#collect_date").val("");
        $("#cost_money").val("");
        $("#use_status").val("");
        $("#cost_date").val("");
        $("#impl_note").val("");
    }
}