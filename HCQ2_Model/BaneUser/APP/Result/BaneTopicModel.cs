using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  随机答题 题库
    /// </summary>
    public class BaneTopicModel
    {
        /// <summary>
        ///  试题ID
        /// </summary>
        public int sub_id { get; set; }
        /// <summary>
        ///  短文
        /// </summary>
        public string sub_essay { get; set; }
        /// <summary>
        ///  问题描述
        /// </summary>
        public string issue_title { get; set; }
        /// <summary>
        ///  正确答案
        /// </summary>
        public string sub_value { get; set; }
        /// <summary>
        ///  备选项
        /// </summary>
        public List<BaneTopicDetial> topic { get; set; }
    }

    public class BaneTopicDetial
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
