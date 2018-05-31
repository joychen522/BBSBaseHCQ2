using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_DocumentSetTypeDAL:HCQ2_IDAL.IT_DocumentSetTypeDAL
    {
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  获取分享人员数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByPerson()
        {
            sb?.Clear();
            sb.Append(@"SELECT text=u.user_name,CAST(u.user_id AS NVARCHAR(100)) AS value FROM
                (SELECT user_name,user_id FROM dbo.T_User WHERE reg_from=0) u LEFT JOIN
                (SELECT UnitID,user_id FROM dbo.T_Org_User) o ON u.user_id=o.user_id;");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.SelectModel.ListBoxModel>(dt);
        }
        /// <summary>
        ///  获取分享角色数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByRole()
        {
            sb?.Clear();
            sb.Append("SELECT role_name AS text,CAST(role_id AS NVARCHAR(20)) AS value FROM dbo.T_Role");
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.SelectModel.ListBoxModel>(dt);
        }
    }
}
