using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  用户组-角色关联数据层接口定义
    /// </summary>
    public partial interface IT_RoleGroupRelationDAL
    {
        /// <summary>
        ///  获取用户组-角色对应数据
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        List<HCQ2_Model.T_Role> GetRoleAndGroupData(string roleName, int page, int rows);
        /// <summary>
        ///  统计用户组-用户记录
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        int GetRoleAndGroupDataCount(string roleName);
    }
}
