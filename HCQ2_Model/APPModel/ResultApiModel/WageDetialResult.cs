using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class WageDetialResult
    {
        /// <summary>
        ///  发薪方式
        /// </summary>
        public string WGJG0203 { get; set; }
        /// <summary>
        ///  基本工资
        /// </summary>
        public decimal WGJG0204 { get; set; }
        /// <summary>
        ///  其他费用
        /// </summary>
        public decimal WGJG0501 { get; set; }
        /// <summary>
        ///  扣除费用
        /// </summary>
        public decimal WGJG0502 { get; set; }
        /// <summary>
        ///  应发工资
        /// </summary>
        public decimal WGJG0207 { get; set; }
        /// <summary>
        ///  实发工资
        /// </summary>
        public decimal WGJG0208 { get; set; }
        /// <summary>
        ///  预计发放时间
        /// </summary>
        public string WGJG0202 { get; set; }
    }
}
