using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{
    /// <summary>
    /// 重复用工返回的实体
    /// </summary>
    public class RepeatWorker
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 征信状态
        /// </summary>
        public string person_zx { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public string unit_project { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string person_jobs { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public string work_start { get; set; }
        /// <summary>
        /// 出工天数
        /// </summary>
        public int work_days { get; set; }
        /// <summary>
        /// 薪资金额
        /// </summary>
        public string person_wage { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        public string A0177 { get; set; }
    }
}
