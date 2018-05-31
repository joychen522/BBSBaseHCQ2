using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    ///  组织机构用户登录模型
    /// </summary>
    public class SysLoginModel
    {
        /// <summary>
        ///  登录名
        /// </summary>
        [DisplayName("登录名")]
        [Required]
        public string user_name { get; set; }
        /// <summary>
        ///  密码
        /// </summary>
        [DisplayName("密码")]
        [Required]
        public string user_password { get; set; }
    }
}
