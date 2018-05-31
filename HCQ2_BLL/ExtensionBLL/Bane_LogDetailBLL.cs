using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class Bane_LogDetailBLL:HCQ2_IBLL.IBane_LogDetailBLL
    {
        /// <summary>
        ///  根据参数获取禁毒日志记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<HCQ2_Model.Bane_LogDetail> GetLogDataByParams(HCQ2_Model.BaneUser.BaneLogParam param)
        {
            return DBSession.IBane_LogDetailDAL.GetLogDataByParams(param);
        }
        /// <summary>
        ///  统计日志数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetLogDataCount(HCQ2_Model.BaneUser.BaneLogParam param)
        {
            return DBSession.IBane_LogDetailDAL.GetLogDataCount(param);
        }
    }
}
