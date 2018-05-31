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
    ///  上次/本次 答题
    /// </summary>
    public class AnswerModel:BaseBaneModel
    {
        /// <summary>
        ///  上次答题ID
        /// </summary>
        [DisplayName("上次答题ID")]
        [Required]
        public int answer_id { get; set; }
    }
}
