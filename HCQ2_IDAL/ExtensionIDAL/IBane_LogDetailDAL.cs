using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    public partial interface IBane_LogDetailDAL
    {
        /// <summary>
        ///  根据参数获取禁毒日志记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<HCQ2_Model.Bane_LogDetail> GetLogDataByParams(HCQ2_Model.BaneUser.BaneLogParam param);
        /// <summary>
        ///  统计日志数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int GetLogDataCount(HCQ2_Model.BaneUser.BaneLogParam param);
    }
}
