using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  APP征信
    /// </summary>
    public class CreditModelResult
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
        /// <summary>
        ///  征信ID
        /// </summary>
        public int ed_id { get; set; }
        /// <summary>
        ///  更新单位
        /// </summary>
        public string ent_name { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string ent_type { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public string update_name { get; set; }
        /// <summary>
        ///  处理状态
        /// </summary>
        public string solve_type { get; set; }
    }
}
