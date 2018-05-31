/*******************************************************
 *  工资发放 相关操作js
 * <p>Title: sumGrantManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table,
    selUnit = "";//单位选择
 var sumGrantManager= {
     //初始化单位树
     initUnitDataByAjax: function () {
         var $treeUnit = $('#tree').initB01TreeView({
             data: null,
             checkFirst: true,
             onNodeSelected: function (e, o) {
                 if (o.unitID != null && o.unitID != "null" && o.unitID != "") {
                     selUnit = o.unitID;
                     $table.bootstrapTable('refresh');
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
     //初始化时间按钮
     initDate:function() {
         var dateStart = {
             elem: '#dateStart',
             format: 'YYYY-MM-DD',
             max: '2099-06-16 23:59:59', //最大日期
             istime: true,
             istoday: false,
             choose: function (datas) {
                 dateEnd.min = datas; //开始日选好后，重置结束日的最小日期
                 dateEnd.start = datas; //将结束日的初始值设定为开始日
             }
         };
         var dateEnd = {
             elem: '#dateEnd',
             format: 'YYYY-MM-DD',
             min: laydate.now(),
             max: '2099-06-16 23:59:59',
             istime: true,
             istoday: false,
             choose: function (datas) {
                 dateStart.max = datas; //结束日选好后，重置开始日的最大日期
             }
         };
         laydate(dateStart);
         laydate(dateEnd);
         $('#dateStart').val(laydate.now().substring(0, 4) + '-01-01');
         $('#dateEnd').val(laydate.now().substring(0,4)+'-12-31');
     },
     //初始化Table
     initTable:function() {
        var options = {
            url: ctx + "/SumGrant/initSumGrantDataList",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            showExport: true,
            singleSelect: false,
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    a0101: encodeURI($('#a0101').val()),
                    dateStart: $('#dateStart').val(),//开始时间
                    dateEnd: $('#dateEnd').val(),//截止时间
                    unitID: selUnit//单位代码
                }
                return params;
            },
            columns: [
            {
                     field: 'index',//第一列序号
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
                     field: 'B0002',
                     title: '用工单位',
                     align: 'center'
                 }, {
                     field: 'E0386',
                     title: '工种',
                     align: 'center'
                 }, {
                     field: 'WGJG0203',
                     title: '发放方式',
                     align: 'center'
                 }, {
                     field: 'WGJG0202',
                     title: '约定发放时间',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined) {
                             var data = $.formatDate(new Date(parseInt(value.slice(6))));"1-01-01";
                             return data === "1-01-01" ? "-" : data;
                         }
                         else
                            return "-";
                     }
                 }, {
                     field: 'WGJG0201',
                     title: '确定时间',
                     align: 'center',
                     formatter: function (value, row, index) {
                         if (value != null && value != "" && value != undefined) {
                             var data = $.formatDate(new Date(parseInt(value.slice(6))));
                             return data === "1-01-01" ? "-" : data;
                         }
                         else
                             return "-";
                     }
                 }, {
                     field: 'WGJG0207',
                     title: '应发金额',
                     align: 'center'
                 }, {
                     field: 'WGJG0208',
                     title: '实发金额',
                     align: 'center'
                 }],
            onClickRow: function (row, $element) {
                
            },
            onLoadSuccess:function(data) {
                if (data && data.total > 0)
                    sumGrantManager.mergeSpan();
            }
        }
        $table = tableHelper.initTable("sumGrantTableToolbar", options);
     },
     //合并单元格
     mergeSpan: function () {
         var data = $table.bootstrapTable('getData');
         var a0177 = data[0].A0177;
         var array = [];
         var start=null;//开始
         for (var i = 0; i < data.length; i++) {
             if (data[i].A0177 === a0177) {
                 if (start === null)
                     start = i;
                 array.push(data[i]);
             }
             else {
                 sumGrantManager.mergeCol(array, start);
                 array = [];
                 start = i;
                 array.push(data[i]);
                 a0177 = data[i].A0177;
             }
         }
         if(array!=[])
             sumGrantManager.mergeCol(array, start);
     },
     //合并元素：数据，开始位置
     mergeCol: function (data, start) {
         if (data.length === 1)
             return false;
         //外层遍历属性A0101
         var change = null;
         var isChange;
         for (var item in data[0]) {
             change = data[0][item];
             isChange = true;
             //内层遍历对象
             for (var i = 0; i < data.length; i++) {
                 if (change != data[i][item]) {
                     isChange = false;
                     break;
                 }
             }
            if (isChange)
                $table.bootstrapTable('mergeCells', { index: start, field: item, colspan: 1, rowspan: data.length });
         }
     },
     //初始化事件
     initEvent: function () {
         //查询
         $('#btnSearch').click(function() {
             $table.bootstrapTable('refresh');
         });
         //导出
         $('#sumGrantExport').click(function () {
             //确认一栏中是否有数据
             var data = $table.bootstrapTable('getData');
             if (data.length === null || data.length === 0) {
                 layer.closeAll();
                 layer.msg("没有需要导出的数据~", { icon: 5 });
                 return false;
             }
             layer.msg("导出中......", { icon: 1, time: 6000 });
             var params = "a0101=" + encodeURI($('#a0101').val()) + "&dateStart=" + $('#dateStart').val() + "&dateEnd=" + $('#dateEnd').val() + "&unitID=" + selUnit;
             var sumExport = $('#sumExport');
             sumExport.attr("href", ctx + "/SumGrant/ExportToExcel?" + params);
             document.getElementById("sumExport").click();
             return false;
             $.ajax({
                 type: 'post',
                 url: ctx + '/SumGrant/ExportToExcel',
                 data: {
                     a0101: encodeURI($('#a0101').val()),
                     dateStart: $('#dateStart').val(), //开始时间
                     dateEnd: $('#dateEnd').val(), //截止时间
                     unitID: selUnit  //单位代码
                 },
                 dataType: 'json',
                 async: false,
                 success: function (data) {
                     layer.closeAll();
                 },
                 error: function () {
                     layer.closeAll();
                     layer.msg("导出失败~", {icon:5});
                 }
             });
         });
     }
 }

//汇总统计
 var chartGrantManager = {
    //初始化
    initPage: function () {
        parent.showLoadBoxs();
        chartGrantManager.initUnitTree();
        chartGrantManager.initDate();
        chartGrantManager.initTable();
        chartGrantManager.initEvent();
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
                    $table.bootstrapTable('refresh');
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
    //绑定事件
    initEvent: function () {
        $('#btnSearch').click(function() {
            $table.bootstrapTable('refresh');
        });
        $('#btnCreateChart').click(function () {
            var rowID = "";
            var data = $table.bootstrapTable('getData');
            if (data === null || data.length === 0) {
                layer.msg("没有需要生成图表的数据~", { icon: 5 });
                return false;
            }
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length > 0) 
                rowID = row[0].RowID;
            chartGrantManager.openChart(rowID);
        });
    },
    //初始化时间按钮
    initDate:function() {
         var dateStart = {
             elem: '#dateStart',
             format: 'YYYY-MM-DD',
             max: '2099-06-16 23:59:59', //最大日期
             istime: true,
             istoday: false,
             choose: function (datas) {
                 dateEnd.min = datas; //开始日选好后，重置结束日的最小日期
                 dateEnd.start = datas; //将结束日的初始值设定为开始日
             }
         };
         var dateEnd = {
             elem: '#dateEnd',
             format: 'YYYY-MM-DD',
             min: laydate.now(),
             max: '2099-06-16 23:59:59',
             istime: true,
             istoday: false,
             choose: function (datas) {
                 dateStart.max = datas; //结束日选好后，重置开始日的最大日期
             }
         };
         laydate(dateStart);
         laydate(dateEnd);
         $('#dateStart').val(laydate.now().substring(0, 4) + '-01-01');
         $('#dateEnd').val(laydate.now().substring(0,4)+'-12-31');
    },
    initTable:function() {
        var options = {
            url: ctx + "/SumGrant/InitTable",
            cutHeight: 0,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    dateStart: $('#dateStart').val(),
                    dateEnd: $('#dateEnd').val(),
                    unitID: selUnit//单位代码
                }
                return params;
            },
            columns: [
            {
                radio: true
            }, {
                field: '',//第一列序号
                title: '序号',
                align: 'center',
                width: 50,
                formatter: function (value, row, index) {
                    return index + 1;
                }
            },{
                field: 'allPerson',
                title: '应发人数',
                align: 'center'
            }, {
                field: 'surePerson',
                title: '已发人数',
                align: 'center'
            }, {
                field: 'payPerson',
                title: '欠薪人数',
                align: 'center'
            }]
        }
        $table = tableHelper.initTable("countGrantTableToolbar", options);
    },
    //打开图表
    openChart: function (rowID) {
        //打开编辑
        layer.open({
            title: ['汇总统计'],
            type: 1,
            content: $('#chart_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: '',
            area: ['1000px', '95%'],
            success: function (li, o) {
                parent.showLoadBoxs();
                chartGrantManager.getChartData(rowID);
            }
        });
    },
    getChartData: function (rowID) {
        var unitID = selUnit, dateStart = $('#dateStart').val(), dateEnd = $('#dateEnd').val();
        $.ajax({
            url: ctx + '/SumGrant/GetCountGrantData',
            type: "post",
            async: false,
            data: { rowID: rowID, unitID: unitID, dateStart: dateStart, dateEnd: dateEnd },
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0)
                    chartGrantManager.drawCountGrant(mess.Data);
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
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById("echarts-Timebar-chart"), 'macarons');
        // 指定图表的配置项和数据
        var option = {
            title: {
                text: '发放汇总统计 单位(万)',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['金额']
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
        var data0 = {}, mark0 = [], markData = result.seriesList;
        data0.type = "max"; data0.name = "金额最多";
        mark0.push(data0);
        data0 = {};
        data0.type = "min"; data0.name = "金额最少";
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
            chartGrantManager.personDetial(param.name);
        });
    },
    personDetial:function(name) {
        if (name === null || name === "" || name === undefined)
            return false;
        var rowid = "", sendType = "", row = $table.bootstrapTable('getSelections');
        if (name === "实发金额")
            sendType = "1";
        else if (name === "欠薪金额")
            sendType = "2";
        if (row != null && row.length > 0)
            rowid = row[0].RowID;
        var unitID = selUnit, dateStart = $('#dateStart').val(), dateEnd = $('#dateEnd').val();
        window.parent.main_openWindowByLink({
            url: ctx + '/Finance/GrantPersonsList?rowid=' + rowid + '&sendType=' + sendType + '&unitID=' + unitID + '&dateStart=' + dateStart + '&dateEnd=' + dateEnd,
            height: "550px",
            width: "1100px",
            title: "详细信息"
        });
    }
}

