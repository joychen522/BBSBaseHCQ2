using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  用户组业务接口层定义
    /// </summary>
    public partial interface IT_UserGroupBLL
    {
        /// <summary>
        ///  获取用户组数据
        /// </summary>
        /// <param name="groupName">用户组名称</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        /// <param name="sm_code">所属模块代码</param>
        /// <returns></returns>
        List<T_UserGroup> GetUserGroupData(string groupName, int page, int rows,string sm_code);
        /// <summary>
        ///  添加用户组
        /// </summary>
        /// <param name="userGroup">用户组对象</param>
        bool AddGroup(T_UserGroup userGroup);
        /// <summary>
        ///  编辑用户组对象
        /// </summary>
        /// <param name="userGroup">对象</param>
        /// <param name="group_id">主键值</param>
        /// <returns></returns>
        bool EditGroup(T_UserGroup userGroup, int group_id);
    }
}
