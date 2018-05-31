using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Params
{
    /// <summary>
    ///  提交试题答案模型
    /// </summary>
    public class SubmitAnswerModel
    {
        /// <summary>
        ///  guid
        /// </summary>
        [DisplayName("guid")]
        [Required]
        public string userid { get; set; }
        /// <summary>
        ///  答案项
        /// </summary>
        public List<SubmitAnswerDetail> options { get; set; }
        
    }
    public class SubmitAnswerDetail
    {
        /// <summary>
        ///  试题ID
        /// </summary>
        public int sub_id { get; set; }
        /// <summary>
        ///  答案选项
        /// </summary>
        public string score_option { get; set; }
    }
}
