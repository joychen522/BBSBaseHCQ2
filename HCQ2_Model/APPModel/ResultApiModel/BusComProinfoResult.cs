using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  公司详情
    /// </summary>
    public class BusComProinfoResult
    {
        /// <summary>
        ///  公司名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        ///  公司性质
        /// </summary>
        public string busType { get; set; }
        /// <summary>
        ///  公司规模
        /// </summary>
        public string busScale { get; set; }
        /// <summary>
        ///  公司地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        ///  公司描述
        /// </summary>
        public string busNotes { get; set; }
        /// <summary>
        ///  logo
        /// </summary>
        public byte[] logo { get; set; }
    }
}
