using System.Collections.Generic;
using System.Linq;
using HCQ2_Model;
using HCQ2_Common.Bean;
using HCQ2_Common.Constant;
using HCQ2_Model.Constant;

namespace HCQ2UI_Helper.Session
{
    /// <summary>
    ///  权限验证缓存
    /// </summary>
    public class SysPermissSession
    {
        /// <summary>
        ///  获取当前登录用户 角色集合
        /// </summary>
        public static List<int> RolesList
        {
            get
            {
                object cache = SessionHelper.GetSessionValue(CacheConstant.loginUserCacheRoles);
                List<int> list = (cache != null) ? (List<int>)cache : null;
                if (null == list)
                {
                    list= OperateContext.Current.bllSession.T_Permissions.GetRolesById(OperateContext.Current.Usr.user_id);
                    SessionHelper.AddSessionValue(CacheConstant.loginUserCacheRoles,list);
                }
                return list;
            }
        }
        /// <summary>
        ///  获取当前登录用户 权限集合
        /// </summary>
        public static List<T_Permissions> PermissList
        {
            get
            {
                object cache = SessionHelper.GetSessionValue(CacheConstant.loginUserCachePermiss);
                List<T_Permissions> list = (cache != null) ? (List<T_Permissions>)cache : null;
                if (null == list)
                {
                    list= OperateContext.Current.bllSession.T_Permissions.GetPermissById(OperateContext.Current.Usr.user_id);
                    SessionHelper.AddSessionValue(CacheConstant.loginUserCachePermiss,list);
                }
                return list;
            }
        }
        /// <summary>
        ///  获取当前登录用户 菜单集合
        /// </summary>
        public static List<T_PageFolder> MenusList
        {
            get
            {
                return OperateContext.Current.bllSession.T_Permissions.GetMenusById(OperateContext.Current.Usr.user_id); 
            }
        }
        /// <summary>
        ///  获取当前登录用户 所有元素集合
        /// </summary>
        public static List<T_PageElement> ElementsList
        {
            get
            {
                object cache = SessionHelper.GetSessionValue(CacheConstant.allCacheElements);
                List<T_PageElement> list = (null != cache) ? (List<T_PageElement>)cache : null;
                if (null == list)
                {
                    list= OperateContext.Current.bllSession.T_Permissions.GetElementsById(OperateContext.Current.Usr.user_id);
                    SessionHelper.AddSessionValue(CacheConstant.allCacheElements,list);
                }
                return list;
            }
        }
        /// <summary>
        ///  判断是否有权访问页面
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public static bool CheckPermiss(string controller, string action)
        {
            if (null == MenusList)
                return false;
            var query = MenusList.Where(s => s.folder_url == (controller + "/" + action)).ToList();
            if (query.Count <= 0)
                return false;
            return true;
        }
        /// <summary>
        ///  获取请求页面的元素集合
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static List<T_PageElement> GetElementByUser(string controller,string action)
        {
            if (null == MenusList)
                return null;
            T_PageFolder folder =
                MenusList.FirstOrDefault(s => s.folder_url == (controller + "/" + action));
            if (null == folder)
                return null;
            if (null == ElementsList)
                return null;
            return ElementsList.Where(s => s.folder_id == folder.folder_id).ToList();
        }
        /// <summary>
        ///  获取当前登录用户 指定页面的元素id集合
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static List<string> GetElementCodeByUser(string controller, string action)
        {
            List<string> list = new List<string>();
            if (null == MenusList)
                return list;
            T_PageFolder folder =
                MenusList.FirstOrDefault(s => (!string.IsNullOrEmpty(s.folder_url)) && s.folder_url.Contains(controller + "/" + action));
            if (null == folder)
                return list;
            if (null == ElementsList)
                return list;
            return ElementsList.Where(s => s.folder_id == folder.folder_id).Select(s => s.pe_code).ToList();
        }
        
        /// <summary>
        ///  获取当前登录用户分配 模块集合
        /// </summary>
        public static List<T_SysModule> SysModule
        {
            get
            {
                return OperateContext.Current.bllSession.T_SysModule.GetPermissById(OperateContext.Current.Usr.user_id);
            }
        }
        /// <summary>
        ///  判断当前登录用户是否属于 区域管理员
        /// </summary>
        public static bool isAreaManager
        {
            get
            {
                if (PermissList == null || PermissList.Count <= 0)
                    return false;
                var query = PermissList.Where(s => s.per_type == "areaAdmin");
                return (query != null && query.Count() > 0) ? true : false;
            }
        }
        /// <summary>
        ///  将用户传递的guid转换为身份证
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ChangeIdByGuid(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return null;
            return OperateContext.Current.bllSession.Bane_User.Select(s => s.user_guid == guid).FirstOrDefault()?.user_identify;
            //object cache = SessionHelper.GetSessionValue(CacheConstant.baneUserID);
            //string user_identify = (cache != null) ? (string)cache : null;
            //if (string.IsNullOrEmpty(user_identify))
            //{
            //    user_identify = OperateContext.Current.bllSession.Bane_User.Select(s => s.user_guid == guid).FirstOrDefault()?.user_identify;
            //    SessionHelper.AddSessionValue(CacheConstant.baneUserID, user_identify);
            //}
            //return user_identify;
        }
    }
}
