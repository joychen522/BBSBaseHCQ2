namespace HCQ2_Model.WeiXinApiModel.ResultApiModel
{
    /// <summary>
    ///  企业征信返回值模型
    /// </summary>
    public class CompanyEnterResultModel
    {
        /// <summary>
        ///  企业名称
        /// </summary>
        public string WGJG_ZX02 { get; set; }
        /// <summary>
        ///  营业执照
        /// </summary>
        public string WGJG_ZX03 { get; set; }
        /// <summary>
        ///  法人
        /// </summary>
        public string WGJG_ZX04 { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string WGJG_ZX05 { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string WGJG_ZX06 { get; set; }
        /// <summary>
        ///  状态变更原因
        /// </summary>
        public string WGJG_ZX07 { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string WGJG_ZX09 { get; set; }
        /// <summary>
        ///  排序字段
        /// </summary>
        public long DispOrder { get; set; }
    }

    /// <summary>
    ///  个人征信返回值模型
    /// </summary>
    public class CompanyOwnResultModel
    {
        /// <summary>
        ///  姓名
        /// </summary>
        public string WGJG_GRZX02 { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        public string WGJG_GRZX03 { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string WGJG_GRZX05 { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string WGJG_GRZX06 { get; set; }
        /// <summary>
        ///  变更原因
        /// </summary>
        public string WGJG_GRZX07 { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string WGJG_GRZX09 { get; set; }
        /// <summary>
        ///  排序字段
        /// </summary>
        public long DispOrder { get; set; }
    }
}
