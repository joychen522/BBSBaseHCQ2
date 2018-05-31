using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_AreaPermissRelationBLL
    {
        /// <summary>
        ///  根据权限id获取对于 单位信息
        /// </summary>
        /// <param name="per_id"></param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetLimitAreaDataById(int per_id);
        /// <summary>
        ///  保存单位--权限配置
        /// </summary>
        /// <param name="userData">选中单位数据</param>
        /// <param name="per_id">权限ID</param>
        /// <returns></returns>
        bool SaveAreaPerData(string userData, string reak, int per_id);
    }
}
