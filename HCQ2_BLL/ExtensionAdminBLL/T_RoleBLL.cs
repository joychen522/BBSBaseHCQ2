using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2_BLL
{
    public partial class T_RoleBLL : HCQ2_IBLL.IT_RoleBLL
    {
        /// <summary>
        /// 获取所有的角色列表
        /// </summary>
        /// <returns></returns>
        public List<T_Role> GetRoleData(string roleName, int page, int rows,string sm_code)
        {
            if (!string.IsNullOrEmpty(roleName))
                return Select<int>(s => s.role_name.Contains(roleName) && s.sm_code.Equals(sm_code), s => s.role_order, page, rows, true);
            return Select<int>(s => (!string.IsNullOrEmpty(s.role_name)) && s.sm_code.Equals(sm_code), s => s.role_order,
                page, rows, true);
        }
        /// <summary>
        ///  编辑角色实现
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool EditRole(T_Role role,int id)
        {
            if (role == null || id<=0)
                return false;
            int modify = Modify(role, s => s.role_id == id, "role_name", "role_code","role_note","sm_code");
            return true;
        }
        /// <summary>
        ///  添加角色实现
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool AddRole(T_Role role)
        {
            if (null == role)
                return false;
            int add = Add(role);
            if (add > 0)
                return true;
            return false;
        }
        /// <summary>
        ///  删除角色实现
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelRole(int id)
        {
            if (id <= 0)
                return false;
            //1.删除角色表
            int del = Delete(s => s.role_id == id);
            //2. 删除用户-角色表
            DBSession.IT_UserRoleRelationDAL.Delete(s => s.role_id == id);
            //3. 删除用户组-角色表
            DBSession.IT_RoleGroupRelationDAL.Delete(s => s.role_id == id);
            //4. 删除角色-权限表
            DBSession.IT_RolePermissRelationDAL.Delete(s => s.role_id == id);
            if (del > 0)
                return true;
            return false;
        }
    }
}
