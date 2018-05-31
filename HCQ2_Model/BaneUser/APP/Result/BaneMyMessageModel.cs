using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Result
{
    /// <summary>
    ///  我的消息模型
    /// </summary>
    public class BaneMyMessageModel
    {
        /// <summary>
        ///  消息标题
        /// </summary>
        public string mess_title { get; set; }
        /// <summary>
        ///  消息内容
        /// </summary>
        public string mess_content { get; set; }
        /// <summary>
        ///  发送者
        /// </summary>
        public string send_name { get; set; }
        /// <summary>
        ///  发送时间 2018年5月2日
        /// </summary>
        public string send_date { get; set; }
    }
}
