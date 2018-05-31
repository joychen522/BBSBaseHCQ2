using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  权限管理数据接口
    /// </summary>
    public partial interface IT_PermissionsDAL
    {
        /// <summary>
        ///  获取权限数据
        /// </summary>
        /// <param name="perName">权限名称 </param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页记录数</param>
        /// <param name="sm_code">所属模块代码</param>
        /// <returns></returns>
        List<HCQ2_Model.T_Permissions> GetLimitData(string perName, int page, int rows,string perType,string sm_code);
        /// <summary>
        ///  编辑权限对象
        /// </summary>
        /// <param name="permission">权限对象</param>
        void EditLimit(HCQ2_Model.T_Permissions permission);
        /// <summary>
        ///  获取角色id集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<int> GetRolesListById(int user_id);
        /// <summary>
        ///  根据用户id获取所有角色集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_Role> GetRolesById(int user_id);
        /// <summary>
        ///  根据用户id获取所有权限集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_Permissions> GetPermissById(int user_id);
        /// <summary>
        ///  根据用户id获取所有菜单资源
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageFolder> GetMenusById(int user_id);
        /// <summary>
        ///  根据用户id，请求信息获取完成菜单信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        HCQ2_Model.T_PageFolder GetMenuById(int user_id, string controller, string action);
        /// <summary>
        ///  根据用户id以及请求信息获取页面元素
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementsById(int user_id, string controller, string action);
        /// <summary>
        ///  获取用户所有元素 用户登录成功后缓存
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementsById(int user_id);
    }
}
