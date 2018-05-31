using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.RollScreenModel
{
    public class RollHelp
    {

    }

    public class Roll {

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string unit_name { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Required]
        public string check_date { get; set; }
    }

    public class UnitStatis
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public int total_person { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string unit_conact { get; set; }
    }

     
}
