using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    /// <summary>
    ///  项目台账 业务模型
    /// </summary>
    public class ItemPreserveModel
    {
        public int ip_id { get; set; }
        public string unit_name { get; set; }
        public string unit_code { get; set; }
        public string ip_status { get; set; }
        /// <summary>
        ///  项目状态 name
        /// </summary>
        public string ip_status_text { get; set; }
        public decimal pact_money { get; set; }
        public decimal pay_money { get; set; }
        public string pay_date { get; set; }
        public string area_code { get; set; }
        public decimal pay_cash_money { get; set; }
        public decimal pra_cash_money { get; set; }
        public string duty_person { get; set; }
        public string touch_phone { get; set; }
        public string bag_file { get; set; }
        public string pact_start { get; set; }
        public string record_name { get; set; }
        public string update_date { get; set; }
        public string user_note { get; set; }
        /// <summary>
        ///  跟踪过
        /// </summary>
        public int tail_after { get; set; }
    }
}
