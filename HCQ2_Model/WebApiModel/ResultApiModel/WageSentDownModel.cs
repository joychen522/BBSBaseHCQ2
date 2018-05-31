namespace HCQ2_Model.WebApiModel.ResultApiModel
{
    /// <summary>
    ///  工资下发模型
    /// </summary>
    public class WageSentDownModel
    {
        /// <summary>
        ///  单位代码
        /// </summary>
        public string orgid { get; set; }
        /// <summary>
        ///  员工编号(PersonID)
        /// </summary>
        public string personid { get; set; }
        /// <summary>
        ///  工资发放内部编号(WGJG02表PersonSalaryID)
        /// </summary>
        public string personsalaryid { get; set; }
        /// <summary>
        ///  工资发放金额(WGJG02表WGJG0204)
        /// </summary>
        public decimal? person_salaryvalue { get; set; }
        /// <summary>
        ///  所属项目(B0001对应UnitName)
        /// </summary>
        public string person_project { get; set; }
        /// <summary>
        ///  用工单位 (B0002对应UnitName)
        /// </summary>
        public string person_enduser { get; set; }
        /// <summary>
        ///  工种(E0386)  :测试工种
        /// </summary>
        public string person_jobtype { get; set; }
        /// <summary>
        ///  姓名(A0101)
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        ///  身份证号(A0177)
        /// </summary>
        public string person_cardno { get; set; }
        /// <summary>
        ///  应发工资(WGJG02表WGJG0207)
        /// </summary>
        public decimal? person_salaryplanvalue { get; set; }
        /// <summary>
        ///  实发工资(WGJG02表WGJG0208)
        /// </summary>
        public decimal? person_salaryrealvalue { get; set; }
        /// <summary>
        ///  虹膜(A0118)
        /// </summary>
        public string iris_data { get; set; }
        /// <summary>
        ///  虹膜(big_iris_data)
        /// </summary>
        public string big_iris_data { get; set; }
    }
}
