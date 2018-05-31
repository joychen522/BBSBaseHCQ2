using System.Web;
using System.Web.Mvc;
using HCQ2WebAPI_Logic;

namespace HCQ2WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //添加全局异常处理
            //filters.Add(new DExceptionFilterAttribute());
            //添加全局Action验证
            //filters.Add(new MyApiAuthorizeFilterAttribute());
        }
    }
}
