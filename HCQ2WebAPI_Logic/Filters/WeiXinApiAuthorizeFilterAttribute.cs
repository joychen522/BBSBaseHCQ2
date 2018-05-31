using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HCQ2UI_Helper;
using HCQ2_Common;
using HCQ2_Common.Constant;

namespace HCQ2WebAPI_Logic
{
    /// <summary>
    ///  自定义此特性用于接口的身份验证
    /// </summary>
    public class WeiXinApiAuthorizeFilterAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///  身份合法验证
        ///  match_signature：加密签名
        ///  match_timestamp：时间戳
        ///  match_nonce：1-20个随机数字字符
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            #region 1. 判断是否登录合法用户

            if (actionContext.ActionDescriptor.ControllerDescriptor
                    .GetCustomAttributes<HCQ2_Common.Attributes.SkipApiAttribute>(false).Count == 0)
            {
                //控制器未加登录排除特性 需要验证是否登录
                string userid = HttpContext.Current.Request[AuthorityConstant.USER_ID];//用户编码
                userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Headers[AuthorityConstant.USER_ID] : userid;
                userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Form[AuthorityConstant.USER_ID] : userid;
                if (string.IsNullOrEmpty(userid))
                {
                    System.IO.Stream postData = HttpContext.Current.Request.InputStream;
                    System.IO.StreamReader sreader = new System.IO.StreamReader(postData);
                    string postContext = sreader.ReadToEnd();
                    //sreader.Close();
                    if (!string.IsNullOrEmpty(postContext) && postContext.IndexOf(AuthorityConstant.USER_ID) > -1)
                    {
                        HCQ2_Model.WebApiModel.ParamModel.CheckLoginBaseModel model =
                            JsonHelper.JsonStrToObject<HCQ2_Model.WebApiModel.ParamModel.CheckLoginBaseModel>(
                                postContext);
                        userid = model.userid;
                    }
                }
                if (string.IsNullOrEmpty(userid))
                {
                    //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                        new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                        {
                            errcode = WebResultCode.Error,
                            errmsg = "非法用户~",
                            value = null
                        });
                }
                else
                {
                    //验证是否合法登录用户
                    HCQ2_Model.T_User user =
                        OperateContext.Current.bllSession.T_User.Select(s => s.user_guid.Equals(userid))
                            .FirstOrDefault();
                    if (null == user)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                        new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                        {
                            errcode = WebResultCode.Error,
                            errmsg = "非法用户~",
                            value = null
                        });
                    }
                }
            }

            #endregion
        }
    }
}