using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  权限-模块数据接口层
    /// </summary>
    public partial interface IT_ModulePermissRelationDAL
    {
        /// <summary>
        ///  删除之前配置的权限
        /// </summary>
        /// <param name="sm_id"></param>
        /// <param name="per_id"></param>
        /// <returns></returns>
        int Delete(List<int> sm_id, int per_id);
    }
}
