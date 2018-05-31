using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  角色--权限设置业务实现层
    /// </summary>
    public partial class T_RolePermissRelationBLL: HCQ2_IBLL.IT_RolePermissRelationBLL
    {
        /// <summary>
        ///  保存实现
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        public bool SaveRoleLimitData(string roles, int role_id)
        {
            if (role_id <= 0)
                return false;
            //保存之前删除之前设置的权限
            Delete(s => s.role_id == role_id);
            if (string.IsNullOrEmpty(roles))
                return true;
            string[] str = roles.Trim(',').Split(',');
            foreach (string item in str)
            {
                Add(new T_RolePermissRelation()
                {
                     role_id= role_id,
                     per_id= HCQ2_Common.Helper.ToInt(item)
                });
            }
            return true;
        }
        /// <summary>
        ///  获取数据实现
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public List<T_RolePermissRelation> GetRoleLimitData(int role_id)
        {
            if (role_id <= 0)
                return null;
            return Select(s => s.role_id == role_id);
        }
    }
}
