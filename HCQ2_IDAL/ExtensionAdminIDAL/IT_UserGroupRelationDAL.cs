using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  用户组-用户数据层定义
    /// </summary>
    public partial interface IT_UserGroupRelationDAL
    {
        /// <summary>
        ///  获取用户组-用户对应数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        List<HCQ2_Model.T_User> GetUserAndGroupData(string userName,int page,int rows);
        /// <summary>
        ///  统计用户组-用户记录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        int GetUserAndGroupDataCount(string userName);
    }
}
