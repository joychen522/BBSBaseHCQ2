using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  用户--角色业务实现层
    /// </summary>
    public partial class T_UserRoleRelationBLL:HCQ2_IBLL.IT_UserRoleRelationBLL
    {
        /// <summary>
        ///  保存数据方法实现
        /// </summary>
        /// <param name="userRoles"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public bool SaveUserRoleData(string userRoles, int user_id)
        {
            if (user_id <= 0)
                return false;
            //保存之前删除之前设置的用户
            Delete(s => s.user_id == user_id);
            if (string.IsNullOrEmpty(userRoles))
                return true;
            string[] str = userRoles.Trim(',').Split(',');
            foreach (string item in str)
            {
                Add(new T_UserRoleRelation()
                {
                    user_id = user_id,
                    role_id = HCQ2_Common.Helper.ToInt(item)
                });
            }
            return true;
        }
        /// <summary>
        ///  获取数据方法实现
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_UserRoleRelation> GetUserRoleData(int user_id)
        {
            if (user_id <= 0)
                return null;
            return Select(s => s.user_id == user_id);
        }
    }
}
