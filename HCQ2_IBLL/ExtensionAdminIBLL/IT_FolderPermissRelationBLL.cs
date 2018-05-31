using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  菜单--权限业务接口层
    /// </summary>
    public partial interface IT_FolderPermissRelationBLL
    {
        /// <summary>
        ///  保存菜单--权限设置数据
        /// </summary>
        /// <param name="menus">菜单选择集合</param>
        /// <param name="per_id">权限ID</param>
        /// <returns></returns>
        bool SaveMenuLimitData(string menus,string reak, int per_id);
        /// <summary>
        ///  根据权限ID 获取所有菜单设置项
        /// </summary>
        /// <param name="per_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_FolderPermissRelation> GetMenuLimitData(int per_id);
    }
}
