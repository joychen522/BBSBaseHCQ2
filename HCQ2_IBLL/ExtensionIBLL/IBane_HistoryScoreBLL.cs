using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model.BaneUser.APP.Result;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  历史答题记录
    /// </summary>
    public partial interface IBane_HistoryScoreBLL
    {
        /// <summary>
        ///  根据用户身份证获取 本周答题提醒记录数量
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        int GetAnswerNumByID(string user_identify);
        /// <summary>
        ///  获取每周完整的答题记录 有积分的答题记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        int GetAnswerNums(string user_identify);
        /// <summary>
        ///  根据用户身份证获取是否需要提醒 答题
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        BaneMyMessageModel GetAnswerContentByID(string user_identify);
        /// <summary>
        ///  获取用户历史答题记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        List<AnswerHistoryResultModel> GetAnswerHistoryList(string user_identify);
        /// <summary>
        ///  根据答题历史记录ID 获取详细的答题记录
        /// </summary>
        /// <param name="answer_id"></param>
        /// <returns></returns>
        AnswerResultModel GetAnswerHistoryDetial(int answer_id);
    }
}
