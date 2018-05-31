using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.AppModel
{
    /// <summary>
    /// 写入投诉举报
    /// </summary>
    public class Complaints
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        [Required]
        public string userid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string image { get; set; }
    }

    public class ComplaintsList
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string date { get; set; }
    }

    /// <summary>
    /// 详细情况传入参数
    /// </summary>
    public class ComDetail
    {
        /// <summary>
        /// 投诉编号
        /// </summary>
        [Required]
        public string id { get; set; }
    }

    /// <summary>
    /// 政法局处理投诉接口参入参数
    /// </summary>
    public class ComSove: ComDetail
    {
        /// <summary>
        /// 处理意见
        /// </summary>
        [Required]
        public string re_note { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public string userid { get; set; }
    }

    /// <summary>
    /// 详细
    /// </summary>
    public class ComplaintsDetail
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string com_title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string com_content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string com_image { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string com_date { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 处理日期
        /// </summary>
        public string re_data { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>
        public string re_content { get; set; }
    }

    /// <summary>
    /// 政法局接口传入参数
    /// </summary>
    public class ComReTypeInter
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        [Required]
        public int rows { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        [Required]
        public int page { get; set; }
        /// <summary>
        /// 类型：0未处理，1：已处理
        /// </summary>
        [Required]
        public string type { get; set; }
    }

    /// <summary>
    /// 政法局查看接口
    /// </summary>
    public class ComReType
    {
        /// <summary>
        /// 投诉信息编号
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        /// 投诉标题
        /// </summary>
        public string com_title { get; set; }
        /// <summary>
        /// 投诉人
        /// </summary>
        public string com_person { get; set; }
        /// <summary>
        /// 投诉日期
        /// </summary>
        public string com_date { get; set; }
    }

}
