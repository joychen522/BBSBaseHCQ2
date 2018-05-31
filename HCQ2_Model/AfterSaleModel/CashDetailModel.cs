using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    public class CashDetailModel
    {
        public int cd_id { get; set; }
        public int ip_id { get; set; }
        public string cash_start_date { get; set; }
        public string cash_end_date { get; set; }
        public decimal cash_money { get; set; }
    }
}
