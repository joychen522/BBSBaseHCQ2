using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户组-角色表业务接口层定义
    /// </summary>
    public partial interface IT_RoleGroupRelationBLL
    {
        /// <summary>
        ///  获取用户组-角色对应数据集合
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        List<HCQ2_Model.T_Role> GetRoleAndGroupData(string roleName, int page, int rows);
        /// <summary>
        ///  统计数量
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        int GetRoleAndGroupCount(string roleName);

        /// <summary>
        ///  保存角色--用户组数据
        /// </summary>
        /// <param name="userRoles">用户组字符串</param>
        /// <param name="role_id">用户id</param>
        /// <returns></returns>
        bool SaveRoleGroupData(string userGroups, int role_id);
        /// <summary>
        ///  根据角色id获取所有对应的组数据集合
        /// </summary>
        /// <param name="role_id">角色id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_RoleGroupRelation> GetRoleGroupData(int role_id);
    }
}
