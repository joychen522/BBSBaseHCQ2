using HCQ2_Common;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  微信答题 题库维护控制器
    /// </summary>
    public class BaneChatController:BaseLogic
    {
        #region 1.0 首次进入微信答题一栏页面 + ActionResult ScoreList()
        /// <summary>
        ///  1.0 首次进入微信答题一栏页面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult ScoreList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取一栏数据 +ActionResult GetBaneScoreData()
        /// <summary>
        ///  获取一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBaneScoreData()
        {
            string keyword = RequestHelper.GetDeStrByName("keyword");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]);
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            List<Bane_QuestionInfo> list = operateContext.bllSession.Bane_QuestionInfo.GetAllAnswerQuestion(keyword, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_QuestionInfo.SelectCount(s=>s.sub_title.Contains(keyword)),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 添加题库用户 + ActionResult AddScore()
        /// <summary>
        ///  添加用户
        /// </summary>
        /// <param name="user">用户基本对象</param>
        /// <param name="model">戒毒情况对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddScore(Bane_QuestionInfo model)
        {
            string options = RequestHelper.GetStrByName("options_value");
            if(string.IsNullOrEmpty(options))
                return operateContext.RedirectAjax(1, "添加试题失败，选项为空~", "", "");
            int mark = operateContext.bllSession.Bane_QuestionInfo.AddAnswerQuestion(model, options);
            if(mark<=0)
                return operateContext.RedirectAjax(1, "添加试题失败~", "", "");
            return operateContext.RedirectAjax(0, "添加试题成功~", "", "");
        }
        #endregion

        #region 2.2 编辑题库数据 + ActionResult EditScore()
        /// <summary>
        ///  编辑题库数据
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditScore(Bane_QuestionInfo model)
        {
            string options = RequestHelper.GetStrByName("options_value");
            if (string.IsNullOrEmpty(options))
                return operateContext.RedirectAjax(1, "编辑试题失败，选项为空~", "", "");
            int mark = operateContext.bllSession.Bane_QuestionInfo.EditAnswerQuestion(model, options);
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "编辑试题失败~", "", "");
            return operateContext.RedirectAjax(0, "编辑试题成功~", "", "");
        }
        #endregion

        #region 2.3 根据ID获取试题选项 + ActionResult GetQuestionValue()
        /// <summary>
        ///  根据ID获取试题选项
        /// </summary>
        /// <param name="id">试题ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuestionValue(int id)
        {
            if(id<=0)
                return operateContext.RedirectAjax(1, "获取试题选项失败，参数为空~", "", "");
            List<Bane_QuestionValue> list = operateContext.bllSession.Bane_QuestionValue.Select(s => s.sub_id == id).ToList();
            return operateContext.RedirectAjax(0, "成功获取试题选项~", list, "");
        }
        #endregion
    }
}
