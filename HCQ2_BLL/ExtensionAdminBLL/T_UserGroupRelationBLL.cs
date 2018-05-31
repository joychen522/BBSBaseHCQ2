using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  用户组-用户管理业务层实现
    /// </summary>
    public partial class T_UserGroupRelationBLL:HCQ2_IBLL.IT_UserGroupRelationBLL
    {
        /// <summary>
        ///  获取用户组-用户对应数据实现
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<T_User> GetUserAndGroupData(string userName, int page, int rows)
        {
            return DBSession.IT_UserGroupRelationDAL.GetUserAndGroupData(userName, page, rows);
        }

        /// <summary>
        ///  获取数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserAndGroupCount(string userName)
        {
            return DBSession.IT_UserGroupRelationDAL.GetUserAndGroupDataCount(userName);
        }
        /// <summary>
        ///  保存
        /// </summary>
        /// <param name="userGroups"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public bool SaveUserGroupData(string userGroups, int user_id)
        {
            if (user_id <= 0)
                return false;
            //保存之前删除之前设置的用户
            Delete(s => s.user_id == user_id);
            if (string.IsNullOrEmpty(userGroups))
                return true;
            string[] str = userGroups.Trim(',').Split(',');
            foreach (string item in str)
            {
                Add(new T_UserGroupRelation()
                {
                    user_id = user_id,
                    group_id = HCQ2_Common.Helper.ToInt(item)
                });
            }
            return true;
        }
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_UserGroupRelation> GetUserGroupData(int user_id)
        {
            if (user_id <= 0)
                return null;
            return Select(s => s.user_id == user_id);
        }
    }
}
