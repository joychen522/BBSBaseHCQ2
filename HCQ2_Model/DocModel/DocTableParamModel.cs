using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.DocModel
{
    public class DocTableParamModel
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
        ///  文档类别
        /// </summary>
        public int doc_type { get; set; }
        /// <summary>
        ///  发布开始时间
        /// </summary>
        public string issue_start { get; set; }
        /// <summary>
        ///  发布截止时间
        /// </summary>
        public string issue_end { get; set; }
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; } = 10;
        /// <summary>
        ///  用户ID
        /// </summary>
        public int user_id { get; set; }
        //VR资源查询
        /// <summary>
        ///  文档状态：私有，待审核，已上架
        /// </summary>
        public string file_status { get; set; }
    }
}
