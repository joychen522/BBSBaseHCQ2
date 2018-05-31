using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{
    /// <summary>
    ///  征信模型
    /// </summary>
    public class CompanyModel:BaseWeiXinModel
    {
        /// <summary>
        ///  查询征信关键字
        /// </summary>
        [DisplayName("关键字")]
        public string keyword { get; set; }
    }

    /// <summary>
    ///  征信明细接收参数模型
    /// </summary>
    public class CompanyDetailModel: BaseWeiXinModel
    {
        /// <summary>
        ///  企业主键ID
        /// </summary>
        [Required]
        public int com_id { get; set; }
    }
}
