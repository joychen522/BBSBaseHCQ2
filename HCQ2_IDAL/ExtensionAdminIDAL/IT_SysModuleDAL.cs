using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    public partial interface IT_SysModuleDAL
    {
        /// <summary>
        /// 获取指定用户分配 模块集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_SysModule> GetPermissById(int user_id);
    }
}
