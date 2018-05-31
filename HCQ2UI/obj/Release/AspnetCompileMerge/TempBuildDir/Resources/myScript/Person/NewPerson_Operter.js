$(function () {
    SetTextBoxDateSelect();
    // 手机号码验证
    jQuery.validator.addMethod("isMobile", function (value, element) {
        var length = value.length;
        var mobile = /^(13[0-9]{9})|(18[0-9]{9})|(14[0-9]{9})|(17[0-9]{9})|(15[0-9]{9})$/;
        return this.optional(element) || (length == 11 && mobile.test(value));
    });
    //身份证号码验证
    jQuery.validator.addMethod("isIdCardNo", function (value, element) {
        return this.optional(element) || isIdCardNo(value);
    }, "请输入正确的身份证号码");

    //表单验证
    $("#PersonDetail").validate({
        rules: {
            A0101: "required",
            A0177: {
                required: true,
                isIdCardNo: true
            },
            B0001: "required",
            A0179: {
                required: false,
                dateISO: true
            },
            A0178: {
                required: false,
                number: true
            },
            E0368: {
                required: false,
                number: true
            },
            C0101: {
                required: false,
                digits: true
            },
            A0116: {
                required: false,
                dateISO: true
            },
            A0117: {
                required: false,
                dateISO: true
            },
            C0102: {
                required: false,
                email: true
            },
            C0104: {
                required: false,
                minlength: 11,
                isMobile: true
            },
            SSDWZW: "required",
            GZGZHDFS: "required",
            GZGZHDBZ: "required",
            SFQDJYYGHT: "required",
            NMGZHSSYH: "required",
            A0146: "required"
        },
        messages: {
            A0101: "姓名不能为空",
            A0177: {
                required: "身份证号码不能为空",
                isIdCardNo: "请输入正确的身份证号码"
            },
            B0001: "所属项目不能为空",
            A0179: {
                required: "",
                dateISO: "请选择正确的日期格式"
            },
            A0178: {
                required: false,
                number: "请填写正确的数字"
            },
            E0368: {
                required: false,
                number: "请填写正确的数字"
            },
            C0101: {
                required: false,
                digits: "请填写正确的数字"
            },
            A0116: {
                required: false,
                dateISO: "请选择正确的日期格式"
            },
            A0117: {
                required: false,
                dateISO: "请选择正确的日期格式"
            },
            C0102: {
                required: false,
                email: "请填写正确的邮箱"
            },
            C0104: {
                required: false,
                minlength: "请输入正确的手机号码",
                isMobile: "请输入正确的手机号码"
            },
            SSDWZW: "所属单位职务不能为空",
            GZGZHDFS: "工种工资核定方式不能为空",
            GZGZHDBZ: "工种工资核定标准不能为空",
            SFQDJYYGHT: "是否签订简易用工合同不能为空",
            NMGZHSSYH: "农民工账户所属银行不能为空",
            A0146: "农民工银行卡号不能为空"
        }
    });

    //虹膜采集
    Iris.Load();

})

//图片上传
$("#PersonPhoto").change(function () {
    $.ajaxFileUpload({
        type: "post",
        url: $.GetIISName() + "/Person/PersonPhoto",
        secureuri: false,
        fileElementId: "PersonPhoto",
        dataType: "text",
        success: function (result) {
            if (result.indexOf(".") > 0)
                $("#PersonPhotoSrc").val(result);
            $("#person_image").attr("src", "../Person/" + result);
        }
    })
})

//合同扫描件
$("#scan_contractFile").change(function () {
    $.ajaxFileUpload({
        type: "post",
        url: $.GetIISName() + "/Person/PersonScan",
        secureuri: false,
        fileElementId: "scan_contractFile",
        dataType: "text",
        success: function (result) {
            if (result.indexOf(".") > 0) {
                $("#scan_contract").val(result);
                $("#person_scan_img").attr("src", result.replace("~", ".."));
            }
        }
    })
})

