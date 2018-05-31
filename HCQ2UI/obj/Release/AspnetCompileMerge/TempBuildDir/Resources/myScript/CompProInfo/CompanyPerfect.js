var arJson = [
    { "name": "dwbh" },
    { "name": "dwmc" },
    { "name": "tyshxydm" },
    { "name": "zzjgdm" },
    { "name": "gsdjzzhm" },
    { "name": "xxly" },
    { "name": "gsdjzzzl" },
    { "name": "gsdjyxqxqs" },
    { "name": "qsdjyxqxzz" },
    { "name": "Shbxdjzbm" },
    { "name": "Fddbrxm" },
    { "name": "Fddbrsfzhm" },
    { "name": "Fddbrdh" },
    { "name": "Dwlx" },
    { "name": "Jjlx" },
    { "name": "Lsgx" },
    { "name": "Jyfs" },
    { "name": "Zczb" },
    { "name": "ZYFW" },
    { "name": "JYFW" },
    { "name": "XZQHDM" },
    { "name": "DJZCLX" },
    { "name": "FDDBRZW" },
    { "name": "DWFZRXM" },
    { "name": "DWFZRZW" },
    { "name": "DWFZRDH" },
    { "name": "ZCDZ" },
    { "name": "LZFZR" },
    { "name": "LZFZRSFZHM" },
    { "name": "LZFZRZW" },
    { "name": "LZFZRDH" },
    { "name": "SSHY" },
    { "name": "DWQTLXFS" },
    { "name": "LXR" },
    { "name": "LXDH" },
    { "name": "BGDZ" },
    { "name": "YZBM" },
    { "name": "DWJBZHKHYH" },
    { "name": "DWJBZHKHMC" },
    { "name": "DWJBZHZH" },
    { "name": "CZHM" },
    { "name": "DZYX" },
    { "name": "WZ" },
    { "name": "JGZSBH" },
    { "name": "LYYJY" },
    { "name": "JGLB" },
    { "name": "SSWG" },
    { "name": "DWMCPY" },
    { "name": "FDDBRZJHM" },
    { "name": "ZGBM" },
    { "name": "tiISYLWPQJYXMtle" },
    { "name": "BZ" },
    { "name": "SSKS" },
    { "name": "CZRXM" },
    { "name": "CZRDZ" },
    { "name": "CZRLXDH" },
    { "name": "ZLQX" },
    { "name": "CZRFBDR" },
    { "name": "ISYYZX" },
    { "name": "YRDWWHSJ" },
    { "name": "SJDW" },
    { "name": "LZJBRXM" },
    { "name": "LZJBRDH" },
    { "name": "LDJCBH" },
    { "name": "STDJZH" },
    { "name": "QXLB" }
]

$(function () {
    //日期控件
    SetTextBoxDateSelect();

    //首次加载table
    tableHelp.LoadData(Table.SetTable(), Table.queryParams);

    $("#btnSearch").click(function () {
        tableHelp.refresh(Table.SetTable());
    });

    $("#editCom").click(function () {
        var row = $("#TableFromData").bootstrapTable('getSelections');
        if (row != null && row != "" && row != undefined) {
            $("#EditComID").val(row[0]["com_id"]);
            Opertion.SetValue(row);
            layer.open({
                type: 1,
                area: ["80%", "80%"],
                title: "编辑企业",
                fixed: false, //不固定
                maxmin: true,
                content: $("#add_area"),
                btn: ["保存", "返回"],
                yes: function (index, layero) {
                    Opertion.Save();
                },
                btn2: function (index, layero) {
                    Opertion.SetNull();
                    layer.close(index);
                },
                cancel: function (index, layero) {
                    Opertion.SetNull();
                    layer.close(index);
                }
            });
        } else {
            layer.msg("请选择需要编辑的企业！", { icon: 5 });
        }

    });

    //身份证号码验证
    jQuery.validator.addMethod("isIdCardNo", function (value, element) {
        return this.optional(element) || isIdCardNo(value);
    }, "请输入正确的身份证号码");

    //表单验证
    $("#addComData").validate({
        rules: {
            QXLB: "required",
            dwmc: "required",
            tyshxydm: "required",
            Fddbrxm: "required",
            Fddbrdh: "required",
            LZFZR: "required",
            LZFZRSFZHM: {
                required: true,
                isIdCardNo: true,
            },
            LZFZRDH: "required",
            SSWG: "required",
            Fddbrsfzhm: {
                required: false,
                isIdCardNo: true
            },
            LZFZRSFZHM: {
                required: false,
                isIdCardNo: true
            },
            ZCDZ: "required",
            LZFZRSFZHM: {
                required: true,
                isIdCardNo: true,
            },
            Zczb: {
                required: false,
                digits: true
            }
        },
        messages: {
            QXLB: "类别不能为空",
            dwmc: "单位名称不能为空",
            tyshxydm: "统一社会信用代码不能为空",
            Fddbrxm: "法定代表人姓名不能为空",
            Fddbrdh: "法定代表人电话不能为空",
            LZFZR: "劳资负责人姓名不能为空",
            LZFZRSFZHM: {
                required: "劳资负责人公民身份号码不能为空",
                isIdCardNo: "请输入正确的身份证号码"
            },
            LZFZRDH: "劳资负责人联系电话",
            SSWG: "所属网格不能为空",
            Fddbrsfzhm: {
                required: "",
                isIdCardNo: "请输入正确的身份证号码"
            },
            LZFZRSFZHM: {
                required: "法定代表人身份证号码不能为空",
                isIdCardNo: "请输入正确的身份证号码"
            },
            ZCDZ: "注册地址不能为空",
            LZFZRSFZHM: {
                required: "劳资负责人公民身份号码不能为空",
                isIdCardNo: "请输入正确的身份证号码",
            },
            Zczb: {
                required: "",
                digits: "请输入正确的注册资本金额(单位元)"
            }
        }
    });
});

