using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.Constant
{
    /// <summary>
    ///  区域表模型
    /// </summary>
     public class AreaModel
    {
        /// <summary>
        ///  区域代码
        /// </summary>
        public string CodeItemID { get; set; }
        /// <summary>
        ///  区域名称
        /// </summary>
        public string CodeItemName { get; set; }
        /// <summary>
        ///  父 区域代码
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        ///  是否有只节点 有则为：1，没有则为：0
        /// </summary>
        public int Child { get; set; }
        /// <summary>
        ///  拼音首字母
        /// </summary>
        public string JPSign { get; set; }
    }
}
