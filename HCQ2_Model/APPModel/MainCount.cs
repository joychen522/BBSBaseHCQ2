using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AppModel
{
    /// <summary>
    /// 首页统计
    /// </summary>
    public class MainCount
    {
        /// <summary>
        /// 项目数量
        /// </summary>
        public int compay_unit_count { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int total_person_count { get; set; }
        /// <summary>
        /// 日出工数
        /// </summary>
        public int check_day_count { get; set; }
        /// <summary>
        /// 欠薪金额
        /// </summary>
        public double back_pay_money { get; set; }
    }
}
