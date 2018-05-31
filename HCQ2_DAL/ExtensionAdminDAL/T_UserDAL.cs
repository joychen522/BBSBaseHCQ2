using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_UserDAL:HCQ2_IDAL.IT_UserDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        public void AddUser(T_User user)
        {
            base.Add(user);
        }
        /// <summary>
        ///  编辑用户
        /// </summary>
        /// <param name="user">用户</param>
        public void EditUser(T_User user,string selUnit)
        {
            if(string.IsNullOrEmpty(selUnit))
                Modify(user, s => s.user_id == user.user_id, "user_name", "login_name", "user_sex",
                "user_qq", "user_email", "user_phone", "user_address", "user_birth", "user_note", "user_type", "user_identify");
            else
                Modify(user, s => s.user_id == user.user_id, "user_name", "login_name", "user_sex",
                "user_qq", "user_email", "user_phone", "user_address", "user_birth", "user_note", "user_identify");
        }

        public int GetCountByData(string keyword, int folder_id)
        {
            //系统成员根据名称查询
            if (folder_id<=0 && !string.IsNullOrEmpty(keyword))
              return  HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(
                    string.Format(
                        "select COUNT(*) from T_User where (user_name like '%{0}%' or login_name like '%{0}%') and user_id not in(select user_id from T_Org_User)",
                        keyword)));
            //单位下 根据名查询
            if (folder_id>0 && !string.IsNullOrEmpty(keyword))
                return (from user in db.Set<T_User>()
                    join org in db.Set<T_Org_User>() on user.user_id equals org.user_id
                    where org.UnitID.Equals(folder_id.ToString()) && user.user_name.Contains(keyword)
                    select user).Count();
            //单位下所有
            if(folder_id >0&& string.IsNullOrEmpty(keyword))
                return (from user in db.Set<T_User>()
                        join org in db.Set<T_Org_User>() on user.user_id equals org.user_id
                        where org.UnitID.Equals(folder_id.ToString())
                        select user).Count();
            //系统用户所有
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(
                    "select COUNT(*) from T_User where user_id not in(select user_id from T_Org_User)"));
        }

        public List<UserModel> GetUserLimt(string keyword, int page, int rows)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select top {0} * from(
SELECT users.*,limuser.id,ROW_NUMBER() OVER(order by users.user_id) as DispOrder FROM
(SELECT * FROM dbo.T_User where user_name<>'' ",rows));
            if (!string.IsNullOrEmpty(keyword))
                sb.Append(string.Format(" and (login_name like '%{0}%' or user_name like '%{0}%') ", keyword));
            sb.Append(string.Format(@")  users LEFT JOIN 
(SELECT id, user_id FROM dbo.T_LimitUser) limuser ON users.user_id = limuser.user_id 
	WHERE users.user_id NOT IN(SELECT user_id FROM dbo.T_Org_User) 
)as A where A.DispOrder>{0}", (page - 1)*rows));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<UserModel>(dt,true);
            //Linq写法
            //var query =
            //    (from o in base.db.Set<T_User>()
            //        join limuser in db.Set<T_LimitUser>() on o.user_id equals limuser.user_id
            //        select new
            //        {
            //            o.user_id,o.user_name,o.login_name,o.user_pwd,o.user_sex,o.user_qq,
            //            o.user_email,o.user_phone,o.user_address,o.user_birth,o.user_note,limuser.id,
            //            limuser.from_time,limuser.to_time
            //        }).ToList();
            //return query;
        }
        /// <summary>
        ///  实现根据组织机构获取用户
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<T_User> GetUserByUnitID(int folder_id, string keyword, int page, int rows)
        {
            if (folder_id<=0)
                return null;
            sb?.Clear();
            sb.AppendFormat(@"SELECT TOP {0} users.* FROM (SELECT * FROM dbo.T_User WHERE 1=1 ", rows);
            if (!string.IsNullOrEmpty(keyword))
                sb.AppendFormat(" AND user_name LIKE '%{0}%' ", keyword);
            sb.AppendFormat(@") users INNER JOIN
            (SELECT ROW_NUMBER() OVER(ORDER BY user_id ASC) AS rowNumber,user_id FROM dbo.T_Org_User WHERE UnitID 
            IN(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path LIKE (SELECT folder_path FROM T_OrgFolder WHERE folder_id={0})+'%')) org ON
            org.user_id = users.user_id WHERE org.rowNumber>{1};", folder_id, (page - 1) * rows);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_User>(dt);
            //只查询自己本身所在区域用户
            //if (string.IsNullOrEmpty(keyword))
            //    return (from user in db.Set<T_User>()
            //        join org in db.Set<T_Org_User>()
            //            on user.user_id equals org.user_id
            //        where org.UnitID.Equals(folder_id.ToString())
            //        select user).OrderBy(u => u.user_id).ToList().Skip(rows*(page - 1)).Take(rows).ToList();
            //return (from user in db.Set<T_User>()
            //        join org in db.Set<T_Org_User>()
            //            on user.user_id equals org.user_id
            //        where org.UnitID.Equals(folder_id.ToString()) && user.user_name.Contains(keyword)
            //        select user).OrderBy(u => u.user_id).ToList().Skip(rows*(page - 1)).Take(rows).ToList();
        }
    }
}
