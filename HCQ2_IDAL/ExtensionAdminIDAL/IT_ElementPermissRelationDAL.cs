using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  元素--权限数据层接口
    /// </summary>
   public partial interface IT_ElementPermissRelationDAL
    {
        /// <summary>
        ///  根据pe_ids字符串集合删除
        /// </summary>
        /// <param name="pe_ids">元素id集合</param>
        /// <param name="per_id">权限id</param>
        /// <returns></returns>
        int Delete(List<string> pe_ids, int per_id);
    }
}
