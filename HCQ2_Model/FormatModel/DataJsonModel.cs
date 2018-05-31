using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.FormatModel
{
    /// <summary>
    ///  前端列表模型
    /// </summary>
    public class DataJsonModel<T>
    {
        [DisplayName("每页显示几条记录")]
        public int PageSize { get; set; }
        [DisplayName("当前页索引")]
        public int PageIndex { get; set; }
        [DisplayName("总的记录条数")]
        public int RowCount { get; set; }
        [DisplayName("分页总数")]
        public int PageCount { get; set; }
        [DisplayName("数据")]
        public List<T> PagedData { get; set; }
    }
}
