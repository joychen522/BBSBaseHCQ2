using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Common.Constant;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  菜单目录管理控制器
    /// </summary>
    public class SysMenuController:BaseLogic
    {
        //******************************1.菜单管理***************************************
        #region 1.0 目录树菜单首次进入 +ActionResult MenuList()
        /// <summary>
        ///  目录树菜单首次进入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult MenuList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取子节点 +ActionResult GetMenuChildsByParentID()
        /// <summary>
        ///  获取子节点 菜单管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuChildsByParentID()
        {
            int id = Helper.ToInt(Request["pid"]);
            string type = Helper.ToString(Request["type"]),
                 sm_code = RequestHelper.GetStrByName("sm_code");
            List<HCQ2_Model.TreeModel.TreeTableAttribute> list =
                operateContext.bllSession.T_PageFolder.GetMenuDataByPid(id,type, sm_code);
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("nodes", list);
            return Json(map);
        }
        #endregion

        #region 1.1 权限设置获取子节点 +ActionResult GetSysMenuChildsByParentID()
        /// <summary>
        ///  权限设置获取子节点 菜单管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSysMenuChildsByParentID()
        {
            int id = Helper.ToInt(Request["pid"]);
            string type = Helper.ToString(Request["type"]),
                 sm_code = RequestHelper.GetStrByName("sm_code");
            List<HCQ2_Model.TreeModel.TreeTableAttribute> list =
                operateContext.bllSession.T_PageFolder.GetMenuDataByPid(id, type, sm_code,true);
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("nodes", list);
            return Json(map);
        }
        #endregion

        #region 1.3 编辑菜单 +ActionResult EditMenu(HCQ2_Model.T_PageFolder pageFolder, int id)
        /// <summary>
        ///  编辑菜单
        /// </summary>
        /// <param name="pageFolder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditMenu(HCQ2_Model.T_PageFolder pageFolder, int id)
        { 
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            bool IsBack = operateContext.bllSession.T_PageFolder.EditPageFolder(pageFolder, id);
            //清理菜单缓存
            //CacheHelper.RemoveCache(CacheConstant.loginUserCacheMenus + operateContext.Usr.user_id);
            if (IsBack){
                pageFolder.folder_id = id;
                return operateContext.RedirectAjax(0, "编辑信息成功~", pageFolder, "");
            }
            return operateContext.RedirectAjax(1, "编辑信息失败~", "", "");
        }
        #endregion

        #region 1.4 新增菜单 + ActionResult AddMenu(HCQ2_Model.T_PageFolder pageFolder)
        /// <summary>
        ///  新增菜单
        /// </summary>
        /// <param name="pageFolder"></param>
        /// <returns></returns>
        public ActionResult AddMenu(HCQ2_Model.T_PageFolder pageFolder)
        {
            int folder_pid = Helper.ToInt(Request["pid"]);
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int addID = operateContext.bllSession.T_PageFolder.AddFolder(pageFolder,folder_pid);
            if (addID>0)
                return operateContext.RedirectAjax(0, "保存信息成功~", addID, "");
            return operateContext.RedirectAjax(1, "保存信息失败~", "", "");
        }
        #endregion

        #region 1.5 删除菜单 +ActionResult DeleteMenu(int id)
        /// <summary>
        ///  删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMenuById(int id)
        {
            string name = HCQ2_Common.Helper.ToString(Request["name"]);
            if(string.IsNullOrEmpty(name))
                return operateContext.RedirectAjax(1, "需要删除的菜单为空~", "", "");
            name = HttpUtility.UrlDecode(name);
            if (name.Equals("系统管理"))
                return operateContext.RedirectAjax(1, "系统管理菜单不允许被删除~", "", "");
            bool count = operateContext.bllSession.T_PageFolder.DelFolder(id);
            //1. 删除菜单-权限表
            operateContext.bllSession.T_PageElement.Delete(s => s.folder_id == id);
            operateContext.bllSession.T_FolderPermissRelation.Delete(s => s.folder_id == id);
            //2. 清理菜单缓存
            //CacheHelper.RemoveCache(CacheConstant.loginUserCacheMenus + operateContext.Usr.user_id);
            if (count)
                return operateContext.RedirectAjax(0, "删除信息成功~", "", "");
            return operateContext.RedirectAjax(1, "删除信息失败~", "", "");
        }
        #endregion

        #region 1.6 根据ID排序 +ActionResult OrderMenuById(int id)
        /// <summary>
        ///  根据ID排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OrderMenuById(int id)
        {
            string type = HCQ2_Common.Helper.ToString(Request["type"]);
            int newId = operateContext.bllSession.T_PageFolder.OrderMenuById(id, type);
            if(newId>0)
                return operateContext.RedirectAjax(0, "成功排序~", newId, "");
            return operateContext.RedirectAjax(1, "排序失败~", "", "");
        }
        #endregion

        #region 1.7 首页获取菜单数据 +ActionResult GetMenuData()
        /// <summary>
        ///  首页获取菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuData()
        {
            string sm_code = RequestHelper.GetStrByName("sm_code");
            List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> list =
                operateContext.bllSession.T_PageFolder.GetMainMenu(sm_code);
            if (list != null)
                return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 1.7.1 系统管理元素管理 +ActionResult GetSysMenuData()
        /// <summary>
        ///  首页获取菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSysMenuData()
        {
            string sm_code = RequestHelper.GetStrByName("sm_code");
            List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> list =
                operateContext.bllSession.T_PageFolder.GetMainMenu(sm_code,true);
            if (list != null)
                return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 1.7.2 系统管理元素管理 +ActionResult GetSysMenuAllData()
        /// <summary>
        ///  系统管理元素管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSysMenuAllData()
        {
            string sm_code = RequestHelper.GetStrByName("sm_code");
            List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> list =
                operateContext.bllSession.T_PageFolder.GetMainMenu(sm_code, true,false);
            if (list != null)
                return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 1.7 首页获取菜单数据 +ActionResult GetIndexMenuData()
        /// <summary>
        ///  首页获取菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetIndexMenuData()
        {
            List<HCQ2_Model.T_SysModule> mList = HCQ2UI_Helper.Session.SysPermissSession.SysModule;
            if (null == mList || mList.Count <= 0)
                return null;
            List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> list =
                operateContext.bllSession.T_PageFolder.GetMainMenu(MainDateConstant.mainPageType);
            if (null == list || list.Count <= 0)
                return null;
            List<HCQ2_Model.ExtendsionModel.T_PageFolderModel> oList = new List<HCQ2_Model.ExtendsionModel.T_PageFolderModel>();
            if(mList.Count==1)
                return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
            foreach (var item in mList)
            {
                oList.Add(new HCQ2_Model.ExtendsionModel.T_PageFolderModel {
                    text=item.sm_name,
                    have_child=true,
                    folder_image=item.sm_image1,
                    sm_code=item.sm_code,
                    nodes = list.Where(s => s.sm_code.Equals(item.sm_code)).ToList() });
            }
            if (oList != null)
                return operateContext.RedirectAjax(0, "数据获取成功~", oList, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 2.0 菜单图标选择 + ActionResult SelectMenuIcon()
        /// <summary>
        ///  菜单图标选择
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectMenuIcon()
        {
            return View();
        }
        #endregion

        #region 2.1 根据ID获取目录树信息 +ActionResult GetMenuDataById(int id)
        /// <summary>
        ///  根据ID获取目录树信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuDataById(int id)
        {
            HCQ2_Model.T_PageFolder page =
                operateContext.bllSession.T_PageFolder.Select(s => s.folder_id == id).FirstOrDefault();
            if (page != null)
                return operateContext.RedirectAjax(0, "获取成功", page, "");
            return operateContext.RedirectAjax(1, "获取失败", "", "");
        }
        #endregion

        //******************************2.菜单--权限设置***************************************
        #region 3.0 菜单权限关联首次进入跳转 +ActionResult MenuLimitList()
        /// <summary>
        ///  菜单权限关联首次进入跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult MenuLimitList()
        {
            return View();
        }
        #endregion

        #region 3.1 保存菜单--权限设置数据 +ActionResult SaveMenuLimitData()
        /// <summary>
        ///  保存菜单--权限设置数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveMenuLimitData(FormCollection form,int id)
        {
            string menus = Helper.ToString(form["menuData"]);
            string reak = Helper.ToString(form["reak"]);//是否处理标记
            //清理菜单缓存
            //CacheHelper.RemoveCache(CacheConstant.loginUserCacheMenus + operateContext.Usr.user_id);
            try
            {
                bool back = operateContext.bllSession.T_FolderPermissRelation.SaveMenuLimitData(menus,reak, id);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 3.2 根据权限ID获取菜单项 +ActionResult GetMenuLimitData(int id)
        /// <summary>
        ///  根据权限ID获取菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuLimitData(int id)
        {
            List<HCQ2_Model.T_FolderPermissRelation> list =
                operateContext.bllSession.T_FolderPermissRelation.GetMenuLimitData(id);
            if(null!=list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        } 
        #endregion
    }
}
