using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.AppModel
{

    /// <summary>
    /// 新闻公告
    /// </summary>
    public class NoticeModel
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        [Required]
        public int rows { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        [Required]
        public int page { get; set; }
        /// <summary>
        /// 类别和数据字典对应
        /// </summary>
        public string type { get; set; }
    }

    public class Message
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string notice_title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string notice_content { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public byte[] notice_image { get; set; }

        /// <summary>
        /// 类别 新闻0 公告1
        /// </summary>
        public string notice_type { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string release_name { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public string release_date { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string notice_image_src { get; set; }
    }
}
