using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Common.Constant;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  权限管理控制器
    /// </summary>
    public class SysLimitController:BaseLogic
    {
        //*************************1.0权限管理******************************************
        #region 1.0 默认跳转权限管理页面 +ActionResult List()
        /// <summary>
        ///  默认跳转权限管理页面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult List()
        {
            return View();
        }
        #endregion

        #region 1.1 获取权限数据 + ActionResult GetLinitData()
        /// <summary>
        ///  获取权限数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLinitData()
        {
            //权限名称
            string per_name = Helper.ToString(Request["per_name"]),
                sm_code = RequestHelper.GetStrByName("sm_code"),
                perType = RequestHelper.GetStrByName("perType");//权限类别
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            per_name = (!string.IsNullOrEmpty(per_name)) ? HttpUtility.UrlDecode(per_name) : per_name;
            List<T_Permissions> list = operateContext.bllSession.T_Permissions.GetLimitData(per_name, page, rows, perType, sm_code);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_Permissions.SelectCount(s => s.per_name.Contains(per_name)),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 添加权限 + ActionResult AddLimit(T_Permissions permission)
        /// <summary>
        ///  添加权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddLimit(T_Permissions permission)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                bool Isback = base.operateContext.bllSession.T_Permissions.AddLimit(permission);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加权限成功~", "", "");
                return operateContext.RedirectAjax(1, "添加权限失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.3 编辑权限数据 + ActionResult EditLimit(T_Permissions permission)
        /// <summary>
        ///  编辑权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditLimit(T_Permissions permission)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int per_id = HCQ2_Common.Helper.ToInt(Request["per_id"]);
            if (per_id <= 0)
                return operateContext.RedirectAjax(1, "权限主键值为空~", "", "");
            bool IsBack = base.operateContext.bllSession.T_Permissions.EditLimit(permission, per_id);
            //清理权限缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserCachePermiss);
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑权限成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑权限失败~", "", "");
        }
        #endregion

        #region 1.4 删除权限数据 +ActionResult DelUser(int id)
        /// <summary>
        ///  删除权限数据
        /// </summary>
        /// <param name="id">权限id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelLimitById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.T_Permissions.Delete(s => s.per_id == id);
            //1. 删除角色-权限表
            operateContext.bllSession.T_RolePermissRelation.Delete(s => s.per_id == id);
            //2. 删除菜单-权限表
            operateContext.bllSession.T_FolderPermissRelation.Delete(s => s.per_id == id);
            //3. 删除元素-权限表
            operateContext.bllSession.T_ElementPermissRelation.Delete(s => s.per_id == id);
            //4. 清理权限缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserCachePermiss);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        #region 1.5 验证权限是否存在 +public ActionResult CheckLimit()
        /// <summary>
        ///  验证权限是否存在
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckLimit()
        {
            string per_code = Helper.ToString(Request["per_code"]);
            if (string.IsNullOrEmpty(per_code))
                return operateContext.RedirectAjax(1, "权限代码为空~", "", "");
            per_code = (!string.IsNullOrEmpty(per_code)) ? HttpUtility.UrlDecode(per_code) : "";
            int count =
                operateContext.bllSession.T_Permissions.SelectCount(s => s.per_code == per_code);
            if (count > 0)
                return operateContext.RedirectAjax(1, "权限代码已经存在，请重新设置~", "", "");
            return operateContext.RedirectAjax(0, "权限代码可用~", "", "");
        }
        #endregion

        #region 1.6 获取全部权限数据 +ActionResult GetAllLimitData()
        /// <summary>
        ///  获取全部权限数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllLimitData()
        {
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            List<HCQ2_Model.T_Permissions> list =
                operateContext.bllSession.T_Permissions.Select<int>(s => (!string.IsNullOrEmpty(s.per_name)),
                    s => s.per_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_Permissions.SelectCount(null),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
         
        //*************************2.0单位代管---权限管理*****************************
        #region 2.0 单位代管权限配置页面首次进入 + ActionResult UnitLimitList()
        /// <summary>
        ///  2.0 单位代管权限配置页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult UnitLimitList()
        {
            return View();
        }
        #endregion


        //*************************3.0区域代管---权限管理*****************************
        #region 3.0 区域代管权限配置页面首次进入 + ActionResult AreaLimitList()
        /// <summary>
        ///  3.0 区域代管权限配置页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult AreaLimitList()
        {
            return View();
        }
        #endregion

        #region 3.1 保存区域代管--权限设置 + ActionResult SaveAreaLimitData(int id)
        /// <summary>
        ///  3.1 保存区域代管--权限设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAreaLimitData(int id)
        {
            string menus = RequestHelper.GetStrByName("menuData");
            string reak = RequestHelper.GetStrByName("reak");//是否处理标记
            try
            {
                bool back = operateContext.bllSession.T_AreaPermissRelation.SaveAreaPerData(menus, reak, id);
                //清理区域代管权限缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserPerminssAreaData);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex)
            {
                HCQ2_Common.Log.LogHelper.ErrorLog(typeof(SysLimitController), ex);
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 3.2 根据权限ID获取 对应区域信息 + ActionResult GetAreaTreeData(int id)
        /// <summary>
        ///  3.2 根据权限ID获取 对应区域信息
        /// </summary>
        /// <param name="per_id">权限ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAreaTreeData(int id)
        {
            List<Dictionary<string, object>> list = operateContext.bllSession.T_AreaPermissRelation.GetLimitAreaDataById(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
