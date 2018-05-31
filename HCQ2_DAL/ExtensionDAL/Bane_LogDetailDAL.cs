using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_LogDetailDAL:HCQ2_IDAL.IBane_LogDetailDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  根据参数获取禁毒日志记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<HCQ2_Model.Bane_LogDetail> GetLogDataByParams(HCQ2_Model.BaneUser.BaneLogParam param)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER(ORDER BY log_id ASC) rowNUmber,* FROM Bane_LogDetail WHERE 1=1 ", param.rows);
            if (param.user_id > 0)
                sb.AppendFormat(" AND user_id={0} ", param.user_id);
            if (!string.IsNullOrEmpty(param.log_title))
                sb.AppendFormat(" AND log_title LIKE '%{0}%' ", param.log_title);
            if (!string.IsNullOrEmpty(param.log_date_start) && !string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23) BETWEEN '{0}' AND '{1}' ", param.log_date_start, param.log_date_end);
            else if (!string.IsNullOrEmpty(param.log_date_start) && string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23)>='{0}' ", param.log_date_start);
            else if (string.IsNullOrEmpty(param.log_date_start) && !string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23)<='{0}' ", param.log_date_end);
            sb.AppendFormat(") logs WHERE logs.rowNUmber>{0};", (param.page - 1) * param.rows);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<HCQ2_Model.Bane_LogDetail>(dt);
        }
        /// <summary>
        ///  统计日志数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetLogDataCount(HCQ2_Model.BaneUser.BaneLogParam param)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT COUNT(*) FROM Bane_LogDetail WHERE 1=1 ");
            if (param.user_id > 0)
                sb.AppendFormat(" AND user_id={0} ", param.user_id);
            if (!string.IsNullOrEmpty(param.log_title))
                sb.AppendFormat(" AND log_title LIKE '%{0}%' ", param.log_title);
            if (!string.IsNullOrEmpty(param.log_date_start) && !string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23) BETWEEN '{0}' AND '{1}' ", param.log_date_start, param.log_date_end);
            else if (!string.IsNullOrEmpty(param.log_date_start) && string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23)>='{0}' ", param.log_date_start);
            else if (string.IsNullOrEmpty(param.log_date_start) && !string.IsNullOrEmpty(param.log_date_end))
                sb.AppendFormat(" AND CONVERT(NVARCHAR(20),log_date,23)<='{0}' ", param.log_date_end);
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
    }
}
