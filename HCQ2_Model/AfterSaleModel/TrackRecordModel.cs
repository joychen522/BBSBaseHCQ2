using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    public class TrackRecordModel
    {
        public int tr_id { get; set; }
        public string unit_name { get; set; }
        public string unit_code { get; set; }
        public string fa_number { get; set; }
        public string tr_status { get; set; }
        public string tr_status_text { get; set; }
        public string track_name { get; set; }
        public string track_date { get; set; }
        public string tr_note { get; set; }
        public string area_code { get; set; }
    }
}
