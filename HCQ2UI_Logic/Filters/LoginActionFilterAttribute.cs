using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HCQ2UI_Helper;

namespace HCQ2UI_Logic
{
    public class LoginActionFilterAttribute : ActionFilterAttribute
    {
        private ActionDescriptor loadAttribute;
        /// <summary>
        ///  Action执行之前验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //1. RouteData 中 保存了 当前请求 匹配的 路由信息和路由对象
            //如果本次请求 是请求了某个 区域 里的 控制器方法，还可以通过filterContext.RouteData.DataTokens["area"]获取区域名
            //2.检查 被请求的 方法 和 控制器是否有 Skip 标签，如果有，则不验证；如果没有，则验证
            loadAttribute = filterContext.ActionDescriptor;
            if (!filterContext.ActionDescriptor.IsDefined(typeof (HCQ2_Common.Attributes.SkipAttribute), false) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                    typeof (HCQ2_Common.Attributes.SkipAttribute), false))
            {
                #region 1.验证用户是否登陆(Session && Cookie)
                //1.验证用户是否登陆(Session && Cookie)
                if (!OperateContext.Current.IsLogin())
                {
                    filterContext.Result = OperateContext.Current.Redirect("/SysLogin/Login", filterContext.ActionDescriptor);
                }
                #endregion

                #region  2.验证登陆用户 是否有访问该页面的权限
                else
                {
                    //2.获取 登陆用户权限
                    //区域名称
                    //string strAreaName = filterContext.RouteData.DataTokens["area"].ToString();
                    //Controller 名称
                    string strContrllerName =
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    //Action 名称
                    string strActionName = filterContext.ActionDescriptor.ActionName;
                    //请求方式
                    string strHttpMethod = filterContext.HttpContext.Request.HttpMethod;

                    if (!OperateContext.Current.HasPemission(strContrllerName, strActionName, strHttpMethod))
                    {
                        filterContext.Result = OperateContext.Current.Redirect("/Html/noPermiss.html",
                            filterContext.ActionDescriptor);
                    }

                    #region 封装页面元素到集合中 判断有Action有ElementAttribute特性加载元素
                    else if (filterContext.ActionDescriptor.IsDefined(typeof(HCQ2_Common.Attributes.ElementAttribute), false))
                    {
                        filterContext.Controller.ViewBag.elementList = HCQ2UI_Helper.Session.SysPermissSession.GetElementCodeByUser(
                         strContrllerName, strActionName);
                    }
                    #endregion

                }
                #endregion
            }
        }

        /// <summary>
        ///   Action执行之后验证
        /// </summary>
        /// <param name="filterContext"></param>
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    filterContext.HttpContext.Response.Write("Action执行之后验证事件~！OnActionExecuted<br/>");
        //    base.OnActionExecuted(filterContext);
        //}

        /// <summary>
        ///  视图执行完成后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (loadAttribute.IsDefined(typeof (HCQ2_Common.Attributes.LoadAttribute), false))
                filterContext.HttpContext.Response.Write(
                    "<script>parent.delLoadBoxs();</script>");
            base.OnResultExecuted(filterContext);
        }
    }
}