//日期类型控件
var SetTextBoxDateSelect = function () {
    var boxStr = [{ "boxname": "A0179" }, { "boxname": "E0359" }, { "boxname": "A0111" }
        , { "boxname": "A0116" }, { "boxname": "A0117" }, { "boxname": "A0119" }
    , { "boxname": "A0415" }, { "boxname": "A0430" }, { "boxname": "A1905" }, { "boxname": "A1910" }];
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

$("#btnSave").click(function () {
    if ($("#PersonDetail").valid()) {
        var validataEditId = $("#RowID").val();
        if (validataEditId != null && validataEditId != "") {
            SubmitFormPerson();
        } else {
            //新增要验证用户是否重复注册
            $.ajax({
                type: "post",
                dataType: "text",
                async: false,
                data: $("#PersonDetail").serialize(),
                url: $.GetIISName() + "/Person/ValidateUnitPerson",
                success: function (result) {
                    if (result != "ok")
                        SubmitFormPerson();
                    else
                        layer.msg("该身份证号码在该项目已存在！");
                }, error: function () {
                    layer.msg("未知错误！");
                }
            })
        }
    }
});

var SubmitFormPerson = function () {
    $.post($.GetIISName() + "/Person/InsertNewPerson", $("#PersonDetail").serialize(), function (result) {
        if (result != "") {
            var data = result.split(',');
            if (data[0] == "ok") {
                $("#RowID").val(data[1]);
                layer.msg("保存成功", { icon: 6 });
            } else {
                layer.msg("保存失败", { icon: 5 });
            }
        } else {
            layer.msg("保存失败", { icon: 5 });
        }
    })
}

$("#btnClose").click(function () {
    TextBoxOper();
    //parent.location.reload();//刷新父窗口    
    window.parent.SetFatherFuc();
    parent.layer.closeAll();//关闭所有layer窗口  
});

//判断Json是否为空，如果为空则为新增，不为空则为编辑，编辑的话需要给文本框赋值
var SetTextBoxValue = function (row) {
    if (row != null && row != "") {
        $("#RowID").val(row[0].RowID);
        $("#B00011").val(row[0].B0001);
        $("#B00021").val(row[0].B0002);
        $("#B0002011").val(row[0].B000201);
        $("#B0002021").val(row[0].B000202);

        $("#A0101").val(row[0].A0101);
        $("#C0104").val(row[0].C0104);
        $("#A0178").val(row[0].A0178);
        if (row[0].A0179 != null && row[0].A0179 != "" && row[0].A0179 != undefined) {
            $("#A0179").val(row[0].A0179.replace("0:00:00", "").replace(/\//g, '-').replace(" ", ""));
        }
        $("#E0386").val(row[0].E03861);
        $("#E0387").val(row[0].E0387);
        $("#B0001").val(row[0].B0001x);
        $("#B0002").val(row[0].B0002x);
        $("#B000201").val(row[0].B000201x);
        $("#B000202").val(row[0].B000202x);
        $("#E0359").val(row[0].E0359);
        $("#A0180").val(row[0].A0180);
        $("#E0394").val(row[0].E0394);
        $("#E0368").val(row[0].E0368);
        $("#A0177").val(row[0].A0177);
        $("#A0111").val(row[0].A0111);
        $("#A0107").val(row[0].A01071);
        $("#C0101").val(row[0].C0101);
        $("#A0108").val(row[0].A01081);
        $("#A0121").val(row[0].A01211);
        $("#A0114").val(row[0].A01141);
        $("#A0115").val(row[0].A0115);
        $("#A0109").val(row[0].A0109);
        $("#A0112").val(row[0].A0112);

        if (row[0].A0116 != null && row[0].A0116 != "" && row[0].A0116 != undefined)
            $("#A0116").val(row[0].A0116.replace("0:00:00", "").replace(/\//g, '-').replace(" ", ""));
        if (row[0].A0117 != null && row[0].A0117 != "" && row[0].A0117 != undefined)
            $("#A0117").val(row[0].A0117.replace("0:00:00", "").replace(/\//g, '-').replace(" ", ""));

        $("#A0110").val(row[0].A01101);
        $("#A0127").val(row[0].A01271);
        $("#A0128").val(row[0].A01281);
        $("#C01SS").val(row[0].C01SS);
        $("#C01RV").val(row[0].C01RV);
        $("#C0102").val(row[0].C0102);
        $("#A0146").val(row[0].A0146);
        $("#A0119").val(row[0].A0119);
        $("#A0122").val(row[0].A0122);
        $("#A0181").val(row[0].A0181);
        $("#scan_contract").val(row[0].scan_contract);
        $("#in_team").val(row[0].in_team);

        $("#SSDWLB").val(row[0].SSDWLB);
        $("#SSDWZW").val(row[0].SSDWZW);
        $("#GZGZHDFS").val(row[0].GZGZHDFS);
        $("#GZGZHDBZ").val(row[0].GZGZHDBZ);
        $("#SSBZ").val(row[0].SSBZ);
        $("#SFBZFZR").val(row[0].SFBZFZR);
        $("#SFQDJYYGHT").val(row[0].SFQDJYYGHT);
        $("#lzyy").val(row[0].lzyy);

        $("#A0146").val(row[0].A0146);
        $("#NMGZHSSYH").val(row[0].NMGZHSSYH);

        $.ajax({
            type: "post",
            dataType: "text",
            data: { RowID: row[0].RowID },
            url: $.GetIISName() + "/Person/GetBugIrisStr",
            async: false,
            success: function (data) {
                $("#big_iris").val(data);
            }
        })
    }
}

//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        RowID: $("#RowID").val()
    };
    return temp;
}

//档案袋信息
var Detail = {
    Edu: function () {
        //构建传入参数
        var table = {
            Columns: [
                {
                    radio: true
                },
                {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1
                    }
                },
                {
                    field: "A0415",
                    title: "入学时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                    }
                }
                ,
                {
                    field: "A0430",
                    title: "毕业时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                    }
                }
                ,
                {
                    field: "A0405",
                    title: "学历",
                    align: "center"
                }
                ,
                {
                    field: "A0435",
                    title: "毕业院校",
                    align: "center"
                }
                ,
                {
                    field: "A0410",
                    title: "所学专业",
                    align: "center"
                },
                {
                    field: "C0401",
                    title: "教育方式",
                    align: "center"
                }

            ],
            Contoller: "TableFromEdu",
            Height: 358,
            url: $.GetIISName() + "/Person/GetEdu",
            tool: "#eduTool"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    Work: function () {
        //构建传入参数
        var table = {
            Columns: [
                {
                    radio: true
                },
                {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1
                    }
                },
                {
                    field: "A1915",
                    title: "所在公司名称",
                    align: "center"
                }
                ,
                {
                    field: "A1905",
                    title: "起始时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                    }
                }
                ,
                {
                    field: "A1905",
                    title: "终止时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                    }
                }
                ,
                {
                    field: "A1927",
                    title: "所在部门",
                    align: "center"
                }
                ,
                {
                    field: "A1920",
                    title: "所在岗位",
                    align: "center"
                },
                {
                    field: "A1928",
                    title: "工作内容",
                    align: "center"
                },
                {
                    field: "A1929",
                    title: "薪资待遇",
                    align: "center"
                },
                {
                    field: "A1930",
                    title: "离职原因",
                    align: "center"
                },
                {
                    field: "A1925",
                    title: "证明人",
                    align: "center"
                },
                {
                    field: "A1926",
                    title: "证明人联系方式",
                    align: "center"
                }

            ],
            Contoller: "TableFromWork",
            Height: 358,
            url: $.GetIISName() + "/Person/GetWork",
            tool: "#workTool"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    Attendance: function () {
        //构建传入参数
        var table = {
            Columns: [
                {
                    radio: true
                },
                {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1
                    }
                },
                {
                    field: "A0201",
                    title: "签到时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value.replace("T", " ");
                    }
                }

            ],
            Contoller: "TableFromAttendance",
            Height: 358,
            url: $.GetIISName() + "/Person/GetPersonAttendance",
            tool: $("#AttendanceTool")
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    Wage: function () {
        //构建传入参数
        var table = {
            Columns: [
                {
                    radio: true
                },
                {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                },
                {
                    field: "A0301",
                    title: "签到时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != "" && value != null ? value.replace("T", " ") : "";
                    }
                },
                {
                    field: "A0302",
                    title: "发放月份",
                    align: "center"
                }

            ],
            Contoller: "TableFromWage",
            Height: 358,
            url: $.GetIISName() + "/Person/GetPersonWage",
            tool: "#WageTool"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    }
}

//基本信息轮空处理
var TextBoxOper = function () {
    $("#A0101").val("");
    $("#C0104").val("");
    $("#A0178").val("");
    $("#A0179").val("");
    $("#E0386").val("");
    $("#E0387").val("");
    $("#B0001").val("");
    $("#B0002").val("");
    $("#B000201").val("");
    $("#B000202").val("");
    $("#E0359").val("");
    $("#A0180").val("");
    $("#E0394").val("");
    $("#E0368").val("");
    $("#PersonPhoto").val("");
    $("#A0177").val("");
    $("#A0111").val("");
    $("#A0107").val("");
    $("#C0101").val("");
    $("#A0108").val("");
    $("#A0121").val("");
    $("#A0114").val("");
    $("#A0115").val("");
    $("#A0109").val("");
    $("#A0112").val("");
    $("#A0116").val("");
    $("#A0117").val("");
    $("#A0110").val("");
    $("#A0127").val("");
    $("#A0128").val("");
    $("#C01SS").val("");
    $("#C01RV").val("");
    $("#C0102").val("");
    $("#A0146").val("");
    $("#A0119").val("");
    $("#A0120").val("");
    $("#A0122").val("");
    $("#A0180").val("");
    $("#in_team").val("");

    $("#SSDWLB").val("");
    $("#SSDWZW").val("");
    $("#GZGZHDFS").val("");
    $("#GZGZHDBZ").val("");
    $("#SSBZ").val("");
    $("#SFBZFZR").val("");
    $("#SFQDJYYGHT").val("");
    $("#lzyy").val("");

    $("#A0146").val("");
    $("#NMGZHSSYH").val("");
}

//-----------------------------------------------------------
//------------学历档案袋
//-----------------------------------------------------------
$("#btnEdu").click(function () {
    var RowID = $("#RowID").val();
    if (RowID != "" && RowID != null && RowID != undefined) {
        $("#EduRowID").val(RowID);
        Detail.Edu();
        $("#eduTool").css("display", "block");
        layer.open({
            type: 1,
            title: "学位学历信息",
            content: $("#edu"),
            area: ["800px", "400px"],
            maxmin: true,
            cancel: function () {
                $("#eduTool").css("display", "none");
                EduTextBoxNull();
            }
        });
    }
});

$("#edu_delete").click(function () {
    var row = $("#TableFromEdu").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        layer.confirm("确定要删除该学历学位吗？", { btn: ["确定", "取消"] }, function (index) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/Person/DeletePersonEdu",
                data: { RowID: row[0].RowID },
                success: function (result) {
                    if (result == "ok") {
                        layer.close(index);
                        tableHelp.refresh(EdtTableRe());
                    } else
                        layer.msg("删除失败", { icon: 5 });
                }
            });
        })
    } else {
        layer.msg("请选择需要删除的学历学位！", { icon: 5 });
    }
});

$("#edu_edit").click(function () {
    var row = $("#TableFromEdu").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        if (row[0].A0415 != null && row[0].A0415 != "" && row[0].A0415 != undefined)
            $("#A0415").val(row[0].A0415.replace("T00:00:00", ""));
        if (row[0].A0430 != null && row[0].A0430 != "" && row[0].A0430 != undefined)
            $("#A0430").val(row[0].A0430.replace("T00:00:00", ""));
        $("#A0405").val(row[0].A0405);
        $("#A0435").val(row[0].A0435);
        $("#A0410").val(row[0].A0410);
        $("#C0401").val(row[0].C0401);
        $("#EduIsEdit").val(row[0].RowID);
    } else {
        layer.msg("请选择需要编辑的学历学位！", { icon: 5 });
    }
});

$("#edu_save").click(function () {
    var A0405 = $("#A0405").val();
    if (A0405 != "" && A0405 != null && A0405 != undefined) {
        $.post($.GetIISName() + "/Person/PersonEduAdd", $("#eduForm").serialize(), function (result) {
            if (result == "ok") {
                $("#EduIsEdit").val("");
                EduTextBoxNull();
                tableHelp.refresh(EdtTableRe());
            }
        });
    } else {
        layer.msg("学历不能为空！", { icon: 5 });
    }
});

var EdtTableRe = function () {
    var table = {
        Columns: [
            {
                radio: true
            },
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1
                }
            },
            {
                field: "A0415",
                title: "入学时间",
                align: "center",
                formatter: function (value, row, index) {
                    return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                }
            }
                ,
                {
                    field: "A0430",
                    title: "毕业时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                    }
                }
            ,
            {
                field: "A0405",
                title: "学历",
                align: "center"
            }
            ,
            {
                field: "A0435",
                title: "毕业院校",
                align: "center"
            }
            ,
            {
                field: "A0410",
                title: "所学专业",
                align: "center"
            },
            {
                field: "C0401",
                title: "教育方式",
                align: "center"
            }

        ],
        Contoller: "TableFromEdu",
        Height: 358,
        url: $.GetIISName() + "/Person/GetEdu",
        tool: "#eduTool"
    };
    return table;
}

