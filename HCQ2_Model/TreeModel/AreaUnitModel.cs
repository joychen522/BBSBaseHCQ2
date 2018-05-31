using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    public class AreaUnitModel
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 类别，1：区域，2：项目
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string area_name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 项目上级编号
        /// </summary>
        public string unit_pid { get; set; }
        /// <summary>
        /// 子节点信息
        /// </summary>
        public List<AreaUnitModel> children { get; set; }
    }
}
