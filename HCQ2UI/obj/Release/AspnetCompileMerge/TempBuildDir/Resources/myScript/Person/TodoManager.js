$(function () {
    Table.Load();
    LoadPage.Init();
    formValidate();
});

function formValidate() {
    $("#todoForm").validate({
        rules: {
            to_title: "required",
            to_user_id: "required"
        },
        messages: {
            to_title: "请录入待办事宜标题",
            to_user_id: "请选择待办事宜接收人"
        }
    });
}

var Table = {

    Load: function () {
        tableHelp.LoadData(Table.Table(), Table.queryParams);
    },

    Table: function () {
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
                    field: "to_title",
                    title: "标题",
                    align: "left"
                }, {
                    field: "to_send_user_name",
                    title: "发信人",
                    align: "center"
                }, {
                    field: "to_send_date",
                    title: "发送时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != "" && value != null)
                            return $.DateFormatTime(value, "yyyy-MM-dd hh:mm:ss");
                    }
                }, {
                    field: "is_system",
                    title: "是否系统消息",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == 0) {
                            return "否";
                        } else {
                            return "是";
                        }
                    }
                }, {
                    field: "to_user_id",
                    title: "能否回复",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value.indexOf(";") > 0)
                            return "否";
                        else
                            return "能";
                    }
                }, {
                    field: "re_content",
                    title: "是否回复",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value == null || value == "")
                            return "否";
                        else
                            return "已回复";
                    }
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight(),
            url: $.GetIISName() + "/Todo/GetTodoSoure",
            tool: "#table-tool"
        };
        return table;
    },

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            itemType: $("#JianDie").val(),
            title: $("#txtSearchName").val()
        };
        return temp;
    },

    refresh: function () {
        tableHelp.refresh(Table.Table());
    }
}

var LoadPage = {

    Init: function () {

        $("#btnSearch").click(function () {
            Table.refresh();
        });

        $("#addTodo").click(function () {
            $("#to_user_id").removeAttr("disabled");
            $("#to_title").removeAttr("disabled");
            $("#btnSave").removeAttr("disabled");
            $("#divPerson").removeAttr("disabled");
            var index = layer.open({
                type: 1,
                title: "发送待办事宜",
                content: $("#add_area"),
                area: ["100%", "100%"],
                maxmin: true,
                cancel: function () {
                    LoadPage.TextBoxNull();
                }
            });
            layer.full(index);
        });

        $("#btnSave").click(function () {
            if ($("#todoForm").valid()) {
                $("#content").val($("#to_content").code());
                $.ajax({
                    type: "post",
                    dataType: "text",
                    url: $.GetIISName() + "/Todo/AddTodo",
                    async: false,
                    data: $("#todoForm").serialize(),
                    success: function (result) {
                        if (result == "ok") {
                            layer.closeAll();
                            Table.refresh();
                            layer.msg("发送成功");
                            LoadPage.TextBoxNull();

                        } else {
                            layer.msg("发送失败");
                        }
                    }
                })
            }
        });

        $("#btnClose").click(function () {
            $("#re_grounp").hide();
            layer.closeAll();
            LoadPage.TextBoxNull();
        });

        $("#deleteTodo").click(function () {
            var codeValue = $("#JianDie").val();
            if (codeValue != "0002") {
                layer.msg("接收的待办事宜不能删除", { icon: 5 });
                return;
            }
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                layer.confirm("确定要删除该待办事宜吗？", { btn: ["确定", "取消"] }, function () {
                    $.ajax({
                        type: "post",
                        dataType: "text",
                        url: $.GetIISName() + "/Todo/DeleteTodo",
                        data: { to_id: row[0].to_id },
                        success: function (result) {
                            if (result == "ok") {
                                Table.refresh();
                                layer.msg("删除成功", { icon: 6 });
                            } else {
                                layer.msg("删除失败", { icon: 5 });
                            }
                        }
                    })
                });
            }
        });

        $("#editDetail").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "") {
                $("#re_grounp").show();
                $("#to_content_d").val(row[0]["re_content"]).attr("disabled", "disabled");

                $("#to_content").code(row[0].to_content);
                $("#to_user_id").val(row[0].to_user_name).attr("disabled", "disabled");
                $("#to_title").val(row[0].to_title).attr("disabled", "disabled");
                $("#btnSave").attr("disabled", "disabled");
                var index = layer.open({
                    type: 1,
                    title: "查看",
                    content: $("#add_area"),
                    area: ["100%", "100%"],
                    maxmin: true,
                    cancel: function () {
                        $("#re_grounp").hide();
                        LoadPage.TextBoxNull();
                    }
                });
                //var index = layer.open({
                //    type: 2,
                //    title: "待办事宜",
                //    content: $.GetIISName() + "/Todo/TodoDetail?to_id=" + row[0].to_id,
                //    area: ["850px", "400px"],
                //    maxmin: true
                //});
            } else {
                layer.msg("请选择待办事宜", { icon: 5 });
            }
        });

        $("#reTodo").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != "" && row != null) {
                if (row[0]["to_user_id"].indexOf(";") <= 0) {
                    $("#re_content").val(row[0]["re_content"]);
                    layer.open({
                        type: 1,
                        title: "回复：" + row[0]["to_send_user_name"] + "(" + row[0]["to_title"] + ")",
                        content: $("#re_area"),
                        area: ["500px", "300px"],
                        btn: ["确定", "取消"],
                        maxmin: true,
                        yes: function (index) {
                            var re_content = $("#re_content").val();
                            if (re_content != null && re_content != "") {
                                $.ajax({
                                    type: "post",
                                    dataType: "text",
                                    url: $.GetIISName() + "/Todo/ReContent",
                                    data: { to_id: row[0]["to_id"], re_content: re_content },
                                    success: function (result) {
                                        if (result == "ok") {
                                            layer.closeAll();
                                            layer.msg("已回复", { icon: 6 });
                                            Table.refresh();
                                            $("#re_content").val("");

                                        } else {
                                            layer.msg("回复失败", { icon: 5 });
                                        }
                                    }
                                })
                            } else {
                                layer.msg("回复内容不能为空", { icon: 5 });
                            }
                        },
                        btn2: function () {
                            $("#re_content").val("");
                        },
                        cancel: function () {
                            $("#re_content").val("");
                        }
                    });
                } else {
                    layer.msg("群发待办事宜不能回复", { icon: 5 });
                }
            } else {
                layer.msg("请选择待办事宜", { icon: 5 });
            }
        });
    },

    UserTreeClick: function (o) {
        var value = $("#to_user_id").val();
        if (value == null || value == "") {
            $("#to_user_id").val(o.text);
        } else {
            $("#to_user_id").val(value + ";" + o.text);
        }
    },

    TextBoxNull: function () {
        $("#to_user_id").val("");
        $("#to_title").val("");
        $("#to_content").code("");
    }

}
