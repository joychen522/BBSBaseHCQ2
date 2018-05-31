using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  文档菜单权限设置 数据层接口定义
    /// </summary>
    public partial interface IT_DocFolderPermissRelationDAL
    {
        /// <summary>
        ///  根据权限删除配置权限
        /// </summary>
        /// <param name="folder_ids"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        int Delete(List<string> folder_ids, int per_id);
    }
}
