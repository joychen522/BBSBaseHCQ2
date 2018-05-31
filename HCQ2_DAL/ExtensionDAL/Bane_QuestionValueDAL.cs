using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_QuestionValueDAL:HCQ2_IDAL.IBane_QuestionValueDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  根据指定试题ID 获取答题选项
        /// </summary>
        /// <param name="sub_ids"></param>
        /// <returns></returns>
        public List<Bane_QuestionValue> GetOptionsById(List<int> sub_ids)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT * FROM dbo.Bane_QuestionValue WHERE sub_id IN({0});", string.Join(",", sub_ids));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<Bane_QuestionValue>(dt);
        }
    }
}
