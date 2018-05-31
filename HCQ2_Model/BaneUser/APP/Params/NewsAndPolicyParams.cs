using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Params
{
    /// <summary>
    ///  新闻、政策参数模型
    /// </summary>
    public class NewsAndPolicyParams: BaseBaneModel
    {
        /// <summary>
        ///  新闻、政策ID
        /// </summary>
        [DisplayName("新闻、政策ID")]
        [Required]
        public int m_id { get; set; }
    }
}
