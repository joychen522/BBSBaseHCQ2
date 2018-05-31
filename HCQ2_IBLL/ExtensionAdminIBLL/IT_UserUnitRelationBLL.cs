using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户--代管配置业务接口定义
    /// </summary>
    public partial interface IT_UserUnitRelationBLL
    {
        /// <summary>
        ///  获取所有单位，并且根据用户id设置对应的代管单位
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetUserUnitDataById(int user_id);
        /// <summary>
        ///  保存用户-代管设置数据
        /// </summary>
        /// <param name="userData">设置数据json字符串格式</param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        bool SaveUserUnitData(string userData,int user_id);
        /// <summary>
        ///  获取全部区域 以及区域下的人员  根据权限设置对于数据
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="isAll">是否获取全部区域</param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetUserAreaAndPersonById(int user_id,bool isAll=true);
    }
}
