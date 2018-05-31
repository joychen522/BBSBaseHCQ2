using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  我的积分模型
    /// </summary>
    public class AnswerIntegralModel
    {
        /// <summary>
        ///  总积分
        /// </summary>
        public int num_count { get; set; }
        /// <summary>
        ///  历史积分详情
        /// </summary>
        public List<AnswerIntegralDetial> interral { get; set; }
    }

    public class AnswerIntegralDetial
    {
        /// <summary>
        ///  获取积分描述
        /// </summary>
        public string hs_title { get; set; }
        /// <summary>
        ///  获得积分
        /// </summary>
        public int hs_total { get; set; }
        /// <summary>
        ///  答题时间
        /// </summary>
        public string answer_date { get; set; }
    }
}
