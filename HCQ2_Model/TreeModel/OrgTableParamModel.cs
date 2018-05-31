using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    /// <summary>
    ///  组织机构参数模型
    /// </summary>
    public class OrgTableParamModel
    {
        /// <summary>
        ///  关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        ///  节点ID
        /// </summary>
        public int folder_id { get; set; }
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; } = 10;
    }
}
