using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  用户组-角色业务层实现
    /// </summary>
    public partial class T_RoleGroupRelationBLL:HCQ2_IBLL.IT_RoleGroupRelationBLL
    {
        public List<T_Role> GetRoleAndGroupData(string roleName, int page, int rows)
        {
            return DBSession.IT_RoleGroupRelationDAL.GetRoleAndGroupData(roleName, page, rows);
        }

        public int GetRoleAndGroupCount(string roleName)
        {
            return DBSession.IT_RoleGroupRelationDAL.GetRoleAndGroupDataCount(roleName);
        }
        /// <summary>
        ///  保存
        /// </summary>
        /// <param name="userGroups"></param>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public bool SaveRoleGroupData(string userGroups, int role_id)
        {
            if (role_id <= 0)
                return false;
            //保存之前删除之前设置的用户
            Delete(s => s.role_id == role_id);
            if (string.IsNullOrEmpty(userGroups))
                return true;
            string[] str = userGroups.Trim(',').Split(',');
            foreach (string item in str)
            {
                Add(new T_RoleGroupRelation()
                {
                    role_id = role_id,
                    group_id = HCQ2_Common.Helper.ToInt(item)
                });
            }
            return true;
        }

        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public List<T_RoleGroupRelation> GetRoleGroupData(int role_id)
        {
            if (role_id <= 0)
                return null;
            return Select(s => s.role_id == role_id);
        }
    }
}
