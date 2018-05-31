using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class DebtSelResultModel
    {
        /// <summary>
        ///  项目代码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  项目名
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal QXTJ01 { get; set; }
        /// <summary>
        ///  保障金额
        /// </summary>
        public decimal B0116 { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int People { get; set; }
    }

    /// <summary>
    ///  欠薪数据
    /// </summary>
    public class DebtDataModel
    {
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal QXTJ01 { get; set; }
        /// <summary>
        ///  保障金额
        /// </summary>
        public decimal B0116 { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int People { get; set; }
        /// <summary>
        ///  总人数
        /// </summary>
        public int People2 { get; set; }
    }
    /// <summary>
    ///  欠薪金额图表数据
    /// </summary>
    public class DebtMoneyModel
    {
        /// <summary>
        ///  欠薪月份
        /// </summary>
        public int month { get; set; }
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal money { get; set; }
    }

    /// <summary>
    ///  欠薪人数模型
    /// </summary>
    public class DebtPersonsModel
    {
        /// <summary>
        ///  欠薪月份
        /// </summary>
        public int month { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int number { get; set; }
    }

    /// <summary>
    /// 出工打卡统计返回值
    /// </summary>
    public class CheckDatePerson
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string check_date { get; set; }
        /// <summary>
        /// 打卡人数
        /// </summary>
        public int check_num { get; set; }
    }

}