var EduTextBoxNull = function () {
    $("#A0415").val("");
    $("#A0430").val("");
    $("#A0405").val("");
    $("#A0435").val("");
    $("#A0410").val("");
    $("#C0401").val("");
}

//-----------------------------------------------------------
//------------工作经历档案袋
//-----------------------------------------------------------
$("#btnWork").click(function () {
    var RowID = $("#RowID").val();
    if (RowID != "" && RowID != null && RowID != undefined) {
        $("#workRowID").val(RowID);
        Detail.Work();
        $("#workTool").css("display", "block");
        layer.open({
            type: 1,
            title: "工作经历信息",
            content: $("#work"),
            area: ["1000px", "400px"],
            maxmin: true,
            cancel: function () {
                $("#eduTool").css("display", "none");
            }
        });
    }
});

$("#work_save").click(function () {
    var A1915 = $("#A1915").val();
    if (A1915 != "" && A1915 != null && A1915 != undefined) {
        $.post($.GetIISName() + "/Person/PersonWorkAdd", $("#workForm").serialize(), function (result) {
            if (result == "ok") {
                tableHelp.refresh(worktTableRe());
                SetWorkTextNul();
            }
            $("#workIsEdit").val("");
        });
    } else {
        layer.msg("所在公司名称不能为空！", { icon: 5 });
    }
});

