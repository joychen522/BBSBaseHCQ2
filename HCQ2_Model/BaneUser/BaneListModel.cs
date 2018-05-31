using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneListModel
    {
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
        ///  联系电话
        /// </summary>
        public string user_phone { get; set; }
        /// <summary>
        ///  报到时间
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        ///  结束时间
        /// </summary>
        public string end_date { get; set; }
    }
}
