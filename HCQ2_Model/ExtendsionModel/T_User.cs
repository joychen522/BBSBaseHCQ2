using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2_Model
{
    /// <summary>
    ///  用户POCO类
    /// </summary>
    public partial class T_User
    {
        public UserModel ToPOCO()
        {
            return new UserModel()
            {
                user_id = this.user_id,
                user_name = this.user_name,
                login_name = this.login_name,
                user_pwd = "",//密码不能解码
                user_sex = this.user_sex,
                user_qq = this.user_qq,
                user_email = this.user_email,
                user_phone = this.user_phone,
                user_address = this.user_address,
                user_birth = string.Format("{0:yyyy-MM-dd}", this.user_birth),
                user_note = this.user_note
            };
        }
    }
}
