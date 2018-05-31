using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  记录登录信息业务接口层
    /// </summary>
    public partial interface IT_LoginBLL
    {
        /// <summary>
        ///  添加登录信息记录
        /// </summary>
        /// <param name="loginUser">登录记录对象</param>
        int AddLoginInfo(HCQ2_Model.T_Login loginUser);
        /// <summary>
        ///  通过用户ID查询登录信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        HCQ2_Model.T_Login selectLoginById(int userId);
    }
}
