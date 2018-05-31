using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  公司详情返回值
    /// </summary>
    public class CompProInfoResult
    {
        /// <summary>
        ///  单位名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        ///  职位类型
        /// </summary>
        public string postType { get; set; }
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
        public string busNote { get; set; }
    }
}
