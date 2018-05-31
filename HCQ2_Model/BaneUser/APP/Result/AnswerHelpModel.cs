using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  答题须知模型
    /// </summary>
    public class AnswerHelpModel
    {
        /// <summary>
        ///  答题分钟数
        /// </summary>
        public int minute { get; set; }
        /// <summary>
        ///  答题注意事项
        /// </summary>
        public List<string> options { get; set; }
    }
}
