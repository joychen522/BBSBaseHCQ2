using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    /// <summary>
    ///  职位详情
    /// </summary>
    public class PostDetialParam: BaseAPI
    {
        /// <summary>
        ///  招聘信息主键ID
        /// </summary>
        [Required]
        public int use_id { get; set; }
    }
    public class BusComProinfo: BaseAPI
    {
        /// <summary>
        ///  单位ID
        /// </summary>
        [Required]
        public int com_id { get; set; }
    }
}
