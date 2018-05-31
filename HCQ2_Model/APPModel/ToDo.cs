using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AppModel
{

    /// <summary>
    /// 获取待办事宜
    /// </summary>
    public class ToDoRecred
    {
        /// <summary>
        /// 用户guid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 待办事宜类别 0：未处理。1：已处理
        /// </summary>
        public string todo_type { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; }
    }

    /// <summary>
    /// 待办事宜返回值
    /// </summary>
    public class TodoReturn
    {
        /// <summary>
        /// 待办事宜编号
        /// </summary>
        public int todo_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string todo_title { get; set; }
        /// <summary>
        /// 待办事宜内容
        /// </summary>
        public string todo_content { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public string send_msg_user { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string send_msg_date { get; set; }

    }
}
