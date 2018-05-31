$(function(){
	// 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('echarts-bar-chart'),'macarons');

    // 指定图表的配置项和数据
    //arrayObj.join(separator); //返回字符串
    var option = {
	    title : {
	        text: '欠薪金额与保障金统计 单位(万)',
	        subtext: ''
	    },
	    tooltip : {
	        trigger: 'axis'
	    },
	    legend: {
	        data: ['欠薪金额','保障金']
	    },
	    toolbox: {
	        show : true,
	        feature : {
	            dataView : {show: true, readOnly: false},
	            magicType : {show: true, type: ['line', 'bar']},
	            restore : {show: true},
	            saveAsImage : {show: true}
	        }
	    },
	    calculable : true,
	    xAxis : [
	        {
	            type : 'category',
	            data: ['经典天成三期', '云客咖啡', '建设大道', '桥梁五队', '桥梁五队', '隧道二队']
	        }
	    ],
	    yAxis : [
	        {
	            type : 'value'
	        }
	    ],
	    series : [  
	        {
	            name: '欠薪金额',
	            type:'bar',
	            data:[0.60, 1.60, 8.50,12.30,0.00,0.00],
	            markPoint : {
	                data : [
	                    {name : '欠薪最高', value : 12.30, xAxis: 3, yAxis: 12.3},
	                    {name : '欠薪最低', value : 0.00, xAxis: 4, yAxis: 0}
	                ]
	            },
	            markLine : {
	                data : [
	                    {type : 'average', name : '平均值'}
	                ]
	            }
	        },
            {
	            name: '保障金',
	            type:'bar',
	            data:[5.00,12.00, 0.00,0.00,0.00,0.00],
	            markPoint : {
	                data : [
	                    { name: '保障金最高', value: 12.00, xAxis: 1, yAxis: 12.00 },
	                    { name: '保障金最低', value: 0.00, xAxis: 2, yAxis: 0 }
	                ]
	            },
	            markLine : {
	                data : [
	                    {type : 'average', name : '平均值'}
	                ]
	            }
	        }
	    ]
	};
	// 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
});