using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic.BaseController
{
    /// <summary>
    ///  
    /// </summary>
    public class BasePersonController:BaseLogic
    {
        #region 1.0 人员页面首次 + ActionResult ToPersonList()
        /// <summary>
        ///  1.0 人员页面首次 
        /// </summary>
        /// <returns></returns>
        public ActionResult ToPersonList()
        {
            //查询项目
            //List<T_TableColumn> list = operateContext.bllSession.T_TableColumn.Select(s => s.if_enable == true && s.table_name == "d_person").ToList();
            //ViewBag.ColumnList = list;
            return View();
        } 
        #endregion
    }
}
