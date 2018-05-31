using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneProModel
    {
        /// <summary>
        ///  尿检ID
        /// </summary>
        public int ur_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public string user_sex { get; set; }
        /// <summary>
        ///  年龄，根据出生日期和当前时间计算得到
        /// </summary>
        public int user_age { get; set; }
        /// <summary>
        ///  身份证
        /// </summary>
        public string user_identify { get; set; }
        /// <summary>
        ///  身份证部分隐藏
        /// </summary>
        public string hiden_identify { get; set; }
        /// <summary>
        ///  人员类别：社区戒毒，社区康复
        /// </summary>
        public string user_type { get; set; }
        /// <summary>
        ///  本次尿检时间：生成记录时间
        /// </summary>
        public string this_date { get; set; }
        /// <summary>
        ///  下次尿检时间：根据生成记录时间结合设置项推算下次时间
        /// </summary>
        public string next_date { get; set; }
        /// <summary>
        ///  状态
        /// </summary>
        public int approve_status { get; set; }
    }
}
