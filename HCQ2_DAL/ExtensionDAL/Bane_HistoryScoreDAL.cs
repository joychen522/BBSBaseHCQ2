using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_HistoryScoreDAL:HCQ2_IDAL.IBane_HistoryScoreDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  根据用户身份证获取 本周答题提醒记录数量
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public int GetAnswerNumByID(string user_identify, string startDate)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT COUNT(*) FROM dbo.Bane_HistoryScore WHERE user_identify='{0}' AND hs_score>89 AND hs_total>0 AND hs_time BETWEEN '{1}' AND '{2}';", user_identify, startDate,DateTime.Now.ToString("yyyy-MM-dd"));
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  获取用户历史答题记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public List<AnswerHistoryResultModel> GetAnswerHistoryList(string user_identify)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT hs_id AS answer_id,(CAST(YEAR(hs_time) AS VARCHAR(10))+'年'+CAST(MONTH(hs_time) AS VARCHAR(10))+'月'+CAST(DAY(hs_time) AS VARCHAR(10))+'日') AS answer_date,hs_score AS answer_score,hs_total AS answer_total FROM dbo.Bane_HistoryScore WHERE user_identify='{0}' ORDER BY hs_time ASC;", user_identify);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<AnswerHistoryResultModel>(dt);
        }
    }
}
