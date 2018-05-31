$(function () {
    tableHelp.LoadData(TableFormAction.SetTable(), TableFormAction.queryParams, TableFormAction.responseHandler);
    $("#btnSearch").click(function () {
        tableHelp.refresh(TableFormAction.SetTable());
    });

    //表单验证
    $("#payAccountForm").validate({
        rules: {
            UnitID: "required",
            ssyh: "required",
            khmc: "required",
            zh: "required",
            khh: "required",
            ye: {
                required: true,
                number: true
            }
        },
        messages: {
            UnitID: "所属项目不能为空",
            ssyh: "所属银行不能为空",
            khmc: "开户名称不能为空",
            zh: "账户不能为空",
            khh: "开户行不能为空",
            ye: {
                required: "余额不能为空",
                number: "请输入正确的余额"
            }
        }
    });

    Query.Load();
})

var TableFormAction = {
    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        return {
            page: pageIndex,
            rows: params.limit,
            bank_name: $("#txtSearchName").val()
        };
    },

    SetTable: function () {
        return {
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
                    field: "UnitName",
                    title: "项目名称",
                    align: "left"
                }, {
                    field: "ssyh",
                    title: "所属银行",
                    align: "center",
                    formatter: function (value, row, index) {
                        return GetBankName(value);
                    }
                }, {
                    field: "khmc",
                    title: "开户名称",
                    align: "center"
                }, {
                    field: "zh",
                    title: "账户",
                    align: "center"
                }, {
                    field: "khh",
                    title: "开户行",
                    align: "center"
                }, {
                    field: "ye",
                    title: "余额",
                    align: "center"
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight(),
            url: $.GetIISName() + "/PayAccount/GetPaySoure",
            tool: $("#table-tool")
        };
    }
};

var Query = {

    Load: function () {

        $("#addPay").click(function () {
            var index = layer.open({
                type: 1,
                area: ["800px", "500px"],
                title: "添加专户",
                fixed: false, //不固定
                maxmin: true,
                content: $("#divPayAccount"),
                btn: ["保存", "返回"],
                yes: function (index, layero) {
                    Query.Save();
                },
                btn2: function (index, layero) {
                    Query.Null();
                    layer.closeAll();
                },
                cancel: function (index, layero) {
                    Query.Null();
                    layer.closeAll();
                }
            });
        });

        $("#editPay").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                Query.setValue(row);
                $("#JianDie").val(row[0]["pay_id"]);
                var index = layer.open({
                    type: 1,
                    area: ["800px", "500px"],
                    title: "编辑专户",
                    fixed: false, //不固定
                    maxmin: true,
                    content: $("#divPayAccount"),
                    btn: ["保存", "返回"],
                    yes: function (index, layero) {
                        Query.Save();
                    },
                    btn2: function (index, layero) {
                        Query.Null();
                        layer.closeAll();
                    },
                    cancel: function (index, layero) {
                        Query.Null();
                        layer.closeAll();
                    }
                });
            } else {
                layer.msg("请选择需要编辑的专户！", { icon: 5 });
            }
        });

        $("#deletePay").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                if (layer.confirm("确认要删除该项目专户吗？", { btn: ["确认", "取消"] }, function () {
                    Query.DeletePay(row[0]["pay_id"]);
                }));
            } else {
                layer.msg("请选择需要删除的专户！", { icon: 5 });
            }
        })

    },

    Save: function () {
        if ($("#payAccountForm").valid()) {
            $.ajax({
                type: "post",
                dataType: "text",
                data: $("#payAccountForm").serialize(),
                url: $.GetIISName() + "/PayAccount/SavePayAccount",
                success: function (result) {
                    if (result == "ok") {
                        tableHelp.refresh(TableFormAction.SetTable());
                        Query.Null();
                        layer.closeAll();
                    } else {
                        layer.msg("保存失败！")
                    }
                }
            })
        }
    },

    DeletePay: function (pay_id) {
        $.ajax({
            type: "post",
            dataType: "text",
            data: { pay_id: pay_id },
            url: $.GetIISName() + "/PayAccount/DeletePayAccount",
            success: function (result) {
                if (result == "ok") {
                    tableHelp.refresh(TableFormAction.SetTable());
                    Query.Null();
                    layer.closeAll();
                } else {
                    layer.msg("删除失败！")
                }
            }
        })
    },

    setValue: function (row) {
        $("#UnitID").val(row[0]["UnitID"]);
        $("#ssyh").val(row[0]["ssyh"]);
        $("#khmc").val(row[0]["khmc"]);
        $("#zh").val(row[0]["zh"]);
        $("#khh").val(row[0]["khh"]);
        $("#pzzl").val(row[0]["pzzl"]);
        $("#pzhm").val(row[0]["pzhm"]);
        $("#ye").val(row[0]["ye"]);
        $("#JianDie").val(row[0]["JianDie"]);
    },

    Null: function () {
        $("#UnitID").val("");
        $("#ssyh").val("");
        $("#khmc").val("");
        $("#zh").val("");
        $("#khh").val("");
        $("#pzzl").val("");
        $("#pzhm").val("");
        $("#ye").val("");
        $("#JianDie").val("");
    }

}
