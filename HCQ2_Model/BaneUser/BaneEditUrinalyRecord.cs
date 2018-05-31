using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneEditUrinalyRecord
    {
        public int ur_id { get; set; }
        public string ur_code { get; set; }
        public string user_identify { get; set; }
        public Nullable<System.DateTime> ur_should_date { get; set; }
        public Nullable<System.DateTime> ur_reality_date { get; set; }
        public string ur_site { get; set; }
        public string ur_method { get; set; }
        public string ur_manager { get; set; }
        public string ur_result { get; set; }
        public string ur_result_show { get; set; }
        public string ur_file_name { get; set; }
        public string ur_attach { get; set; }
        public Nullable<System.DateTime> ur_input_date { get; set; }
        public string ur_note { get; set; }
        public int approve_status { get; set; }
        public string user_name { get; set; }
        public string user_sex { get; set; }
        public string user_birth { get; set; }
    }
}
