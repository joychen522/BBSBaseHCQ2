
//参数列表
function queryParams(params) {
    var pageIndex = params.offset / params.limit + 1
    var temp = {
        page: pageIndex,
        rows: params.limit,
        PersonID: $.GetUrlParam("PersonID")
    };
    return temp;
}

//人员详细信息
var Detail = {

    //学历学位
    Edu: function () {
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
                    field: "A0415",
                    title: "入学时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != "" && value != null ? value.replace("T", " ") : "";
                    }
                }
                ,
                {
                    field: "A0430",
                    title: "毕业时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value != "" && value != null ? value.replace("T", " ") : "";
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
            Height: window.innerHeight - 66,
            url: $.GetIISName() + "/Person/GetEduInfor"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    //工作经历
    Work: function () {
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
            Height: window.innerHeight - 66,
            url: $.GetIISName() + "/Person/GetWorkInfo"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    //考勤情况
    Attendance: function () {
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
                    field: "A0201",
                    title: "签到时间",
                    align: "center",
                    formatter: function (value, row, index) {
                        return value.replace("T", " ");
                    }
                }

            ],
            Contoller: "TableFromAttendance",
            Height: window.innerHeight - 66,
            url: $.GetIISName() + "/Person/GetAttendanceInfo"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    //员工工资发放登记
    Wage: function () {
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
            Height: window.innerHeight - 66,
            url: $.GetIISName() + "/Person/GetWageInfo"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    },

    //维权凭证
    Credentials: function (row) {
        var d = new Date();
        var html = "<table style='width:600px; height:auto;' border='1'>";
        html += "<tr><td align='center' style='height:60px;'>" + d.getFullYear() + "年" + row.A0101 + "的维权凭证</td></tr>";

        html += "<tr><td><table style='width:100%;'>";
        html += "<tr><td style='height:50px; width:300px;'>姓名：" + row.A0101 + "</td><td>联系电话：" + row.C0104 + "</td></tr>";
        var date = (row.E0359 != "" && row.E0359 != null) ? $.DateFormat(row.E0359) : "";
        html += "<tr><td style='height:50px;width:300px;'>工种：" + row.E0386 + "</td><td>入职日期：" + date + "</td></tr>";
        html += "</table></td></tr>";

        html += "<tr><td><table style='width:100%; '>";
        html += "<tr><td style='height:50px;width:300px;'>项目名称：" + row.B0001 + "</td><td>业主单位：" + row.B0002 + "</td></tr>";
        html += "<tr><td style='height:50px;width:300px;'>承建单位：" + row.B000201 + "</td><td></td></tr>";
        $.ajax({
            type: "post",
            dataType: "text",
            data: { unitid: row.B000201 },
            url: $.GetIISName() + "/Person/GetUnit",
            async: false,
            success: function (data) {
                html += "<tr><td colspan='2' style='height:50px;width:300px;'>负责人以及联系电话：" + data + "</td></tr>";
            }
        });
        html += "</table></td></tr>";
        html += "<tr style='width:600px; height:400px;'><td style='width:600px; height:400px;'><div class='flot-chart' style='width:600px; height:400px;'>";
        html += "<div class='flot-chart-content' id='monthTime' style='width:600px; height:400px;'></div>";
        html += "</div></td></tr>";
        html += "</table>";
        $("#DivContent").html(html);

        $.ajax({
            type: "post",
            dataType: "json",
            data: { person_id: row.PersonID },
            url: $.GetIISName() + "/Person/GetMonthTime",
            async: false,
            success: function (data) {
                if (data.day == null || data.day == "" || data.day == undefined) {
                    $("#monthTime").html("暂无打卡记录");
                } else {
                    var sals = echarts.init(document.getElementById("monthTime"));
                    var n = {
                        title: {
                            text: '月打卡天数',
                            left: 'center'
                        },
                        tooltip: {
                            trigger: 'item',
                            formatter: '{a} <br/>{b} : {c}'
                        },
                        legend: {
                            left: 'left',
                            data: ['天数']
                        },
                        xAxis: {
                            type: 'category',
                            name: '月',
                            splitLine: { show: false },
                            data: data.month //['一', '二', '三', '四', '五', '六', '七', '八', '九']
                        },
                        grid: {
                            left: '3%',
                            right: '4%',
                            bottom: '3%',
                            containLabel: true
                        },
                        yAxis: {
                            type: 'log',
                            name: '天'
                        },
                        series: [
                            {
                                name: '天数',
                                type: 'line',
                                data: data.day//[5, 3, 50, 27, 8, 247, 741, 1000, 5000]
                            }
                        ]
                    };
                    sals.setOption(n);
                }
            }
        })
    },

    WGJG_GRZX: function () {
        //构建传入参数
        var table = {
            Columns: [
            {
                field: "Number",
                title: "序号",
                align: "center",
                formatter: function (value, row, index) {
                    return index + 1;
                }
            }, {
                field: "WGJG_GRZX01",
                title: "编号",
                align: "center"
            }, {
                field: "WGJG_GRZX02",
                title: "姓名",
                align: "center"
            }, {
                field: "WGJG_GRZX04",
                title: "籍贯",
                align: "center"
            }, {
                field: "WGJG_GRZX03",
                title: "身份证",
                align: "center"
            }, {
                field: "WGJG_GRZX06",
                title: "征信状态",
                align: "center"
            }, {
                field: "WGJG_GRZX05",
                title: "联系电话",
                align: "center"
            }, {
                field: "WGJG_GRZX07",
                title: "状态变更原因",
                align: "center"
            }, {
                field: "WGJG_GRZX10",
                title: "更新人",
                align: "center"
            }, {
                field: "WGJG_GRZX11",
                title: "更新时间",
                align: "center",
                formatter: function (value, row, index) {
                    if (value != null && value != "" && value != undefined)
                        return $.DateFormat(value);
                    else
                        return "";
                }
            }],
            Contoller: "TableFromWGJGGRZX",
            Height: window.innerHeight - 66,
            url: $.GetIISName() + "/Person/WGJGGRZX"
        };

        //首次加载table
        tableHelp.LoadData(table, queryParams);
    }
}