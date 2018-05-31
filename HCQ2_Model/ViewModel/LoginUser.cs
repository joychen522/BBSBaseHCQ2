using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    /// <summary>
    ///  登录视图 模型
    /// </summary>
    public class LoginUser
    {
        [Required]
        [DisplayName("登录名")]
        public string LoginName { get; set; }
        [Required]
        [DisplayName("密码")]
        public string UserPwd { get; set; }
        [Required]
        [DisplayName("验证码")]
        public string ReCode { get; set; }

        [DisplayName("是否记住用户名&密码")]
        public bool IsAlways { get; set; }
    }
}
