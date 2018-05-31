using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  发放信息
    /// </summary>
    public class DebtWGJG01Model
    {
        /// <summary>
        ///  发放日期 yyyy-MM-dd
        /// </summary>
        public string WGJG0102 { get; set; }
        /// <summary>
        ///  发放主题
        /// </summary>
        public string WGJG0103 { get; set; }
        /// <summary>
        ///  归档标记
        /// </summary>
        public string WGJG0101 { get; set; }
    }
}
