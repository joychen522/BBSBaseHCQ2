using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{

    #region 传入参数

    public class PersonSelect
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_identity_code { get; set; }
    }

    #endregion

    #region 返回参数

    /// <summary>
    /// 人员基本信息
    /// </summary>
    public class PersonDetail
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string person_sex { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? person_birth { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string person_address { get; set; }
        /// <summary>
        /// 相片
        /// </summary>
        public byte[] person_photo { get; set; }
    }

    /// <summary>
    /// 项目信息
    /// </summary>
    public class UnitDetail
    {
        /// <summary>
        /// 企业
        /// </summary>
        public string unit_enterprise { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public string unit_project { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string unit_contact { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string unit_contact_phone { get; set; }
    }

    public class WageDetail
    {

    }

    /// <summary>
    /// 出工信息
    /// </summary>
    public class WorkDay
    {
        /// <summary>
        /// 时间月份
        /// </summary>
        public string work_date { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public string work_day { get; set; }
    }

    #endregion
}
