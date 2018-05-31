using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ParamModel
{
    public class JobEmployModel: BaseAPI
    {
        /// <summary>
        ///  排序字段
        ///  money：薪资排序
        ///  issueDate：发布时间
        /// </summary>
        //[RegularExpression("^[0-9]{4}-(0[1-9]{1})|(1[0-2]{1})$")]
        public string orderType { get; set; }
        /// <summary>
        ///  工作城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        ///  薪资起薪
        /// </summary>
        public int payStart { get; set; }
        /// <summary>
        ///  薪资截止
        /// </summary>
        public int payEnd { get; set; }
        /// <summary>
        ///  学历：字典
        /// </summary>
        public string use_edu { get; set; }
        /// <summary>
        ///  工作经验(工作年限)：字典
        /// </summary>
        public string useLimit { get; set; }
        /// <summary>
        ///  发布时间
        /// </summary>
        public string issueDate { get; set; }
        /// <summary>
        ///  职位类型：字典
        /// </summary>
        public string postType { get; set; }
        /// <summary>
        ///  公司性质：字典
        /// </summary>
        public string busType { get; set; }
        /// <summary>
        ///  公司规模：字典
        /// </summary>
        public string busScale { get; set; }
    }
}
