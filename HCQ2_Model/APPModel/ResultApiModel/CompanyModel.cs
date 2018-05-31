using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  企业征信
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        ///  征信ID
        /// </summary>
        public int ed_id { get; set; }
        /// <summary>
        ///  更新单位
        /// </summary>
        public string ent_name { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string ent_type { get; set; }
        /// <summary>
        ///  更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        ///  更新人
        /// </summary>
        public string update_name { get; set; }
        /// <summary>
        ///  处理状态
        /// </summary>
        public string solve_type { get; set; }
    }

    public class EnterCompanyResult
    {
        /// <summary>
        ///  企业信息
        /// </summary>
        public EnterModel enter { get; set; }
        /// <summary>
        ///  征信信息
        /// </summary>
        public CompanyModel company { get; set; }
    }
    /// <summary>
    ///  征信明细
    /// </summary>
    public class EnterCompanyDetail
    {
        /// <summary>
        ///  征信ID
        /// </summary>
        public int ed_id { get; set; }
        /// <summary>
        ///  失信记录名称
        /// </summary>
        public string ed_title { get; set; }
        /// <summary>
        ///  失信原因
        /// </summary>
        public string ed_reason { get; set; }
        /// <summary>
        ///  处理情况
        /// </summary>
        public string ed_note { get; set; }
        /// <summary>
        ///  失信时间
        /// </summary>
        public string ed_time { get; set; }
        /// <summary>
        ///  记录人
        /// </summary>
        public string ed_create { get; set; }
        /// <summary>
        ///  处理状态
        /// </summary>
        public string solve_type { get; set; }
    }
}
