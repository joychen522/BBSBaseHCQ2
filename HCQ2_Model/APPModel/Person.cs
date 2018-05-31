using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.AppModel
{

    #region 人员列表查询

    public class BaseUserID
    {
        /// <summary>
        /// 用户guid
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 人员查询根据姓名或者身份证号码
    /// </summary>
    public class Person : BaseUserID
    {
        /// <summary>
        /// 姓名或者是身份证号码
        /// </summary>
        [Required]
        public string person_name_or_identity { get; set; }
    }

    /// <summary>
    /// 人员查询返回实体类
    /// </summary>
    public class ReturePerson
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_identify_code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string unit_code { get; set; }
    }
    #endregion

    #region 基本信息查询

    /// <summary>
    /// 基本信息查询人员详细信息 参数接收
    /// </summary>
    public class PersonDetail
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string person_name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_identify_code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string unit_name { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string unit_code { get; set; }
    }

    /// <summary>
    /// 人员详细信息 出工记录
    /// </summary>
    public class PersonDetailCkeck : PersonDetail
    {
        /// <summary>
        /// 查询年
        /// </summary>
        public string query_year { get; set; }
    }

    /// <summary>
    /// 人员基本信息查询参数返回值
    /// </summary>
    public class PersonDetailReturn
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string person_identify_code { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string person_photo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string person_sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string person_nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string person_birth { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string person_address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string person_phone { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string person_jobs { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string person_status { get; set; }
    }

    /// <summary>
    /// 返回的当前所在项目
    /// </summary>
    public class PersonUnit
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public string work_start { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string work_jobs { get; set; }
        /// <summary>
        /// 项目联系人
        /// </summary>
        public string unit_contact { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string unit_contact_phone { get; set; }
    }

    /// <summary>
    /// 返回的出工记录
    /// </summary>
    public class WorkDays
    {
        /// <summary>
        /// 打卡月份
        /// </summary>
        public string check_month { get; set; }
        /// <summary>
        /// 打卡天数
        /// </summary>
        public int check_count { get; set; }
    }
    #endregion

    #region 人员信息编辑

    public class EditPerson
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Required]
        public string unit_code { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required]
        public string identify_code { get; set; }
    }

    /// <summary>
    /// 上传数据
    /// </summary>
    public class EditPersonToSave : EditPerson
    {
        /// <summary>
        /// 用户guid
        /// </summary>
        [Required]
        public string userid { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string work_type { get; set; }
        /// <summary>
        /// 所属劳务公司
        /// </summary>
        public string team_name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string person_phone { get; set; }
    }

    #endregion
}
