using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ExtendsionModel
{
    public class WGJG_ZXDetail
    {
        /// <summary>
        ///  企业名称
        /// </summary>
        public string WGJG_ZX02 { get; set; }
        public int ed_id { get; set; }
        /// <summary>
        ///  ID
        /// </summary>
        public int ent_id { get; set; }
        /// <summary>
        ///  标题
        /// </summary>
        public string ed_title { get; set; }
        /// <summary>
        ///  原因
        /// </summary>
        public string ed_reason { get; set; }
        /// <summary>
        ///  征信状态中文
        /// </summary>
        public string ent_text { get; set; }
        /// <summary>
        ///  征信状态
        /// </summary>
        public string ent_type { get; set; }
        /// <summary>
        ///  创建人
        /// </summary>
        public string ed_create { get; set; }
        /// <summary>
        ///  时间
        /// </summary>
        public string ed_time { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string ed_note { get; set; }
        /// <summary>
        ///  是否影响企业征信
        /// </summary>
        public int is_success { get; set; }
        /// <summary>
        ///  案件类型 
        ///  0：欠薪
        ///  1：其他 
        /// </summary>
        public int case_type { get; set; }
        /// <summary>
        ///  欠薪金额
        /// </summary>
        public decimal pay_money { get; set; }
        /// <summary>
        ///  欠薪人数
        /// </summary>
        public int pay_person { get; set; }
        /// <summary>
        ///  处理状态
        /// </summary>
        public int solve_type { get; set; }
    }
}
