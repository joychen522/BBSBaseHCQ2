using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户组--用户业务层接口定义
    /// </summary>
    public partial interface IT_UserGroupRelationBLL
    {
        /// <summary>
        ///  获取用户组-用户对应数据集合
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        List<HCQ2_Model.T_User> GetUserAndGroupData(string userName, int page, int rows);
        /// <summary>
        ///  统计数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int GetUserAndGroupCount(string userName);

        /// <summary>
        ///  保存用户--用户组数据
        /// </summary>
        /// <param name="userRoles">用户字符串</param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        bool SaveUserGroupData(string userGroups, int user_id);
        /// <summary>
        ///  根据用户id获取所有对应的组数据集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_UserGroupRelation> GetUserGroupData(int user_id);
    }
}
