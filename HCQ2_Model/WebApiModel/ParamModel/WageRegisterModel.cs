using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    ///  工资发放登记模型
    /// </summary>
    public class WageRegisterModel
    {
        /// <summary>
        ///  用户编码，登录后获取
        /// </summary>
        [Required]
        public string userid { get; set; }
        /// <summary>
        ///  人员工资发放内部编号，
        ///  工资下发的时候获
        /// </summary>
        [Required]
        public string personsalaryid { get; set; }
        /// <summary>
        ///  人员编码（根据虹膜信息获取）
        /// </summary>
        [Required]
        public string personid { get; set; }
        /// <summary>
        ///  签到时间（字符格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        [Required]
        public string signtime { get; set; }
    }
}
