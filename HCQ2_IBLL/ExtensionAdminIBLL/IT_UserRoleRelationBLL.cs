using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户--角色设置业务接口定义
    /// </summary>
    public partial interface IT_UserRoleRelationBLL
    {
        /// <summary>
        ///  保存用户--角色数据
        /// </summary>
        /// <param name="userRoles">用户字符串</param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        bool SaveUserRoleData(string userRoles,int user_id);
        /// <summary>
        ///  根据角色id获取所有对应的用户数据集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_UserRoleRelation> GetUserRoleData(int user_id);
    }
}
