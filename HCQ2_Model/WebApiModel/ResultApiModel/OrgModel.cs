using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WebApiModel.ResultApiModel
{
    /// <summary>
    ///  组织机构下发模型
    /// </summary>
    public class OrgModel
    {
        /// <summary>
        ///  单位代码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  父单位代码
        /// </summary>
        public string UnitParentID { get; set; }
        /// <summary>
        ///  单位类型
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  子节点
        /// </summary>
        public List<OrgModel> Nodes { get; set; }
    }
}
