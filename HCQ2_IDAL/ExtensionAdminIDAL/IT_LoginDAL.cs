using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  登录记录数据层
    /// </summary>
    public partial interface IT_LoginDAL
    {
        /// <summary>
        ///  登录成功时编辑
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        int EditLoginByOk(HCQ2_Model.T_Login login);
        /// <summary>
        ///  登录失败时编辑
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        int EditLoginByError(HCQ2_Model.T_Login login);
        /// <summary>
        ///  添加记录
        /// </summary>
        /// <param name="login">登录记录对象</param>
        /// <returns></returns>
        int AddLoginInfo(HCQ2_Model.T_Login login);
    }
}