$("#work_edit").click(function () {
    var row = $("#TableFromWork").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        if (row[0].A1905 != null && row[0].A1905 != "" && row[0].A1905 != undefined)
            $("#A1905").val(row[0].A1905.replace("T00:00:00", ""));
        if (row[0].A1910 != null && row[0].A1910 != "" && row[0].A1910 != undefined)
            $("#A1910").val(row[0].A1910.replace("T00:00:00", ""));
        $("#A1915").val(row[0].A1915);
        $("#A1920").val(row[0].A1920);
        $("#A1925").val(row[0].A1925);
        $("#A1926").val(row[0].A1926);
        $("#A1927").val(row[0].A1927);
        $("#A1928").val(row[0].A1928);
        $("#A1929").val(row[0].A1929);
        $("#A1930").val(row[0].A1930);
        $("#workIsEdit").val(row[0].RowID);
    } else {
        layer.msg("请选择需要编辑的工作经历！", { icon: 5 });
    }
});

$("#work_delete").click(function () {
    var row = $("#TableFromWork").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        layer.confirm("确定要删除该工作经历吗？", { btn: ["确定", "取消"] }, function (index) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/Person/DeletePersonWork",
                data: { RowID: row[0].RowID },
                success: function (result) {
                    if (result == "ok") {
                        layer.close(index);
                        SetWorkTextNul();
                        tableHelp.refresh(worktTableRe());
                    } else
                        layer.msg("删除失败", { icon: 5 });
                }
            });
        })
    } else {
        layer.msg("请选择需要删除的工作经历！", { icon: 5 });
    }
});

