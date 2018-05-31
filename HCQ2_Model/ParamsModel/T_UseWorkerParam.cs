using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ParamsModel
{
    public class T_UseWorkerParam
    {
        /// <summary>
        ///  单位ID
        /// </summary>
        public int com_id { get; set; }
        /// <summary>
        ///  状态
        /// </summary>
        public int use_status { get; set; } = 1;
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        ///  岗位 
        /// </summary>
        public string jobName { get; set; }
        /// <summary>
        ///  起薪
        /// </summary>
        public int jobStartMoney { get; set; }
        /// <summary>
        ///  薪资截止
        /// </summary>
        public int jobEndMoney { get; set; }
    }
    public class T_IssueListParam
    {
        /// <summary>
        ///  招聘数据
        /// </summary>
        public int use_id { get; set; }
        /// <summary>
        ///  姓名
        /// </summary>
        public string A0101 { get; set; }
        /// <summary>
        ///  电话
        /// </summary>
        public string C0104 { get; set; }
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        ///  每页数量
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        ///  专业 
        /// </summary>
        public string A0410 { get; set; }
    }
}
