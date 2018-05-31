using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.UploadModel
{
    public class Result
    {
        /// <summary>
        /// 表示图片是否已上传成功。
        /// </summary>
        public bool success;
        /// <summary>
        /// 自定义的附加消息。
        /// </summary>
        public string msg;
        /// <summary>
        /// 表示原始图片的保存地址。
        /// </summary>
        public string sourceUrl;
        /// <summary>
        /// 表示所有头像图片的保存地址，该变量为一个数组。
        /// </summary>
        public List<string> avatarUrls;
    }
}
