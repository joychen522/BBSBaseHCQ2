using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    public class CheckModle
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string PersonID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime? A0201 { get; set; }
        /// <summary>
        /// 是否补签
        /// </summary>
        public string isFill { get; set; }
        /// <summary>
        /// 补签用户
        /// </summary>
        public string buUser { get; set; }
        /// <summary>
        /// 补签原因
        /// </summary>
        public string buReason { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public string B0001Name { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string E0386 { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string C0104 { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string A0177 { get; set; }
        /// <summary>
        /// 进出场标识
        /// </summary>
        public string A0202 { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 打卡次数
        /// </summary>
        public string check_count { get; set; }
        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime? lowWorkDate { get; set; }
        /// <summary>
        /// 是否补签
        /// </summary>
        public string lowIsFill { get; set; }
        /// <summary>
        /// 补签原因
        /// </summary>
        public string lowReason { get; set; }
        /// <summary>
        /// 补签用户
        /// </summary>
        public string lowUser { get; set; }
    }
}
