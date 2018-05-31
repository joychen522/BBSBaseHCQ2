using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_DAL_MSSQL
{
    /// <summary>
    ///  用户组-用户数据层实现
    /// </summary>
    public partial class T_UserGroupRelationDAL:HCQ2_IDAL.IT_UserGroupRelationDAL
    {
        public List<T_User> GetUserAndGroupData(string userName, int page, int rows)
        {
            List<T_User> list = new List<T_User>();
            if (!string.IsNullOrEmpty(userName))
            {
               var query = (from userGroup in db.Set<T_UserGroupRelation>()
                    join users in db.Set<T_User>()
                        on userGroup.user_id equals users.user_id
                    where users.user_name.Contains(userName)
                    select users).OrderByDescending(s => s.user_id).ToList().Skip(rows * (page - 1)).Take(rows);
                foreach (var item in query)
                {
                    list.Add(item);
                }
            }
            else
            {
                var query = (from userGroup in db.Set<T_UserGroupRelation>()
                         join users in db.Set<T_User>()
                             on userGroup.user_id equals users.user_id
                         select users).OrderByDescending(s => s.user_id).ToList().Skip(rows*(page - 1)).Take(rows);
                foreach (var item in query)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        ///  统计数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserAndGroupDataCount(string userName)
        {
            int count;
            if (!string.IsNullOrEmpty(userName))
            {
                count = (from userGroup in db.Set<T_UserGroupRelation>()
                    join users in db.Set<T_User>()
                        on userGroup.user_id equals users.user_id
                    where users.user_name.Contains(userName)
                    select users).ToList().Count();
            }
            else
            {
                count = (from userGroup in db.Set<T_UserGroupRelation>()
                         join users in db.Set<T_User>()
                             on userGroup.user_id equals users.user_id
                         select users).ToList().Count();
            }
            return count;
        }
    }
}
