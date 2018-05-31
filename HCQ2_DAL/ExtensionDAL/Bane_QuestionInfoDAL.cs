using HCQ2_Model;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_QuestionInfoDAL:HCQ2_IDAL.IBane_QuestionInfoDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  随机获取题目
        /// </summary>
        /// <param name="howLen">随机几条</param>
        /// <returns></returns>
        public List<BaneTopicModel> GetAnswerQuestion(int howLen)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT * FROM (SELECT TOP {0} sub_id,sub_title AS issue_title,sub_value,sub_essay FROM dbo.Bane_QuestionInfo ORDER BY NEWID()) answer ORDER BY answer.sub_id;", howLen);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneTopicModel>(dt);
        }
        /// <summary>
        ///  根据选项获取试题对象
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public List<Bane_QuestionInfo> GetAnswerByOptions(List<SubmitAnswerDetail> options)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT * FROM dbo.Bane_QuestionInfo WHERE sub_id IN({0});", string.Join(",", options.Select(s => s.sub_id).ToList()));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<Bane_QuestionInfo>(dt);
        }
        /// <summary>
        ///  获取试题详细对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Bane_QuestionInfo> GetAnswerByOptions(List<int> list)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT * FROM dbo.Bane_QuestionInfo WHERE sub_id IN({0});", string.Join(",", list));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<Bane_QuestionInfo>(dt);
        }
    }
}
