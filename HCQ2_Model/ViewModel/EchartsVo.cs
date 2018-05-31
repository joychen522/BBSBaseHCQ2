using System;
using System.Collections.Generic;

namespace HCQ2_Model.ViewModel
{
    public class EchartsVo
    {
        /// <summary>
        ///  X轴数据：['经典天成三期', '云客咖啡', '建设大道', '桥梁五队', '桥梁五队', '隧道二队']
        /// </summary>
        public List<string> xAxis { get; set; }

        /// <summary>
        ///  统计项目：['欠薪金额','保障金']
        /// </summary>
        public List<string> legend { get; set; }

        /// <summary>
        ///  series：数据集合
        /// </summary>
        public List<Series> seriesList { get; set; }
    }

    public class Series
    {
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///  bar
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///  金额：[0.60, 1.60, 8.50,12.30,0.00,0.00]
        /// </summary>
        public List<decimal?> data { get; set; }
    }
}
