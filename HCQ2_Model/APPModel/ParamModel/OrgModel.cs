using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    public class OrgModel:BaseAPI
    {
        /// <summary>
        ///  单位代码
        /// </summary>
        [Required]
        public string orgid { get; set; }
    }
}
