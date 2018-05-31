using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SelectModel
{
    /// <summary>
    ///  A02查询模型
    /// </summary>
    public class A02Model
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
        ///  单位代码
        /// </summary>
        public string unitID { get; set; }
        /// <summary>
        ///  打卡日期
        /// </summary>
        public string cardDate { get; set; }
        /// <summary>
        ///  开始时间
        /// </summary>
        public string dateStart { get; set; }
        /// <summary>
        ///  结束时间
        /// </summary>
        public string dateEnd { get; set; }
    }
}
