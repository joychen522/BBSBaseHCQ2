using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  APP首页新闻、政策模型
    /// </summary>
    public class NewsAndPolicyModel
    {
        /// <summary>
        ///  新闻、政策ID
        /// </summary>
        public int m_id { get; set; }
        /// <summary>
        ///  新闻、政策标题
        /// </summary>
        public string news_title { get; set; }
        /// <summary>
        ///  浏览次数
        /// </summary>
        public int browse_num { get; set; }
        /// <summary>
        ///  发布时间 2018-01-02
        /// </summary>
        public string issue_date { get; set; }
        /// <summary>
        ///  图片地址
        /// </summary>
        public string img_url { get; set; }
    }
    public class NewsAndPolicyList: NewsAndPolicyModel
    {
        /// <summary>
        ///  新闻类别
        /// </summary>
        public string m_type { get; set; }
        /// <summary>
        ///  新闻列表小图标 请求地址
        /// </summary>
        public string messList_url { get; set; }
    }
     public class NewsAndPolicyDetialModel: NewsAndPolicyModel
    {
        /// <summary>
        ///  新闻类别
        /// </summary>
        public string m_type { get; set; }
        /// <summary>
        ///  详细图片请求地址
        /// </summary>
        public string messDetail_url { get; set; }
        /// <summary>
        ///  详细内容
        /// </summary>
        public string m_content { get; set; }
    }
}
