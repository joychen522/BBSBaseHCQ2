using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    public class MainCheckUnit
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 出工人数
        /// </summary>
        public int CheckWorkers { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int totalWorkers { get; set; }
        /// <summary>
        /// 出工率
        /// </summary>
        public double CheckPepe { get; set; }
    }
}
