using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    ///  App 日志模型
    /// </summary>
    public class AppLogModel
    {
        /// <summary>
        ///  日志标题
        /// </summary>
        [Required]
        public string log_message { get; set; }
        /// <summary>
        ///  日志内容
        /// </summary>
        [Required]
        public string log_content { get; set; }
    }
}
