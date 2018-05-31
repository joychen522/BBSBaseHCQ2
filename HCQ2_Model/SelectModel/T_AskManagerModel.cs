using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SelectModel
{
    /// <summary>
    ///  请假管理 查询模型
    /// </summary>
    public class T_AskManagerModel
    {
        /// <summary>
        ///  所属单位
        /// </summary>
        public string unitID { get; set; }
        /// <summary>
        ///  用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        ///  开始时间
        /// </summary>
        public string dateStart { get; set; }
        /// <summary>
        ///  结束时间
        /// </summary>
        public string dateEnd { get; set; }
        /// <summary>
        ///  第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        ///  每页记录数
        /// </summary>
        public int rows { get; set; }
    }
}
