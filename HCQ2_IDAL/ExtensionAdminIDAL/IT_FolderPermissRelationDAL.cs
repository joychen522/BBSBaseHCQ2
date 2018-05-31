using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  权限--菜单权限管理数据层接口定义
    /// </summary>
    public partial interface IT_FolderPermissRelationDAL
    {
        /// <summary>
        ///  根据folder_ids字符串集合删除
        /// </summary>
        /// <param name="folder_ids">菜单id集合</param>
        /// <param name="per_id">权限id</param>
        /// <returns></returns>
        int Delete(List<string> folder_ids,int per_id);
    }
}
