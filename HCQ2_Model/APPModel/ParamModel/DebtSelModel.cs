using System.ComponentModel.DataAnnotations;
using HCQ2_Model.WeiXinApiModel.ParamModel;

namespace HCQ2_Model.APPModel.ParamModel
{
    public class DebtSelModel: BaseAPI
    {
        /// <summary>
        ///  开始时间
        /// </summary>
        [Required]
        [RegularExpression("^([0-9]+)$")]
        public int startDate { get; set; }

        /// <summary>
        ///  截止时间
        /// </summary>
        [RegularExpression("^([0-9]+)$")]
        public int endDate { get; set; } = 0;
    }
    /// <summary>
    ///  欠薪项目详细查询
    /// </summary>
    public class  DebtSelGrantModel: DebtMoneyPeopleModel
    {
        /// <summary>
        ///  时间区间 开始时间
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])$")]
        public string startDate { get; set; }
        /// <summary>
        ///  时间区间 截止时间
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])$")]
        public string endDate { get; set; }
    }
    /// <summary>
    ///  出工打卡人数参数接收
    /// </summary>
    public class DebtAllGrantModel
    {
        /// <summary>
        ///  时间区间 开始时间 2017-01
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])$")]
        [Required]
        public string startDate { get; set; }
        /// <summary>
        ///  时间区间 截止时间 2017-04
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])$")]
        [Required]
        public string endDate { get; set; }
    }
    public class DebtChartMoneyModel
    {
        /// <summary>
        ///  时间区间 开始时间 2017-01-01
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])[-](0[1-9]|[1-2][0-9]|3[0-1])$")]
        [Required]
        public string startDate { get; set; }
        /// <summary>
        ///  时间区间 截止时间 2017-04-30
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])[-](0[1-9]|[1-2][0-9]|3[0-1])$")]
        [Required]
        public string endDate { get; set; }
    }
    public class DebtChartByYearModel:BaseAPI
    {
        /// <summary>
        ///  时间 2017
        /// </summary>
        [RegularExpression("^(19|20)\\d{2}$")]
        [Required]
        public string year { get; set; }
    }
}
