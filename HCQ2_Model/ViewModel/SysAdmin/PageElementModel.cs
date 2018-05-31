using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel.SysAdmin
{
    public class PageElementModel
    {
        public int pe_id { get; set; }
        public string pe_name { get; set; }
        public string pe_code { get; set; }
        public string pe_event { get; set; }
        public string pe_func { get; set; }
        public string pe_create_time { get; set; }
        public string pe_note { get; set; }
    }
}
