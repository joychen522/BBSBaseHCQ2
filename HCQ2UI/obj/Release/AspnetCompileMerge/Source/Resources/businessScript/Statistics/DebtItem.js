/************************************
*axisLabel rotate: 60度角是倾斜的控制所在
grid: y2:100 可以控制 X轴跟Zoom控件之间的间隔，避免以为倾斜后造成 label重叠到zoom上
list.push(App.formatDate(x));是处理 20140508 -> 140508
*
***************************************/
//欠薪时间统计
var params_unit = [];
var DrawDebtMoney = {
    //动态设置统计图表的宽度
    resizeEchart:function(myChart) {
        window.addEventListener("resize", function () { myChart.resize(); });
    },
    //欠薪金额统计
    DrawDebtMoney: function (result, EchartName) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(EchartName), 'macarons');//macarons
        // 指定图表的配置项和数据
        var option = {
            title: {
                text: '欠薪金额统计 单位(万元)',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['保证金额', '欠薪金额']
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
            xAxis: [
                {
                    axisLabel: {
                        interval: 0,//横轴信息全部显示
                        rotate: 50//60度角倾斜显示
                    },
                    type: 'category',
                    data: []
                    //data: ['经典天成三期', '云客咖啡', '建设大道', '桥梁五队', '桥梁五队', '隧道二队']
                }
            ],
            grid: { // 控制图的大小，调整下面这些值就可以，
                x: 40,
                x2: 20,
                y2: 100// y2可以控制 X轴跟Zoom控件之间的间隔，避免以为倾斜后造成 label重叠到zoom上
            },
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: []
        };
        option.xAxis[0].data = result.xAxis;
        var data0 = {}, data1 = {},
            mark0 = [], mark1 = [], markData = result.seriesList;
        data0.type = "max"; data0.name = "保证金最高";
        mark0.push(data0);
        data0 = {};
        data0.type = "min"; data0.name = "保证金最低";
        mark0.push(data0);
        //*****************************
        data1.type = "max"; data1.name = "欠薪最高";
        mark1.push(data1);
        data1 = {};
        data1.type = "min"; data1.name = "欠薪最低";
        mark1.push(data1);
        var average = {};
        average.type = "average";
        average.name = "平均值";

        for (var i = 0; i < markData.length; i++) {
            markData[i].markPoint = {};
            markData[i].markPoint.data = (i == 0) ? mark0 : mark1;
            //******************************
            markData[i].markLine = {};
            markData[i].markLine.data = [];
            markData[i].markLine.data.push(average);
        }
        option.series = markData;
        //option.series = result.seriesList;
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
        DrawDebtMoney.resizeEchart(myChart);
        parent.delLoadBoxs();

        myChart.on('click', function eConsole(param) {
            DrawDebtMoney.unitCodeByName(param.name);
        });
    },
    //欠薪人数统计
    DrawDebtPerson: function (result, EchartName) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById(EchartName), 'macarons');
        // 指定图表的配置项和数据
        var option = {
            title: {
                text: '欠薪人数统计',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['总人数', '欠薪人数']
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
            xAxis: [
                {
                    axisLabel: {
                        interval: 0,//横轴信息全部显示
                        rotate: 50//60度角倾斜显示
                    },
                    type: 'category',
                    data: []
                    //data: ['经典天成三期', '云客咖啡', '建设大道', '桥梁五队', '桥梁五队', '隧道二队']
                }
            ],
            grid: { // 控制图的大小，调整下面这些值就可以，
                x: 40,
                x2: 20,
                y2: 100// y2可以控制 X轴跟Zoom控件之间的间隔，避免以为倾斜后造成 label重叠到zoom上
            },
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: []
        };
        option.xAxis[0].data = result.xAxis;
        var data0 = {}, data1 = {},
            mark0 = [], mark1 = [], markData = result.seriesList;
        data0.type = "max"; data0.name = "总人数最多";
        mark0.push(data0);
        data0 = {};
        data0.type = "min"; data0.name = "总人数最少";
        mark0.push(data0);
        //*****************************
        data1.type = "max"; data1.name = "欠薪人数最多";
        mark1.push(data1);
        data1 = {};
        data1.type = "min"; data1.name = "欠薪人数最少";
        mark1.push(data1);
        var average = {};
        average.type = "average";
        average.name = "平均值";

        for (var i = 0; i < markData.length; i++) {
            markData[i].markPoint = {};
            markData[i].markPoint.data = (i == 0) ? mark0 : mark1;
            //******************************
            markData[i].markLine = {};
            markData[i].markLine.data = [];
            markData[i].markLine.data.push(average);
        }
        option.series = markData;
        //option.series = result.seriesList;
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
        DrawDebtMoney.resizeEchart(myChart);
        parent.delLoadBoxs();
        //绑定点击事件
        myChart.on('click', function eConsole(param) {
            DrawDebtMoney.unitCodeByName(param.name);
        });
    },
    //欠薪时间统计
    DrawDebtTime: function (result) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById("echarts-Timebar-chart"), 'macarons');
        // 指定图表的配置项和数据
        var option = {
            title: {
                text: '欠薪时间分段统计 单位(人)',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['欠薪人数']
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
                        interval: 0,//横轴信息全部显示
                        rotate: 50//60度角倾斜显示
                    },
                    type: 'category',
                    data: ['一个月内', '1-3个月','3-6个月','半年以上']
                    //data: ['经典天成三期', '云客咖啡', '建设大道', '桥梁五队', '桥梁五队', '隧道二队']
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
        var data0 = {}, mark0 = [], markData = result.seriesList;
        data0.type = "max"; data0.name = "欠薪人数最多";
        mark0.push(data0);
        data0 = {};
        data0.type = "min"; data0.name = "欠薪人数最少";
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
        DrawDebtMoney.resizeEchart(myChart);
        parent.delLoadBoxs();
    },
    //获取绘图数据并绘制统计图
    DrawTableByData: function (DrawUrl, DrawFunc, EchartName, Params) {
        if (params_unit.length > 0 && !Params)
            Params = { "selUnit": JSON.stringify(params_unit) };
        $.ajax({
            url: DrawUrl,
            type: "post",
            async: false,
            dataType: 'json',
            data: Params,
            success: function (mess) {
                if (mess.Statu === 0) {
                    var funCol = eval('DrawDebtMoney.' + DrawFunc);
                    if (funCol != undefined && funCol != null)
                        new funCol(mess.Data, EchartName)
                }
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
    //组织tree数据
    initTreeData: function ($Url, drawUrl, EchartName) {
        $.ajax({
            url: $Url,
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu == 0) 
                    DrawDebtMoney.bindTree(mess.Data,drawUrl,EchartName);
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
    bindTree: function (result, $url, EchartName) {
        $("#tree").initB01TreeView({
            width:'col-sm-3',
            data: result,
            onNodeSelected: function (e, o) {
                parent.showLoadBoxs();
                var data= {
                    unitID: o.unitID,
                    keyChild: o.keyChild
                }
                DrawDebtMoney.DrawTableByData($url, "DrawDebtTime",EchartName, data);
            }
        });
        parent.delLoadBoxs();
    },
    //初始化欠薪金额
    initDebtUnit: function (func,echartName) {
        parent.showLoadBoxs();
        $.ajax({
            url: ctx + '/DebtStatistics/GetUnitInfoByView_QXTJ',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu == 0)
                    DrawDebtMoney.bindDebtTree(mess.Data, func, echartName);
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
    bindDebtTree: function (result, func, echartName) {
        $("#tree").initB01TreeView({
            width: 'col-sm-3',
            showCheckbox: true,
            nodeIcon:'',
            data: result,
            onNodeChecked: function (e, o) {
                var temp = {};
                temp.UnitID = o.unitID;
                params_unit.push(temp);
                DrawDebtMoney.DrawTableByData(ctx + '/DebtStatistics/GetMoneyDataBySelUnit', "DrawDebtMoney", "echarts-Moneybar-chart");
            },
            onNodeUnchecked: function (e, o) {
                var index;
                for (var i = 0; i < params_unit.length; i++) {
                    if (params_unit[i].UnitID === o.unitID) {
                        index = i;
                        break;
                    }
                }
                params_unit.splice(index, 1);
                DrawDebtMoney.DrawTableByData(ctx + '/DebtStatistics/GetMoneyDataBySelUnit', "DrawDebtMoney", "echarts-Moneybar-chart");
            }
        });
        parent.delLoadBoxs();
    },
    //初始化单位
    initUnit: function (timeUrl, moneyUrl, EchartName) {
        parent.showLoadBoxs();
        DrawDebtMoney.initTreeData(timeUrl, moneyUrl, EchartName);
    },
    //根据单位名称获取单位代码
    unitCodeByName: function (unitName) {
        if (unitName == null || unitName == "" || unitName == undefined)
            return false;
        $.ajax({
            url:ctx+ '/DebtStatistics/GetUnitCodeByName',
            type: "post",
            async: false,
            data:{unitName: encodeURI(unitName)},
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu == 0 && mess.Data != null && mess.Data != "") {
                    //欠薪
                    //alert(mess.Data);
                    window.parent.main_openWindowByLink({
                        url: ctx+'/Person/Index?UnitID=' + mess.Data,
                        height: "550px",
                        width: "1100px",
                        title:"欠薪数据"
                    });
                }
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('获取单位代码数据异常~', { icon: 5 });
            }
        });
    }
}
