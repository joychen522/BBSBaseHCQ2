using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  首页控制器
    /// </summary>
    public class IndexController : BaseLogic
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //待办事宜
            ViewBag.DBSY = operateContext.bllSession.T_TodoList.SendTodo(operateContext.Usr.user_id);
            //待办事宜禁毒提醒
            //2.0 即将过检
            ViewBag.UpComming = operateContext.bllSession.Bane_User.GetWeekCountPerson(operateContext.Usr.user_id);

            //政策信息
            ViewBag.BMQ = operateContext.bllSession.BMQ_Document.GetDocumentSortName(
                operateContext.bllSession.BMQ_Document.GetDocumentInfo());

            //新闻公告
            ViewBag.Mess = operateContext.bllSession.T_MessageNotice.Select(s => s.m_type.Contains("新闻"), s => s.m_id, 1, 4, true); 
            //政策
            ViewBag.Polity= operateContext.bllSession.T_MessageNotice.Select(s => s.m_type.Contains("政策"), s => s.m_id, 1, 4, true);
            //operateContext.bllSession.T_MessageNotice.GetAllMess();
            //ViewBag.Mess = messList.Take(4).ToList();

            //社区（乡）个数  根据权限分配的
            ViewBag.Villages = operateContext.bllSession.Bane_User.GetBBSCount(operateContext.Usr.user_id);
            //总人数
            ViewBag.Count = operateContext.bllSession.Bane_User.AllPersonCount(operateContext.Usr.user_id);
            //本月应检人数
            ViewBag.ShouldCount = operateContext.bllSession.Bane_User.GetCountByMonth(operateContext.Usr.user_id);
            //已检人数
            ViewBag.FinishCount = operateContext.bllSession.Bane_UrinalysisRecord.GetDetectionCount(operateContext.Usr.user_id);
            //过检人数
            ViewBag.PastCount = operateContext.bllSession.Bane_User.PassCountPerson(operateContext.Usr.user_id);
            return View("List");
        }

        #region 政策新闻

        [HCQ2_Common.Attributes.Load]
        public ActionResult DocumentDetail(string documentID)
        {
            ViewBag.Title = "政策详细信息";
            ViewBag.Document = operateContext.bllSession.BMQ_Document.GetByDocumentID(documentID);
            return View("DocumentDetail");
        }

        /// <summary>
        /// 获取政策对象
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public ActionResult GetDocument(string documentID)
        {
            var data = operateContext.bllSession.BMQ_Document.GetByDocumentID(documentID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }

    public class StaticWorkDay
    {
        /// <summary>
        /// 总人数
        /// </summary>
        public int totalPerson { get; set; }
        /// <summary>
        /// 打卡人数
        /// </summary>
        public int workPerson { get; set; }
        /// <summary>
        /// 打卡率
        /// </summary>
        public double pepe { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string unitName { get; set; }
    }
}
