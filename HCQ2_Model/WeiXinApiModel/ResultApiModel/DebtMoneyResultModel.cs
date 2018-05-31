namespace HCQ2_Model.WeiXinApiModel.ResultApiModel
{
    /// <summary>
    ///  欠薪金额返回值模型
    /// </summary>
    public class DebtMoneyResultModel
    {
        /// <summary>
        ///  单位编码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal QXTJ01 { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int People { get; set; }
        /// <summary>
        ///  排序字段
        /// </summary>
        public long DispOrder { get; set; }
    }
}
