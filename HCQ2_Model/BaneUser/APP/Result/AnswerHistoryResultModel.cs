using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  查看答题历史 模型
    /// </summary>
    public class AnswerHistoryResultModel
    {
        /// <summary>
        ///  答题ID
        /// </summary>
        public int answer_id { get; set; }
        /// <summary>
        ///  答题时间 2018年5月2日
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
    }
}
