using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    /// 出工信息、发放明细、薪资发放信息 所有实体类
    /// </summary>
    public class WorkInformationModel
    {
        public string PersonID { get; set; }
        public string UnitID { get; set; }
        public DateTime? WGJG0212 { get; set; }
        public string UnitChildID { get; set; }
        public string WGJG0209 { get; set; }
        public DateTime? WGJGFather { get; set; }
        public string B0002 { get; set; }
        public string B0001 { get; set; }
        public string E0386 { get; set; }
        public string A0145 { get; set; }
        public string WGJG0203 { get; set; }
        public string WGJG0211 { get; set; }
        public DateTime? WGJG0201 { get; set; }
        public DateTime? WGJG0202 { get; set; }
        public string A0101 { get; set; }
        public string A0177 { get; set; }
        public decimal? WGJG0204 { get; set; }
        public int WGJG0205 { get; set; }
        public int WGJG0206 { get; set; }
        public decimal? WGJG0207 { get; set; }
        public decimal? WGJG0208 { get; set; }
    }
}
