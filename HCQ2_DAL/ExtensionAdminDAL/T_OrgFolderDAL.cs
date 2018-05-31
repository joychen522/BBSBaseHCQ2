using HCQ2_Model.TreeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HCQ2_Model;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_OrgFolderDAL:HCQ2_IDAL.IT_OrgFolderDAL
    {
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  根据组织机构ID获取所对应的用户集合
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        public List<T_User> GetOrgUsers(OrgTableParamModel model,out int total)
        {
            total = 0;
            if (null == model || model.folder_id <= 0)
                return null;
            if (string.IsNullOrEmpty(model.keyword))
            {
                total = (from o in db.Set<HCQ2_Model.T_OrgUserRelation>()
                         join s in db.Set<HCQ2_Model.T_User>()
                         on o.user_id equals s.user_id
                         where o.folder_id == model.folder_id
                         select s).ToList().Count;
                return (from o in db.Set<HCQ2_Model.T_OrgUserRelation>()
                        join s in db.Set<HCQ2_Model.T_User>()
                        on o.user_id equals s.user_id
                        where o.folder_id == model.folder_id
                        select s).OrderBy(s => s.user_id).ToList().Skip((model.page - 1) * model.rows).Take(model.rows).ToList();
            }else
            {
                total = (from o in db.Set<HCQ2_Model.T_OrgUserRelation>()
                         join s in db.Set<HCQ2_Model.T_User>()
                         on o.user_id equals s.user_id
                         where o.folder_id == model.folder_id &&  s.user_name.Contains(model.keyword)
                         select s).ToList().Count;
                return (from o in db.Set<HCQ2_Model.T_OrgUserRelation>()
                        join s in db.Set<HCQ2_Model.T_User>()
                        on o.user_id equals s.user_id
                        where o.folder_id == model.folder_id && s.user_name.Contains(model.keyword)
                        select s).OrderBy(s => s.user_id).ToList().Skip((model.page - 1) * model.rows).Take(model.rows).ToList();
            }
        }
        /// <summary>
        ///  获取待分配人员数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetOrgDataByPerson()
        {
            sb?.Clear();
            sb.Append(@"SELECT text=u.user_name,CAST(u.user_id AS NVARCHAR(100)) AS value FROM
                (SELECT user_name,user_id FROM dbo.T_User) u LEFT JOIN
                (SELECT UnitID,user_id FROM dbo.T_Org_User) o ON u.user_id=o.user_id 
            WHERE not exists(SELECT user_id FROM T_OrgUserRelation WHERE user_id=u.user_id); ");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.SelectModel.ListBoxModel>(dt);
        }
        /// <summary>
        ///  获取已分配人员数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetFineOrgDataByPerson(int folder_id)
        {
            if (folder_id <= 0)
                return null;
            sb?.Clear();
            sb.Append(string.Format(@"SELECT text=u.user_name,CAST(u.user_id AS NVARCHAR(100)) AS value FROM
                (SELECT user_name,user_id FROM dbo.T_User WHERE reg_from=0) u LEFT JOIN
                (SELECT UnitID,user_id FROM dbo.T_Org_User) o ON u.user_id=o.user_id
            WHERE exists(SELECT user_id FROM T_OrgUserRelation WHERE folder_id={0} AND user_id=u.user_id); ", folder_id));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.SelectModel.ListBoxModel>(dt);
        }
        /// <summary>
        ///  排除指定组织机构下的人员
        /// </summary>
        /// <param name="list"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        public bool DelOrgUserRelation(List<int> list, int folder_id)
        {
            if (null == list || folder_id <= 0)
                return false;
            sb?.Clear();
            sb.Append(string.Format("DELETE T_OrgUserRelation WHERE folder_id={0} AND user_id IN({1});", folder_id, string.Join(",", list)));
            HCQ2_Common.SQL.SqlHelper.ExecuteNonQuery(sb.ToString());
            return true;
        }
        /// <summary>
        ///  根据权限获取用户代管区域
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_OrgFolder> GetFolderByRelation(int user_id,List<int> perList)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT folder.* FROM 
                (SELECT * FROM dbo.T_OrgFolder) folder INNER JOIN
                (SELECT user_id,unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0}) relation ON folder.folder_id=relation.unit_id ", user_id));
            if (perList!=null && perList.Count > 0)
                sb.AppendFormat(@"UNION 
                SELECT areaFolder.*  FROM 
                (SELECT * FROM dbo.T_OrgFolder) areaFolder INNER JOIN
                (SELECT area_code,per_id FROM T_AreaPermissRelation WHERE per_id IN({0})) permiss ON areaFolder.folder_id=permiss.area_code ", string.Join(",", perList));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_OrgFolder>(dt);
        }
        /// <summary>
        ///  根据单位代码获取子单位信息集合
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public List<T_OrgFolder> GetOrgFolderInfo(int user_id)
        {
            List<T_OrgFolder> list = null;
            //ID小于0表示系统用户 查询全部
            if (user_id <= 0)
                list = Select<string>(o => o.folder_id > 0, o => o.folder_path);
            else
            {
                sb?.Clear();
                sb.AppendFormat(@"SELECT * FROM dbo.T_OrgFolder WHERE folder_path LIKE 
                (SELECT folder_path FROM T_OrgFolder WHERE folder_id=
                (SELECT UnitID FROM dbo.T_Org_User WHERE user_id={0}))+'%';", user_id);
                DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
                list = HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_OrgFolder>(dt);
            }
            return list;
        }
        /// <summary>
        ///  根据用户ID统计 所属区域及子区域
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetAreaCountByID(int user_id)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT COUNT(*) FROM dbo.T_OrgFolder WHERE folder_path LIKE 
            (SELECT folder_path FROM T_OrgFolder WHERE folder_id=
            (SELECT UnitID FROM dbo.T_Org_User WHERE user_id={0}))+'%';", user_id);
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
    }
}
