using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    public class AttendanceModel
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string PersonID { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string A0177 { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string GZ { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public string XM { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public string DW { get; set; }
        /// <summary>
        /// 出工天数
        /// </summary>
        public int TS { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string YF { get; set; }
    }
}
