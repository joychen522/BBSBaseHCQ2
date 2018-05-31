/*******************************************************
 *  月趋势图 相关操作js
 * <p>Title: monthChartManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//月趋势图
var $table, selUnit = "";//单位选择
var monthChartManager = {
    //初始化
    initPage: function () {
        parent.showLoadBoxs();
        monthChartManager.initUnitTree();
        monthChartManager.initDate();
        monthChartManager.initTable();
        monthChartManager.initEvent();
        monthChartManager.initChartData();
        parent.delLoadBoxs();
    },
    //初始化单位树
    initUnitTree: function () {
        var $treeUnit = $('#tree').initB01TreeView({
            data: null,
            checkFirst: true,
            onNodeSelected: function (e, o) {
                if (o.unitID != null && o.unitID != "null" && o.unitID != "") {
                    selUnit = o.unitID;
                    parent.showLoadBoxs();
                    $table.bootstrapTable('refresh');
                    monthChartManager.initChartData();
                }
            }
        });
        //初始化时选择一个节点
        var temp = $treeUnit.treeview('getEnabled')[0];
        if (temp != null) {
            $treeUnit.treeview('selectNode', [temp.nodeId, { silent: true }]);
            selUnit = temp.unitID;
        }
    },
    //初始化事件
    initEvent:function() {
        $('#btnSearch').click(function() {
            $table.bootstrapTable('refresh');
            monthChartManager.initChartData();
        });
    },
    //初始化时间按钮
    initDate: function () {
        var dateStart = {
            elem: '#dateStart',
            format: 'YYYY-MM',
            max: '2099-06', //最大日期
            istime: true,
            istoday: false,
            choose: function (datas) {
                dateEnd.min = datas; //开始日选好后，重置结束日的最小日期
                dateEnd.start = datas; //将结束日的初始值设定为开始日
            }
        };
        var dateEnd = {
            elem: '#dateEnd',
            format: 'YYYY-MM',
            min: laydate.now(),
            max: '2099-06',
            istime: true,
            istoday: false,
            choose: function (datas) {
                dateStart.max = datas; //结束日选好后，重置开始日的最大日期
            }
        };
        laydate(dateStart);
        laydate(dateEnd);
        $('#dateStart').val(laydate.now().substring(0, 4) + '-01');
        $('#dateEnd').val(laydate.now().substring(0, 4) + '-12');
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/MonthChart/GetTableData",
            cutHeight: 0,
            showRefresh:false,
            showToggle: false,
            showColumns:false,
            pagination:false,//不启用分页
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    dateStart: $('#dateStart').val(),
                    dateEnd: $('#dateEnd').val(),
                    unitID: selUnit//单位代码
                }
                return params;
            },
            columns: [
            {
                field: '',//第一列序号
                title: '序号',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    return index + 1;
                }
            }, {
                field: 'cardDate',
                title: '打卡年月',
                align: 'center'
            }, {
                field: 'countPersons',
                title: '打卡人数',
                align: 'center',
                formatter: function (value, row, index) {
                    return "<a href='#' onclick=monthChartManager.openCardDetail('" + row.cardDate + "')>" + value + "</a>";
                }
            }]
        }
        $table = tableHelper.initTable("monthGrantTableToolbar", options);
    },
    //打开详细信息
    openCardDetail:function(cardDate) {
        if (!cardDate || selUnit==="")
            layer.msg("必传参数为空~", { icon: 5 });
        window.parent.main_openWindowByLink({
            url: ctx + '/MonthChart/PersonList?unitID=' + selUnit + '&cardDate=' + cardDate,
            height: "550px",
            width: "1100px",
            title: "详细信息"
        });
    },
    //图表统计
    initChartData: function () {
        var unitID = selUnit, dateStart = $('#dateStart').val(), dateEnd = $('#dateEnd').val();
        if (!unitID) 
            return false;
        $.ajax({
            url: ctx + '/MonthChart/GetChartDataByUnit',
            type: "post",
            async: false,
            data: { unitID: unitID, dateStart: dateStart, dateEnd: dateEnd },
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0)
                    monthChartManager.drawCountGrant(mess.Data);
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    drawCountGrant: function (result) {
        if (!result) {
            parent.delLoadBoxs();
            return false;
        }
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById("echarts-Monthbar-chart"), 'macarons');
        // 指定图表的配置项和数据
        var option = {
            title: {
                text: '打开趋势图 单位(人)',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['人数']
            },
            toolbox: {
                show: true,
                feature: {
                    dataView: { show: true, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            calculable: true,
            //X轴数据
            xAxis: [
                {
                    axisLabel: {
                        interval: 0 //横轴信息全部显示
                        //rotate: 50//60度角倾斜显示
                    },
                    type: 'category',
                    data: ['应发金额', '实发金额', '欠薪金额']
                }
            ],
            grid: { // 控制图的大小，调整下面这些值就可以，
                x: 40,
                x2: 20,
                y2: 100// y2可以控制 X轴跟Zoom控件之间的间隔，避免以为倾斜后造成 label重叠到zoom上
            },
            //Y轴数据
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: []
        };
        //x轴数据
        var xStr = [];
        $.each(result.xAxis, function(index,value) {
            xStr.push(value);
        });
        option.xAxis[0].data = xStr;
        var data0 = {}, mark0 = [], markData = result.seriesList;
        data0.type = "max"; data0.name = "人数最多";
        mark0.push(data0);
        data0 = {};
        data0.type = "min"; data0.name = "人数最少";
        mark0.push(data0);
        //*****************************
        var average = {};
        average.type = "average";
        average.name = "平均值";

        for (var i = 0; i < markData.length; i++) {
            markData[i].markPoint = {};
            markData[i].markPoint.data = mark0;
            //******************************
            markData[i].markLine = {};
            markData[i].markLine.data = [];
            markData[i].markLine.data.push(average);
        }
        option.series = markData;
        //option.series = result.seriesList;
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
        parent.delLoadBoxs();
        //图表点击事件
        myChart.on('click', function eConsole(param) {
            monthChartManager.openCardDetail(param.name);
        });
    }
}

//打卡记录详细
var cardManager = {
    //初始化Table
    initTable:function() {
        var options = {
            url: ctx + "/MonthChart/InitPersonsTable",
            cutHeight: 0,
            singleSelect: false,//非单选
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    unitID: $.getUrls("unitID"),
                    cardDate: $.getUrls("cardDate")
                }
                return params;
            },
            columns: [
                {
                    field: '',//第一列序号
                    title: '序号',
                    align: 'center',
                    width: 50,
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: 'A0101',
                    title: '姓名',
                    align: 'center'
                }, {
                    field: 'A0177',
                    title: '身份证',
                    align: 'center'
                }, {
                    field: 'A0178',
                    title: '基本工资',
                    width: 100,
                    align: 'center'
                }, {
                    field: 'A0177',
                    title: '身份证',
                    align: 'center'
                }, {
                    field: 'UnitName',
                    title: '用工单位',
                    align: 'center'
                }, {
                    field: 'A0141',
                    title: '考勤卡号',
                    align: 'center'
                }, {
                    field: 'A0201',
                    title: '签到时间',
                    align: 'center'
                }]
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    }
}