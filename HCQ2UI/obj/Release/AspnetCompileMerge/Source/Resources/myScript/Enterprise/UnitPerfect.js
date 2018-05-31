$(function () {
    SetTextBoxDateSelect();

    //查询事件
    $("#btnSearch").click(function () {
        tableHelp.refresh(table);
    });

    //首次加载table
    tableHelp.LoadData(table, queryParams);

    $("#editUnit").click(function () {
        var row = $("#TableFromData").bootstrapTable('getSelections');
        if (row != null && row != "" && row != undefined) {
            operion.Edit(row);
        } else {
            layer.msg("请选择需要编辑的项目！", { icon: 5 });
        }
    });

    FBDW.Load();

    //表单验证
    $("#unitAdd").validate({
        rules: {
            UnitName: "required",
            B0114: {
                required: false,
                number: true
            },
            B0116: {
                required: false,
                number: true
            },
            FBR: "required",
            CBR: "required",
            B0112: "required",
            CBRXMJLSFZH: "required",
            FBRDZ: "required",
            CBRDH: "required",
            CBRFDDBR: "required",
            CBRDZ: "required",
            SSXZZGBM: "required",
            LSJG: "required",
            CBRXMJLXM: "required",
            FXRQ: {
                required: true,
                range: [1, 31]
            },
            SHXYDM: "required",
            GZFFZHSSYH: "required"
        },
        messages: {
            UnitName: "项目名称不能为空",
            B0114: {
                required: false,
                number: "请输入正确的合同金额单位（万元）"
            },
            B0116: {
                required: false,
                number: "请输入正确的保障金单位（万元）"
            },
            FBR: "发包人不能为空",
            CBR: "承包人不能为空",
            B0112: "工程地点不能为空",
            CBRXMJLSFZH: "承包人项目经理身份证号不能为空",
            FBRDZ: "发包人公司地址不能为空",
            CBRDH: "承包人电话不能为空",
            CBRFDDBR: "承包人法定代表人不能为空",
            CBRDZ: "承包人地址",
            SSXZZGBM: "所属行政主管部门不能为空",
            LSJG: "项目所属行业主管部门所在地区编号不能为空",
            CBRXMJLXM: "承包人项目经理姓名不能为空",
            FXRQ: {
                required: "发薪日期不能为空",
                range: "发薪日期必须是介于1到31之间的数字"
            },
            SHXYDM: "项目法人社会信用代码不能为空",
            GZFFZHSSYH: "单位工资发放账户所属银行不能为空"
        }
    });

})

//日期类型控件
var SetTextBoxDateSelect = function () {
    var boxStr = [{ "boxname": "XMHTQDRQ" }, { "boxname": "B0109" }, { "boxname": "B0110" }];
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
}

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        UnitID: "",//$("#JianDie").val(),
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
        },{
            field: "UnitID",
            title: "编码",
            align: "center"
        },{
            field: "UnitType",
            title: "类型",
            align: "center",
            formatter: function (value, row, index) {
                return value == "N" ? "单位" : "部门";
            }
        },{
            field: "UnitName",
            title: "名称",
            align: "left"
        },{
            field: "B0107",
            title: "简称",
            align: "center"
        },{
            field: "B0108",
            title: "业主单位",
            align: "center"
        },{
            field: "B0120",
            title: "业主单位地址",
            align: "center"
        }
    ],
    Contoller: "TableFromData",
    Height: $.GetBodyHeight(),
    url: $.GetIISName() + "/Enterprise/GetUnitPerfectSoure",
    tool: "#table-tool"
};

