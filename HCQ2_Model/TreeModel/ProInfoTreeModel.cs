using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    public class ProInfoTreeModel
    {
        /// <summary>
        ///  树节点ID
        /// </summary>
        public int nodeId { get; set; }
        /// <summary>
        ///  节点名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  单位ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  子节点，可以用递归的方法读取
        /// </summary>
        public List<ProInfoTreeModel> nodes { get; set; }
    }
}
