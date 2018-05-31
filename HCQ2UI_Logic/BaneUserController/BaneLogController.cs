using HCQ2_Common;
using HCQ2_Model.BaneUser;
using HCQ2_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  禁毒日志控制器
    /// </summary>
    public class BaneLogController:BaseLogic
    {
        #region 1.0 禁毒日志首次进入 + ActionResult ProUserList()
        /// <summary>
        ///  禁毒日志首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult LogList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取禁毒日志一栏数据 +ActionResult GetBaneUserData()
        /// <summary>
        ///  获取禁毒日志一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBaneLogData()
        {
            string log_title = RequestHelper.GetDeStrByName("log_title"),//标题
                    log_date_start = RequestHelper.GetStrByName("log_date_start"),//操作开始日期
                    log_date_end = RequestHelper.GetStrByName("log_date_end");//操作结束日期
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]),
               user_id = RequestHelper.GetIntByName("user_id");
            BaneLogParam param = new BaneLogParam(page, rows, user_id, log_title, log_date_start, log_date_end);
            List<HCQ2_Model.Bane_LogDetail> bane = operateContext.bllSession.Bane_LogDetail.GetLogDataByParams(param);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_LogDetail.GetLogDataCount(param),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 获取用户字典 + ActionResult GetUserDict()
        /// <summary>
        ///  1.2 获取用户字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserDict()
        {
            var query = operateContext.bllSession.T_User.Select(s => !string.IsNullOrEmpty(s.user_name)).Select(s => new { s.user_name, s.user_id }).ToList();
            return operateContext.RedirectAjax(0, "数据获取成功~", query, "");
        } 
        #endregion
    }
}
