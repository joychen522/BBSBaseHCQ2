using HCQ2_Common.Constant;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using HCQ2UI_Helper;
using HCQ2UI_Helper.Session;
using HCQ2WebAPI_Logic.BaseAPIController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HCQ2WebAPI_Logic.BaneController
{
    public class BaneAnswerController: BaneApiLogic
    {
        //****************************1.答题********************************
        #region ✔1.0 随机抽取10答题 + object GetRandomAnswer(BaneRegModel bane)
        /// <summary>
        ///  随机抽取10答题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetRandomAnswer(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            int howLen = 10;//随机几条试题
            List<BaneTopicModel> baneResult = operateContext.bllSession.Bane_QuestionInfo.GetAnswerQuestion(howLen);
            if (baneResult == null || baneResult.Count < howLen)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "试题获取失败，题库没有试题或不足" + howLen + "条", baneResult);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "成功获取试题", baneResult);
        }
        #endregion

        #region ✔1.1 提交试题答案 + object SubRandomAnswer(BaneRegModel bane)
        /// <summary>
        ///  提交试题答案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object SubRandomAnswer(SubmitAnswerModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //返回数据
            int core;
            int score = operateContext.bllSession.Bane_QuestionInfo.CheckAnswer(bane.options, SysPermissSession.ChangeIdByGuid(bane.userid),out core);
            //更新答题次数
            HCQ2_Model.Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_guid == bane.userid).FirstOrDefault();
            int num = user.the_num + 1, tcore = user.user_total + core;
            operateContext.bllSession.Bane_User.Modify(new HCQ2_Model.Bane_User { the_num= num, user_total = tcore }, s => s.user_guid == bane.userid, "the_num", "the_score");
            string title = (score > 89) ? "恭喜您通过考核，每周只有第一次合格才能获得积分~" : "很遗憾您未通过考核~";
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, title, score);
        }
        #endregion  

        //#region 1.2 答题须知 + object GetAnswerHelp()
        ///// <summary>
        /////  提交试题答案
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public object GetAnswerHelp()
        //{
        //    AnswerHelpModel model = new AnswerHelpModel();
        //    return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "", model);
        //}
        //#endregion  
    }
}
