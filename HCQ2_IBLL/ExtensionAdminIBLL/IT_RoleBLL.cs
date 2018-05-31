using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_RoleBLL
    {
        /// <summary>
        /// 获取所有的角色列表
        /// </summary>
        /// <returns></returns>
        List<T_Role> GetRoleData(string keyword,int page,int rows,string sm_code);
        /// <summary>
        ///  编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool EditRole(T_Role role,int id);
        /// <summary>
        ///  添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool AddRole(T_Role role);
        /// <summary>
        ///  删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DelRole(int id);
    }
}
