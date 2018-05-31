using System;
using System.Text;
using System.Web.Mvc;
using HCQ2UI_Helper;
using HCQ2_Common.Log;

namespace HCQ2UI_Logic
{
    public class MyHandleErrorAttribute:HandleErrorAttribute
    {
        /// <summary>
        ///  自定义全局异常
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            //1：获取异常对象
            Exception ex = filterContext.Exception;
            //2：记录异常日志
            StringBuilder str = new StringBuilder();
            str.AppendLine("\r\n.捕获异常信息：");
            string iP = string.Empty;
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_VIA"]))
                iP = Convert.ToString(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(iP))
                iP = Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);
            str.AppendLine("IP：" + iP);
            str.AppendLine("浏览器：" + Request.Browser.Browser);
            str.AppendLine("浏览器版本：" + Request.Browser.MajorVersion);
            str.AppendLine("操作系统：" + Request.Browser.Platform);
            str.AppendLine("错误信息：");
            str.AppendLine("错误页面：" + Request.Url);
            str.AppendLine("错误信息：" + ex.Message);
            str.AppendLine("错误源：" + ex.Source);
            str.AppendLine("异常方法：" + ex.TargetSite);
            str.AppendLine("堆栈信息：" + ex.StackTrace);
            LogHelper.ErrorLog(typeof(MyHandleErrorAttribute), str.ToString());
            //3：重定向友好页面
            filterContext.Result = new RedirectResult(Request.ApplicationPath + "/error.html");
            //4：标记异常已经处理完毕
            filterContext.ExceptionHandled = true;
            //base.OnException(filterContext);
        }
    }
}