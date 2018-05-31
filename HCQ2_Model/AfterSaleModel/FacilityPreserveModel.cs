using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    public class FacilityPreserveModel
    {
        public int fp_id { get; set; }
        /// <summary>
        ///  设备编码
        /// </summary>
        public string deviceid { get; set; }
        public string unit_name { get; set; }
        public string unit_code { get; set; }
        public string fa_number { get; set; }
        public string fa_status_text { get; set; }
        /// <summary>
        ///  跟踪开始时间
        /// </summary>
        public string tail_after { get; set; }
        public string touch_phone { get; set; }
        public string install_name { get; set; }
        public string install_id { get; set; }
        public string install_date { get; set; }
        public string skiller { get; set; }
        public string skiller_id { get; set; }
        public string user_note { get; set; }
        public string touch_name { get; set; }
        public string area_code { get; set; }
    }
}
