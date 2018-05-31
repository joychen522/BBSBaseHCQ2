using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using HCQ2_Common.Attributes;
using HCQ2_DI;
using HCQ2_IBLL;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using HCQ2_Common;
using HCQ2_Model.EnumModel;

namespace HCQ2UI_Helper
{
    /// <summary>
    ///  操作上下文
    /// </summary>
    public class OperateContext
    {
        #region 0 属性 & 字段
        #region 0.0 常量
        /// <summary>
        ///  存储区域
        /// </summary>
        private const string Admin_CookiePath = "/admin/";
        /// <summary>
        ///  当前用户对象
        ///  Session 存储的是当前对象，Cookie 存储的是user_id
        /// </summary>
        private const string Admin_InfoKey = "ainfo";
        /// <summary>
        ///  权限key
        /// </summary>
        private const string Admin_PermissionKey = "apermission";
        private const string Admin_TreeString = "aTreeString";
        /// <summary>
        ///  业务仓储session key值
        /// </summary>
        private const string Admin_LogicSessionKey = "BLLSession";
        #endregion

        #region 0.1 获取当前HttpContext 及相关属性
        /// <summary>
        ///  获取当前HttpContext
        /// </summary>
        HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        HttpResponse Response
        {
            get { return Context.Response; }
        }

        HttpRequest Request
        {
            get { return Context.Request; }
        }

        HttpSessionState Session
        {
            get { return Context.Session; }
        }
        #endregion

        #region 0.2 用户权限 + List<T_AccessUser> UsrPermission
        /// <summary>
        ///  用户权限
        /// </summary>
        //public List<T_AccessUser> UsrPermission
        //{
        //    get { return Session[Admin_PermissionKey] as List<T_AccessUser>; }
        //    set { Session[Admin_PermissionKey] = value; }
        //}
        #endregion

        #region 0.3 当前用户对象(Session中) + HCQ2_Model.T_User Usr
        /// <summary>
        ///  当前用户对象
        /// </summary>
        public HCQ2_Model.T_User Usr
        {
            get { return Session[Admin_InfoKey] as HCQ2_Model.T_User; }
            set { Session[Admin_InfoKey] = value; }
        }
        #endregion

        #region 0.4 业务仓储
        /// <summary>
        ///  业务仓储：对象
        /// </summary>
        public IBLLSession bllSession;
        #endregion

        #region 0.5 实例化构造函数 初始化 业务仓储对象
        public OperateContext()
        {
            bllSession = SpringHelper.GetObject<IBLLSessionFactory>("BLLSessionFactory").GetBLLSession();
        }
        #endregion

        #region 0.6 获取当前 操作上下文 + static OperateContext Current
        /// <summary>
        ///  获取操作上下文对象
        /// </summary>
        public static OperateContext Current
        {
            get
            {
                OperateContext oContext = CallContext.GetData(typeof(OperateContext).Name) as OperateContext;
                if (oContext == null)
                {
                    oContext = new OperateContext();
                    CallContext.SetData(typeof(OperateContext).Name, oContext);
                }
                return oContext;
            }
        }
        #endregion
        #endregion

        //**************************** 2.0 登录权限 ***********************************

        #region 2.1 根据登录名查询用户权限 +GetUserPermission(string login_name)
        /// <summary>
        ///  根据登录名查询权限
        /// </summary>
        /// <param name="login_name"></param>
        /// <returns></returns>
        //public List<T_AccessUser> GetUserPermission(string login_name)
        //{
        //    //1.0 查询T_Userinfo的user_id
        //    List<int> listUserId =
        //        bllSession.Auth_User.Select(s => s.UserName == login_name).Select(s => s.UserID).ToList();
        //    //2.0 根据user_id查询T_AccessUser表权限
        //    List<T_AccessUser> listPermission =
        //        bllSession.T_AccessUser.Select(s => listUserId.Contains(s.user_id)).Select(s => s.ToPOCO()).ToList();
        //    //listPermission.ForEach(p =>
        //    //{
        //    //    //p为当前遍历集合的元素
        //    //});
        //    return listPermission;
        //}
        #endregion

