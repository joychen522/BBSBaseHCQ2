using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneUrinalysisRecordModel
    {
        /// <summary>
        ///  主键
        /// </summary>
        public int ur_id { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        [Required]
        public string user_identify { get; set; }
        /// <summary>
        ///  本次应到尿检时间
        /// </summary>
        [Required]
        public string ur_should_date { get; set; }
        /// <summary>
        ///  实际尿检时间
        /// </summary>
        [Required]
        public string ur_reality_date { get; set; }
        /// <summary>
        ///  尿检监督人员
        /// </summary>
        public string ur_manager { get; set; }
        /// <summary>
        ///  尿检结果
        /// </summary>
        public string ur_result { get; set; }
        /// <summary>
        ///  尿检附件文件路径
        /// </summary>
        public string ur_attach { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string ur_note { get; set; }
        /// <summary>
        ///  任务状态
        ///  0：未完成
        ///  1：完成
        /// </summary>
        public int approve_status { get; set; }
        /// <summary>
        ///  编号
        /// </summary>
        [Required]
        public string ur_code { get; set; }
        /// <summary>
        ///  现场检测地点
        /// </summary>
        public string ur_site { get; set; }
        /// <summary>
        ///  现场检测方法
        /// </summary>
        public string ur_method { get; set; }
        /// <summary>
        ///  结果呈
        /// </summary>
        public string ur_result_show { get; set; }
        /// <summary>
        ///  录单日期
        /// </summary>
        [Required]
        public string ur_input_date { get; set; }
    }
}
