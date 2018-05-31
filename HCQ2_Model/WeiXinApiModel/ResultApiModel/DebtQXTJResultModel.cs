namespace HCQ2_Model.WeiXinApiModel.ResultApiModel
{
    /// <summary>
    ///  欠薪项目查询模型
    /// </summary>
    public class DebtQXTJResultModel
    {
        /// <summary>
        ///  单位代码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  项目名称
        /// </summary>
        public string B0001Name { get; set; }
        /// <summary>
        ///  欠薪金额(/万)
        /// </summary>
        public decimal? QXTJ01 { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int? People { get; set; }
        /// <summary>
        ///  保障金(/万)
        /// </summary>
        public decimal B0116 { get; set; }
        /// <summary>
        ///  总人数
        /// </summary>
        public int? People2 { get; set; }
        /// <summary>
        ///  排序字段
        /// </summary>
        public long DispOrder { get; set; }
    }
}
