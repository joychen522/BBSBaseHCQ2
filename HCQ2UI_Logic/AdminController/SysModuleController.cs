using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  模块控制器
    /// </summary>
    public class SysModuleController: BaseLogic
    {
        //*************************1.0模块管理---权限管理*****************************
        #region 1.0 模块管理配置页面首次进入 + ActionResult ChildModuleList()
        /// <summary>
        ///  1.0 单位代管权限配置页面首次进入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HCQ2_Common.Attributes.Load]
        public ActionResult ChildModuleList()
        {
            return View();
        }
        #endregion

        #region 1.1 保存单位代管--权限设置 + ActionResult SaveChildModule(int id)
        /// <summary>
        ///  1.1 保存单位代管--权限设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveChildModule(int id)
        {
            string menus = RequestHelper.GetStrByName("menuData");
            string reak = RequestHelper.GetStrByName("reak");//是否处理标记
            try
            {
                bool back = operateContext.bllSession.T_ModulePermissRelation.SaveModulePerData(menus, reak, id);
                //清理单位代管权限缓存
                SessionHelper.RemoveSession(HCQ2_Common.Constant.CacheConstant.modulePerminss);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex)
            {
                HCQ2_Common.Log.LogHelper.ErrorLog(typeof(SysLimitController), ex);
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            return null;
        }
        #endregion

        #region 1.2 获取所有模块信息 + ActionResult GetUnitTreeData(int id)
        /// <summary>
        ///  1.2 获取所有模块信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUnitTreeData(int id)
        {
            //1.0 获取所有启用的模块
            List<HCQ2_Model.T_SysModule> list = operateContext.bllSession.T_SysModule.Select(s => !string.IsNullOrEmpty(s.sm_name) && s.if_start == true).ToList();
            //1.1 获取当前权限所有配置的模块信息
            List<HCQ2_Model.T_ModulePermissRelation> listPer = operateContext.bllSession.T_ModulePermissRelation.Select(s => s.per_id == id).ToList();
            //1.1 封装单位键值对数据
            List<Dictionary<string, object>> listModule = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            foreach (var item in list)
            {
                map = new Dictionary<string, object>();
                map.Add("id", item.sm_id);
                map.Add("pId", 0);
                map.Add("name", item.sm_name);
                //3.1当前节点是否被选中
                var temp = listPer.Where(s => s.sm_id == item.sm_id).ToList();
                if (temp.Count > 0) {
                    map.Add("checked", true);
                    map.Add("everstate", "checked");//标记
                }
                else
                    map.Add("everstate", "uncheck");//标记
                listModule.Add(map);
            }
            return Json(listModule, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //*************************2.0系统模块管理************************************
        #region 2.0 模块管理页面首次进入 + ActionResult ModuleList()
        /// <summary>
        ///  模块管理页面首次进入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult ModuleList()
        {
            return View();
        }
        #endregion

        #region 2.1 初始化Table列表 + ActionResult InitModuleTable()
        /// <summary>
        ///  初始化Table列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitModuleTable()
        {
            //用户组名称
            string moduleName = RequestHelper.GetDeStrByName("moduleName");
            int page = Helper.ToInt(Request["page"]),
                 rows = Helper.ToInt(Request["rows"]);
            List<HCQ2_Model.T_SysModule> list = operateContext.bllSession.T_SysModule.GetModuleTableData(moduleName, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_SysModule.SelectCount(s => s.sm_name.Contains(moduleName)),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 2.2 添加模块 + ActionResult AddModule(HCQ2_Model.T_SysModule model)
        /// <summary>
        ///  添加模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddModule(HCQ2_Model.T_SysModule model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                model.create_id = operateContext.Usr.user_id;
                model.create_name = operateContext.Usr.user_name;
                model.create_time = DateTime.Now;
                if(operateContext.bllSession.T_SysModule.Add(model)>0)
                    return operateContext.RedirectAjax(0, "添加成功~", "", "");
                return operateContext.RedirectAjax(1, "添加失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 2.3 编辑模块 + ActionResult EditModule(HCQ2_Model.T_SysModule model)
        /// <summary>
        ///  编辑模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditModule(HCQ2_Model.T_SysModule model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int sm_id = RequestHelper.GetIntByName("sm_id");
            if(sm_id <= 0)
                return operateContext.RedirectAjax(1, "模块主键值为空~", "", "");
            int IsBack = operateContext.bllSession.T_SysModule.Modify(model, s => s.sm_id == sm_id, "sm_name", "sm_code", "sm_image1", "sm_image2", "if_start", "sm_note");
            //清理角色缓存
            SessionHelper.RemoveSession(HCQ2_Common.Constant.CacheConstant.modulePerminss);
            return operateContext.RedirectAjax(0, "编辑权限成功~", "", "");
        } 
        #endregion

        #region 2.4 删除模块 + ActionResult DelModule(int id)
        /// <summary>
        ///  删除模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelModule(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据已删除或者不存在~", "", "");
            HCQ2_Model.T_SysModule model = operateContext.bllSession.T_SysModule.Select(s => s.sm_id == id).FirstOrDefault();
            int delCount = operateContext.bllSession.T_SysModule.Delete(s => s.sm_id == id);
            //1. 删除菜单--模块
            operateContext.bllSession.T_PageFolder.Delete(s => s.sm_code.Equals(model.sm_code));
            //2. 删除组--模块
            operateContext.bllSession.T_UserGroup.Delete(s => s.sm_code.Equals(model.sm_code));
            //3. 删除角色--模块
            operateContext.bllSession.T_Role.Delete(s => s.sm_code.Equals(model.sm_code));
            //4. 删除权限--模块
            operateContext.bllSession.T_Permissions.Delete(s => s.sm_code.Equals(model.sm_code));
            //5. 清理角色缓存
            SessionHelper.RemoveSession(HCQ2_Common.Constant.CacheConstant.modulePerminss);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        } 
        #endregion
    }
}