var worktTableRe = function () {
    //构建传入参数
    var table = {
        Columns: [
            {
                radio: true
            },
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1
                }
            },
            {
                field: "A1915",
                title: "所在公司名称",
                align: "center"
            }
            ,
            {
                field: "A1905",
                title: "起始时间",
                align: "center",
                formatter: function (value, row, index) {
                    return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                }
            }
            ,
            {
                field: "A1905",
                title: "终止时间",
                align: "center",
                formatter: function (value, row, index) {
                    return value != null && value != "" ? value.replace("T00:00:00", "") : "";
                }
            }
            ,
            {
                field: "A1927",
                title: "所在部门",
                align: "center"
            }
            ,
            {
                field: "A1920",
                title: "所在岗位",
                align: "center"
            },
            {
                field: "A1928",
                title: "工作内容",
                align: "center"
            },
            {
                field: "A1929",
                title: "薪资待遇",
                align: "center"
            },
            {
                field: "A1930",
                title: "离职原因",
                align: "center"
            },
            {
                field: "A1925",
                title: "证明人",
                align: "center"
            },
            {
                field: "A1926",
                title: "证明人联系方式",
                align: "center"
            }

        ],
        Contoller: "TableFromWork",
        Height: 358,
        url: $.GetIISName() + "/Person/GetWork",
        tool: "#workTool"
    };
    return table;
}

