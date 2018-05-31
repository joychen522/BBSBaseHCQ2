using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{

    /// <summary>
    /// 企业目录树实体
    /// </summary>
    public class ComInfoModle
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public int com_pid { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public ComInfoModle com { get; set; }
    }
}
