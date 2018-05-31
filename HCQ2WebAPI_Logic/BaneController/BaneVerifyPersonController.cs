using HCQ2WebAPI_Logic.BaseAPIController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2UI_Helper;
using HCQ2_Common.Constant;
using System.Web.Http;
using HCQ2_Model;
using HCQ2_Model.WebApiModel.ParamModel;

namespace HCQ2WebAPI_Logic.BaneController
{
    /// <summary>
    ///  验证戒毒人员是否属于本系统
    /// </summary>
    public class BaneVerifyPersonController: BaseApiLogic
    {
        #region 1.0 验证戒毒人员是否属于本系统 + object VerifyPerson()
        /// <summary>
        ///  1.0 验证戒毒人员是否属于本系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object VerifyPerson(VerifyModel model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectWebApi(WebResultCode.Exception, GlobalConstant.参数异常.ToString(), null);
            Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_identify == model.user_identify).FirstOrDefault();
            if (null == user)
                return operateContext.RedirectWebApi(WebResultCode.Error, "系统不存在此身份证的用户，请核对后再试！", null);
            //1：验证当前人员是否到达检测时间：检测时间为：下一次时间的前7天内
            int days = (user.ur_next_date - DateTime.Now).Days;//下一次检测时间，当前时间（时间差天数）
            //1.1 判断是否达到检测日期
            if (days>7)
                return operateContext.RedirectWebApi(WebResultCode.Error, "当前人员还未到达检测日期，请："+(days-7).ToString()+"天后再检测！", null);
            //2：验证成功自动生成尿检记录
            operateContext.bllSession.Bane_UrinalysisRecord.AutoAddUrinalysisRecordUser(model.user_identify);
            //3：更新下次尿检时间
            Bane_UrinalysisTimeSet set = operateContext.bllSession.Bane_UrinalysisTimeSet.Select(s => s.user_type.Equals(user.user_type), o => o.gap_month, true).FirstOrDefault();
            int addMonth = 1,//间隔检测月数
                gapMonth;
            if (set != null)
                addMonth = set.gap_month;
            gapMonth = (addMonth - 1) * 30 + 24;
            if (days < gapMonth)
                user.ur_next_date = user.ur_next_date.AddMonths(addMonth);
            else if(days == gapMonth)
                user.ur_next_date = user.ur_next_date.AddMonths(addMonth*2);
            else if(days > gapMonth)
            {
                int tempMonth = (days / gapMonth) * addMonth + (days % gapMonth) > 0 ? addMonth : 0;
                user.ur_next_date = user.ur_next_date.AddMonths(tempMonth);
            }
            operateContext.bllSession.Bane_User.Modify(user, s => s.user_identify == model.user_identify, "ur_next_date");
            return operateContext.RedirectWebApi(WebResultCode.Ok, "验证成功流程任务已启动", null);
        }
        #endregion

        #region 1.1 人员数据同步 + object PersonsSynchronous(BaseModel wage)
        /// <summary>
        ///  1.1 人员数据同步
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object PersonsSynchronous(PersonSysn person)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectWebApi(WebResultCode.Exception, GlobalConstant.参数异常.ToString(), null);
            List<PersonCL> userList = operateContext.bllSession.Bane_User.PersonsSynchronous(person);
            if (null == userList || userList.Count <= 0)
                return operateContext.RedirectWebApi(WebResultCode.Error, "没有需要同步的数据", null);
            return operateContext.RedirectWebApi(WebResultCode.Ok, GlobalConstant.操作成功.ToString(), userList);
        }
        #endregion

        #region 1.2 人员信息和虹膜下发 + object PersonsSentDown(BaseModel wage)

        /// <summary>
        ///  1.2 人员信息和虹膜下发
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object PersonsSentDown(BaseModel model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectWebApi(WebResultCode.Exception, GlobalConstant.参数异常.ToString(), null);
            List<PersonCL> userList = operateContext.bllSession.Bane_User.GetPersonsSentDownData(model.userid);
            if(null== userList || userList.Count<=0)
                return operateContext.RedirectWebApi(WebResultCode.Exception, GlobalConstant.数据获取失败.ToString(), null);
            return operateContext.RedirectWebApi(WebResultCode.Ok, GlobalConstant.操作成功.ToString(), userList); 
        }

        #endregion
    }
}
