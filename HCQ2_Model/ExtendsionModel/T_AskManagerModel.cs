using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HCQ2_Common.Code;

namespace HCQ2_Model.ExtendsionModel
{
    /// <summary>
    ///  请假管理模型
    /// </summary>
    public class T_AskManagerModel
    {
        /// <summary>
        ///  主键
        /// </summary>
        public int ask_id { get; set; }
        /// <summary>
        ///  所属单位
        /// </summary>
        public string ask_unit { get; set; }
        /// <summary>
        ///  用户名
        /// </summary>
        [DisplayName("用户名")]
        [Required]
        public string user_name { get; set; }
        /// <summary>
        ///  开始时间
        /// </summary>
        [Required]
        public string ask_startDate { get; set; }
        /// <summary>
        ///  结束时间
        /// </summary>
        [Required]
        public string ask_endDate { get; set; }
        /// <summary>
        ///  请假天数
        /// </summary>
        public string ask_day { get; set; }
        /// <summary>
        ///  标题
        /// </summary>
        [Required]
        public string ask_title { get; set; }
        /// <summary>
        ///  内容
        /// </summary>
        [Required]
        public string ask_context { get; set; }
        /// <summary>
        ///  请假类别
        /// </summary>
        [Required]
        public string ask_type { get; set; }
        public string ask_type_text { get; set; }
        /// <summary>
        ///  请假状态
        /// </summary>
        [Required]
        public string ask_status { get; set; }
        public string ask_status_text { get; set; }
        /// <summary>
        ///  操作者
        /// </summary>
        public string ask_operUser { get; set; }
        /// <summary>
        ///  操作时间
        /// </summary>
        public string ask_operDate { get; set; }
        public HCQ2_Model.T_AskManager ToPOCO()
        {
            return new HCQ2_Model.T_AskManager()
            {
                ask_unit = this.ask_unit,
                user_name = this.user_name,
                ask_startDate = DateTimeHelper.StrToDate(this.ask_startDate),
                ask_endDate = DateTimeHelper.StrToDate(this.ask_endDate),
                ask_day = DateTimeHelper.GetDateByTwo(DateTimeHelper.StrToDate(this.ask_startDate), DateTimeHelper.StrToDate(this.ask_endDate)),
                ask_title = this.ask_title,
                ask_context = this.ask_context,
                ask_type = this.ask_type,
                ask_status = this.ask_status,
                ask_operDate = DateTimeHelper.StrToDate(this.ask_operDate),
                ask_operUser = this.ask_operUser
            };
        }
    }
}
