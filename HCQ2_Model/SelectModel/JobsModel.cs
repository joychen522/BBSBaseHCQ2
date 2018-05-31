using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SelectModel
{
    /// <summary>
    /// 工种统计
    /// </summary>
    public class JobsModel
    {
        /// <summary>
        /// 工种名称
        /// </summary>
        public string jobsName { get; set; }
        /// <summary>
        /// 工种数量
        /// </summary>
        public int jobsCount { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public string unitName { get; set; }
    }
}
