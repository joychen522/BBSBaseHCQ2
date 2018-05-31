using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    /// <summary>
    ///  职位列表返回值model
    /// </summary>
    public class JobEmployResultModel
    {
        /// <summary>
        ///  招聘信息主键ID
        /// </summary>
        public int use_id { get; set; }
        /// <summary>
        ///  标题
        /// </summary>
        public string use_title { get; set; }
        /// <summary>
        ///  单位名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        ///  地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        ///  薪资范围
        /// </summary>
        public string payMoney { get; set; }
        /// <summary>
        ///  发布时间
        ///  2017-09-01 17:34
        /// </summary>
        public string issueDate { get; set; }
        /// <summary>
        ///  logo
        /// </summary>
        public byte[] logo { get; set; }
    }
}
