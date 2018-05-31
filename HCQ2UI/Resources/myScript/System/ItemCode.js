//table获取数据的URL
var urlaction = $.GetIISName() + "/ItemCode/GetItemCodeView";

//查询事件
$("#btnSearch").click(function () {
    tableHelp.refresh(table);
});

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        code_id: $("#hi_code_id").val(),
        item_id: $("#hi_item_id").val(),
        txtSearchName: $("#txtSearchName").val(),
    };
    return temp;
}

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
        },
        {
            field: "item_type",
            title: "类别",
            align: "center",
            formatter: function (value, row, index) {
                return value == "M" ? "<span style='color:Red'>字典标题</span>" : "<span style='color:Blue'>字典值</span>";
            }
        },
        {
            field: "code_name",
            title: "分类名称",
            align: "center"
        },
        {
            field: "code_value",
            title: "分类代码",
            align: "center"
        },
        {
            field: "code_note",
            title: "备注",
            align: "center"
        },
        {
            field: "code_id",
            title: "操作",
            align: "center",
            formatter: function (value, row, index) {
                return "<a href='#' class='badge badge-info' onclick='operion.Edit(" + index + ")'>编辑</a>" + " " + "<a href='#' class='badge badge-info' onclick='operion.Delete(" + index + ")' >删除</a>";
            }
        }
    ],
    Contoller: "TableFromData",
    Height: $.GetBodyHeight(),
    url: urlaction,
    tool: "#shop_tool"
};
//首次加载table
tableHelp.LoadData(table, queryParams);

$("#addItemCode").click(function () {
    layer.open({
        type: 1,
        area: ["700px", "350px"],
        title: "添加数据字典",
        fixed: false, //不固定
        maxmin: true,
        content: $("#add_area"),
        btn: ["保存", "返回"],
        yes: function (index, layero) {
            operion.Add();
        },
        btn2: function (index, layero) {
            TextBoxOper.Null();
        },
        cancel: function () {
            TextBoxOper.Null();
        }
    });
})

var operion = {
    Add: function () {
        var code_id = $("#hi_code_id").val();
        var item_id = $("#hi_item_id").val();
        var input_name = $("#item_name").val();
        var input_code = $("#item_code").val();
        var input_note = $("#item_note").val();
        var input_code_type = $("#code_type").val();
        if (input_name != null && input_name != "" && input_name != undefined
            || input_code != null && input_code != "" && input_code != undefined) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/itemCode/AddItemCode",
                data: {
                    code_id: code_id, item_id: item_id, input_name: input_name,
                    input_code: input_code, input_note: input_note,
                    input_code_type: input_code_type
                },
                success: function (result) {
                    if (result == "ok") {
                        TreeRefresh.Update();
                        tableHelp.refresh(table);
                        layer.closeAll();
                        TextBoxOper.Null();
                    } else {
                        layer.msg("添加失败，请检查是否重复添加!", { icon: 5 });
                        layer.closeAll();
                        TextBoxOper.Null();

                    }
                }
            })
        } else {
            layer.msg("数据不完整", { icon: 5 });
        }
    },
    Edit: function (index) {
        var SelectRow = $("#TableFromData").bootstrapTable("getData")[index];
        $("#item_name").val(SelectRow["code_name"]);
        $("#item_code").val(SelectRow["code_value"]);
        $("#item_note").val(SelectRow["code_note"]);
        if (SelectRow["code_type"] != null && SelectRow["code_type"] != "" && SelectRow["code_type"] != undefined) {
            $("#code_type").val(SelectRow["code_type"])
        }
        layer.open({
            type: 1,
            area: ["700px", "350px"],
            title: "编辑数据典",
            fixed: false, //不固定
            maxmin: true,
            content: $("#add_area"),
            btn: ["保存", "返回"],
            yes: function (index, layero) {
                var code_id = SelectRow["code_id"];
                var item_id = SelectRow["item_id"];
                var item_type = SelectRow["item_type"];
                var input_name = $("#item_name").val();
                var input_code = $("#item_code").val();
                var input_note = $("#item_note").val();
                var code_type = $("#code_type").val();
                if (input_name != null && input_name != "" && input_name != undefined
                    || input_code != null && input_code != "" && input_code != undefined) {
                    $.ajax({
                        type: "post",
                        dataType: "text",
                        url: $.GetIISName() + "/itemCode/Edit",
                        data: {
                            code_id: code_id, item_id: item_id, input_name: input_name,
                            input_code: input_code, input_note: input_note, item_type: item_type,
                            code_type: code_type
                        },
                        success: function (result) {
                            if (result == "ok") {
                                TreeRefresh.Update();
                                tableHelp.refresh(table);
                                layer.closeAll();
                                TextBoxOper.Null();
                            } else {
                                layer.msg("编辑失败", { icon: 5 });
                                layer.closeAll();
                                TextBoxOper.Null();

                            }
                        }
                    })
                } else {
                    layer.msg("数据不完整", { icon: 5 });
                }
            },
            btn2: function (index, layero) {
                TextBoxOper.Null();
            },
            cancel: function () {
                TextBoxOper.Null();
            }
        });
    },
    Delete: function (index) {
        layer.confirm("确定要删除该数据字典吗？", { btn: ["确定", "取消"] }, function () {
            var SelectRow = $("#TableFromData").bootstrapTable("getData")[index];
            //var index = layer.msg("删除中...", { icon: 6, time: 3000 });
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/ItemCode/Delete",
                data: { code_id: SelectRow["code_id"], item_id: SelectRow["item_id"], item_type: SelectRow["item_type"] },
                success: function (result) {
                    if (result == "ok") {
                        TreeRefresh.Update();
                        layer.closeAll();
                        tableHelp.refresh(table);
                    } else
                        layer.msg("删除失败", { icon: 5 });
                }
            });
        })
    }
}

var TextBoxOper = {
    Null: function () {
        $("#hi_code_id").val("");
        $("#hi_item_id").val("");
        $("#item_name").val("");
        $("#item_code").val("");
        $("#item_note").val("");
        $("#code_type").val("");
    }
}

var TreeRefresh = {
    Update: function () {
        $.ajax({
            type: "post",
            dataType: "text",
            async: false,
            url: $.GetIISName() + "/itemCode/GetItemCodeTree",
            success: function (result) {
                treeHelp.BindTree("treeview", eval("(" + result + ")"), treeClick);
            }
        });
    }
}

$("#item_code").blur(function () {
    var code_id = $("#hi_code_id").val();
    var item_id = $("#hi_item_id").val();
    if (code_id == "" && item_id == "") {
        var value = $("#item_code").val();
        $.ajax({
            type: "post",
            dataType: "text",
            data: { item_code: value },
            url: $.GetIISName() + "/ItemCode/VlidataItemCode",
            success: function (result) {
                if (result == "ok") {
                    //$("#item_code").css("border-color", "#127E68");
                } else {
                    $("#item_code").val("");
                    $("#item_code").css("border-color", "red");
                    layer.msg("该字典代码已经存在！", { icon: 5 });
                }
            }
        });
    }
});

$("#item_name").blur(function () {
    var value = $(this).val();
    if (value == "") {
        $("#item_name").val("");
        $("#item_name").css("border-color", "red");
        layer.msg("字典名称不能为空！", { icon: 5 });
    }

});