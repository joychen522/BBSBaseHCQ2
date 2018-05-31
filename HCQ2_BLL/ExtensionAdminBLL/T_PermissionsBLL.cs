using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2UI_Helper;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  权限管理业务层
    /// </summary>
    public partial class T_PermissionsBLL:HCQ2_IBLL.IT_PermissionsBLL
    {
        /// <summary>
        ///  获取权限数据
        /// </summary>
        /// <param name="perName">权限名称</param>
        /// <returns></returns>
        public List<T_Permissions> GetLimitData(string perName,int page,int rows,string perType,string sm_code)
        {
            return DBSession.IT_PermissionsDAL.GetLimitData(perName, page, rows,perType,sm_code);
        }
        /// <summary>
        ///  添加权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns></returns>
        public bool AddLimit(T_Permissions permission)
        {
            if (null == permission)
                return false;
            permission.creator_date = DateTime.Now;
            permission.creator_id = OperateContext.Current.Usr.user_id;
            permission.creator_name = OperateContext.Current.Usr.user_name;
            DBSession.IT_PermissionsDAL.Add(permission);
            return true;
        }
        /// <summary>
        ///  编辑权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <param name="per_id">主键值</param>
        /// <returns></returns>
        public bool EditLimit(T_Permissions permission, int per_id)
        {
            if (null == permission)
                return false;
            permission.per_id = per_id;
            permission.creator_date = DateTime.Now;
            permission.creator_id = OperateContext.Current.Usr.user_id;
            permission.creator_name = OperateContext.Current.Usr.user_name;
            DBSession.IT_PermissionsDAL.EditLimit(permission);
            return true;
        }
        //*******************************权限--验证*************************************
        /// <summary>
        ///  根据用户id以及请求信息验证用户是否有权限访问页面
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool CheckMenuByUser(int user_id, string controller, string action)
        {
            T_PageFolder folder = DBSession.IT_PermissionsDAL.GetMenuById(user_id, controller, action);
            if (null == folder)
                return false;
            return true;
        }

        /// <summary>
        ///  根据用户id获取所有菜单资源
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<HCQ2_Model.T_PageFolder> GetMenusById(int user_id)
        {
            return DBSession.IT_PermissionsDAL.GetMenusById(user_id);
        }

        /// <summary>
        ///  根据用户id以及请求信息获取页面元素
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public List<HCQ2_Model.T_PageElement> GetElementsById(int user_id, string controller, string action)
        {
            return DBSession.IT_PermissionsDAL.GetElementsById(user_id, controller, action);
        }

        public List<T_PageElement> GetElementsById(int user_id)
        {
            return DBSession.IT_PermissionsDAL.GetElementsById(user_id);
        }

        public List<int> GetRolesById(int user_id)
        {
            return DBSession.IT_PermissionsDAL.GetRolesListById(user_id);
        }

        public List<T_Permissions> GetPermissById(int user_id)
        {
            return DBSession.IT_PermissionsDAL.GetPermissById(user_id);
        }
    }
}
