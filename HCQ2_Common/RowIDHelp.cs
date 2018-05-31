using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common
{
    public static class RowIDHelp
    {
        /// <summary>
        /// 获取一个新的编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewRowID()
        {
            return HCQ2_Common.SQL.SqlHelper.ExecuteScalar("select NEWID()").ToString().Replace("-", "").ToUpper();
        }
    }
}
