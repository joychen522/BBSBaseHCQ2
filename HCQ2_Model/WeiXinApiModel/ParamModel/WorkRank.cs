using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{

    /// <summary>
    /// 出工排名
    /// </summary>
    public class WorkRank
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string unit_name { get; set; }

        /// <summary>
        /// 出工年
        /// </summary>
        [Required]
        public string work_year { get; set; }

        /// <summary>
        /// 出工月
        /// </summary>
        [Required]
        public string work_month { get; set; }
    }

    public class Rank
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_identify_code { get; set; }
        /// <summary>
        /// 出工天数
        /// </summary>
        public int work_days { get; set; }
    }

}
