using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.SelectModel
{
    /// <summary>
    ///  工资汇总统计查询模型
    /// </summary>
    public class WGJG01ChartModel
    {
        /// <summary>
        ///  主键id
        /// </summary>
        public string rowID { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        ///  状态
        /// </summary>
        public string stauts { get; set; }
        /// <summary>
        ///  单位代码
        /// </summary>
        public string unitID { get; set; }
        /// <summary>
        ///  开始时间
        /// </summary>
        public string dateStart { get; set; }
        /// <summary>
        ///  结束时间
        /// </summary>
        public string dateEnd { get; set; }
        /// <summary>
        ///  是否所有
        /// </summary>
        public bool isAll { get; set; }
        /// <summary>
        ///  是否发放1：发放，其他：未发放
        /// </summary>
        public string isGive { get; set; }
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
