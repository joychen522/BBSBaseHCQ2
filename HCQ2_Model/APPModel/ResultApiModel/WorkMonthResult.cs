using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class WorkIndexResult
    {
        /// <summary>
        ///  月出工天数
        /// </summary>
        public int workMonth { get; set; }
        /// <summary>
        ///  总出工天数
        /// </summary>
        public int workAll { get; set; }
        /// <summary>
        ///  实发薪资
        /// </summary>
        public decimal takeMoney { get; set; }
        /// <summary>
        ///  拖欠薪资
        /// </summary>
        public decimal payMoney { get; set; }
    }
    public class WorkSQLMonth
    {
        /// <summary>
        ///  唯一标记
        /// </summary>
        public string RowID { get; set; }
        /// <summary>
        ///  标记in,out
        /// </summary>
        public string mark { get; set; }
        /// <summary>
        ///  日期：几号
        /// </summary>
        public int day { get; set; }
        /// <summary>
        ///  时间
        /// </summary>
        public string hour { get; set; }
    }
    public class WorkMonthResult
    {
        /// <summary>
        ///  天
        /// </summary>
        public int day { get; set; }
        /// <summary>
        ///  上班时间
        /// </summary>
        public string start_work { get; set; }
        /// <summary>
        ///  下班时间
        /// </summary>
        public string end_work { get; set; }
    }
    public  class WorkAllResult
    {
        /// <summary>
        ///  年-月
        /// </summary>
        public string work_date { get; set; }
        /// <summary>
        ///  出工天数
        /// </summary>
        public int work_days { get; set; }
    }
    public class WorkMoneyResult
    {
        /// <summary>
        ///  发放日期
        /// </summary>
        public string pay_date { get; set; }
        /// <summary>
        ///  应发
        /// </summary>
        public decimal WGJG0207 { get; set; }
        /// <summary>
        ///  实发
        /// </summary>
        public decimal WGJG0208 { get; set; }
        /// <summary>
        ///  是否确认
        /// </summary>
        public string WGJG0211 { get; set; }
    }
}
