using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneFamilyRecordModel
    {
        /// <summary>
        ///  主键
        /// </summary>
        public int fr_id { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        [Required]
        public string user_identify { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string fr_name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string fr_sex { get; set; }
        /// <summary>
        ///  出生日期
        /// </summary>
        public string fr_birth { get; set; }
        /// <summary>
        ///  学历
        /// </summary>
        public string fr_edu { get; set; }
        /// <summary>
        ///  家庭住址
        /// </summary>
        public string fr_family_url { get; set; }
        /// <summary>
        ///  职业
        /// </summary>
        public string fr_job { get; set; }
        /// <summary>
        ///  工作单位
        /// </summary>
        public string fr_unit { get; set; }
        /// <summary>
        ///  相互关系
        /// </summary>
        public string fr_relation { get; set; }
        /// <summary>
        ///  联系电话
        /// </summary>
        public string fr_phone { get; set; }
        /// <summary>
        ///  关系类别 
        ///  0：家庭成员
        ///  1：社会关系
        /// </summary>
        [Required]
        public int fr_type { get; set; }
    }
}
