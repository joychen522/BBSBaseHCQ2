using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    /// 用工查询接口
    /// </summary>
    public class CheckUseWorker
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string unitName { get; set; }

        /// <summary>
        /// 查询日期
        /// </summary>
        [Required]
        public string checkDate { get; set; }
    }
}
