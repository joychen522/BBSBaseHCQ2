using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    /// <summary>
    ///  APP用户注册模型
    /// </summary>
    public class AppRegUserModel
    {
        [DisplayName("身份证")]
        [Required]
        [RegularExpression("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$")]//验证身份证
        public string user_identify { get; set; }
        [DisplayName("手机号码")]
        [Required]
        [RegularExpression("^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\\d{8}$ ")]//验证手机
        public string user_phone { get; set; }
    }
}
