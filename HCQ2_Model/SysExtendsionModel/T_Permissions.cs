using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model
{
    /// <summary>
    ///  权限展现实体
    /// </summary>
    public partial class T_Permissions
    {
        /// <summary>
        ///  开打设置地址
        /// </summary>
        public string pc_openUrl { get; set; }
        /// <summary>
        ///  保存设置地址
        /// </summary>
        public string pc_saveUrl { get; set; }
        /// <summary>
        ///  宽度
        /// </summary>
        public string pc_width { get; set; }
        /// <summary>
        ///  高度
        /// </summary>
        public string pc_height { get; set; }
    }
}
