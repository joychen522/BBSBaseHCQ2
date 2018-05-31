using System.Collections.Generic;

namespace HCQ2_Model.DocModel
{
    /// <summary>
    ///  文档结构模型
    /// </summary>
    public class DocTreeModel
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
        ///  是否只读
        /// </summary>
        public bool read_only { get; set; }
        /// <summary>
        ///  是否允许创建子目录
        /// </summary>
        public bool if_create_child { get; set; }
        /// <summary>
        ///  是否能被分享到此目录
        /// </summary>
        public bool was_share { get; set; }
        /// <summary>
        ///  是否属于系统字典
        /// </summary>
        public bool if_sys { get; set; }
        /// <summary>
        ///  折叠状态
        /// </summary>
        public bool open { get; set; } = false;
        /// <summary>
        ///  节点类型
        ///  0：仅自己
        ///  1：分享
        ///  2：公用
        /// </summary>
        public int doc_type { get; set; }
        /// <summary>
        ///  页面类型
        /// </summary>
        public string pageType { get; set; }
        /// <summary>
        ///  子目录数据
        /// </summary>
        public List<DocTreeModel> children { get; set; }
    }
}
