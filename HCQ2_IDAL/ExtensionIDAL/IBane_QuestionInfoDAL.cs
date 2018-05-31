using HCQ2_Model;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  在线答题库数据层接口
    /// </summary>
    public partial interface IBane_QuestionInfoDAL
    {
        /// <summary>
        ///  随机获取题目
        /// </summary>
        /// <param name="howLen">随机几条</param>
        /// <returns></returns>
        List<BaneTopicModel> GetAnswerQuestion(int howLen);
        /// <summary>
        ///  根据选项获取试题对象
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        List<Bane_QuestionInfo> GetAnswerByOptions(List<SubmitAnswerDetail> options);
        /// <summary>
        ///  获取试题详细对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<Bane_QuestionInfo> GetAnswerByOptions(List<int> list);
    }
}
