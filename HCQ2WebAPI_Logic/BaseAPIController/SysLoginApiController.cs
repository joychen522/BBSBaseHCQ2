using System.Collections.Generic;
using System.Web.Http;
using HCQ2UI_Helper;
using HCQ2_Common.Constant;
using HCQ2_Model.ViewModel;
using HCQ2_Model.WebApiModel.ParamModel;
using HCQ2_Model.APPModel.ResultApiModel;

namespace HCQ2WebAPI_Logic.BaseAPIController
{
    /// <summary>
    ///  Web Api登录控制器
    /// </summary>
    [HCQ2_Common.Attributes.SkipApi]
    public class SysLoginApiController: BaseWeiXinApiLogic
    {
        /// <summary>
        ///  登录服务 只返回userid
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object BbsUserLogin(SysLoginModel model)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            LoginUser user = new LoginUser()
            { LoginName = model.user_name, UserPwd = model.user_password };
            LoginResultModel rModel = operateContext.Login(user, false);
            if (rModel.Status)
            {
                BaseModel rUser = new BaseModel() { userid = rModel.user.user_guid };
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "登录成功", rUser);
            }
            return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, (!string.IsNullOrEmpty(rModel.Message)) ? rModel.Message : rModel.Msg.ToString(), null);
        }

        /// <summary>
        ///  登录服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object AppUserLogin(SysLoginModel model)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            return Login(model);
        }

        /// <summary>
        ///  微信用户登录接口
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns></returns>
        [HttpPost]
        public object WeiXinUserLogin(SysLoginModel model)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                        WebResultCode.Exception, "参数验证失败", null);
            return Login(model);
        }

        /// <summary>
        ///  APP登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object AppUserAPILogin(SysLoginModel model)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                        WebResultCode.Exception, "参数验证失败", null);
            LoginUser user = new LoginUser()
            { LoginName = model.user_name, UserPwd = model.user_password };
            LoginResultModel rModel = operateContext.Login(user, false);
            if (rModel.Status)
            {
                LoginAPPResultModel reg = new LoginAPPResultModel { userid = rModel.user.user_guid, user_type = rModel.user.user_type };
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "登录成功", reg);
            }
            return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, (!string.IsNullOrEmpty(rModel.Message)) ? rModel.Message : rModel.Msg.ToString(), null);
        }

        /// <summary>
        ///  公共登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private object Login(SysLoginModel model)
        {
            LoginUser user = new LoginUser()
            { LoginName = model.user_name, UserPwd = model.user_password };
            LoginResultModel rModel = operateContext.Login(user, false);
            if (rModel.Status)
            {
                List<HCQ2_Model.T_Org_User> org =
                    operateContext.bllSession.T_Org_User.Select(s => s.user_id == rModel.user.user_id);
                BaseModel rUser;
                if (null != org && org.Count > 0)
                    rUser = new BaseModel() { userid = rModel.user.user_guid, orgid = org?[0].UnitID };
                else
                    rUser = new BaseModel() { userid = rModel.user.user_guid };
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "登录成功", rUser);
            }
            return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, (!string.IsNullOrEmpty(rModel.Message)) ? rModel.Message : rModel.Msg.ToString(), null);
        }
    }
}