//日期类型控件
var SetTextBoxDateSelect = function () {
    var boxStr = [{ "boxname": "gsdjyxqxqs" }, { "boxname": "qsdjyxqxzz" }, { "boxname": "YRDWWHSJ" }];
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

var Table = {

    queryParams: function (params) {
        var pageIndex = params.offset / params.limit + 1
        var temp = {
            page: pageIndex,
            rows: params.limit,
            com_id: $("#JianDieUnitID").val(),
            txtSearchName: $("#txtSearchName").val()
        };
        return temp;
    },

    SetTable: function () {
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
                    field: "dwmc",
                    title: "单位名称",
                    align: "left"
                }, {
                    field: "QXLB",
                    title: "类别",
                    align: "center",
                    formatter: function (value, row, index) {
                        return GetTypeNameByCode(value);
                    }
                }, {
                    field: "tyshxydm",
                    title: "统一社会信用代码",
                    align: "center"
                }, {
                    field: "zzjgdm",
                    title: "组织机构代码",
                    align: "center"
                }, {
                    field: "gsdjzzhm",
                    title: "工商执照号码",
                    align: "center"
                }, {
                    field: "Fddbrxm",
                    title: "法定代表人姓名",
                    align: "center"
                }, {
                    field: "Fddbrsfzhm",
                    title: "法定代表人身份证号码",
                    align: "center"
                }
            ],
            Contoller: "TableFromData",
            Height: window.innerHeight - 2,
            url: $.GetIISName() + "/CompProInfo/GetCompanyPerfectSoure",
            tool: $("#table-tool")
        };
        return table;
    }
};

var Opertion = {

    //保存
    Save: function () {
        if ($("#addComData").valid()) {
            $.post($.GetIISName() + "/CompProInfo/AddComInfo", $("#addComData").serialize(), function (result) {
                if (result == "ok") {
                    tableHelp.refresh(Table.SetTable());
                    layer.closeAll();
                    Opertion.SetNull();
                } else {
                    layer.msg("保存失败", { icon: 5 });
                }
            });
        }
    },

    SetValue: function (row) {
        $.each(arJson, function (index, value) {
            switch (arJson[index].name) {
                case "YRDWWHSJ":
                    if (row[0]["YRDWWHSJ"] != null && row[0]["YRDWWHSJ"] != "" && row[0]["YRDWWHSJ"] != undefined) {
                        $("#YRDWWHSJ").val($.DateFormat(row[0]["YRDWWHSJ"]));
                    }
                    break;
                case "SSWG":
                    $.ajax({
                        type: "post",
                        async: false,
                        dataType: "text",
                        url: $.GetIISName() + "/Enterprise/GetaAreaName",
                        data: { code: row[0][arJson[index].name] },
                        success: function (result) {
                            $("#" + arJson[index].name + "").val(result);
                        }
                    });
                    $("#SSWG1").val(row[0]["SSWG"]);
                    break;
                default:
                    $("#" + arJson[index].name + "").val(row[0][arJson[index].name]);
                    break;
            }
            
        });
    },

    SetNull: function () {
        $.each(arJson, function (index, value) {
            $("#" + arJson[index].name + "").val("");
        });
        $("#EditComID").val("");
        $("#JianDieUnitID").val("");
    }

}
