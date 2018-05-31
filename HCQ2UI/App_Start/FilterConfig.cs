using System.Web;
using System.Web.Mvc;
using HCQ2UI_Logic;

namespace HCQ2UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //添加全局异常处理
            filters.Add(new MyHandleErrorAttribute());
            //添加全局Action验证
            filters.Add(new LoginActionFilterAttribute());
        }
    }
}
