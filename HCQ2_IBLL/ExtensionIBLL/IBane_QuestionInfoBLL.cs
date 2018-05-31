using HCQ2_Model;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  在线试题业务层接口
    /// </summary>
    public partial interface IBane_QuestionInfoBLL
    {
        /// <summary>
        ///  后端获取一栏试题数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<Bane_QuestionInfo> GetAllAnswerQuestion(string key,int page,int size);
        /// <summary>
        ///  添加试题
        /// </summary>
        /// <param name="model"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        int AddAnswerQuestion(Bane_QuestionInfo model,string options);
        /// <summary>
        ///  编辑试题
        /// </summary>
        /// <param name="model"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        int EditAnswerQuestion(Bane_QuestionInfo model, string options);
        //******************************************APP**************************************
        /// <summary>
        ///  随机获取题目
        /// </summary>
        /// <param name="howLen">随机几条</param>
        /// <returns></returns>
        List<BaneTopicModel> GetAnswerQuestion(int howLen);
        /// <summary>
        ///  批阅试题 记录积分 并返回分数
        /// </summary>
        /// <param name="option">记录</param>
        /// <param name="user_identify">身份证</param>
        /// <returns></returns>
        int CheckAnswer(List<SubmitAnswerDetail> options,string user_identify,out int core);
    }
}
