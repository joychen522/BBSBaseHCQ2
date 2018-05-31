using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Common.Constant;
using HCQ2_Model;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  用户组控制器管理
    /// </summary>
    public class SysUserGroupController:BaseLogic
    {
        //*******************************用户组设置************************************
        #region 1.0 用户组首次进入页面跳转 +ActionResult UserGroupList()
        /// <summary>
        ///  用户组首次进入页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult UserGroupList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取用户组数据 + ActionResult GetUserGroupData()
        /// <summary>
        ///  获取用户组数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserGroupData()
        {
            //用户组名称
            string group_name = Helper.ToString(Request["group_name"]),
               sm_code = RequestHelper.GetStrByName("sm_code");
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            group_name = (!string.IsNullOrEmpty(group_name)) ? HttpUtility.UrlDecode(group_name) : group_name;
            List<HCQ2_Model.T_UserGroup> list = operateContext.bllSession.T_UserGroup.GetUserGroupData(group_name,page,rows, sm_code);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_UserGroup.SelectCount(s => s.group_name.Contains(group_name)),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 添加用户组 + ActionResult AddGroup(T_UserGroup userGroup)
        /// <summary>
        ///  添加用户组
        /// </summary>
        /// <param name="permission">用户组对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddGroup(T_UserGroup userGroup)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                bool Isback = base.operateContext.bllSession.T_UserGroup.AddGroup(userGroup);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加成功~", "", "");
                return operateContext.RedirectAjax(1, "添加失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.3 编辑用户组数据 + ActionResult EditGroup(T_UserGroup userGroup)
        /// <summary>
        ///  编辑用户组
        /// </summary>
        /// <param name="permission">用户组对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditGroup(T_UserGroup userGroup)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int group_id = HCQ2_Common.Helper.ToInt(Request["group_id"]);
            if (group_id <= 0)
                return operateContext.RedirectAjax(1, "权限主键值为空~", "", "");
            bool IsBack = base.operateContext.bllSession.T_UserGroup.EditGroup(userGroup, group_id);
            //清理角色缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑权限成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑权限失败~", "", "");
        }
        #endregion

        #region 1.4 删除权限数据 +ActionResult DelGroupById(int id)
        /// <summary>
        ///  删除权限数据
        /// </summary>
        /// <param name="id">权限id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelGroupById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.T_UserGroup.Delete(s => s.group_id == id);
            //1. 删除用户组-用户表
            operateContext.bllSession.T_UserGroupRelation.Delete(s => s.group_id == id);
            //2. 删除用户组-角色表
            operateContext.bllSession.T_RoleGroupRelation.Delete(s => s.group_id == id);
            //3. 清理角色缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        #region 1.5 获取全部用户组数据 +ActionResult GetAllGroupData()
        /// <summary>
        ///  获取全部用户组数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllGroupData()
        {
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            List<HCQ2_Model.T_UserGroup> list =
                operateContext.bllSession.T_UserGroup.Select<int>(s => (!string.IsNullOrEmpty(s.group_name)),
                    s => s.group_order, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_Permissions.SelectCount(null),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //*******************************用户组--用户一览************************************
        #region 2.0  用户组--用户一览页面首次进入跳转 +ActionResult UserAndGroupList()
        /// <summary>
        ///  用户组--用户一览页面首次进入跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult UserAndGroupList()
        {
            return View();
        }
        #endregion

        #region 2.1 获取用户组--用户对应数据 +ActionResult GetUserAndGroupData()
        /// <summary>
        ///  获取用户组--用户对应数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserAndGroupData()
        {
            //用户组名称
            string user_name = Helper.ToString(Request["userName"]);
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            user_name = (!string.IsNullOrEmpty(user_name)) ? HttpUtility.UrlDecode(user_name) : user_name;
            List<HCQ2_Model.T_User> list = operateContext.bllSession.T_UserGroupRelation.GetUserAndGroupData(user_name, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_UserGroupRelation.GetUserAndGroupCount(user_name),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        //*******************************用户组--角色一览************************************
        #region 3.0 用户组--角色一栏 页面首次进入跳转 + ActionResult RoleAndGroupList()
        /// <summary>
        ///  用户组--角色一栏 页面首次进入跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Skip]
        public ActionResult RoleAndGroupList()
        {
            return View();
        }
        #endregion

        #region 3.1 获取用户组--角色对应数据 +ActionResult GetRoleAndGroupData()
        /// <summary>
        ///  获取用户组--角色对应数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleAndGroupData()
        {
            //用户组名称
            string role_name = Helper.ToString(Request["roleName"]);
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            role_name = (!string.IsNullOrEmpty(role_name)) ? HttpUtility.UrlDecode(role_name) : role_name;
            List<HCQ2_Model.T_Role> list = operateContext.bllSession.T_RoleGroupRelation.GetRoleAndGroupData(role_name, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_RoleGroupRelation.GetRoleAndGroupCount(role_name),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
