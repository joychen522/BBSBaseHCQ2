using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HCQ2UI
{
    public class MvcApplication : HttpApplication //SpringMvcApplication
    {
        protected void Application_Start()
        {
            //注册区域路由
            AreaRegistration.RegisterAllAreas();
            //注册过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //注册路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册资源整合(js,css)合并
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Config/log4net.config")));
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }
        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}
