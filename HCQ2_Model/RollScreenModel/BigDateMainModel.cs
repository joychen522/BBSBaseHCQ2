using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.RollScreenModel
{
    public class BigDateMainModel
    {

    }

    public class MainData
    {
        /// <summary>
        /// 企业总数
        /// </summary>
        public int enterise_num { get; set; }
        /// <summary>
        /// 务工人员总数
        /// </summary>
        public int worker_num { get; set; }
        /// <summary>
        /// 进入打卡人员
        /// </summary>
        public int today_check_workers { get; set; }
    }

    /// <summary>
    /// 项目情况统计
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int total_worker { get; set; }
        /// <summary>
        /// 打卡人数
        /// </summary>
        public int check_worker { get; set; }
    }

    /// <summary>
    /// 项目打卡详细统计 接收参数
    /// </summary>
    public class ProjectCheckUnit
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_id { get; set; }
    }

    /// <summary>
    /// 大数据务工人员工种统计
    /// </summary>
    public class DataViewJosb
    {
        public int rows { get; set; }
    }

    /// <summary>
    /// 项目打卡详细统计
    /// </summary>
    public class ProjectCheck
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string worker_name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string person_phone { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public string check_time { get; set; }
    }

    /// <summary>
    /// 欠薪预警
    /// </summary>
    public class DeWage
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 欠薪金额
        /// </summary>
        public decimal? de_wage { get; set; }
        /// <summary>
        /// 保障金
        /// </summary>
        public decimal? security_money { get; set; }
        /// <summary>
        /// 欠薪人数
        /// </summary>
        public int? de_personcount { get; set; }
    }

    /// <summary>
    /// 欠薪预警>>项目详细信息
    /// </summary>
    public class DeWageProjectDetail
    {
        /// <summary>
        /// 欠薪人数
        /// </summary>
        public string de_personcount { get; set; }
        /// <summary>
        /// 欠薪金额
        /// </summary>
        public string de_wage { get; set; }
        /// <summary>
        /// 开工日期
        /// </summary>
        public string project_start { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string bargain_money { get; set; }
        /// <summary>
        /// 工人数
        /// </summary>
        public string workers_count { get; set; }
        /// <summary>
        /// 保障金
        /// </summary>
        public string security_money { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string project_contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string project_address { get; set; }
        /// <summary>
        /// 施工单位联系人
        /// </summary>
        public string SGDWLXR { get; set; }
        /// <summary>
        /// 施工单位联系人电话
        /// </summary>
        public string SGDWLXRDH { get; set; }
    }

    /// <summary>
    /// 屏幕中间项目模块
    /// </summary>
    public class StatisMiddle
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 项目总人数
        /// </summary>
        public int person_count { get; set; }
    }

}
