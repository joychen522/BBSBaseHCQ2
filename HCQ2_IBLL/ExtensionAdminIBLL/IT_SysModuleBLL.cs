using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IT_SysModuleBLL
    {
        /// <summary>
        /// 获取指定用户分配 模块集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_SysModule> GetPermissById(int user_id);
        /// <summary>
        ///  获取Table数据
        /// </summary>
        /// <param name="sm_name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_SysModule> GetModuleTableData(string sm_name,int page,int rows);
    }
}
