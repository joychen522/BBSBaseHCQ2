using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  系统管理>用户管理数据层
    /// </summary>
    public partial interface IT_UserDAL
    {
        /// <summary>
        ///  添加用户对象
        /// </summary>
        /// <param name="user">用户对象</param>
        void AddUser(HCQ2_Model.T_User user);
        /// <summary>
        ///  编辑用户对象
        /// </summary>
        /// <param name="user">用户对象</param>
        void EditUser(HCQ2_Model.T_User user,string selUnit);
        /// <summary>
        ///  获取记录条数
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        int GetCountByData(string keyword, int folder_id);
        /// <summary>
        ///  获取用户数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页量</param>
        /// <returns></returns>
        List<HCQ2_Model.ViewModel.SysAdmin.UserModel> GetUserLimt(string keyword,int page,int rows);
        /// <summary>
        ///  根据组织机构获取用户
        /// </summary>
        /// <param name="unitid">组织机构代码</param>
        /// <param name="keyword">关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页量</param>
        /// <returns></returns>
        List<HCQ2_Model.T_User> GetUserByUnitID(int folder_id, string keyword, int page, int rows);
    }
}
