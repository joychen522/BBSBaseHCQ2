using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2UI_Helper
{
    public class UnitHelper
    {
        public static HCQ2_IBLL.IUnitWork unitWork
        {
            get
            {
                return HCQ2_DI.SpringHelper.GetObject<HCQ2_IBLL.IUnitWork>("BllUnitWork");
            }
        }
    }
}
