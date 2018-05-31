using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  职位详情返回值
    /// </summary>
    public class PostDetialResultModel
    {
        /// <summary>
        ///  招聘信息主键ID
        /// </summary>
        public int use_id { get; set; }
        /// <summary>
        ///  标题
        /// </summary>
        public string use_title{get;set;}
        /// <summary>
        ///  薪资范围
        /// </summary>
        public string payMoney { get; set; }
        /// <summary>
        ///  发布时间
        ///  09-01
        /// </summary>
        public string issueDate { get; set; }
        /// <summary>
        ///  城市
        /// </summary>
        public string work_city { get; set; }
        /// <summary>
        ///  学历
        /// </summary>
        public string use_edu { get; set; }
        /// <summary>
        ///  工作经验
        /// </summary>
        public string useLimit { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        ///  单位主键ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  详细地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        ///  职位描述
        /// </summary>
        public string postNote { get; set; }
        /// <summary>
        ///  logo
        /// </summary>
        public byte[] logo { get; set; }
    }
}
