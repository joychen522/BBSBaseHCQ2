using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser
{
    public class BaneLogParam
    {
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        ///  操作者
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        ///  日志标题
        /// </summary>
        public string log_title { get; set; }
        /// <summary>
        ///  日志记录开始日期
        /// </summary>
        public string log_date_start { get; set; }
        /// <summary>
        ///  日志记录结束日期
        /// </summary>
        public string log_date_end { get; set; }

        /// <summary>
        ///  构造
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        /// <param name="user_id">操作者</param>
        /// <param name="log_title">日志标题</param>
        /// <param name="log_date">日志日期</param>
        public BaneLogParam(int page,int rows,int user_id,string log_title,string log_date_start,string log_date_end)
        {
            this.page = page;
            this.rows = rows;
            this.user_id = user_id;
            this.log_title = log_title;
            this.log_date_start = log_date_start;
            this.log_date_end = log_date_end;
        }
        public enum BaneRegisterType
        {
            //     成功注册
            OK = 0,
            //     注册失败
            ERROR = 1,
            //     已经注册过
            FINASH = 2,
            //     不属于系统禁毒人员。
            EXCEPTION = 3
        }
    }
}
