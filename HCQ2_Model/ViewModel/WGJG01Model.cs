using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    ///  工资发放模型
    /// </summary>
    public class WGJG01Model
    {
        /// <summary>
        ///  主键ID
        /// </summary>
        public string RowID { get; set; }
        public int DispOrder { get; set; }
        /// <summary>
        ///  所属项目
        /// </summary>
        public string modelName { get; set; }
        /// <summary>
        ///  用工单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  单位代码
        /// </summary>
        public string B0002 { get; set; }
        /// <summary>
        ///  单位代码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  归档标识
        /// </summary>
        public string CodeItemName { get; set; }
        public string CodeItemValue { get; set; }
        public string WGJG0101 { get; set; }
        /// <summary>
        ///  发放时间
        /// </summary>
        public DateTime WGJG0102 { get; set; }
        /// <summary>
        ///  发放原因
        /// </summary>
        public string WGJG0103 { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string WGJG0104 { get; set; }
        /// <summary>
        ///  发放人员
        /// </summary>
        public string WGJG0105 { get; set; }
        /// <summary>
        ///  制单人
        /// </summary>
        public string WGJG0106 { get; set; }
        /// <summary>
        ///  约定发薪日期
        /// </summary>
        public DateTime WGJG0107 { get; set; }
        /// <summary>
        ///  工资发放方式
        /// </summary>
        public string WGJG0203 { get; set; }
        public string WGJG0203_Name { get; set; }
        /// <summary>
        ///  约定发放日
        /// </summary>
        public int WGJGDAY { get; set; }
        /// <summary>
        ///  总人数
        /// </summary>
        public int allPerson { get; set; }
        /// <summary>
        ///  已发放人数：已确认
        /// </summary>
        public int surePerson { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int payPerson { get; set; }
        /// <summary>
        ///  应发工资总额
        /// </summary>
        public decimal allMoney { get; set; }
        /// <summary>
        ///  已发放工资总额
        /// </summary>
        public decimal sureMoney { get; set; }
        /// <summary>
        ///  欠薪工资总额
        /// </summary>
        public decimal payMoney { get; set; }
    }
}
