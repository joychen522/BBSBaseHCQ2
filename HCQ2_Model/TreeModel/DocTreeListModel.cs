using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
   public class DocTreeListModel
    {
        /// <summary>
        ///  节点ID
        /// </summary>
        public int folder_id { get; set; }
        /// <summary>
        ///  父节点ID
        /// </summary>
        public int folder_pid { get; set; }
        /// <summary>
        ///  文档数量 注意统计的是当前节点下的数量
        /// </summary>
        public int total { get; set; }
        /// <summary>
        ///  文档类型
        ///  0：我的文档
        ///  1：我的分享
        ///  2：收到的分享
        ///  3：VR资源
        ///  4：下架资源
        ///  5：待审核资源
        /// </summary>
        public int doc_type { get; set; }
    }
}
