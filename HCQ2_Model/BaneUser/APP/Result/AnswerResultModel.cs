using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  上一次答题详情返回模型
    /// </summary>
    public class AnswerResultModel
    {
        /// <summary>
        ///  答题日期
        /// </summary>
        public string answer_date { get; set; }
        /// <summary>
        ///  得分
        /// </summary>
        public int answer_score { get; set; }
        /// <summary>
        ///  积分
        /// </summary>
        public int answer_total { get; set; }
        /// <summary>
        ///  试题
        /// </summary>
        public List<AnswerResultDetial> issue { get; set; }
    }

    public class AnswerResultDetial
    {
        /// <summary>
        ///  问题描述
        /// </summary>
        public string issue_title { get; set; }
        /// <summary>
        ///  选项
        /// </summary>
        public List<Bane_QuestionValue> option { get; set; } 
        /// <summary>
        ///  正确选项
        /// </summary>
        public string correct_option { get; set; }
        /// <summary>
        ///  用户选项
        /// </summary>
        public string user_option { get; set; }
    }

    public class Bane_QuestionValue
    {
        /// <summary>
        ///  选项 A
        /// </summary>
        public string score_option { get; set; }
        /// <summary>
        ///  选项描述
        /// </summary>
        public string score_value { get; set; }
    }
}
