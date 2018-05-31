using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.AppModel
{
    public class ModleBase
    {
        /// <summary>
        /// 用户guid
        /// </summary>
        public string userid { get; set; }
    }
    /// <summary>
    /// 出工情况
    /// </summary>
    public class CheckWork : ModleBase
    {
        /// <summary>
        ///  项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        /// 打卡日期
        /// </summary>
        [Required]
        public string check_date { get; set; }
    }

    /// <summary>
    /// 出工情况
    /// </summary>
    public class CheckWorkPageRow : ModleBase
    {
        /// <summary>
        ///  项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 打卡日期
        /// </summary>
        [Required]
        public string check_date { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int rows { get; set; } = 20;
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; } = 1;
    }

    /// <summary>
    /// 出工情况统计
    /// </summary>
    public class CheckWorkReturn
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        /// 打卡人数
        /// </summary>
        public int check_count { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int unit_count { get; set; }
        /// <summary>
        /// 出工率
        /// </summary>
        public double check_pepe { get; set; }
    }

    /// <summary>
    /// 返回具体的出工人员信息
    /// </summary>
    public class CheckUnitWorkDetail
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string person_phone { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string person_jobs { get; set; }
        /// <summary>
        /// 是否出工1：已打卡，0：未打卡
        /// </summary>
        public string if_check { get; set; }
    }

    /// <summary>
    /// 具体项目出工统计
    /// </summary>
    public class CheckWorkStatic
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string unit_name { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [Required]
        public string start_date { get; set; }
        /// <summary>
        /// 截至日期
        /// </summary>
        [Required]
        public string end_date { get; set; }
    }

    /// <summary>
    /// 月出工情况
    /// </summary>
    public class CheckWorkMonth
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string check_date { get; set; }
        /// <summary>
        /// 打卡人数
        /// </summary>
        public int check_count { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 出工率
        /// </summary>
        public string check_pepe { get; set; }
    }

    /// <summary>
    /// 出工统计返回实体
    /// </summary>
    public class CheckWorkStaticReturn
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string person_name { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string work_jobs { get; set; }
        /// <summary>
        /// 出工天数
        /// </summary>
        public int work_days { get; set; }
    }

    public class WorkCount : HCQ2_Model.APPModel.ParamModel.OrgModel
    {
        /// <summary>
        ///  出工日期 yyyy-MM-DD
        /// </summary>
        [Required]
        //[RegularExpression("^(19|20)\\d{2}[-](0[1-9]|1[0-2])$")]
        public string work_date { get; set; }
    }
}
