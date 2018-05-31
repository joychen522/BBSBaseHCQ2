using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Common.SQL;

namespace HCQ2_DAL_MSSQL
{
    /// <summary>
    ///  权限管理数据实现层
    /// </summary>
    public  partial class T_PermissionsDAL:HCQ2_IDAL.IT_PermissionsDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  获取权限集合
        /// </summary>
        /// <param name="perName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="perType">权限类别</param>
        /// <param name="sm_code">模块代码</param>
        /// <returns></returns>
        public List<T_Permissions> GetLimitData(string perName, int page, int rows,string perType,string sm_code)
        {
            sb?.Clear();
            _param.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} per.*,fig.pc_openUrl,fig.pc_saveUrl,fig.pc_width,fig.pc_height FROM 
            (SELECT *,ROW_NUMBER() OVER(ORDER BY per_id) AS 'rowNumber' FROM dbo.T_Permissions WHERE 1=1 ", rows));
            if (!string.IsNullOrEmpty(sm_code))
            {
                sb.Append(string.Format(" AND sm_code=@sm_code "));
                _param.Add("@sm_code", sm_code);
            }
            if (!string.IsNullOrEmpty(perName))
            {
                sb.Append(string.Format(" AND per_name LIKE '%@per_name%' "));
                _param.Add("@per_name", perName);
            }
            if (!string.IsNullOrEmpty(perType))
            {
                sb.Append(string.Format(" AND per_type=@per_type "));
                _param.Add("@per_type", perType);
            }
            sb.Append(string.Format(@" ) per INNER JOIN
            (SELECT per_type,pc_openUrl,pc_saveUrl,pc_width,pc_height FROM dbo.T_PermissConfig) fig ON per.per_type = fig.per_type WHERE per.rowNumber>{0};", (page - 1) * rows));
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_Permissions>(dt);
        }

        /// <summary>
        ///  编辑权限对象
        /// </summary>
        /// <param name="permission">权限对象</param>
        public void EditLimit(T_Permissions permission)
        {
            base.Modify(permission, "per_type", "per_code", "creator_date", "creator_id", "creator_name", "per_name",
                "per_note","sm_code");
        }
        /// <summary>
        ///  根据用户id获得所有角色集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<int> GetRolesListById(int user_id)
        {
            List<int> list = new List<int>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT b1.role_id FROM ");
            sb.AppendLine(string.Format("(SELECT group_id FROM dbo.T_UserGroupRelation WHERE user_id = {0}) a1 INNER JOIN ", user_id));
            sb.AppendLine("(SELECT role_id, group_id FROM dbo.T_RoleGroupRelation) b1 ON a1.group_id = b1.group_id ");
            sb.AppendLine(" UNION ");
            sb.AppendLine(string.Format(" SELECT role_id FROM dbo.T_UserRoleRelation WHERE user_id = {0};", user_id));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            for (int i = 0; i < dt.Rows.Count; i++)
                list.Add(HCQ2_Common.Helper.ToInt(dt.Rows[i]["role_id"]));
            return list;
        }

        /// <summary>
        ///  根据用户id获取所有角色id集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_Role> GetRolesById(int user_id)
        {
            List<T_Role> list = new List<HCQ2_Model.T_Role>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.T_Role WHERE role_id IN( ");
            sb.Append("SELECT b1.role_id FROM");
            sb.Append(string.Format("(SELECT group_id FROM dbo.T_UserGroupRelation WHERE user_id = {0}) a1 INNER JOIN", user_id));
            sb.Append("(SELECT role_id, group_id FROM dbo.T_RoleGroupRelation) b1 ON a1.group_id = b1.group_id ");
            sb.Append("UNION");
            sb.Append(string.Format(" SELECT role_id FROM dbo.T_UserRoleRelation WHERE user_id = {0});", user_id));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_Role>(dt);
        }

        /// <summary>
        ///  根据用户id获取所有权限id集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_Permissions> GetPermissById(int user_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT d1.* FROM ");
            sb.Append("(SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN( ");
            sb.Append("SELECT b1.role_id FROM ");
            sb.Append(string.Format("(SELECT group_id FROM dbo.T_UserGroupRelation WHERE user_id = {0}) a1 INNER JOIN",user_id));
            sb.Append("(SELECT role_id, group_id FROM dbo.T_RoleGroupRelation) b1 ON a1.group_id = b1.group_id");
            sb.AppendLine(" UNION ");
            sb.Append(string.Format("SELECT role_id FROM dbo.T_UserRoleRelation WHERE user_id ={0})) c1 INNER JOIN",user_id));
            sb.Append("(SELECT * FROM dbo.T_Permissions) d1 ON c1.per_id = d1.per_id");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_Permissions>(dt);
        }

        /// <summary>
        ///  根据用户id获取所有菜单资源
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_PageFolder> GetMenusById(int user_id)
        {
            List<int> list = GetRolesListById(user_id);
            if (null == list || list.Count <= 0)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select e1.* from ");
            sb.Append(" (SELECT distinct c1.folder_id FROM ");
            sb.Append(string.Format(" (SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN({0})) a1 INNER JOIN ",string.Join(",",list.ToArray())));
            sb.Append(" (SELECT per_id FROM dbo.T_Permissions WHERE per_type = 'menu') b1 ON a1.per_id = b1.per_id  INNER JOIN  ");
            sb.Append(" (SELECT per_id,folder_id FROM dbo.T_FolderPermissRelation) c1 ON b1.per_id = c1.per_id) d1 INNER JOIN ");
            sb.Append(" (SELECT * FROM dbo.T_PageFolder) e1 ON d1.folder_id = e1.folder_id INNER JOIN (SELECT * FROM dbo.T_SysModule WHERE if_start=1) model ON model.sm_code=e1.sm_code ORDER BY e1.folder_pid,e1.folder_order");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.T_PageFolder>(dt);
        }

        /// <summary>
        ///  根据用户id以及请求信息获取页面元素
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public List<T_PageElement> GetElementsById(int user_id, string controller, string action)
        {
            List<int> list = GetRolesListById(user_id);
            if (null == list || list.Count() <= 0)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT d1.* FROM");
            sb.Append(
                string.Format("(SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN({0})) a1 INNER JOIN ",
                    string.Join(",", list.ToArray())));
            sb.Append("(SELECT per_id FROM dbo.T_Permissions WHERE per_type = 'element') b1 ON a1.per_id = b1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append("(SELECT per_id, pe_id FROM dbo.T_ElementPermissRelation) c1 ON b1.per_id = c1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append("(SELECT * FROM dbo.T_PageElement) d1 ON c1.pe_id = d1.pe_id ");
            sb.Append(" INNER JOIN ");
            sb.Append(
                string.Format(
                    "(SELECT folder_id FROM dbo.T_PageFolder WHERE folder_url='{0}/{1}') e1 ON d1.folder_id = e1.folder_id",
                    controller, action));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.T_PageElement>(dt);
        }
        /// <summary>
        ///  获取用户所有元素
        /// </summary>
        /// <returns></returns>
        public List<T_PageElement> GetElementsById(int user_id)
        {
            List<int> list = GetRolesListById(user_id);
            if (null == list || list.Count <= 0)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT d1.* FROM");
            sb.Append(
                string.Format("(SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN({0})) a1 INNER JOIN ",
                    string.Join(",", list.ToArray())));
            sb.Append("(SELECT per_id FROM dbo.T_Permissions WHERE per_type = 'element') b1 ON a1.per_id = b1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append("(SELECT per_id, pe_id FROM dbo.T_ElementPermissRelation) c1 ON b1.per_id = c1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append("(SELECT * FROM dbo.T_PageElement) d1 ON c1.pe_id = d1.pe_id ");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.T_PageElement>(dt);
        }

        /// <summary>
        ///  根据用户id，请求信息获取完成菜单信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public HCQ2_Model.T_PageFolder GetMenuById(int user_id, string controller, string action)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT d1.* FROM ");
            sb.Append(string.Format(" (SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN(2,3)) a1 INNER JOIN "));
            sb.Append("(SELECT per_id FROM dbo.T_Permissions WHERE per_type='menu') b1 ON a1.per_id = b1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append("(SELECT per_id,folder_id FROM dbo.T_FolderPermissRelation) c1 ON b1.per_id = c1.per_id ");
            sb.Append(" INNER JOIN ");
            sb.Append(
                string.Format(
                    "(SELECT * FROM dbo.T_PageFolder WHERE folder_url='{0}/{1}') d1 ON c1.folder_id = d1.folder_id;",
                    controller, action));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.T_PageFolder>(dt).FirstOrDefault();
        }
    }
}
