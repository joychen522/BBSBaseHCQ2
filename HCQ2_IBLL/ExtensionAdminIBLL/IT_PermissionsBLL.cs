using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  权限管理业务接口
    /// </summary>
    public partial interface IT_PermissionsBLL
    {
        /// <summary>
        ///  获取权限数据
        /// </summary>
        /// <param name="perName">权限名称</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页数量</param>
        /// <param name="sm_code">所属模块代码</param>
        /// <returns></returns>
        List<T_Permissions> GetLimitData(string perName,int page,int rows,string perType,string sm_code);
        /// <summary>
        ///  添加权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        bool AddLimit(T_Permissions permission);
        /// <summary>
        ///  编辑权限对象
        /// </summary>
        /// <param name="permission">对象</param>
        /// <param name="per_id">主键值</param>
        /// <returns></returns>
        bool EditLimit(T_Permissions permission, int per_id);

        //***************************权限--验证**********************************
        /// <summary>
        ///  根据用户id以及请求信息验证用户是否有权限访问页面
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool CheckMenuByUser(int user_id, string controller, string action);
        /// <summary>
        ///  根据用户id获取所有菜单资源
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageFolder> GetMenusById(int user_id);
        /// <summary>
        ///  根据用户id以及请求信息获取页面元素
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementsById(int user_id, string controller, string action);
        /// <summary>
        ///  通过用户id 获取所有元素信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<HCQ2_Model.T_PageElement> GetElementsById(int user_id);
        /// <summary>
        ///  根据用户id获取所有角色集合
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<int> GetRolesById(int user_id);
        /// <summary>
        ///  根据用户id获取权限集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<T_Permissions> GetPermissById(int user_id);
    }
}
