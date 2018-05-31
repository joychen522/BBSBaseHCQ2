using HCQ2_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HCQ2_Model.TreeModel;
using HCQ2_Model;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  组织机构控制器
    /// </summary>
    public class SysOrgController:BaseLogic
    {
        //*********************************1.组织机构管理************************************
        #region 1.0 组织机构首次进入 + ActionResult OrgList()
        /// <summary>
        ///  1.0 组织机构首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult OrgList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取文档结构树 数据 + ActionResult GetOrgTreeData()
        /// <summary>
        ///  1.1 获取文档结构树 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrgTreeData()
        {
            return Json(operateContext.bllSession.T_OrgFolder.GetOrgTreeData());
        }
        #endregion

        #region 1.1.1 根据权限获取文档结构树 数据 + ActionResult GetOrgTreeDataByRelation()
        /// <summary>
        ///  1.1 根据权限获取文档结构树 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrgTreeDataByRelation()
        {
            return Json(operateContext.bllSession.T_OrgFolder.GetOrgTreeData(operateContext.Usr.user_id));
        }
        #endregion

        #region 1.2 添加节点 + ActionResult AddNode()
        /// <summary>
        ///  1.2 添加节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNode()
        {
            int pId = RequestHelper.GetIntByName("pId");
            string name = RequestHelper.GetDeStrByName("name");
            OrgTreeModel model = new OrgTreeModel() { pId = pId, name = name };
            int folder_id = operateContext.bllSession.T_OrgFolder.AddNode(model);
            if (folder_id > 0)
                return operateContext.RedirectAjax(0, "添加成功~", folder_id, "");
            return operateContext.RedirectAjax(1, "数据添加失败~", "", "");
        }
        #endregion

        #region 1.3 编辑节点 + ActionResult EditNode()
        /// <summary>
        ///  1.3 编辑节点 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditNode()
        {
            int id = RequestHelper.GetIntByName("id");
            string name = RequestHelper.GetDeStrByName("name");
            OrgTreeModel model = new OrgTreeModel() { id = id, name = name };
            int mark = operateContext.bllSession.T_OrgFolder.EditNode(model);
            if (mark > 0)
                return operateContext.RedirectAjax(0, "编辑成功~", "", "");
            return operateContext.RedirectAjax(1, "数据编辑失败~", "", "");
        }
        #endregion

        #region 1.4 删除节点 + ActionResult DeleteNode(int id)
        /// <summary>
        ///  1.4 删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteNode(int id)
        {
            List<T_OrgFolder> list = operateContext.bllSession.T_OrgFolder.Select(s => s.folder_id == id);
            int mark = operateContext.bllSession.T_OrgFolder.DeleteNode(list[0], id);
            if(mark==2)
                return operateContext.RedirectAjax(1, "温馨提示：当前组织机构下有人员数据，请删除人员数据后再删除组织机构！~", "", "");
            if (mark > 0)
                return operateContext.RedirectAjax(0, "删除成功~", "", "");
            return operateContext.RedirectAjax(1, "需要删除的记录不存在或已删除~", "", "");
        }
        #endregion

        #region 1.5 删除节点 + ActionResult RemoveUser(int id)
        /// <summary>
        ///  1.5 从组织机构中移除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveUser()
        {
            int folder_id = RequestHelper.GetIntByName("folder_id"),
                user_id = RequestHelper.GetIntByName("user_id");
            if(folder_id<=0 || user_id<=0)
                return operateContext.RedirectAjax(1, "参数异常移除失败~", "", "");
            int mark = operateContext.bllSession.T_OrgUserRelation.Delete(s => s.folder_id == folder_id && s.user_id == user_id);
            if (mark > 0)
                return operateContext.RedirectAjax(0, "移除成功~", "", "");
            return operateContext.RedirectAjax(1, "移除失败~", "", "");
        }
        #endregion

        #region 1.6 设置制定节点为某节点的子节点 + ActionResult SetFolderPath()
        /// <summary>
        ///  1.6 设置制定节点为某节点的子节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetFolderPath()
        {
            int folder_id = RequestHelper.GetIntByName("folder_id"),//待设置的节点ID
                folder_pid = RequestHelper.GetIntByName("folder_pid");//设置到某节点下的ID
            int mark = operateContext.bllSession.T_OrgFolder.Modify(new T_OrgFolder
            {
                folder_pid = folder_pid
            }, s => s.folder_id == folder_id, "folder_pid");
            if (mark > 0)
                return operateContext.RedirectAjax(0, "移动成功~", "", "");
            return operateContext.RedirectAjax(1, "移动失败~", "", "");
        } 
        #endregion

        //*********************************2.组织机构-用户管理************************************

        #region 2.0 初始化Tabel数据 + ActionResult InitTable(OrgTableParamModel model)
        /// <summary>
        ///  2.0 初始化Tabel数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitTable(OrgTableParamModel model)
        {
            if (model.folder_id == 0)
                return null;
            int total = 0;
            List<T_User> list = operateContext.bllSession.T_OrgFolder.GetTableData(model, out total);
            TableModel tModel = new TableModel()
            {
                total = total,
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 2.1 获取待分配的人员 + ActionResult GetOrgDataByPerson()
        /// <summary>
        ///  5.1 获取分享给人员数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrgDataByPerson(int id)
        {
            //待分配
            List<HCQ2_Model.SelectModel.ListBoxModel> list = operateContext.bllSession.T_OrgFolder.GetOrgDataByPerson();
            //已分配
            List<HCQ2_Model.SelectModel.ListBoxModel> list2 = operateContext.bllSession.T_OrgFolder.GetFineOrgDataByPerson(id);
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("waitObj", list);
            obj.Add("fineObj", list2);
            return operateContext.RedirectAjax(0, "数据获取成功~", obj, "");
        }
        #endregion

        #region 2.2 保存分配的组织机构数据 + ActionResult SaveOrgData()
        /// <summary>
        ///  2.2 保存分配的组织机构数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveOrgData()
        {
            string personData = RequestHelper.GetStrByName("personData");
            int folder_id = RequestHelper.GetIntByName("folder_id");
            if (string.IsNullOrEmpty(personData) || folder_id<=0)
                return operateContext.RedirectAjax(1, "数据不能为空~", "", "");
            bool mark = operateContext.bllSession.T_OrgFolder.SaveOrgDataByPerson(personData, folder_id);
            if (mark)
                return operateContext.RedirectAjax(0, "组织机构设置成功~", "", "");
            return operateContext.RedirectAjax(1, "组织机构设置失败~", "", "");
        }
        #endregion
    }
}
