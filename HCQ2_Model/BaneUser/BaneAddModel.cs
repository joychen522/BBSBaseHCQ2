using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneAddModel
    {
        public int user_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        [Required]
        public string user_name { get; set; }
        /// <summary>
        ///  别名
        /// </summary>
        public string alias_name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string user_sex { get; set; }
        /// <summary>
        ///  出生日期
        /// </summary>
        public string user_birth { get; set; }
        /// <summary>
        ///  身高
        /// </summary>
        public string user_height { get; set; }
        /// <summary>
        ///  身份证号
        /// </summary>
        [Required]
        public string user_identify { get; set; }
        /// <summary>
        ///  文化程度 UserEdu
        /// </summary>
        public string user_edu { get; set; }
        /// <summary> 
        ///  就业情况 JobStatus
        /// </summary>
        public string job_status { get; set; }
        /// <summary>
        ///  毒品种类 BaneType
        /// </summary>
        public string bane_type { get; set; }
        /// <summary>
        ///  户籍所在地
        /// </summary>
        public string birth_url { get; set; }
        /// <summary>
        ///  家庭电话
        /// </summary>
        public string family_phone { get; set; }
        /// <summary>
        ///  现居住地
        /// </summary>
        public string live_url { get; set; }
        /// <summary>
        ///  移动电话
        /// </summary>
        public string move_phone { get; set; }
        /// <summary>
        ///  主要联系人
        /// </summary>
        public string attn_name { get; set; }
        /// <summary>
        ///  主要联系人地址
        /// </summary>
        public string attn_url { get; set; }
        /// <summary>
        ///  主要联系人关系
        /// </summary>
        public string attn_relation { get; set; }
        /// <summary>
        ///  主要联系人电话
        /// </summary>
        public string attn_phone { get; set; }
        /// <summary>
        ///  婚姻状况 MaritalStatus
        /// </summary>
        public string marital_status { get; set; }
        /// <summary>
        ///  是否和父母居住
        /// </summary>
        public bool is_live_parent { get; set; }
        /// <summary>
        ///  目前状态 UserStatus
        /// </summary>
        public string user_status { get; set; }
        /// <summary>
        ///  是否参加职业培训
        /// </summary>
        public bool is_pro_train { get; set; }
        /// <summary>
        ///  技能特长
        /// </summary>
        public string user_skill { get; set; }
        /// <summary>
        ///  人员类别
        /// </summary>
        public string user_type { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string user_phone { get; set; }
        /// <summary>
        ///  下次尿检时间
        /// </summary>
        public string ur_next_date { get; set; }
        /// <summary>
        ///  照片
        /// </summary>
        public string user_photo { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string user_note { get; set; }
        /// <summary>
        ///  组织机构
        /// </summary>
        //[Required]
        public int org_id { get; set; }
        /// <summary>
        ///  个人简历
        /// </summary>
        public string user_resume { get; set; }
        /// <summary>
        ///  第一段虹膜
        /// </summary>
        public string iris_data1 { get; set; }
        /// <summary>
        ///  第二段虹膜
        /// </summary>
        public string iris_data2 { get; set; }
        /// <summary>
        ///  管控到期
        /// </summary>
        public string control_date { get; set; }
    }
    public class BaneRecoveryModel
    {
        public int ri_id { get; set; }
        /// <summary>
        ///  身份证号
        /// </summary>
        public string user_identify { get; set; }
        /// <summary>
        ///  执行地区
        /// </summary>
        public string exec_area { get; set; }
        /// <summary>
        ///  执行单位详称
        /// </summary>
        public string exec_unit { get; set; }
        /// <summary>
        ///  责令单位详称
        /// </summary>
        public string order_unit { get; set; }
        /// <summary>
        ///  是否感染艾滋病
        /// </summary>
        public bool is_aids { get; set; }
        /// <summary>
        ///  隔离场所
        /// </summary>
        public string isolation_url { get; set; }
        /// <summary>
        ///  隔离场所出所日期
        /// </summary>
        public string isolation_out_date { get; set; }
        /// <summary>
        ///  参加药物治疗门诊
        /// </summary>
        public string cure_ups { get; set; }
        /// <summary>
        ///  是否进入康复场所
        /// </summary>
        public bool in_recovery { get; set; }
        [Required]
        /// <summary>
        ///  报道日期
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        ///  结束日期
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        ///  结束原因 EndReason
        /// </summary>
        public string end_reason { get; set; }
    }

    public class BaneAddUser
    {
        public int user_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        [Required]
        public string user_name { get; set; }
        /// <summary>
        ///  别名
        /// </summary>
        public string alias_name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string user_sex { get; set; }
        /// <summary>
        ///  出生日期
        /// </summary>
        public string user_birth { get; set; }
        /// <summary>
        ///  身高
        /// </summary>
        public string user_height { get; set; }
        /// <summary>
        ///  身份证号
        /// </summary>
        [Required]
        public string user_identify { get; set; }
        /// <summary>
        ///  文化程度 UserEdu
        /// </summary>
        public string user_edu { get; set; }
        /// <summary> 
        ///  就业情况 JobStatus
        /// </summary>
        public string job_status { get; set; }
        /// <summary>
        ///  毒品种类 BaneType
        /// </summary>
        public string bane_type { get; set; }
        /// <summary>
        ///  户籍所在地
        /// </summary>
        public string birth_url { get; set; }
        /// <summary>
        ///  家庭电话
        /// </summary>
        public string family_phone { get; set; }
        /// <summary>
        ///  现居住地
        /// </summary>
        public string live_url { get; set; }
        /// <summary>
        ///  移动电话
        /// </summary>
        public string move_phone { get; set; }
        /// <summary>
        ///  主要联系人
        /// </summary>
        public string attn_name { get; set; }
        /// <summary>
        ///  主要联系人地址
        /// </summary>
        public string attn_url { get; set; }
        /// <summary>
        ///  主要联系人关系
        /// </summary>
        public string attn_relation { get; set; }
        /// <summary>
        ///  主要联系人电话
        /// </summary>
        public string attn_phone { get; set; }
        /// <summary>
        ///  婚姻状况 MaritalStatus
        /// </summary>
        public string marital_status { get; set; }
        /// <summary>
        ///  是否和父母居住
        /// </summary>
        public bool is_live_parent { get; set; }
        /// <summary>
        ///  目前状态 UserStatus
        /// </summary>
        public string user_status { get; set; }
        /// <summary>
        ///  是否参加职业培训
        /// </summary>
        public bool is_pro_train { get; set; }
        /// <summary>
        ///  技能特长
        /// </summary>
        public string user_skill { get; set; }
        /// <summary>
        ///  人员类别
        /// </summary>
        public string user_type { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string user_phone { get; set; }
        /// <summary>
        ///  下次尿检时间
        /// </summary>
        public string ur_next_date { get; set; }
        /// <summary>
        ///  照片
        /// </summary>
        public string user_photo { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string user_note { get; set; }
        /// <summary>
        ///  组织机构
        /// </summary>
        //[Required]
        public int org_id { get; set; }
        /// <summary>
        ///  个人简历
        /// </summary>
        public string user_resume { get; set; }
        public int ri_id { get; set; }
        /// <summary>
        ///  执行地区
        /// </summary>
        public string exec_area { get; set; }
        /// <summary>
        ///  执行单位详称
        /// </summary>
        public string exec_unit { get; set; }
        /// <summary>
        ///  责令单位详称
        /// </summary>
        public string order_unit { get; set; }
        /// <summary>
        ///  是否感染艾滋病
        /// </summary>
        public bool is_aids { get; set; }
        /// <summary>
        ///  隔离场所
        /// </summary>
        public string isolation_url { get; set; }
        /// <summary>
        ///  隔离场所出所日期
        /// </summary>
        public string isolation_out_date { get; set; }
        /// <summary>
        ///  参加药物治疗门诊
        /// </summary>
        public string cure_ups { get; set; }
        /// <summary>
        ///  是否进入康复场所
        /// </summary>
        public bool in_recovery { get; set; }
        [Required]
        /// <summary>
        ///  报道日期
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        ///  结束日期
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        ///  结束原因 EndReason
        /// </summary>
        public string end_reason { get; set; }
        /// <summary>
        ///  第一段虹膜
        /// </summary>
        public string iris_data1 { get; set; }
        /// <summary>
        ///  第二段虹膜
        /// </summary>
        public string iris_data2 { get; set; }
        /// <summary>
        ///  管控到期
        /// </summary>
        public string control_date { get; set; }
    }
}
