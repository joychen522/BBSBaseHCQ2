using HCQ2_Common;
using HCQ2_Model.TreeModel;
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
    /// 表结构控制器 
    /// </summary>
    public class SysTableStructController:BaseLogic
    {
        #region 1.0 首次进入表结构管理页面 + ActionResult TableList()
        /// <summary>
        ///  首次进入表结构管理页面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult TableList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取结构树数据 + ActionResult GetStrcutData()
        /// <summary>
        /// 获取结构树数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetStrcutData()
        {
            List<TableStrcutTreeModel> list = operateContext.bllSession.T_Table.GetTableTreeModel();
            if (list!=null)
                return operateContext.RedirectAjax(0, "", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 1.2 初始化TableList + ActionResult InitTableData()
        /// <summary>
        ///  初始化TableList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitTableData()
        {
            string table_name = Helper.ToString(Request["table_name"]);
            string fieldName = Helper.ToString(Request["fieldName"]);
            if (string.IsNullOrEmpty(table_name))
                return null;
            fieldName = (!string.IsNullOrEmpty(fieldName)) ? System.Web.HttpUtility.UrlDecode(fieldName) : "";
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);

            List<HCQ2_Model.T_TableField> list = operateContext.bllSession.T_Table.GetTableDataByName(table_name,fieldName, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_TableField.SelectCount(s => s.table_name == table_name),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        } 
        #endregion
    }
}
