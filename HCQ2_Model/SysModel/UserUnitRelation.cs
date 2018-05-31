using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SysModel
{
    public class UserUnitRelation
    {
        /// <summary>
        ///  用户ID
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        ///  区域ID 或者 人员ID
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        ///  父ID 或者区域ID
        /// </summary>
        public int unit_pid { get; set; }
        /// <summary>
        ///  区域名称 或者 人员名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        ///  类型 
        ///  area 区域
        ///  person 人员
        /// </summary>
        public string tree_type { get; set; }
    }
}
