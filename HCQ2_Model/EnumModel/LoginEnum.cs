using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.EnumModel
{
    public class LoginEnum
    {
        /// <summary>
        ///  登录错误限制次数
        /// </summary>
        public const int LOGIN_ERR_NUM = 5;
        /// <summary>
        ///  限制小时数
        /// </summary>
        public const int WAIT_HOURS = 1;
        /// <summary>
        ///  登陆结果集
        /// </summary>
        public enum LoginResult
        {
            登陆成功 = 0,
            密码错误 = 1,
            用户名不存在 = 2,
            异常信息 = 3,
            密码错误超过限制 = 4
        }
    }
}
