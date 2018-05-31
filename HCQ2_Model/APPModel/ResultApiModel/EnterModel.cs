using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  企业信息
    /// </summary>
    public class EnterModel
    {
        /// <summary>
        ///  企业ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  企业名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  企业类别
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        ///  营业执照：统一社会信用代码
        /// </summary>
        public string TrustCode { get; set; }
        /// <summary>
        ///  法定代表人
        /// </summary>
        public string LegalPerson { get; set; }
    }
}
