using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Common.Constant;
using HCQ2_Model;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  元素管理控制器
    /// </summary>
    public class SysElementController:BaseLogic
    {
        #region 1.0 元素管理首次进去页面跳转 +ActionResult ElementList()
        /// <summary>
        ///  元素管理首次进去页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Element]
        [HCQ2_Common.Attributes.Load]
        public ActionResult ElementList()
        {
            return View();
        }
        #endregion

        #region 1.1 初始化表格数据 +ActionResult InitTableData(int id)
        /// <summary>
        ///  初始化表格数据
        /// </summary>
        /// <param name="id">菜单主键ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitTableData()
        {
            int id = Helper.ToInt(Request["menuId"]);
            if (id < 0)
                return null;
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            List<HCQ2_Model.T_PageElement> list = operateContext.bllSession.T_PageElement.GetElementDataByFolderId(id,page,rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_PageElement.SelectCount(s=>s.folder_id==id),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 添加元素 +ActionResult AddElement(T_PageElement el)
        /// <summary>
        ///  添加元素
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddElement(T_PageElement el)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int folder_id = Helper.ToInt(Request["folder_id"]);
            if(folder_id<=0)
                return operateContext.RedirectAjax(1, "菜单数据不完整", "", "");
            el.folder_id = folder_id;
            el.pe_create_time = DateTime.Now;
            el.pe_create_id = operateContext.Usr.user_id;
            try{
                operateContext.bllSession.T_PageElement.Add(el);
                return operateContext.RedirectAjax(0, "添加成功~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.3 编辑元素 +ActionResult EditElement(T_PageElement el)
        /// <summary>
        ///  添加元素
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditElement(T_PageElement el)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int pe_id = Helper.ToInt(Request["pe_id"]);
            if (pe_id <= 0)
                return operateContext.RedirectAjax(1, "元素主键不正确", "", "");
            try {
                operateContext.bllSession.T_PageElement.Modify(el, s => s.pe_id == pe_id, "pe_name", "pe_code", "pe_event", "pe_func", "pe_note");
                SessionHelper.RemoveSession(CacheConstant.allCacheElements);
                //清理元素缓存
                return operateContext.RedirectAjax(0, "编辑成功~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.4 根据Id删除元素 +ActionResult DelElementById(int id)
        /// <summary>
        ///  根据Id删除元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelElementById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.T_PageElement.Delete(s => s.pe_id == id);
            //1. 删除元素-权限表
            operateContext.bllSession.T_ElementPermissRelation.Delete(s => s.per_id == id);
            //2.清理元素缓存
            SessionHelper.RemoveSession(CacheConstant.allCacheElements);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        //****************************元素--权限设置********************************
        #region 2.0 元素--权限设置页面首次跳转 +ActionResult ElLimitList()
        /// <summary>
        ///  元素--权限设置页面首次跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult ElLimitList()
        {
            return View();
        }
        #endregion

        #region 2.1 保存元素--权限设置 +ActionResult SaveElLimitData(FormCollection form, int id)
        /// <summary>
        ///  保存元素--权限设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveElLimitData(FormCollection form, int id)
        {
            string menus = Helper.ToString(form["menuData"]);
            string reak = Helper.ToString(form["reak"]);//是否处理标记
            try
            {
                bool back = operateContext.bllSession.T_ElementPermissRelation.SaveElLimitData(menus, reak, id);
                //清理元素缓存 
                SessionHelper.RemoveSession(CacheConstant.allCacheElements);
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

        #region 2.2 根据权限ID获取菜单项 +ActionResult GetMenuLimitData(int id)
        /// <summary>
        ///  根据权限ID获取元素项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetElLimitData(int id)
        {
            List<HCQ2_Model.T_ElementPermissRelation> list =
                operateContext.bllSession.T_ElementPermissRelation.GetElLimitData(id);
            if (null != list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 2.3 根据权限ID获取元素项 +ActionResult GetElementLimitDataById(int id)
        /// <summary>
        ///  根据权限ID获取元素项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetElementLimitDataById(int id)
        {
            List<T_ElementPermissRelation> list =
                operateContext.bllSession.T_ElementPermissRelation.Select(s => s.per_id == id);
            if (null == list || list.Count==0)
                return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
            return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
        }
        #endregion

        #region 2.4 根据folder_pid获取页面元素项 +ActionResult GetElementDataByFolderPId()
        /// <summary>
        ///  获取su页面元素项
        /// </summary>
        /// <param name="id">folder_pid</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetElementDataByFolderPId(int id)
        {
            string sm_code = RequestHelper.GetStrByName("sm_code");
            if (string.IsNullOrEmpty(sm_code))
                return null;
            List<HCQ2_Model.ExtendsionModel.T_PageElementModel> list =
                operateContext.bllSession.T_PageElement.GetElementByFolderPId(id, sm_code);
            if (null != list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion
    }
}
