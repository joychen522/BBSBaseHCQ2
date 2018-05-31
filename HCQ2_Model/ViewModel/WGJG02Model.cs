using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    /// 工资发放明细模型
    /// </summary>
    public class WGJG02Model
    {
        /// <summary>
        ///  主键
        /// </summary>
        public string RowID { get; set; }
        /// <summary>
        ///  发放时间
        /// </summary>
        public DateTime? WGJG0201 { get; set; } 
        /// <summary>
        ///  约定发薪日期
        /// </summary>
        public DateTime? WGJG0202 { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        public string A0177 { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string B0002 { get; set; }
        public string UnitID { get; set; }
        /// <summary>
        ///   工种
        /// </summary>
        public string E0386 { get; set; }
        /// <summary>
        ///   工资发放方式
        /// </summary>
        public string WGJG0203 { get; set; }
        /// <summary>
        ///  工资
        /// </summary>
        [DisplayName("工资")]
        [RegularExpression("^[0-9]*$")]
        public decimal WGJG0204 { get; set; }
        /// <summary>
        ///  打卡天数
        /// </summary>
        [DisplayName("打卡天数")]
        [RegularExpression("^(0|[1-2]?[1-9]|[1-3][0]|[31])$")]
        public int WGJG0205 { get; set; }
        /// <summary>
        ///  实际工作天数
        /// </summary>
        [DisplayName("实际工作天数")]
        [RegularExpression("^(0|[1-2]?[1-9]|[1-3][0]|[31])$")]
        public int WGJG0206 { get; set; }
        /// <summary>
        ///  应发工资
        /// </summary>
        [DisplayName("应发工资")]
        [RegularExpression("^[0-9]*$")]
        public decimal WGJG0207 { get; set; }
        /// <summary>
        ///  实际发放
        /// </summary>
        [DisplayName("实际发放")]
        [RegularExpression("^[0-9]*$")]
        public decimal WGJG0208 { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        [DisplayName("备注")]
        public string WGJG0209 { get; set; }
        /// <summary>
        ///  是否发放
        /// </summary>
        [DisplayName("是否发放")]
        public string WGJG0211 { get; set; }
        /// <summary>
        ///  签到时间
        /// </summary>
        [DisplayName("签到时间")]
        public DateTime? WGJG0212 { get; set; }
        /// <summary>
        ///  人员库
        /// </summary>
        [DisplayName("人员库")]
        public string PClassID { get; set; }
    }
}
