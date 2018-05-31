using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  模块-权限业务接口层
    /// </summary>
    public partial interface IT_ModulePermissRelationBLL
    {
        /// <summary>
        ///  保存权限--模块对于关系
        /// </summary>
        /// <param name="menus">待保存的模块数据集合</param>
        /// <param name="reak">标记</param>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        bool SaveModulePerData(string userData, string reak,int per_id);
    }
}
