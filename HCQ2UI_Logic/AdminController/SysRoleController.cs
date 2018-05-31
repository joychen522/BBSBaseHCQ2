using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HCQ2_Common;
using HCQ2_Common.Constant;
using System.Web.Mvc;
using HCQ2_Common.Bean;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic
{
    public class SysRoleController : BaseLogic
    {
        //*****************************角色设置*******************************
        #region 1.0 角色页面初始化跳转 +ActionResult Index()
        /// <summary>
        ///  角色页面初始化跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 1.1 获取角色列表 +ActionResult GetRoleData(T_Role role)
        /// <summary>
        ///  获取角色列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleData(T_Role role)
        {
            string role_name = Helper.ToString(Request["role_name"]),
                sm_code = RequestHelper.GetStrByName("sm_code");
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            role_name = (!string.IsNullOrEmpty(role_name)) ? HttpUtility.UrlDecode(role_name) : role_name;
            List<T_Role> list = operateContext.bllSession.T_Role.GetRoleData(role_name, page, rows, sm_code);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_Role.SelectCount(s => s.role_name.Contains(role_name)),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 编辑角色 +ActionResult EditRoleByObj(T_Role role)
        /// <summary>
        ///  编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRoleByObj(T_Role role,int id)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try {
                //清理角色缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
                if(operateContext.bllSession.T_Role.EditRole(role,id))
                    return operateContext.RedirectAjax(0, "数据更新成功~", "", "");
                return operateContext.RedirectAjax(1, "数据更新失败~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(0, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.2 添加角色 +ActionResult AddRoleByObj(T_Role role)
        /// <summary>
        ///  添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRoleByObj(T_Role role)
        {
            if (!ModelState.IsValid || null == role)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try{
                role.creator_date = DateTime.Now;
                role.creator_id = operateContext.Usr.user_id;
                if (operateContext.bllSession.T_Role.AddRole(role))
                    return operateContext.RedirectAjax(0, "数据更新成功~", "", "");
                return operateContext.RedirectAjax(1, "数据更新失败~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(0, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.3 删除角色 +ActionResult DelRoleById(int id)
        /// <summary>
        ///  删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelRoleById(int id)
        {
            //清理角色缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
            if (operateContext.bllSession.T_Role.DelRole(id))
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        //*****************************角色--权限设置*******************************
        #region 2.0 角色--权限设置页面跳转 +ActionResult RoleLimitList()
        /// <summary>
        ///  角色--权限设置页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult RoleLimitList()
        {
            return View();
        }
        #endregion

        #region 2.1 保存角色--权限设置 +ActionResult SaveRoleLimitData(FormCollection form, int id)
        /// <summary>
        ///  保存角色--权限设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRoleLimitData(FormCollection form, int id)
        {
            string roles = Helper.ToString(form["roleData"]);
            try
            {
                bool back = operateContext.bllSession.T_RolePermissRelation.SaveRoleLimitData(roles, id);
                //清理角色缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
                SessionHelper.RemoveSession(CacheConstant.loginUserCachePermiss);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 2.2 根据角色ID 获取所有权限 +ActionResult GetRoleLimitData(int id)
        /// <summary>
        ///  根据角色ID 获取所有权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleLimitData(int id)
        {
            List<HCQ2_Model.T_RolePermissRelation> list =
                operateContext.bllSession.T_RolePermissRelation.GetRoleLimitData(id);
            if (null != list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        //*****************************角色--用户组设置*******************************
        #region 2.0 角色--权限设置页面跳转 +ActionResult RoleLimitList()
        /// <summary>
        ///  角色--权限设置页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult RoleGroupList()
        {
            return View();
        }
        #endregion

        #region 2.1 保存角色--组设置 +ActionResult SaveRoleGroupData(FormCollection form, int id)
        /// <summary>
        ///  保存角色--组设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRoleGroupData(FormCollection form, int id)
        {
            string groups = Helper.ToString(form["groupData"]);
            try
            {
                bool back = operateContext.bllSession.T_RoleGroupRelation.SaveRoleGroupData(groups, id);
                //清理角色缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 2.2 根据角色ID 获取所有组数据 +ActionResult GetRoleGroupData(int id)
        /// <summary>
        ///  根据角色ID 获取所有组数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleGroupData(int id)
        {
            List<HCQ2_Model.T_RoleGroupRelation> list =
                operateContext.bllSession.T_RoleGroupRelation.GetRoleGroupData(id);
            if (null != list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 2.3 获取全部组数据 +ActionResult GetAllGroupData()
        /// <summary>
        ///  获取全部组数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllGroupData()
        {
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            List<HCQ2_Model.T_UserGroup> list =
                operateContext.bllSession.T_UserGroup.Select<int>(s => (!string.IsNullOrEmpty(s.group_name)),
                    s => s.group_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_UserGroup.SelectCount(null),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
