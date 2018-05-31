using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SelectModel
{
    /// <summary>
    ///  select值模型
    /// </summary>
    public class SelectValueModel
    {
        /// <summary>
        ///  下拉text
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  下拉value
        /// </summary>
        public string value { get; set; }
        /// <summary>
        ///  代码
        /// </summary>
        public string item_code { get; set; }
    }
    public class SelectModel
    {
        /// <summary>
        ///  下拉text
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  下拉value
        /// </summary>
        public string value { get; set; }
    }
    public class ddlSelectModel
    {
        /// <summary>
        ///  显示值
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  隐藏值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        ///  下级节点
        /// </summary>
        public List<ddlSelectModel> child { get; set; }
    }
}
