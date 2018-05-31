using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model
{
    /// <summary>
    ///  企业征信扩展实体
    /// </summary>
    public partial class WGJG_ZX
    {
        /// <summary>
        ///  用于分页字段
        /// </summary>
        public double RowNumber { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string WGJG_ZX06_text { get; set; }
        /// <summary>
        ///  隶属关系
        /// </summary>
        public string WGJG_ZX16_text { get; set; }
        /// <summary>
        ///  行业类别
        /// </summary>
        public string WGJG_ZX17_text { get; set; }
    }
}
