using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.DocModel
{
    public class DocTreeResultModel
    {
        /// <summary>
        ///  文档id
        /// </summary>
        public int file_id { get; set; }
        /// <summary>
        ///  文档名
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        ///  文档别名
        /// </summary>
        public string alias_name { get; set; }
        /// <summary>
        ///  文档类别
        /// </summary>
        public string file_type { get; set; }
        /// <summary>
        ///  文档大小
        /// </summary>
        public string file_size { get; set; }
        /// <summary>
        ///  文档颁布开始时间
        /// </summary>
        public string issue_start { get; set; }
        /// <summary>
        ///  文档颁布截止时间
        /// </summary>
        public string issue_end { get; set; }
        /// <summary>
        ///  上传者
        /// </summary>
        public string create_name { get; set; }
        /// <summary>
        ///  上传时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        ///  上传者
        /// </summary>
        public int create_id { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string file_note { get; set; }
        /// <summary>
        ///  下载地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///  文档类型
        /// </summary>
        public string doc_type { get; set; }
        /// <summary>
        ///  文号
        /// </summary>
        public string doc_number { get; set; }
        /// <summary>
        ///  文档定价
        /// </summary>
        public decimal file_money { get; set; }
        /// <summary>
        ///  文档下载次数
        /// </summary>
        public int down_number { get; set; }
        /// <summary>
        ///  文档类型
        /// </summary>
        public string file_classify { get; set; }
        /// <summary>
        ///  文档状态
        /// </summary>
        public int file_status { get; set; }
    }
    public class DocTreeOrListModel
    {
        /// <summary>
        ///  文档id
        /// </summary>
        public int file_id { get; set; }
        /// <summary>
        ///  文档名
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        ///  文档别名
        /// </summary>
        public string alias_name { get; set; }
        /// <summary>
        ///  文档类别
        /// </summary>
        public string file_type { get; set; }
        /// <summary>
        ///  文档大小
        /// </summary>
        public string file_size { get; set; }
        /// <summary>
        ///  文档颁布开始时间
        /// </summary>
        public string issue_start { get; set; }
        /// <summary>
        ///  文档颁布截止时间
        /// </summary>
        public string issue_end { get; set; }
        /// <summary>
        ///  上传者
        /// </summary>
        public string create_name { get; set; }
        /// <summary>
        ///  上传时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        ///  上传者
        /// </summary>
        public int create_id { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string file_note { get; set; }
        /// <summary>
        ///  下载地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///  文档类型
        /// </summary>
        public string docType { get; set; }
        /// <summary>
        ///  文号
        /// </summary>
        public string doc_number { get; set; }
        public int folder_id { get; set; }
        public string folder_name { get; set; }
        public string folder_url { get; set; }
        public int folder_pid { get; set; }
        public string folder_image { get; set; }
        public Nullable<int> folder_order { get; set; }
        public bool was_share { get; set; }
        public bool if_create_child { get; set; }
        public bool read_only { get; set; }
        public bool if_sys { get; set; }
        public int doc_type { get; set; }
        public bool have_child { get; set; }
        public string page_type { get; set; }
    }
}
