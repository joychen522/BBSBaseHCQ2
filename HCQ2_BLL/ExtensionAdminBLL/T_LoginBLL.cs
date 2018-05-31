using System;
using System.Linq;
using HCQ2_Model;
using HCQ2_Model.EnumModel;

namespace HCQ2_BLL
{
    /// <summary>
    ///  记录登录信息业务层实现
    /// </summary>
    public partial class T_LoginBLL: HCQ2_IBLL.IT_LoginBLL
    {
        /// <summary>
        ///  实现添加/编辑登录记录
        /// </summary>
        public int AddLoginInfo(T_Login loginUser)
        {
            T_Login login = selectLoginById(loginUser.user_id);
            if (login != null)
                return EditLoginInfo(loginUser, login);
            return DBSession.IT_LoginDAL.AddLoginInfo(new T_Login()
            {
                user_id = loginUser.user_id,
                login_count = 1,
                login_time = loginUser.login_time,
                login_ip = loginUser.login_ip,
                if_false = loginUser.if_false
            });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public int EditLoginInfo(T_Login loginUser, T_Login login)
        {
            if (loginUser.if_false)
            {
                //登录成功
                login.login_count = login.login_count + 1;
                login.error_count = 0;
                login.last_time = login.login_time;
                login.login_time = DateTime.Now;
                login.last_ip = login.login_ip;
                login.login_ip = loginUser.login_ip;
                login.if_false = loginUser.if_false;
                int temp = DBSession.IT_LoginDAL.EditLoginByOk(login);
                if(temp>0 && login.login_count > 10000)
                {
                    login.login_count = 1;
                    Modify(login, s => s.user_id == login.user_id, "login_count");
                }
                return temp;
            }
            //登录失败
            login.error_count += 1;
            login.login_time = DateTime.Now;
            login.login_ip = loginUser.login_ip;
            login.if_false = loginUser.if_false;
            login.note = loginUser.note;
            return DBSession.IT_LoginDAL.EditLoginByError(login);
            
        }
        /// <summary>
        ///  实现查询登录信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public T_Login selectLoginById(int userId)
        {
            return DBSession.IT_LoginDAL.Select(s => s.user_id == userId).FirstOrDefault();
        }
    }
}