var operion = {
    //编辑
    Edit: function (row) {
        operion.SetValue(row);
        $("#EditJianDie").val(row[0]["RowID"]);
        var index = layer.open({
            type: 1,
            area: ["80%", "80%"],
            title: "编辑单位/部门",
            fixed: false, //不固定
            maxmin: true,
            content: $("#add_area"),
            btn: ["保存", "返回"],
            yes: function (index, layero) {
                operion.EditSave();
            },
            btn2: function (index, layero) {
                //编辑保存并上报
                TextBoxOper.Null();
            },
            btn3: function (index, layero) {
                TextBoxOper.Null();
            },
            cancel: function () {
                TextBoxOper.Null();
            }
        });
        layer.full(index);
    },
    //编辑保存
    EditSave: function () {
        if ($("#unitAdd").valid()) {
            $.post($.GetIISName() + "/Enterprise/Edit", $("#unitAdd").serialize(), function (result) {
                if (result == "ok") {
                    TreeRefresh.Update();
                    tableHelp.refresh(table);
                    TextBoxOper.Null();
                    layer.closeAll();
                } else {
                    TextBoxOper.Null();
                    layer.closeAll();
                    layer.msg("编辑失败", { icon: 5 });
                }
            });
        } else {
            layer.msg("请填写必填项！");
        }
    },
    //赋值
    SetValue: function (row) {
        $("#UnitID").val(row[0]["UnitID"]);
        $("#UnitName").val(row[0]["UnitName"]);
        $("#B0107").val(row[0]["B0107"]);
        $("#B0108").val(row[0]["B0108"]);
        $("#B0120").val(row[0]["B0120"]);
        $("#D010H").val(row[0]["D010H"]);
        //$("#UnitStartDate").val(row[0]["UnitStartDate"].substring(0, 10));
        //$("#UnitEndDate").val(row[0]["UnitEndDate"].substring(0, 10));
        $("#B0111").val(row[0]["B0111"]);
        $("#B0112").val(row[0]["B0112"]);
        $("#B0114").val(row[0]["B0114"]);
        $("#B0116").val(row[0]["B0116"]);
        $("#B0118").val(row[0]["B0118"]);
        $("#B0180").val(row[0]["B0180"]);
        $("#UnitType").val(row[0]["UnitType"] == "M" ? "部门" : "单位");
        $.ajax({
            type: "post",
            dataType: "text",
            url: $.GetIISName() + "/Enterprise/GetEditDropValue",
            async: false,
            data: { RowID: row[0]["RowID"] },
            success: function (data) {
                $("#B0175").val(data.split(',')[0]);
                //$("#B0117").val(data.split(',')[1]);
            }
        });
        $("#B0181").val(row[0]["B0181"]);
        $("#project_status").val(row[0]["project_status"]);

        //上报数据
        $("#FBRDH").val(row[0]["FBRDH"]);
        $("#FBRZZ").val(row[0]["FBRZZ"]);
        if (row[0]["XMHTQDRQ"] != null && row[0]["XMHTQDRQ"] != "") {
            $("#XMHTQDRQ").val(row[0]["XMHTQDRQ"].substring(0, 10));
        }
        $("#CBRXMJLZCJZSZYYZH").val(row[0]["CBRXMJLZCJZSZYYZH"]);
        $("#CBRXMJLZCJZSZSZCBH").val(row[0]["CBRXMJLZCJZSZSZCBH"]);
        $("#CBRXMJLZCJZSZGZSBH").val(row[0]["CBRXMJLZCJZSZGZSBH"]);
        $("#CBRXMJLSFZH").val(row[0]["CBRXMJLSFZH"]);
        $("#CBRXMJLZC").val(row[0]["CBRXMJLZC"]);
        $("#CBRXMJLXM").val(row[0]["CBRXMJLXM"]);
        $("#QYHTJ").val(row[0]["QYHTJ"]);
        $("#XMJHZGQ").val(row[0]["XMJHZGQ"]);
        $("#ZBBH").val(row[0]["ZBBH"]);
        $("#HTBH").val(row[0]["HTBH"]);
        $("#ZJLY").val(row[0]["ZJLY"]);
        $("#GCLXPZWH").val(row[0]["GCLXPZWH"]);
        $("#CBR").val(row[0]["CBR"]);
        $("#FBR").val(row[0]["FBR"]);
        $("#BASBBH").val(row[0]["BASBBH"]);
        $("#BASBLX").val(row[0]["BASBLX"]);
        $("#SSXZZGBM").val(row[0]["SSXZZGBM"]);
        $("#CBRDZ").val(row[0]["CBRDZ"]);
        $("#CBRYZBM").val(row[0]["CBRYZBM"]);
        $("#CBRYHZH").val(row[0]["CBRYHZH"]);
        $("#CBRKHYH").val(row[0]["CBRKHYH"]);
        $("#CBRCZ").val(row[0]["CBRCZ"]);
        $("#CBRFDDBR").val(row[0]["CBRFDDBR"]);
        $("#CBRDH").val(row[0]["CBRDH"]);
        $("#CBRZZ").val(row[0]["CBRZZ"]);
        $("#FBRDZ").val(row[0]["FBRDZ"]);
        $("#FBRYZBM").val(row[0]["FBRYZBM"]);
        $("#FBRYHZH").val(row[0]["FBRYHZH"]);
        $("#FBRKHYH").val(row[0]["FBRKHYH"]);
        $("#FBRCZ").val(row[0]["FBRCZ"]);
        $("#FBRFDDBR").val(row[0]["FBRFDDBR"]);
        $("#SHXYDM").val(row[0]["SHXYDM"]);
        $("#GSDJZZHM").val(row[0]["GSDJZZHM"]);
        $("#GSDJZZZL").val(row[0]["GSDJZZZL"]);
        $("#ZZJGDM").val(row[0]["ZZJGDM"]);
        $("#FXRQ").val(row[0]["FXRQ"]);
        $("#LZZGY").val(row[0]["LZZGY"]);
        $("#XMBZJYCJE").val(row[0]["XMBZJYCJE"]);
        $("#SFCJGSBX").val(row[0]["SFCJGSBX"]);
        $("#SSDWYHID").val(row[0]["SSDWYHID"]);
        $("#FXMID").val(row[0]["FXMID"]);
        $("#XMCJR").val(row[0]["XMCJR"]);

        if (row[0]["SSWG"] != null && row[0]["SSWG"] != "") {
            $.ajax({
                type: "post",
                async: false,
                dataType: "text",
                url: $.GetIISName() + "/Enterprise/GetaAreaName",
                data: { code: row[0]["SSWG"] },
                success: function (result) {
                    $("#SSWG").val(result);
                }
            });
            $("#SSWG1").val(row[0]["SSWG"]);
        }

        if (row[0]["LSJG"] != null && row[0]["LSJG"] != "") {
            $.ajax({
                type: "post",
                async: false,
                dataType: "text",
                url: $.GetIISName() + "/Enterprise/GetaAreaName",
                data: { code: row[0]["LSJG"] },
                success: function (result) {
                    $("#LSJG").val(result);
                }
            })
        }
        $("#LSJG1").val(row[0]["LSJG"]);

        $("#in_compay").val(row[0]["in_compay"]);
        $("#GCLB").val(row[0]["GCLB"]);
        $("#FBDW").val(row[0]["FBDW"]);

        if (row[0]["B0109"] != null && row[0]["B0109"] != "" && row[0]["B0109"] != undefined)
            $("#B0109").val($.DateFormat(row[0]["B0109"]));
        if (row[0]["B0110"] != null && row[0]["B0110"] != "" && row[0]["B0110"] != undefined)
            $("#B0110").val($.DateFormat(row[0]["B0110"]));
        $("#JSDWLXR").val(row[0]["JSDWLXR"]);
        $("#JSDWLXRDH").val(row[0]["JSDWLXRDH"]);
        $("#SGDWLXR").val(row[0]["SGDWLXR"]);
        $("#SGDWLXRDH").val(row[0]["SGDWLXRDH"]);
        $("#LZZGYYI").val(row[0]["LZZGYYI"]);
        $("#LZZGYYILXFS").val(row[0]["LZZGYYILXFS"]);
        $("#LZZGYER").val(row[0]["LZZGYER"]);
        $("#LZZGYERLXFS").val(row[0]["LZZGYERLXFS"]);
        $("#LZZGYSAN").val(row[0]["LZZGYSAN"]);
        $("#LZZGYSANLXFS").val(row[0]["LZZGYSANLXFS"]);
        $("#GZFFZHSSYH").val(row[0]["GZFFZHSSYH"]);
        $("#upLoadId").val(row[0]["upLoadId"]);
    }
}

