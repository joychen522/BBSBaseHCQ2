using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel.SysAdmin
{
    /// <summary>
    ///  用户T_User
    ///  业务模型
    /// </summary>
    public class UserModel
    {
        [DisplayName("主键值")]
        public int user_id { get; set; }
        [Required]
        [DisplayName("用户名")]
        public string user_name { get; set; }
        [Required]
        [DisplayName("登录名")]
        public string login_name { get; set; }
        //[Required]
        [DisplayName("密码")]
        public string user_pwd { get; set; }
        [DisplayName("性别")]
        public string user_sex { get; set; }
        [DisplayName("所属角色")]
        public string user_role { get; set; }
        [DisplayName("QQ")]
        public string user_qq { get; set; }
        [DisplayName("Email")]
        public string user_email { get; set; }
        [DisplayName("电话号码")]
        public string user_phone { get; set; }
        [DisplayName("联系地址")]
        public string user_address { get; set; }
        [DisplayName("出生日期")]
        public string user_birth { get; set; }
        [DisplayName("备注")]
        public string user_note { get; set; }
        [DisplayName("用户类别")]
        public string user_type { get; set; }
        [DisplayName("身份证")]
        public string user_identify { get; set; }
        public int id { get; set; }
        /// <summary>
        ///  组织机构代码
        /// </summary>
        public int orgUnit { get; set; }
    }
}
