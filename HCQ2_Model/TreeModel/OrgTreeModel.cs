using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    public class OrgTreeModel
    {
        /// <summary>
        ///  folder_id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///  目录名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  父目录ID
        /// </summary>
        public int pId { get; set; }
        /// <summary>
        ///  是否属于系统字典
        /// </summary>
        public bool if_sys { get; set; }
        /// <summary>
        ///  折叠状态
        /// </summary>
        public bool open { get; set; } = false;
        /// <summary>
        ///  路径
        /// </summary>
        public string folder_path{get;set;}
        /// <summary>
        ///  子目录数据
        /// </summary>
        public List<OrgTreeModel> children { get; set; }
    }
}