var SetWorkTextNul = function () {
    $("#A1905").val("");
    $("#A1910").val("");
    $("#A1915").val("");
    $("#A1920").val("");
    $("#A1925").val("");
    $("#A1926").val("");
    $("#A1927").val("");
    $("#A1928").val("");
    $("#A1929").val("");
    $("#A1930").val("");
}

//-----------------------------------------------------------
//------------考勤情况档案袋
//-----------------------------------------------------------
$("#btnAttendance").click(function () {
    var RowID = $("#RowID").val();
    if (RowID != "" && RowID != null && RowID != undefined) {
        $("#AttendanceRowID").val(RowID);
        Detail.Attendance();
        $("#AttendanceTool").css("display", "block");
        layer.open({
            type: 1,
            title: "考勤情况",
            content: $("#Attendance"),
            area: ["700px", "400px"],
            maxmin: true,
            cancel: function () {
                $("#AttendanceTool").css("display", "none");
            }
        });
    }
});

$("#Attendance_save").click(function () {
    var A0201 = $("#A0201").val();
    if (A0201 != "" && A0201 != null && A0201 != undefined) {
        $.post($.GetIISName() + "/Person/PersonAttendanceAdd", $("#AttendanceForm").serialize(), function (result) {
            if (result == "ok") {
                tableHelp.refresh(AttendancetTableRe());
                $("#A0201").val("");
            }
            $("#AttendanceIsEdit").val("");
        });
    } else {
        layer.msg("签到时间不能为空！", { icon: 5 });
    }
});

$("#Attendance_edit").click(function () {
    var row = $("#TableFromAttendance").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        if (row[0].A0201 != null && row[0].A0201 != null && row[0].A0201 != undefined)
            $("#A0201").val(row[0].A0201.replace("T", " "));
        $("#AttendanceIsEdit").val(row[0].RowID);
    } else {
        layer.msg("请选择需要编辑的考勤情况！", { icon: 5 });
    }
});

$("#Attendance_delete").click(function () {
    var row = $("#TableFromAttendance").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        layer.confirm("确定要删除该考勤情况吗？", { btn: ["确定", "取消"] }, function (index) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/Person/DeletePersonAttendance",
                data: { RowID: row[0].RowID },
                success: function (result) {
                    if (result == "ok") {
                        layer.close(index);
                        $("#A0201").val("");
                        tableHelp.refresh(AttendancetTableRe());
                    } else
                        layer.msg("删除失败", { icon: 5 });
                }
            });
        })
    } else {
        layer.msg("确定要删除该考勤情况吗！", { icon: 5 });
    }
});

