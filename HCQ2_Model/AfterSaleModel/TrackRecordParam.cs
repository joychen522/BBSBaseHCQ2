using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    /// <summary>
    ///  跟踪记录模型
    /// </summary>
    public class TrackRecordParam
    {
        /// <summary>
        ///  设备编码
        /// </summary>
        public string fa_number { get; set; }
        public string area_code { get; set; }
        public string unit_code { get; set; }
        public string trackStatus { get; set; }
        public string trackDate { get; set; }
        public int page { get; set; }
        public int rows { get; set; }
    }
}