var TextBoxOper = {
    Null: function () {
        $("#UnitName").val("");
        $("#B0107").val("");
        $("#B0108").val("");
        $("#B0120").val("");
        $("#D010H").val("");
        $("#B0111").val("");
        $("#B0112").val("");
        $("#B0130").val("");
        $("#B0113").val("");
        $("#B0114").val("");
        $("#B0115").val("");
        $("#B0116").val("");
        $("#B0118").val("");
        $("#project_status").val("");
        $("#B0180").val("");
        $("#B0181").val("");

        $("#FBRDH").val("");
        $("#FBRZZ").val("");
        $("#XMHTQDRQ").val("");
        $("#CBRXMJLZCJZSZYYZH").val("");
        $("#CBRXMJLZCJZSZSZCBH").val("");
        $("#CBRXMJLZCJZSZGZSBH").val("");
        $("#CBRXMJLSFZH").val("");
        $("#CBRXMJLZC").val("");
        $("#CBRXMJLXM").val("");
        $("#QYHTJ").val("");
        $("#XMJHZGQ").val("");
        $("#ZBBH").val("");
        $("#HTBH").val("");
        $("#ZJLY").val("");
        $("#GCLXPZWH").val("");
        $("#CBR").val("");
        $("#FBR").val("");
        $("#BASBBH").val("");
        $("#BASBLX").val("");
        $("#SSXZZGBM").val("");
        $("#CBRDZ").val("");
        $("#CBRYZBM").val("");
        $("#CBRYHZH").val("");
        $("#CBRKHYH").val("");
        $("#CBRCZ").val("");
        $("#CBRFDDBR").val("");
        $("#CBRDH").val("");
        $("#CBRZZ").val("");
        $("#FBRDZ").val("");
        $("#FBRYZBM").val("");
        $("#FBRYHZH").val("");
        $("#FBRKHYH").val("");
        $("#FBRCZ").val("");
        $("#FBRFDDBR").val("");
        $("#LSJG").val("");
        $("#LSJG1").val("");
        $("#SHXYDM").val("");
        $("#GSDJZZHM").val("");
        $("#GSDJZZZL").val("");
        $("#ZZJGDM").val("");
        $("#FXRQ").val("");
        $("#LZZGY").val("");
        $("#XMBZJYCJE").val("");
        $("#SFCJGSBX").val("");
        $("#SSDWYHID").val("");
        $("#FXMID").val("");
        $("#XMCJR").val("");
        $("#SSWG").val("");
        $("#SSWG1").val("");

        $("#in_compay").val("");
        $("#GCLB").val("");
        $("#FBDW").val("");

        $("#B0109").val("");
        $("#B0110").val("");
        $("#JSDWLXR").val("");
        $("#JSDWLXRDH").val("");
        $("#SGDWLXR").val("");
        $("#SGDWLXRDH").val("");
        $("#LZZGYYI").val("");
        $("#LZZGYYILXFS").val("");
        $("#LZZGYER").val("");
        $("#LZZGYERLXFS").val("");
        $("#LZZGYSAN").val("");
        $("#LZZGYSANLXFS").val("");
        $("#GZFFZHSSYH").val("");
        $("#upLoadId").val("");
    }
}

