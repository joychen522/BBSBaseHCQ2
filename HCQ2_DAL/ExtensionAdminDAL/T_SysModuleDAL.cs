using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_SysModuleDAL:HCQ2_IDAL.IT_SysModuleDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 获取指定用户分配 模块集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_SysModule> GetPermissById(int user_id)
        {
            HCQ2_IDAL.IT_PermissionsDAL Permissions = new T_PermissionsDAL();
            List<int> list = Permissions.GetRolesListById(user_id);
            if (null == list || list.Count <= 0)
                return null;
            sb?.Clear();
            sb.Append(string.Format(@"select e1.* FROM
            (SELECT distinct c1.sm_id FROM
	        (SELECT per_id FROM dbo.T_RolePermissRelation WHERE role_id IN({0})) a1 INNER JOIN
	        (SELECT per_id FROM dbo.T_Permissions WHERE per_type = 'ModuleManager') b1 ON a1.per_id = b1.per_id  INNER JOIN
	        (SELECT per_id,sm_id FROM dbo.T_ModulePermissRelation) c1 ON b1.per_id = c1.per_id) d1 INNER JOIN
	        (SELECT * FROM dbo.T_SysModule WHERE if_start=1) e1 ON d1.sm_id = e1.sm_id ORDER BY e1.sm_order;", string.Join(",", list.ToArray())));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            if (null == dt || dt.Rows.Count <= 0)
                return null;
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.T_SysModule>(dt);
        }
    }
}
