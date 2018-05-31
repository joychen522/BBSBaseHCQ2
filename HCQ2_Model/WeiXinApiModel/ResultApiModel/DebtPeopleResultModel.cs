namespace HCQ2_Model.WeiXinApiModel.ResultApiModel
{
    /// <summary>
    ///  欠薪人数返回值模型
    /// </summary>
    public class DebtPeopleResultModel
    {
        /// <summary>
        ///  工资发放登记ID
        /// </summary>
        public string WGJG01RowID { get; set; }
        /// <summary>
        ///  单位编码
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        ///  人员唯一编号
        /// </summary>
        public string PersonID { get; set; }
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal QXTJ01 { get; set; }
        /// <summary>
        ///  排序字段
        /// </summary>
        public long DispOrder { get; set; }
    }
}
