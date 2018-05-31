using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    /// <summary>
    ///  treetable属性模型
    /// </summary>
    public class TreeTableAttribute
    {
        /// <summary>
        ///  主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  链接地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///  图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        ///  父ID
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        ///  是否有子节点
        /// </summary>
        public bool hasChild { get; set; }
        /// <summary>
        ///  指定某一个元素是否可以控制行的展开
        /// </summary>
        public bool controller { get; set; }
        /// <summary>
        ///  所属模块
        /// </summary>
        public string sm_code { get; set; }
        /// <summary>
        ///  页面类型
        /// </summary>
        public string folder_type { get; set; }
    }
}
