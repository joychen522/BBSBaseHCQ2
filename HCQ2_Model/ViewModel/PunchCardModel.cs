using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    ///  月趋势图 打卡记录人员详细模型
    /// </summary>
    public class PunchCardModel
    {
        /// <summary>
        ///  姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        public string A0177 { get; set; }
        /// <summary>
        ///  基本工资
        /// </summary>
        public double A0178 { get; set; } 
        /// <summary>
        ///  用工单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  考勤卡号
        /// </summary>
        public string A0141 { get; set; }
        /// <summary>
        ///  考勤方式 29
        /// </summary>
        public string A0142 { get; set; }
        /// <summary>
        ///  签到时间
        /// </summary>
        public string A0201 { get; set; }
    }
}
