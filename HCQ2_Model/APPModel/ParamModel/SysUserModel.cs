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
    ///  APP用户信息
    /// </summary>
    public class SysUserModel
    {
        /// <summary>
        ///  身份证
        /// </summary>
        [DisplayName("身份证")]
        [Required]
        [RegularExpression("^(^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$)|(^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[Xx])$)$",ErrorMessage ="身份证验证失败")]//验证身份证
        public string user_identify { get; set; }
        /// <summary>
        ///  电话
        /// </summary>
        [DisplayName("手机号码")]
        [Required]
        [RegularExpression("^1[0-9]{10}$", ErrorMessage = "手机号验证失败")]//验证手机
        public string user_phone { get; set; }
        /// <summary>
        ///  用户名
        /// </summary>
        [Required]
        public string user_name { get; set; }
        /// <summary>
        ///  登录名
        /// </summary>
        [Required]
        public string login_name { get; set; }
        /// <summary>
        ///  密码
        /// </summary>
        [Required]
        public string user_password { get; set; }
    }

    public class BaseUser: BaseAPI
    {
        /// <summary>
        ///  电话
        /// </summary>
        [DisplayName("手机号码")]
        [Required]
        [RegularExpression("^1[0-9]{10}$", ErrorMessage = "手机号验证失败")]//验证手机
        public string user_phone { get; set; }
        /// <summary>
        ///  登录名
        /// </summary>
        [Required]
        public string login_name { get; set; }
        /// <summary>
        ///  密码 
        /// </summary>
        [Required]
        public string user_password { get; set; }
    }
}
