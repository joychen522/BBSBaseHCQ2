using System.Collections.Generic;

namespace HCQ2_BLL
{
    public partial class T_SysModuleBLL:HCQ2_IBLL.IT_SysModuleBLL
    {        
        /// <summary>
        /// 获取指定用户分配 模块集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_SysModule> GetPermissById(int user_id)
        {
            if (user_id <= 0)
                return null;
            return DBSession.IT_SysModuleDAL.GetPermissById(user_id);
        }
        /// <summary>
        ///  获取Table数据
        /// </summary>
        /// <param name="sm_name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_SysModule> GetModuleTableData(string sm_name, int page, int rows)
        {
            if (!string.IsNullOrEmpty(sm_name))
                return Select<int?>(s => s.sm_name.Contains(sm_name), s => s.sm_order,page, rows, true);
            return Select<int?>(s => (!string.IsNullOrEmpty(s.sm_name)), s => s.sm_order, page, rows, true);
        }
    }
}
