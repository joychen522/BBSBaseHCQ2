using HCQ2_Model;
using System.Collections.Generic;

namespace HCQ2_IDAL
{
    public partial interface IT_DocumentFolderDAL
    {
        /// <summary>
        ///  根据用户id 权限per_id获取对于的文档菜单集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="per_id">权限id</param>
        /// <returns></returns>
        List<T_DocumentFolder> GetDocTreeData(int user_id, string pageType, List<int> per_id);
    }
}
