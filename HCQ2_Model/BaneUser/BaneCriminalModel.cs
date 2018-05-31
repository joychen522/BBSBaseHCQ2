using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    /// <summary>
    ///  违法犯罪记录--档案袋
    /// </summary>
    public class BaneCriminalModel
    {
        /// <summary>
        ///  主键
        /// </summary>
        public int cr_id { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        [Required]
        public string user_identify { get; set; }
        /// <summary>
        ///  开始吸毒日期
        /// </summary>
        public string start_drug_date { get; set; }
        /// <summary>
        ///  吸毒史(年)
        /// </summary>
        public string drug_year { get; set; }
        /// <summary>
        ///  强制戒毒次数
        /// </summary>
        public int force_time { get; set; }
        /// <summary>
        ///  强制隔离次数
        /// </summary>
        public int force_insulate { get; set; }
        /// <summary>
        ///  其他违法犯罪记录
        /// </summary>
        public string other_record { get; set; }
    }
}
