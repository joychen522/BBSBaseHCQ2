using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Params
{
    public class BaneLoginModel
    {
        /// <summary>
        ///  身份证
        /// </summary>
        [DisplayName("身份证")]
        [Required]
        //[RegularExpression("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$")]//验证身份证
        public string user_identify { get; set; }
        /// <summary>
        ///  密码
        /// </summary>
        [DisplayName("密码")]
        [Required]
        public string user_pwd { get; set; }
    }
}