var TreeRefresh = {
    Update: function () {
        $.ajax({
            type: "post",
            dataType: "text",
            async: false,
            url: $.GetIISName() + "/Enterprise/GetEnterTree",
            success: function (result) {
                treeHelp.BindTree("treeview", eval("(" + result + ")"), treeClick);
            }
        });
    }
}

var FBDW = {
    Load: function () {
        $("#FBDW").click(function () {
            var comUnit = $("#in_compay").val();
            if (comUnit != null && in_compay != "" && comUnit != undefined) {
                FBDW.GetSelectBox(comUnit);
            } else {
                layer.msg("请先选择施工单位！");
            }
        });
    },

    GetSelectBox: function (com_id) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: $.GetIISName() + "/Enterprise/GetFbdwSoure",
            data: { com_id: com_id },
            success: function (data) {
                if (data != null && data != "" && data != undefined) {
                    FBDW.SetFbdw(data);
                } else
                    layer.msg("所选施工单位并未包含分包公司！");
            }
        })
    },

    SetFbdw: function (data) {
        var valEd = $("#FBDW").val();
        var content = "<table id='tbCheckDraw' width='100%'>";
        //<input type="checkbox" name="" value="" checked="checked" />
        $.each(data, function (index, element) {
            var value = data[index]["dwmc"];
            content += "<tr onclick='FBDW.SetCheckBoxIndex(" + index + ")' style=' height:25px; width:100%;border-bottom:1px dashed gray;' onmouseover=$.myover() onmouseout=$.myout() >";
            if (valEd.indexOf(value) >= 0) {
                content += "<td><input type='checkbox' id='ck" + index + "' style='margin:0; width:20px; height:20px;' class='checkbox' name='" + value + "' value='" + value + "' checked='checked' /></td>";
            } else {
                content += "<td><input type='checkbox' id='ck" + index + "' style='margin:0; width:20px; height:20px;' class='checkbox' name='" + value + "' value='" + value + "' /></td>";
            }
            content += "<td><label id='lb" + index + "' style='margin:0; height:20px; vertical-align:center; '  style='font-size:14px;'>" + value + "</label></td>";
            content += "</tr>";
        });
        content += "</table>";
        layer.open({
            type: 1,
            title: false,
            content: content,
            area: ["400px", "400px"],
            maxmin: false,
            btn: ["保存", "返回"],
            yes: function (index, layero) {
                $("#FBDW").val("");
                var FBDW = "";
                $(".checkbox").each(function () {
                    if ($(this).attr("id").substring(0, 2) == "ck") {
                        if ($(this).is(":checked")) {
                            if (FBDW == "")
                                FBDW = $(this).val();
                            else
                                FBDW += ";" + $(this).val();
                        }
                    }
                });
                $("#FBDW").val(FBDW);
                layer.close(index);
            },
            btn2: function (index, layero) {
                layer.close(index);
            },
        });
    },

    SetCheckBoxIndex: function (index) {
        if ($("#ck" + index + "").attr("checked")) {
            $("#ck" + index + "").attr("checked", false);
            $("#ck" + index + "").prop("checked", false);
        } else {
            $("#ck" + index + "").attr("checked", true);
            $("#ck" + index + "").prop("checked", true);
        }
    }
}