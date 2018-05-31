using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace HCQ2WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //1.通过反射解决MVC、Web Api类名不能相同的问题
            //var suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix",
            //    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            //suffix?.SetValue(null, "ApiController");

            //配置跨域请求
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // json数据格式配置 Camel格式（驼峰）
            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //配置
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
