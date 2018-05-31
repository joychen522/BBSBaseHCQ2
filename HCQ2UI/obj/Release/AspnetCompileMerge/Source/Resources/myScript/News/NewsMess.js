var $table;
$(function () {
    Table.LoadTable();
    //Table.Load();
    Load.Init();

    //表单验证
    $("#messForm").validate({
        rules: {
            m_title: {
                required: true,
                rangelength: [1, 20]
            },
            m_type: "required",
        },
        messages: {
            m_title: {
                required: "标题不能为空",
                rangelength: "标题过长，限制输入20个字符"
            },
            m_type: "分类不能为空"
        }
    });
})

//图片上传
$("#NewsImage").change(function () {
    $.ajaxFileUpload({
        type: "post",
        url: $.GetIISName() + "/NewsMessage/NewsImage",
        secureuri: false,
        fileElementId: "NewsImage",
        dataType: "text",
        success: function (result) {
            if (result.indexOf(".") > 0)
                $("#m_image").val(result);
            $("#image").attr("src", result.replace("~", ".."));
        }
    })
})

var Load = {

    Init: function () {

        $("#AddMess").click(function () {
            layer.open({
                type: 1,
                area: ["700px", "450px"],
                title: "发布新闻公告",
                fixed: false, //不固定
                maxmin: true,
                content: $("#updateDiv"),
                btn: ["保存", "返回"],
                yes: function (index, layero) {
                    if ($("#messForm").valid()) {
                        var type = $("#m_type").val();
                        var image = $("#m_image").val();
                        if (type == "新闻" && (image == "" || image == null))
                            layer.msg("新闻分类必须上传照片", { icon: 5 });
                        else
                            Oper.Save();
                    }
                },
                btn2: function (index, layero) {
                    Oper.Null();
                },
                cancel: function () {
                    Oper.Null();
                }
            });
        });

        $("#EditMess").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                Oper.SetValue(row);
                layer.open({
                    type: 1,
                    area: ["700px", "450px"],
                    title: "编辑新闻公告",
                    fixed: false, //不固定
                    maxmin: true,
                    content: $("#updateDiv"),
                    btn: ["保存", "返回"],
                    yes: function (index, layero) {
                        if ($("#messForm").valid()) {
                            var type = $("#m_type").val();
                            var image = $("m_image").val();
                            if (type == "新闻" && (image == "" || image == null))
                                layer.msg("新闻分类必须上传照片", { icon: 5 });
                            else
                                Oper.Save();
                        }
                    },
                    btn2: function (index, layero) {
                        Oper.Null();
                    },
                    cancel: function () {
                        Oper.Null();
                    }
                });
            } else {
                layer.msg("请选择需要编辑的行", { icon: 5 });
            }
        });

        $("#DeleteMess").click(function () {
            var row = $("#TableFromData").bootstrapTable('getSelections');
            if (row != null && row != "" && row != undefined) {
                layer.confirm("确定要删除选中的行吗？", { btn: ["确定", "取消"] }, function () {
                    $.ajax({
                        type: "post",
                        dataType: "text",
                        url: $.GetIISName() + "/NewsMessage/DeleteMess",
                        data: { mess_id: row[0]["m_id"] },
                        success: function (result) {
                            if (result == "ok") {
                                layer.closeAll();
                                Table.refresh();
                            } else {
                                layer.msg("删除失败", { icon: 6 });
                            }
                        }
                    })
                });
            } else {
                layer.msg("请选择需要删除的行", { icon: 5 });
            }
        });
        //查询
        $('#btnSearch').on('click', function () {
            $table.bootstrapTable('refresh');
        });
    }
}

