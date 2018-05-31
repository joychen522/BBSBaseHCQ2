using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.AppModel
{
    public class WorkPerson
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        [Required]
        public string userid { get; set; }
    }

    /// <summary>
    /// 分页
    /// </summary>
    public class WorkPersonRowPage : WorkPerson
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
    }

    /// <summary>
    ///  维权凭证传入参数
    /// </summary>
    public class WorkPersonDetail : WorkPerson
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Required]
        public string unit_code { get; set; }
    }

}
