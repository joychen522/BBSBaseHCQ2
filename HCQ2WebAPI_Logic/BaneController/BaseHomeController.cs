using HCQ2_Common.Constant;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using HCQ2UI_Helper;
using HCQ2WebAPI_Logic.BaseAPIController;
using System.Collections.Generic;
using System.Web.Http;
using HCQ2UI_Helper.Session;
using System.Linq;
using HCQ2_Model;
using System;

namespace HCQ2WebAPI_Logic.BaneController
{
    /// <summary>
    ///  禁毒APP 首页控制器
    /// </summary>
    public class BaseHomeController: BaneApiLogic
    {
        #region ✔1.0 首页答题情况 + object BaneReg(BaneRegModel bane)
        /// <summary>
        ///  首页答题情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetAnswer(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            BaneHomeAnswerModel baneResult = operateContext.bllSession.Bane_User.GetAnswerDAL(SysPermissSession.ChangeIdByGuid(bane.userid));
            if(baneResult==null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "系统没有查询到答题记录~", "");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "成功获取数据~", baneResult);
        }
        #endregion

        #region ✔1.1 答题历史详情 + object GetAnswerDetial(AnswerModel bane)
        /// <summary>
        ///  答题历史详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetAnswerDetial(AnswerModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            AnswerResultModel baneResult = operateContext.bllSession.Bane_HistoryScore.GetAnswerHistoryDetial(bane.answer_id);
            if(baneResult==null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "获取数据失败~", "");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "成功获取数据~", baneResult);
        }
        #endregion

        #region ✔1.2 查看历史答题记录列表 + object GetAnswerHistoryDetial(AnswerModel bane)
        /// <summary>
        ///  查看历史答题记录列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetAnswerHistoryDetial(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            List<AnswerHistoryResultModel> baneResult = operateContext.bllSession.Bane_HistoryScore.GetAnswerHistoryList(SysPermissSession.ChangeIdByGuid(bane.userid));
            if(null== baneResult)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "数据获取失败", "");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功", baneResult);
        }
        #endregion

        #region ✔1.3 我的积分 + object GetAnswerIntegral(AnswerModel bane)
        /// <summary>
        ///  我的积分
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetAnswerIntegral(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            AnswerIntegralModel baneResult = operateContext.bllSession.Bane_IntegralScoreDetial.GetIntegralScoreById(SysPermissSession.ChangeIdByGuid(bane.userid));
            if(baneResult==null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "数据获取失败~", "");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功~", baneResult);
        }
        #endregion

        #region ✔1.4 我的消息数量 + object GetMyMessageNum(AnswerModel bane)
        /// <summary>
        ///  我的消息数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetMyMessageNum(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_guid == bane.userid).FirstOrDefault();
            //1. 在线答题，只有管控人员才需要提醒答题
            int baneResult = 0;
            //属于管控期的人员才统计
            if (user.control_date != null && user.control_date >= DateTime.Now)
                baneResult = operateContext.bllSession.Bane_HistoryScore.GetAnswerNumByID(user.user_identify);
            //2. 定期检测 统计数量 -6天
            baneResult += operateContext.bllSession.Bane_User.GetDetectionNumByID(bane.userid);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功", baneResult);
        }
        #endregion

        #region ✔1.5 获取我的消息记录 + object GetMyMessage(BaseBaneModel bane)
        /// <summary>
        ///  获取我的消息记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetMyMessage(BaseBaneModel bane)
        {
            //1：定期检测提前3天提醒，超期还未检测则一直提醒，直到检测
            //2：（针对管控人员）每周答题提醒，从周4开始没做题则开始提醒，直到答题并合格
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            List<BaneMyMessageModel> baneResult = new List<BaneMyMessageModel>();
            //1.在线答题提醒
            BaneMyMessageModel model = operateContext.bllSession.Bane_HistoryScore.GetAnswerContentByID(SysPermissSession.ChangeIdByGuid(bane.userid));
            if (model != null)
                baneResult.Add(model);
            //2.定期检测提醒
            model = operateContext.bllSession.Bane_User.GetDetectionContentByID(bane.userid);
            if (model != null)
                baneResult.Add(model);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功", baneResult);
        }
        #endregion

        #region ✔1.6 个人状态、本次、下次、管控 (日期)+ object GetBaneDetection(BaseBaneModel bane)
        /// <summary>
        ///  个人状态、本次、下次、管控检测日期
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetBaneDetection(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败~", null);
            //返回数据
            BaneDetectionModel baneResult = operateContext.bllSession.Bane_User.GetBaneDetectionByID(SysPermissSession.ChangeIdByGuid(bane.userid));
            if(baneResult==null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "数据获取失败~", null);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功~", baneResult);
        }
        #endregion


        //****************************2.新闻、政策********************************
        #region ✔2.0 首页焦点新闻图片 + object GetSpotNews(BaseBaneModel bane)
        /// <summary>
        ///  首页焦点新闻图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetSpotNews(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败~", null);
            //返回数据
            List<T_MessageNotice> messNews = operateContext.bllSession.T_MessageNotice.Select(s => s.m_type == "焦点新闻", s => s.create_date, false);
            if(messNews==null || messNews.Count<=0)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "无焦点新闻~", "");
            List<BaneSpotNewsModel> baneResult = new List<BaneSpotNewsModel>();
            foreach (var item in messNews)
                baneResult.Add(new BaneSpotNewsModel { news_title = item.m_title, news_url = item.focus_imgage, m_id = item.m_id });
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功~", baneResult);
        }
        #endregion

        #region ✔2.1 新闻、政策 + object GetNewsAndPolicy(BaneNewsParam bane)
        /// <summary>
        ///  新闻、政策
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetNewsAndPolicy(BaneNewsParam bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败~", null);
            string m_type = string.Empty;
            if (bane.m_type == 1)
                m_type = "新闻";
            else if (bane.m_type == 2)
                m_type = "政策";
            List<T_MessageNotice> messNews;
            //返回数据
            if (!string.IsNullOrEmpty(m_type))
                messNews = operateContext.bllSession.T_MessageNotice.Select(s => (!string.IsNullOrEmpty(s.focus_imgage)) && s.m_type == m_type, s => s.create_date, false);
            else
                messNews = operateContext.bllSession.T_MessageNotice.Select(s => (!string.IsNullOrEmpty(s.focus_imgage)), s => s.create_date, false);
            if (messNews == null || messNews.Count <= 0)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "无新闻政策~", "");
            List<NewsAndPolicyModel> baneResult = new List<NewsAndPolicyModel>();
            foreach (var item in messNews)
                baneResult.Add(new NewsAndPolicyModel { m_id = item.m_id, news_title = item.m_title, browse_num = item.browse_num, issue_date = (item.create_date != null) ? ((DateTime)item.create_date).ToString("D") : DateTime.Now.ToString("D"), img_url = item.focus_imgage });
             return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功~", baneResult);
        }
        #endregion

        #region ✔2.2 新闻、政策列表 + object GetNewsAndPolicyList(BaneNewsParam bane)
        /// <summary>
        ///  普通新闻、政策列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetNewsAndPolicyList(BaneNewsParam bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败", null);
            string m_type = string.Empty;
            if (bane.m_type == 1)
                m_type = "新闻";
            else if (bane.m_type == 2)
                m_type = "政策";
            //返回数据
            List<T_MessageNotice> messNews;
            if (!string.IsNullOrEmpty(m_type))
                messNews = operateContext.bllSession.T_MessageNotice.Select(s => (!string.IsNullOrEmpty(s.m_title)) && s.m_type==m_type, s => s.create_date, false);
            else
                messNews = operateContext.bllSession.T_MessageNotice.Select(s => (!string.IsNullOrEmpty(s.m_title)), s => s.create_date, false);
            if (messNews == null || messNews.Count <= 0)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "无新闻政策~", "");
            List<NewsAndPolicyList> baneResult = new List<NewsAndPolicyList>();
            foreach (var item in messNews)
                baneResult.Add(new NewsAndPolicyList { m_id = item.m_id, news_title = item.m_title, browse_num = item.browse_num, issue_date = (item.create_date != null) ? ((DateTime)item.create_date).ToString("D") : DateTime.Now.ToString("D"), messList_url = item.focus_imgage, m_type = item.m_type });//item.messList_imgage
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功", baneResult);
        }
        #endregion

        #region ✔2.3 新闻、政策 详细内容 + object GetNewsAndPolicyDetail(NewsAndPolicyParams bane)
        /// <summary>
        ///  请求新闻、政策 详细内容
        ///  被请求的新闻，政策浏览次数+1
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetNewsAndPolicyDetail(NewsAndPolicyParams bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败", null);
            T_MessageNotice news = operateContext.bllSession.T_MessageNotice.Select(s => s.m_id == bane.m_id).FirstOrDefault();
            if (news == null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "查无此新闻政策~", "");
            //1.数据
            NewsAndPolicyDetialModel baneResult = new NewsAndPolicyDetialModel
            {
                m_type = news.m_type,
                messDetail_url = news.focus_imgage,
                m_content = news.m_content,
                m_id = news.m_id,
                news_title = news.m_title,
                browse_num = news.browse_num,
                issue_date = (news.create_date != null) ? ((DateTime)news.create_date).ToString("D") : DateTime.Now.ToString("D")
            };
            //2.请求的新闻，政策浏览次数+1
            news.browse_num = news.browse_num + 1;
            operateContext.bllSession.T_MessageNotice.Modify(news, s => s.m_id == bane.m_id, "browse_num");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "数据获取成功", baneResult);
        }
        #endregion
    }
}
