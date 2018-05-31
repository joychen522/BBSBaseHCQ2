using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    ///  bootstrap-table模型
    /// </summary>
    public class TableModel
    {
        /// <summary>
        ///  总数量
        /// </summary>
        public int total { get; set; }
        /// <summary>
        ///  数据
        /// </summary>
        public object rows { get; set; }
    }
}
