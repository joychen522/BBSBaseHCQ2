using HCQ2_Common.SQL;
using HCQ2_Model;
using HCQ2_Model.DocModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_DocumentFolderDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        /// <summary>
        ///  根据用户id 权限per_id获取对于的文档菜单集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="per_id">权限id</param>
        /// <returns></returns>
        public List<T_DocumentFolder> GetDocTreeData(int user_id,string pageType, List<int> per_id)
        {
            if (user_id <= 0)
                return null;
            if (per_id == null || per_id.Count == 0)
            {
                per_id = new List<int>();
                per_id.Add(0);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"SELECT *  FROM (SELECT folder.* FROM 
                        (SELECT folder_id FROM T_DocFolderPermissRelation WHERE per_id in({0})) per INNER JOIN
                        (SELECT * FROM T_DocumentFolder WHERE ISNULL(page_type,'')=@page_type) folder ON per.folder_id=folder.folder_id
                        UNION 
                        (SELECT * FROM T_DocumentFolder WHERE create_id={1} AND ISNULL(page_type,'')=@page_type))A ORDER BY case when A.folder_order is null then 1 else 0 end,A.folder_order  ASC", string.Join(",",per_id).Trim('\''), user_id));
            _param?.Clear();
            _param.Add("@page_type", pageType);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<T_DocumentFolder>(dt);
        }
    }
}
