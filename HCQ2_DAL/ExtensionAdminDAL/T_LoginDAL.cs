using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_IDAL;
using HCQ2_Model;

namespace HCQ2_DAL_MSSQL
{
    /// <summary>
    ///  登录记录数据层实现
    /// </summary>
    public partial class T_LoginDAL:  IT_LoginDAL
    {
        /// <summary>
        ///  登录成功时编辑
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        public int EditLoginByOk(T_Login login)
        {
            return base.Modify(login, s => s.user_id == login.user_id, "login_count", "error_count", "login_time",
                "login_ip", "last_time", "last_ip", "if_false");
        }
        /// <summary>
        ///  登录失败时编辑
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        public int EditLoginByError(T_Login login)
        {
            return base.Modify(login, s => s.user_id == login.user_id, "error_count", "login_time", "login_ip",
                "if_false", "note");
        }
        /// <summary>
        ///  添加登录记录
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        public int AddLoginInfo(T_Login login)
        {
            return base.Add(login);
        }
    }
}
