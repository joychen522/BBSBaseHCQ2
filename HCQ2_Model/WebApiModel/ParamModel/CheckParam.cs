using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    public class CheckParam
    {
        /// <summary>
        ///  加密字符串
        /// </summary>
        public string match_signature { get; set; }
        /// <summary>
        ///  时间戳
        /// </summary>
        public string match_timestamp { get; set; }
        /// <summary>
        ///  1-20位随机数
        /// </summary>
        public string match_nonce { get; set; }
    }
}
