using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class ChartProject
    {
        /// <summary>
        ///  月份
        /// </summary>
        public int month { get; set; }
        /// <summary>
        ///  项目数量
        /// </summary>
        public int projectCount { get; set; }
    }
    public class ChartPerson
    {
        /// <summary>
        ///  月份
        /// </summary>
        public int month { get; set; }
        /// <summary>
        ///  人数数量
        /// </summary>
        public int perCount { get; set; }
    }
}
