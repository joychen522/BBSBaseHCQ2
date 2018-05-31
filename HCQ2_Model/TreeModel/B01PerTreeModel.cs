using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.TreeModel
{
    public class B01PerTreeModel
    {
        public string UnitID { get; set; }
        public string KeyParent { get; set; }
        public string UnitName { get; set; }
        public string text { get; set; }
        public string UnitType { get; set; }
        public string B0107 { get; set; }
        public string B0108 { get; set; }
        public string B0120 { get; set; }
        public string D010H { get; set; }
        public string B0175 { get; set; }
        public string UnitStartDate { get; set; }
        public string UnitEndDate { get; set; }
        public string B0111 { get; set; }
        public string B0112 { get; set; }
        public string B0130 { get; set; }
        public string B0113 { get; set; }
        public decimal? B0114 { get; set; }
        public decimal? B0115 { get; set; }
        public decimal? B0116 { get; set; }
        public string B0118 { get; set; }
        public string project_status { get; set; }
        public List<B01PerTreeModel> nodes { get; set; }
    }
}
