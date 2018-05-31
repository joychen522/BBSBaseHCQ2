using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    public class BaneDetectionModel
    {
        /// <summary>
        ///  个人状态
        ///  正常，过检 两种状态
        /// </summary>
        public string user_status { get; set; }
        /// <summary>
        ///  本次检测日期
        /// </summary>
        public string the_date { get; set; }
        /// <summary>
        ///  下次检测日期
        /// </summary>
        public string next_date { get; set; }
        /// <summary>
        ///  管控到期
        /// </summary>
        public string control_date { get; set; }
    }
}
