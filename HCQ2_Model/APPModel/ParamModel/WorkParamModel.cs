using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    public class WorkParamModel
    {
        public string A0101 { get; set; }
    }
    public class WorkMonthList:BaseAPI
    {
        /// <summary>
        ///  时间区间 2017-01
        /// </summary>
        [Required]
        [RegularExpression("^[0-9]{4}-(0[1-9]{1})|(1[0-2]{1})$")]
        public string work_date { get; set; }
    }
    public class WorkAllList:BaseAPI
    {
        /// <summary>
        ///  统计年
        /// </summary>
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        public string year { get; set; }
    }
   
}