        #region 2.2 管理员登录方法 +LoginAdmin(MODEL.ViewModel.LoginUser user)
        /// <summary>
        ///  管理员登录方法
        /// </summary>
        /// <param name="user">登录对象</param>
        /// <param name="writeSessionByUser">登录成功后是否需要将用户对象写入Session</param>
        /// <returns></returns>
        public LoginResultModel Login(LoginUser user,bool writeSessionByUser=true)
        {
            //2.1 根据用户名查询对象
            LoginResultModel uModel = bllSession.T_User.GetByUser(user.LoginName, user.UserPwd);
            T_Login login = new HCQ2_Model.T_Login()
            {
                login_ip =RequestHelper.GetIP,
                login_time=DateTime.Now
            };
            if (uModel.Status)
            {
                #region 登录成功
                //1.将当前对象保存进Session
                if (writeSessionByUser)
                {
                    Usr = uModel.user;
                    HCQ2_Common.Login.LoginCache.SetCheckCacheLogin(Usr.login_name);
                }
                //添加登录信息
                login.user_id = uModel.user.user_id;
                login.if_false = true;
                bllSession.T_Login.AddLoginInfo(login);
                //清理受限制表
                bllSession.T_LimitUser.Delete(s => s.user_id == uModel.user.user_id);
                #endregion
            }
            else
            {
                #region 登录失败
                if (uModel.Msg == LoginEnum.LoginResult.密码错误)
                {                    
                    //更新登录信息表
                    login.user_id = uModel.user.user_id;
                    login.if_false = false;
                    login.note = LoginEnum.LoginResult.密码错误.ToString();
                    bllSession.T_Login.AddLoginInfo(login);
                    T_Login elogin = bllSession.T_Login.selectLoginById(uModel.user.user_id);
                    //更新提示信息
                    uModel.Message = LoginEnum.LoginResult.密码错误.ToString() + "> 密码错误不能超过：" + LoginEnum.LOGIN_ERR_NUM +
                                     "次，您当前已输入错误：" + elogin.error_count + "次";
                    if (elogin.error_count >= LoginEnum.LOGIN_ERR_NUM)
                    {
                        string srrMsg = "您输入密码错误次数超过" + LoginEnum.LOGIN_ERR_NUM + "次，请" + LoginEnum.WAIT_HOURS +
                                        "个小时后"+ DateTime.Now.AddHours(LoginEnum.WAIT_HOURS).ToString("t") + "再试~";
                        uModel.Message = srrMsg;
                        //添加受限制表记录
                        bllSession.T_LimitUser.Add(new T_LimitUser()
                        {
                            user_id = uModel.user.user_id,
                            from_time = DateTime.Now,
                            to_time = DateTime.Now.AddHours(LoginEnum.WAIT_HOURS),
                            limit_note = srrMsg
                        });
                        //更新登录记录表错误次数
                    }
                }
                return uModel;

                #endregion
            }
            return uModel;
        }
        #endregion

        #region 2.3 判断当前用户是否登录 +IsLogin()
        /// <summary>
        ///  判断当前用户是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            #region
            //判断用户是否登录
            return HCQ2_Common.Login.LoginCache.IsCacheLogin(Current.Usr?.login_name);
            //1.验证用户是否登陆(Session(当前对象) && Cookie(user_id))
            //if (Session[Admin_InfoKey] == null)
            //    return false;
            //return true;
            #endregion
        }
        #endregion

        #region 2.4 判断当前用户是否有 访问当前页面的权限 + bool HasPemission(string controllerName, string actionName, string httpMethod)
        /// <summary>
        ///  2.4 判断当前用户 是否有 访问当前页面的权限
        /// </summary>
        /// <param name="controllerName">控制器名</param>
        /// <param name="actionName">方法名</param>
        /// <param name="httpMethod">提交方式</param>
        /// <returns></returns>
        public bool HasPemission(string controllerName, string actionName, string httpMethod)
        {
            return true;
            //return SysPermissSession.CheckPermiss(controllerName, actionName);
        }
        #endregion

        #region 2.5 清理登录信息 +public void ClearLogin()
        /// <summary>
        ///  清理登录信息
        /// </summary>
        public void ClearLogin()
        {
            Usr = null;
            HCQ2_Common.Login.LoginCache.ExitCacheLogin(Usr?.login_name);
        } 
        #endregion

        //---------------------------------3.0 公用操作方法-------------------------------------

        #region 3.1 生成 Json 格式的返回值 +ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        /// <summary>
        /// 生成 Json 格式的返回值
        /// </summary>
        /// <param name="statu">返回状态0：成功，1：失败</param>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回数据</param>
        /// <param name="backurl">返回跳转地址</param>
        /// <returns></returns>
        public ActionResult RedirectAjax(int statu, string msg, object data, string backurl)
        {
            HCQ2_Model.JsonData.JsonModel ajax = new HCQ2_Model.JsonData.JsonModel()
            {
                 Statu=statu,
                 Msg=msg,
                 Data=data,
                 Url=backurl
            };
            JsonResult res = new JsonResult();
            res.Data = ajax;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
        #endregion

        #region 3.2 重定向方法 根据Action方法特性  +ActionResult Redirect(string url, ActionDescriptor action)
        /// <summary>
        /// 重定向方法 有两种情况：如果是Ajax请求，则返回 Json字符串；如果是普通请求，则 返回重定向命令
        /// </summary>
        /// <returns></returns>
        public ActionResult Redirect(string url, ActionDescriptor action)
        {
            //如果Ajax请求没有权限，就返回 Json消息
            if (action.IsDefined(typeof (AjaxRequestAttribute), false)
                || action.ControllerDescriptor.IsDefined(typeof (AjaxRequestAttribute), false))
                return RedirectAjax(1, "您没有登陆或没有权限访问此页面~~", null, url);
            else //如果 超链接或表单 没有权限访问，则返回 302重定向命令
                return new RedirectResult(Request.ApplicationPath + url);
        }
        #endregion

        #region 3.3 WebApi返回 生成的对象 + object RedirectWebApi(int errcode, string errmsg, object value)
        /// <summary>
        ///  3.3 WebApi返回 生成的对象
        /// </summary>
        /// <param name="errcode">代码</param>
        /// <param name="errmsg"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object RedirectWebApi(int errcode, string errmsg, object value)
        {
            HCQ2_Model.ViewModel.WebAPI.WebApiResultJsonModel jsonModel = new HCQ2_Model.ViewModel.WebAPI.
                WebApiResultJsonModel()
            {
                errcode = errcode,
                errmsg = errmsg,
                value = value
            };
            return jsonModel;
        } 
        #endregion
    }
}
