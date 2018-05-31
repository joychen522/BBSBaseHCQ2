using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    /// <summary>
    ///  B01单位zTree模型
    /// </summary>
    public class B01zTreeModel
    {
        /// <summary>
        ///  id主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///  父ID
        /// </summary>
        public int pId { get; set; }
        /// <summary>
        ///  节点名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  节点打开或关闭
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        ///  是否被选中
        /// </summary>
        public bool check{get;set;}
        /// <summary>
        ///  子节点
        /// </summary>
        public B01zTreeModel children { get; set; }
    }
}
