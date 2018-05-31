using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    public partial interface IBane_HistoryScoreDAL
    {
        /// <summary>
        ///  根据用户身份证获取 本周答题提醒记录数量
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        int GetAnswerNumByID(string user_identify,string startDate);
        /// <summary>
        ///  获取用户历史答题记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        List<AnswerHistoryResultModel> GetAnswerHistoryList(string user_identify);
    }
}
