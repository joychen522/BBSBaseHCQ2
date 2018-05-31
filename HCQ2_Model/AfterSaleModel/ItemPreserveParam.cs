using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.AfterSaleModel
{
    public class ItemPreserveParam
    {
        /// <summary>
        ///  第几页
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        ///  每页数量
        /// </summary>
        public int page { get; set; }
        /// <summary>
        ///  区域代码
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        ///  单位代码
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        ///  项目状态
        /// </summary>
        public string status { get; set; }
        //设备管理所特需字段
        /// <summary>
        ///  安装人
        /// </summary>
        public string install_name { get; set; }
        /// <summary>
        ///  技术支持
        /// </summary>
        public string skiller { get; set; }
    }
}
