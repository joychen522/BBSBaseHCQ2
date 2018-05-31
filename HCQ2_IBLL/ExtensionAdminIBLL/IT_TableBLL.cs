using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  表结构树业务模型借口定义
    /// </summary>
    public partial interface IT_TableBLL
    {
        /// <summary>
        ///  获取表结构树集合
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.TreeModel.TableStrcutTreeModel> GetTableTreeModel();
        /// <summary>
        ///  获取表字段信息通过表名
        /// </summary>
        /// <param name="table_name">表明</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        /// <returns></returns>
        List<HCQ2_Model.T_TableField> GetTableDataByName(string tableName,string fieldName, int page, int rows);
    }
}
