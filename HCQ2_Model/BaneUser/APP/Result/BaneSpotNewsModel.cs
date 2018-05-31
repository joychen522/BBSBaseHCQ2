using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  焦点新闻模型
    /// </summary>
    public class BaneSpotNewsModel
    {
        /// <summary>
        ///  新闻标题
        /// </summary>
        public string news_title { get; set; }
        /// <summary>
        ///  焦点新闻图片地址
        /// </summary>
        public string news_url { get; set; }
        /// <summary>
        ///  新闻id
        /// </summary>
        public int m_id { get; set; }
    }
}
