using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    ///  考勤模型
    /// </summary>
    public class CheckWorkModel
    {
        /// <summary>
        ///  人员编码（根据虹膜信息获取）
        /// </summary>
        [Required]
        public string personid { get; set; }
        /// <summary>
        ///  签到时间
        /// （字符格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        [Required]
        public string signtime { get; set; }

        /// <summary>
        /// 门禁标识 0为出门考勤； 1为入门考勤
        /// </summary>
        public string type { get; set; }
    }

    public class ReturnCheckModle
    {
        /// <summary>
        /// 签到成功之后的RowID
        /// </summary>
        public string worksignid { get; set; }
    }

}