var Oper = {

    Save: function () {
        $.ajax({
            type: "post",
            dataType: "text",
            url: $.GetIISName() + "/NewsMessage/UpdateMess",
            data: $("#messForm").serialize(),
            success: function (result) {
                if (result == "ok") {
                    Table.refresh();
                    layer.closeAll();
                    Oper.Null();
                } else {
                    layer.msg("保存失败", { icon: 5 })
                }
            }
        })
    },

    Detail: function (index) {
        var row = $("#TableFromData").bootstrapTable("getData")[index];
        Oper.DetailSetValue(row);
        layer.open({
            type: 1,
            area: ["700px", "450px"],
            title: "详细信息",
            fixed: false, //不固定
            maxmin: true,
            content: $("#updateDiv"),
            btn: ["确定"],
            yes: function (index, layero) {
                layer.closeAll();
                Oper.Null();
            },
            cancel: function () {
                Oper.Null();
            }
        });
    },

    DetailSetValue: function (row) {
        $("#m_title").val(row.m_title);
        $("#m_type").val(row.m_type);
        $("#m_image").val(row.m_image_src);
        $("#m_content").val(row.m_content);
    },

    SetValue: function (row) {
        $("#m_title").val(row[0]["m_title"]);
        $("#m_type").val(row[0]["m_type"]);
        $("#m_image").val(row[0]["m_image_src"]);
        $("#m_content").val(row[0]["m_content"]);
        $("#mes_id").val(row[0]["m_id"]);
    },

    Null: function () {
        $("#m_title").val("");
        $("#m_type").val("");
        $("#m_image").val("");
        $("#m_content").val("");
        $("#mes_id").val("");
        $("#image").attr("src", "");
    }

}

var Table = {
    //加载table
    LoadTable:function(){
        var options = {
            url: $.GetIISName() + "/NewsMessage/GetMessage",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#news_tool",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    title: $("#txtSearchName").val()
                }
                return params;
            },
            columns: [
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
                    field: "m_title",
                    title: "标题",
                    align: "left"
                }, {
                    field: "m_content",
                    title: "内容",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "") {
                            if (value.length > 15)
                                return value.substring(0, 15) + "...";
                            else
                                return value;
                        }
                    }
                }, {
                    field: "m_type",
                    title: "类别",
                    align: "center"
                }, {
                    field: "create_date",
                    title: "发布日期",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != "" && value != null)
                            return $.DateFormatTime(value, "yyyy-MM-dd hh:mm:ss");
                    }
                }, {
                    field: "create_user_name",
                    title: "发布用户",
                    align: "center"
                }, {
                    field: "m_id",
                    title: "详细",
                    align: "center",
                    formatter: function (value, row, index) {
                        return "<a href='#' onclick='Oper.Detail(" + index + ")'>详细</a>";
                    }
                }
            ]
        }
        $table = tableHelper.initTable("TableFromData", options);
    },

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
                    field: "m_title",
                    title: "标题",
                    align: "left"
                }, {
                    field: "m_content",
                    title: "内容",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "") {
                            if (value.length > 15)
                                return value.substring(0, 15) + "...";
                            else
                                return value;
                        }
                    }
                }, {
                    field: "m_type",
                    title: "类别",
                    align: "center"
                }, {
                    field: "create_date",
                    title: "发布日期",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != "" && value != null)
                            return $.DateFormatTime(value, "yyyy-MM-dd hh:mm:ss");
                    }
                }, {
                    field: "create_user_name",
                    title: "发布用户",
                    align: "center"
                }, {
                    field: "m_id",
                    title: "详细",
                    align: "center",
                    formatter: function (value, row, index) {
                        return "<a href='#' onclick='Oper.Detail(" + index + ")'>详细</a>";
                    }
                }
            ],
            Contoller: "TableFromData",
            Height: $.GetBodyHeight() - 5,
            url: $.GetIISName() + "/NewsMessage/GetMessage",
            tool: "#news_tool"
        };
        return table;
    },

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            title: $("#txtSearchName").val()
        };
        return temp;
    },

    refresh: function () {
        tableHelp.refresh(Table.Table());
    }
}