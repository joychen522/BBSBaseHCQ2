using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    /// <summary>
    ///  公司详情
    /// </summary>
    public  class CompProInfoParam: BaseAPI
    {
        /// <summary>
        ///  公司主键
        /// </summary>
        [Required]
        public int com_id { get; set; }
    }
}
