using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  元素--权限 设置业务层接口
    /// </summary>
    public partial interface IT_ElementPermissRelationBLL
    {
        /// <summary>
        ///  保存元素--权限设置数据
        /// </summary>
        /// <param name="menus">元素选择集合</param>
        /// <param name="per_id">权限ID</param>
        /// <returns></returns>
        bool SaveElLimitData(string menus,string reak, int per_id);
        /// <summary>
        ///  根据权限ID 获取所有元素设置项
        /// </summary>
        /// <param name="per_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_ElementPermissRelation> GetElLimitData(int per_id);
    }
}
