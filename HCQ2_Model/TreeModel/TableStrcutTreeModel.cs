using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    /// <summary>
    ///  结构树树模型
    /// </summary>
    public class TableStrcutTreeModel
    {
        /// <summary>
        ///  节点名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  表名称
        /// </summary>
        public string table_name { get; set; }
        /// <summary>
        ///  主键名称
        /// </summary>
        public string key_name { get; set; }
    }
}
