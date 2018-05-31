using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_IntegralScoreDetialDAL:HCQ2_IDAL.IBane_IntegralScoreDetialDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  获取用户获取积分详情
        /// </summary>
        /// <param name="user_identify">身份证</param>
        /// <returns></returns>
        public List<AnswerIntegralDetial> GetIntegralScoreById(string user_identify)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT his_title AS hs_title,CONVERT(varchar(100),his_time,20) AS answer_date,his_total AS hs_total FROM dbo.Bane_IntegralScoreDetial WHERE user_identify='{0}' ORDER BY answer_date ASC;", user_identify);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<AnswerIntegralDetial>(dt);
        }
    }
}
