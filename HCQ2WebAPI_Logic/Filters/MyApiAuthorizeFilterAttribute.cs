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
    public class MyApiAuthorizeFilterAttribute : AuthorizeAttribute
    {
        public string StreamToBytes(System.IO.Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
            String d = converter.GetString(bytes);
            return d;
        }
        /// <summary>
        ///  身份合法验证
        ///  match_signature：加密签名
        ///  match_timestamp：时间戳
        ///  match_nonce：1-20个随机数字字符
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //获取请求的Action
            string action = actionContext.ActionDescriptor.ActionName;
            string postContext = string.Empty;
            //已被注释
            #region 1. 从http请求头里获取需要验证的数据信息

            string match_signature = HttpContext.Current.Request[AuthorityConstant.MATCH_SIGNATURE];//加密签名
            string match_timestamp = HttpContext.Current.Request[AuthorityConstant.MATCH_TIMESTAMP];//时间戳
            string match_nonce = HttpContext.Current.Request[AuthorityConstant.MATCH_NONCE];//随机字符串1-20
            match_signature = (string.IsNullOrEmpty(match_signature)) ? HttpContext.Current.Request.Headers[AuthorityConstant.MATCH_SIGNATURE] : match_signature;
            match_signature = (string.IsNullOrEmpty(match_signature)) ? HttpContext.Current.Request.Form[AuthorityConstant.MATCH_SIGNATURE] : match_signature;
            match_timestamp = (string.IsNullOrEmpty(match_timestamp))
                ? HttpContext.Current.Request.Headers[AuthorityConstant.MATCH_TIMESTAMP]
                : match_timestamp;
            match_timestamp = (string.IsNullOrEmpty(match_timestamp)) ? HttpContext.Current.Request.Form[AuthorityConstant.MATCH_TIMESTAMP] : match_timestamp;
            match_nonce = (string.IsNullOrEmpty(match_nonce))
                ? HttpContext.Current.Request.Headers[AuthorityConstant.MATCH_NONCE]
                : match_nonce;
            match_nonce = (string.IsNullOrEmpty(match_nonce)) ? HttpContext.Current.Request.Form[AuthorityConstant.MATCH_NONCE] : match_nonce;
            if(string.IsNullOrEmpty(match_signature))
            {
                System.IO.Stream postData = HttpContext.Current.Request.InputStream;
                System.IO.StreamReader sreader = new System.IO.StreamReader(postData);
                postContext = sreader.ReadToEnd();
                //sreader.Close();
                if (!string.IsNullOrEmpty(postContext) && postContext.IndexOf(AuthorityConstant.MATCH_SIGNATURE) > -1)
                {
                    HCQ2_Model.WebApiModel.ParamModel.CheckParam model =
                        JsonHelper.JsonStrToObject<HCQ2_Model.WebApiModel.ParamModel.CheckParam>(
                            postContext);
                    match_signature = model.match_signature;
                    match_timestamp = model.match_timestamp;
                    match_nonce = model.match_nonce;
                }
            }
            if (string.IsNullOrEmpty(match_signature))
            {
                //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                    {
                        errcode = WebResultCode.Exception,
                        errmsg = "授权验证信息不全~",
                        value = null
                    });
            }
            #endregion

            #region 2. 验证数据
            else
            {
                //2. 获取请求头部参数不为空Request[AuthorityConstant.MATCH_SIGNATURE]
                bool mark = Authority.AuthorityCheck.AuthoritySignature(match_signature, match_timestamp,
                    match_nonce);
                if (!mark)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                        new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                        {
                            errcode = WebResultCode.Error,
                            errmsg = WebApiStatusCode.认证失败.ToString(),
                            value = null
                        });
                else
                {
                    #region 3. 判断是否登录合法用户  ----------注释

                    //if (actionContext.ActionDescriptor.ControllerDescriptor
                    //        .GetCustomAttributes<HCQ2_Common.Attributes.SkipApiAttribute>(false).Count == 0)
                    //{
                    //    //控制器未加登录排除特性 需要验证是否登录
                    //    string userid = HttpContext.Current.Request[AuthorityConstant.USER_ID];//用户编码
                    //    userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Headers[AuthorityConstant.USER_ID] : userid;
                    //    userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Form[AuthorityConstant.USER_ID] : userid;
                    //    if (string.IsNullOrEmpty(userid))
                    //    {
                    //        //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                    //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    //            new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                    //            {
                    //                errcode = WebResultCode.Error,
                    //                errmsg = "非法用户~",
                    //                value = null
                    //            });
                    //    }
                    //    else
                    //    {
                    //        //验证是否合法登录用户
                    //        HCQ2_Model.T_User user =
                    //            OperateContext.Current.bllSession.T_User.Select(s => s.user_guid.Equals(userid))
                    //                .FirstOrDefault();
                    //        if (null == user)
                    //        {
                    //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    //            new HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel()
                    //            {
                    //                errcode = WebResultCode.Error,
                    //                errmsg = "非法用户~",
                    //                value = null
                    //            });
                    //        }
                    //    }
                    //}

                    #endregion
                    #region 3. 判断是否登录合法用户

                    if (actionContext.ActionDescriptor.ControllerDescriptor
                            .GetCustomAttributes<HCQ2_Common.Attributes.SkipApiAttribute>(false).Count == 0)
                    {
                        //控制器未加登录排除特性 需要验证是否登录
                        string userid = HttpContext.Current.Request[AuthorityConstant.USER_ID];//用户编码
                        userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Headers[AuthorityConstant.USER_ID] : userid;
                        userid = (string.IsNullOrEmpty(userid)) ? HttpContext.Current.Request.Form[AuthorityConstant.USER_ID] : userid;
                        if (string.IsNullOrEmpty(userid))
                        {
                            if (string.IsNullOrEmpty(postContext))
                            {
                                System.IO.Stream postData = HttpContext.Current.Request.InputStream;
                                System.IO.StreamReader sreader = new System.IO.StreamReader(postData);
                                postContext = sreader.ReadToEnd();
                            } 
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
            #endregion
            //3. 判断是否登录合法用户

        }

        protected void MyHandleUnauthorizedRequest(HttpActionContext actionContext, object obj)
        {
            var response = actionContext.Request.CreateResponse(obj);//HttpStatusCode.Unauthorized, 
            actionContext.Response = actionContext.Request.CreateResponse(obj);
        }
    }
}