using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ExtendsionModel
{
    /// <summary>
    ///  用工需求一栏模型
    /// </summary>
    public class T_UseWorkerModel
    {
        public int use_id { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  标题
        /// </summary>
        public string use_title { get; set; }
        /// <summary>
        ///  岗位中文名
        /// </summary>
        public string work_type_value { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string work_type { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 工种城市
        /// </summary>
        public string work_city { get; set; }
        /// <summary>
        ///  地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 薪酬起薪
        /// </summary>
        public int use_wage_start { get; set; }

        /// <summary>
        /// 薪酬最高
        /// </summary>
        public int use_wage_end { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string use_sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string use_age { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string use_edu { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public string use_ageLimit { get; set; }
        /// <summary>
        ///  工作年限 text
        /// </summary>
        public string ageLimit { get; set; }
        /// <summary>
        /// 专业要求
        /// </summary>
        public string use_major { get; set; }
        /// <summary>
        /// 就职日期
        /// </summary>
        public string work_start { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string issue_start { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int use_status { get; set; }
        /// <summary>
        /// 职位描述
        /// </summary>
        public string postNote { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string use_note { get; set; }
        /// <summary>
        ///  logo
        /// </summary>
        public byte[] logo { get; set; }
    }
    /// <summary>
    ///  查看用工需求一栏模型
    /// </summary>
    public class T_UseIssueListModel
    {
        public int use_id { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  投递简历数量
        /// </summary>
        public int subLen { get; set; }
        /// <summary>
        ///  岗位中文名
        /// </summary>
        public string work_type_value { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string work_type { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 工种城市
        /// </summary>
        public string work_city { get; set; }
        /// <summary>
        /// 薪酬起薪
        /// </summary>
        public int use_wage_start { get; set; }
        /// <summary>
        /// 薪酬最高
        /// </summary>
        public int use_wage_end { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string use_sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string use_age { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string use_edu { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public string use_ageLimit { get; set; }
        public string ageLimit { get; set; }
        /// <summary>
        /// 专业要求
        /// </summary>
        public string use_major { get; set; }
        /// <summary>
        /// 就职日期
        /// </summary>
        public string work_start { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string issue_start { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int use_status { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string use_note { get; set; }
    }
    /// <summary>
    ///  简历模型
    /// </summary>
    public class T_JobResumeRelationModel
    {
        /// <summary>
        ///  主键
        /// </summary>
        public int job_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        ///  电话
        /// </summary>
        public string C0104 { get; set; }
        /// <summary>
        ///  籍贯
        /// </summary>
        public string A0114 { get; set; }
        /// <summary>
        ///  出生日期
        /// </summary>
        public string A0111 { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string A0107 { get; set; }
        /// <summary>
        ///  学历
        /// </summary>
        public string A0108 { get; set; }
        /// <summary>
        ///  专业
        /// </summary>
        public string A0410 { get; set; }
        /// <summary>
        ///  申请日期
        /// </summary>
        public string send_date { get; set; }
        /// <summary>
        ///  状态
        /// </summary>
        public string job_status { get; set; }
    }
}
