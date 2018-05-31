using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  首页答题情况模型
    /// </summary>
    public class BaneHomeAnswerModel
    {
        /// <summary>
        ///  上次答题ID
        /// </summary>
        public int last_id { get; set; }
        /// <summary>
        ///  上次答题分数
        /// </summary>
        public int last_score { get; set; }
        /// <summary>
        ///  本次答题分数
        /// </summary>
        public int the_score { get; set; }
        /// <summary>
        ///  本次答题ID
        /// </summary>
        public int the_id { get; set; }
        /// <summary>
        ///  总积分
        /// </summary>
        public int num_count { get; set; }
        /// <summary>
        ///  历史答题次数
        /// </summary>
        public int history_count { get; set; }
    }
}
