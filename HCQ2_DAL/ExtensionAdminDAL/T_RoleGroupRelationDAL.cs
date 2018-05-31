using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_RoleGroupRelationDAL:HCQ2_IDAL.IT_RoleGroupRelationDAL
    {
        /// <summary>
        ///  获取用户组-角色数据集合
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<T_Role> GetRoleAndGroupData(string roleName, int page, int rows)
        {
            List<T_Role> list = new List<T_Role>();
            if (!string.IsNullOrEmpty(roleName))
            {
                var query = (from roleGroup in db.Set<T_RoleGroupRelation>()
                             join roles in db.Set<T_Role>()
                                 on roleGroup.role_id equals roles.role_id
                             where roles.role_name.Contains(roleName)
                             select roles).OrderByDescending(s => s.role_id).ToList().Skip(rows * (page - 1)).Take(rows);
                foreach (var item in query)
                {
                    list.Add(item);
                }
            }
            else
            {
                var query = (from roleGroup in db.Set<T_RoleGroupRelation>()
                             join roles in db.Set<T_Role>()
                                 on roleGroup.role_id equals roles.role_id
                             select roles).OrderByDescending(s => s.role_id).ToList().Skip(rows * (page - 1)).Take(rows);
                foreach (var item in query)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        ///  统计数据记录
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int GetRoleAndGroupDataCount(string roleName)
        {
            int count;
            if (!string.IsNullOrEmpty(roleName))
            {
                count = (from roleGroup in db.Set<T_RoleGroupRelation>()
                         join roles in db.Set<T_Role>()
                             on roleGroup.role_id equals roles.role_id
                         where roles.role_name.Contains(roleName)
                         select roles).ToList().Count();
            }
            else
            {
                count = (from roleGroup in db.Set<T_RoleGroupRelation>()
                         join roles in db.Set<T_Role>()
                             on roleGroup.role_id equals roles.role_id
                         select roles).ToList().Count();
            }
            return count;
        }
    }
}
