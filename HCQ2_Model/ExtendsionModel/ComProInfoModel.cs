using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ExtendsionModel
{
    public class ComProInfoModel
    {
        public string txtSearchName { get; set; }
        public string JianDieUnitID { get; set; }
        public int page { get; set; } = 1;
        public int rows { get; set; } = 10;
    }
}
