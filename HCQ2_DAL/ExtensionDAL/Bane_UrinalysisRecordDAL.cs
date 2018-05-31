using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_UrinalysisRecordDAL:HCQ2_IDAL.IBane_UrinalysisRecordDAL
    {
        // <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        ///  已检人数
        /// </summary>
        /// <returns></returns>
        public int GetDetectionCount(int user_id)
        {
            sb?.Clear();
            if (user_id > 0)
                sb.AppendFormat(@"SELECT COUNT(*) FROM 
                (SELECT user_identify,ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY ur_id DESC) AS rowNumber FROM Bane_UrinalysisRecord WHERE approve_status=1 AND LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_reality_date,23),7)) record INNER JOIN
                (SELECT user_id,user_identify FROM dbo.Bane_User) users ON users.user_identify = record.user_identify INNER JOIN
                (SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={0}) relation ON users.user_id=relation.person_id WHERE record.rowNumber=1;", user_id);
            else
                sb.Append(@"SELECT COUNT(*) FROM
(SELECT ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY ur_id DESC) AS rowNumber,* FROM Bane_UrinalysisRecord WHERE approve_status=1 AND LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_reality_date,23),7)) record
WHERE record.rowNumber=1;");
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  获取尿检记录数据
        /// </summary>
        /// <param name="ur_id"></param>
        /// <returns></returns>
        public BaneEditUrinalyRecord GetRecordData(int ur_id)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT * FROM 
            (SELECT user_name, user_identify, user_sex, CONVERT(varchar(100), user_birth, 23) AS user_birth FROM dbo.Bane_User) users INNER JOIN
            (SELECT TOP 1 * FROM dbo.Bane_UrinalysisRecord WHERE ur_id = {0}) record ON record.user_identify = users.user_identify", ur_id);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneEditUrinalyRecord>(dt).FirstOrDefault();
        }
    }
}
