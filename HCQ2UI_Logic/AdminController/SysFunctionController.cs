using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Model;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  功能操作控制器
    /// </summary>
    public class SysFunctionController:BaseLogic
    {
        #region 1.0  功能操作首次进去页面跳转 +ActionResult FuncList()
        /// <summary>
        ///  元素管理首次进去页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult FuncList()
        {
            return View();
        }
        #endregion
    }
}