var AttendancetTableRe = function () {
    var table = {
        Columns: [
            {
                radio: true
            },
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1
                }
            },
            {
                field: "A0201",
                title: "签到时间",
                align: "center",
                formatter: function (value, row, index) {
                    return value.replace("T", " ");
                }
            }

        ],
        Contoller: "TableFromAttendance",
        Height: 358,
        url: $.GetIISName() + "/Person/GetPersonAttendance",
        tool: $("#AttendanceTool")
    };
    return table;
}

//-----------------------------------------------------------
//------------员工工资发放登记档案袋
//-----------------------------------------------------------
$("#btnWage").click(function () {
    var RowID = $("#RowID").val();
    if (RowID != "" && RowID != null && RowID != undefined) {
        $("#WageRowID").val(RowID);
        Detail.Wage();
        $("#WageTool").css("display", "block");
        layer.open({
            type: 1,
            title: "员工工资发放登记",
            content: $("#Wage"),
            area: ["700px", "400px"],
            maxmin: true,
            cancel: function () {
                $("#WageTool").css("display", "none");
            }
        });
    }
});

$("#Wage_save").click(function () {
    var A0301 = $("#A0301").val();
    if (A0301 != "" && A0301 != null && A0301 != undefined) {
        $.post($.GetIISName() + "/Person/PersonWageAdd", $("#WageForm").serialize(), function (result) {
            if (result == "ok") {
                tableHelp.refresh(WageTableRe());
                $("#A0301").val("");
                $("#A0302").val("");
            }
            $("#AttendanceIsEdit").val("");
        });
    } else {
        layer.msg("签到时间不能为空！", { icon: 5 });
    }
});

$("#Wage_edit").click(function () {
    var row = $("#TableFromWage").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        if (row[0].A0301 != null && row[0].A0301 != null && row[0].A0301 != undefined) {
            $("#A0301").val(row[0].A0301.replace("T", " "));
        }
        $("#A0302").val(row[0].A0302);
        $("#WageIsEdit").val(row[0].RowID);
    } else {
        layer.msg("请选择需要编辑的考勤情况！", { icon: 5 });
    }
});

$("#Wage_delete").click(function () {
    var row = $("#TableFromWage").bootstrapTable('getSelections');
    if (row != null && row != "" && row != undefined) {
        layer.confirm("确定要删除该员工工资发放登记吗？", { btn: ["确定", "取消"] }, function (index) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: $.GetIISName() + "/Person/DeletePersonWage",
                data: { RowID: row[0].RowID },
                success: function (result) {
                    if (result == "ok") {
                        layer.close(index);
                        $("#A0301").val("");
                        $("#A0302").val("");
                        tableHelp.refresh(WageTableRe());
                    } else
                        layer.msg("删除失败", { icon: 5 });
                }
            });
        })
    } else {
        layer.msg("请选择员工工资发放登记记录！", { icon: 5 });
    }
});

var WageTableRe = function () {
    //构建传入参数
    var table = {
        Columns: [
            {
                radio: true
            },
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1;
                }
            },
            {
                field: "A0301",
                title: "签到时间",
                align: "center",
                formatter: function (value, row, index) {
                    return value != "" && value != null ? value.replace("T", " ") : "";
                }
            },
            {
                field: "A0302",
                title: "发放月份",
                align: "center"
            }

        ],
        Contoller: "TableFromWage",
        Height: 358,
        url: $.GetIISName() + "/Person/GetPersonWage",
        tool: "#WageTool"
    };
    return table;
}


var Iris = {

    Load: function () {
        $("#btnIris").click(function () {
            layer.open({
                type: 2,
                title: "虹膜采集",
                content: $.GetIISName() + "/Iris.html",
                area: ["700px", "400px"],
                maxmin: true,
                cancel: function () {

                }
            });
        });
    }
}

var IdentifyCodeCard = {

    Set: function (small, big) {
        $("#small_iris").val(small);
        $("#big_iris").val(big);
        layer.msg("虹膜信息采集成功！", { icon: 6 });
    }

}

