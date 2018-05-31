using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class WorkTypeCount
    {
        /// <summary>
        ///  工种
        /// </summary>
        public string E0386 { get; set; }
        /// <summary>
        ///  出工数量
        /// </summary>
        public int numCount { get; set; }
        /// <summary>
        ///  未出工数量
        /// </summary>
        public int unCount { get; set; }
    }
}
