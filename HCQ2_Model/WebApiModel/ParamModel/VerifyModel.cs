using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    public class VerifyModel:BaseModel
    {
        /// <summary>
        ///  身份证
        /// </summary>
        [DisplayName("身份证")]
        [Required]
        public string user_identify { get; set; }
    }
}
