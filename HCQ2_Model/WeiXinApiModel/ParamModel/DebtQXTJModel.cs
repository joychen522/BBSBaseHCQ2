using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model.APPModel.ParamModel;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{
    /// <summary>
    ///  欠薪项目模型
    /// </summary>
    public class DebtQXTJModel:BaseWeiXinModel
    {
        /// <summary>
        ///  用于查询欠薪项目 
        ///  非必传，不传递时查询所有欠薪项目
        /// </summary>
        [DisplayName("项目名称")]
        public string project_name { get; set; }
    }

    /// <summary>
    ///  欠薪人数、金额模型
    /// </summary>
    public class DebtMoneyPeopleModel: BaseAPI
    {
        /// <summary>
        ///  项目/单位编码，必传项目
        /// </summary>
        [DisplayName("项目/单位编码")]
        [Required]
        public string orgid { get; set; }
    }
}
