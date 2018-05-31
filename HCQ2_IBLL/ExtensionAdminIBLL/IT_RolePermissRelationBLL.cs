using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  角色--权限设置业务模型接口
    /// </summary>
    public partial interface IT_RolePermissRelationBLL
    {
        /// <summary>
        ///  保存菜单--权限设置数据
        /// </summary>
        /// <param name="roles">菜单选择集合</param>
        /// <param name="per_id">权限ID</param>
        /// <returns></returns>
        bool SaveRoleLimitData(string roles, int role_id);
        /// <summary>
        ///  根据角色ID 获取所有权限数据
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_RolePermissRelation> GetRoleLimitData(int role_id);
    }
}
